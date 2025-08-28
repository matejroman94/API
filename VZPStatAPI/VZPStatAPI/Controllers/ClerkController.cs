using AutoMapper;
using Common.Exceptions;
using Common;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Microsoft.Extensions.Primitives;
using System.Linq;
using System;
using VZPStatAPI.Wrappers;
using VZPStatAPI.Services;
using System.Collections.Immutable;
using VZPStatAPI.Wrappers;
using VZPStatAPI.Filter;
using Microsoft.AspNetCore.Routing;
using VZPStatAPI.Helpers;
using Bogus;
using System.Linq.Expressions;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClerkController : Controller
    {
        protected APIResponse _APIResponse;
        protected PagedAPIResponse _pagedAPIResponse;
        protected ReturnResponse<ClerkController> _returnResponse;
        private readonly ILogger<ClerkController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IURIService uriService;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Clerk";
        private readonly string nameOfObjects = "Clerks";
        private readonly string nameOfClass = "ClerkController";

        private string includeProperties = $"{nameof(Domain.Models.Clerk.Counters)}";

        public ClerkController(
            ILogger<ClerkController> Logger,
            IUnitOfWork UnitOfWork,
            IMapper Mapper,
            IConfiguration Configuration,
            IURIService uriService
            )
        {
            logger = Logger;
            unitOfWork = UnitOfWork;
            mapper = Mapper;
            configuration = Configuration;
            this.uriService = uriService;
            _APIResponse = new APIResponse();
            _pagedAPIResponse = new(null, 1, 0);
            _returnResponse = new ReturnResponse<ClerkController>(unitOfWork, this.logger, mapper);

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllClerks")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedAPIResponse>> GetAllClerks([FromQuery] PaginationFilter paginationFilter, string? ClerkName, string? IncludeProperties,
            string? BranchIds)
        {            
            string info = nameOfObject + ": Function GetAllClerks(): ";
            List<ClerkGetDTO> dataDTO = new List<ClerkGetDTO>();
            try
            {
                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
                (IEnumerable<Clerk> data, int TotalCount) result = (Enumerable.Empty<Clerk>(),0);

                if (!string.IsNullOrWhiteSpace(ClerkName))
                {
                    result = await unitOfWork.Clerks.GetAllAsync(new Repository.Pagination.Filter(paginationFilter.PageNumber, paginationFilter.PageSize),
                        u => u.ClerkName.ToLower().Contains(ClerkName.ToLower()),
                        includeProperties: includeProperties + $",{IncludeProperties}");
                }
                else
                {
                    result = await unitOfWork.Clerks.GetAllAsync(new Repository.Pagination.Filter(paginationFilter.PageNumber, paginationFilter.PageSize),null,
                        includeProperties: includeProperties + $",{IncludeProperties}");
                }

                if (!string.IsNullOrWhiteSpace(BranchIds))
                {
                    string[] brnchIds = BranchIds.Split(',');
                    var b = Array.ConvertAll(brnchIds, s => int.Parse(s));
                    var res = await unitOfWork.Branches.GetAllAsync(Repository.Pagination.Filter.AllRecords(),x => b.Contains(x.BranchId),nameof(Branch.Counters));

                    var filterData = result.data.Where(cl => res.Item1.SelectMany(x => x.Counters).Select( c => c.CounterId).Any(y => cl.Counters.Select(d => d.CounterId).Contains(y)));
                    result.TotalCount -= (result.data.Count() - filterData.Count());
                    result.data = filterData;                   
                }
                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                dataDTO = mapper.Map<ClerkGetDTO[]>(result.data).ToList();

                SetClerkStatus(ref dataDTO);
                SetBranchToClerk(ref dataDTO);

                if(!IncludeProperties?.Contains(nameof(Clerk.Counters)) ?? true)
                {
                    dataDTO.ForEach(clerk => clerk.CounterGetDTOs.Clear());
                }
                var totalRecords = result.TotalCount;
                _pagedAPIResponse = PaginationHelper.CreatePagedReponse(dataDTO, validFilter, totalRecords, uriService, route);
                throw new ControllerExceptionGetAllSuccess(nameOfObjects);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;              
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (ControllerExceptionGetAllSuccess ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _returnResponse.Response(true, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("{ID}", Name = "GetClerkByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetClerkByID(int ID, [FromQuery] string? IncludeProperties)
        {
            string info = nameOfObject + ": Function GetClerkByID(int ID): ";

            ClerkGetDTO dataDTO = new ClerkGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Clerks.GetFirstOrDefaultAsync(x => x.ClerkId == ID,
                    includeProperties: includeProperties + $",{IncludeProperties}");

                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);

                dataDTO = mapper.Map<ClerkGetDTO>(data);

                List<ClerkGetDTO> enumDataDTO = Enumerable.Repeat(dataDTO,1).ToList();
                SetClerkStatus(ref enumDataDTO);
                SetBranchToClerk(ref enumDataDTO);
                dataDTO = enumDataDTO.First();

                throw new ControllerExceptionFoundByIdSuccess(nameOfObject, ID);

            }
            catch (ControllerExceptionNotFoundById ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (ControllerExceptionFoundByIdSuccess ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _APIResponse.Result = dataDTO;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("ClerkStatus", Name = "GetClerkStatus")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetClerkStatus([FromQuery] int? ClerkStatusId, string? branchIds, string? includeProperties)
        {
            string info = nameOfObject + ": Function GetClerkStatus(): ";
            List<ClerkStatus> data = new List<ClerkStatus>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

#pragma warning disable CS8602
                var clerkStatus = await unitOfWork.Clerks.GetClerkStatusAsync((ClerkStatusId != null) ? (x => (x.ClerkEvents.LastOrDefault() == null ? -1 : x.ClerkEvents.LastOrDefault().ClerkStatusId) == ClerkStatusId) : null,
                                                                     (!string.IsNullOrWhiteSpace(includeProperties)) ? includeProperties : string.Empty);
                data = clerkStatus.ToList();
#pragma warning restore CS8602

                if (!string.IsNullOrWhiteSpace(branchIds))
                {
                    string[] brnchIds = branchIds.Split(',');
                    IEnumerable<int> branchInts = Array.ConvertAll(brnchIds, s => int.Parse(s));
                    var counters = await unitOfWork.Counters.GetAllAsync(Repository.Pagination.Filter.AllRecords(),x => branchInts.Contains(x.BranchId),
                        $"{nameof(Counter.Branch)}");
                    var counterIds = counters.Item1.Select(x => x.CounterId);
                    data.ForEach(x => x.Clerks = x.Clerks.Where(clerk => counterIds.Contains(clerk.ClerkId)).ToList());
                }

                if (!data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                throw new ControllerExceptionGetAllSuccess(nameOfObjects);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (ControllerExceptionGetAllSuccess ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _APIResponse.Result = data;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPost("ClerkEvent/Filter", Name = "GetAllFilteredClerkEvents")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedAPIResponse>> GetAllFilteredClerkEvents(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] string? IncludeProperties,
            [FromBody] ClerkEventFilter? clerkEventFilter
            )
        {
            string info = nameOfObject + ": Function GetAllFilteredClerkEvents(): ";
            IEnumerable<ClerkEventGetDTO> dataDTO;
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                (IEnumerable<ClerkEvent> data, int TotalCount) result = (Enumerable.Empty<ClerkEvent>(), 0);
                if (clerkEventFilter != null)
                {
                    IEnumerable<int>? res = Common.Common.IfEmptyReturnNull(clerkEventFilter.BranchIds);
                    if (res is null)
                    {
                        throw new ControllerExceptionNotFoundAny($"{nameOfObjects}\nBranchIds are empty!");
                    }
                    IEnumerable<int> branchIds = res;
                    var clerkIds = Common.Common.IfEmptyReturnNull(clerkEventFilter.ClerksIds);

                    Expression<Func<ClerkEvent, bool>> fDateFrom = x =>
                        (clerkEventFilter.DateFrom != null
                        && x.EventOccurredDate != null) ? ((DateTime)x.EventOccurredDate).Date >= ((DateTime)clerkEventFilter.DateFrom).Date : true;
                    Expression<Func<ClerkEvent, bool>> fDateTo = x =>
                        (clerkEventFilter.DateTo != null
                        && x.EventOccurredDate != null) ? ((DateTime)x.EventOccurredDate).Date <= ((DateTime)clerkEventFilter.DateTo).Date : true;
                    Expression<Func<ClerkEvent, bool>> fClerkIds = x =>
                        (clerkIds != null) ? clerkIds.Contains(x.ClerkId) : false;

                    List<Expression<Func<ClerkEvent, bool>>> filters = new List<Expression<Func<ClerkEvent, bool>>>();
                    filters.Add(fDateFrom);
                    filters.Add(fDateTo);
                    filters.Add(fClerkIds);

                    result = await unitOfWork.ClerkEvents.GetAllFilterAsync(new Repository.Pagination.Filter(paginationFilter.PageNumber, paginationFilter.PageSize),
                                    branchIds,
                                    filter: filters,
                                    includeProperties: IncludeProperties ?? string.Empty);
                }

                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                var totalRecords = result.TotalCount;
                dataDTO = mapper.Map<ClerkEventGetDTO[]>(result.data);
                _pagedAPIResponse = PaginationHelper.CreatePagedReponse(dataDTO, validFilter, totalRecords, uriService, route);
                throw new ControllerExceptionGetAllSuccess(nameOfObjects);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (ControllerExceptionGetAllSuccess ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _returnResponse.Response(true, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPost("Filter", Name = "GetAllFilteredClerks")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedAPIResponse>> GetAllFilteredClerks(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] string? IncludeProperties,
            [FromBody] ClerkFilter? clerkFilter
            )
        {
            string info = nameOfObject + ": Function GetAllFilteredClerks(): ";
            List<ClerkGetDTO> dataDTO = new List<ClerkGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                (IEnumerable<Clerk> data, int TotalCount) result = (Enumerable.Empty<Clerk>(), 0);
                if (clerkFilter != null)
                {
                    IEnumerable<int>? res = Common.Common.IfEmptyReturnNull(clerkFilter.BranchIds);
                    if(res is null)
                    {
                        throw new ControllerExceptionNotFoundAny($"{nameOfObjects}\nBranchIds are empty!");
                    }
                    IEnumerable<int> branchIds = res;
                    var clerkIds = Common.Common.IfEmptyReturnNull(clerkFilter.ClerksIds);

                    int? clerkStatusID = null;
                    if(clerkFilter.ClerkStatusID ==null)
                    {
                        if (!string.IsNullOrWhiteSpace(clerkFilter.ClerkStatus))
                        {
                            var sts = await unitOfWork.Clerks.GetClerkStatusAsync(x => x.Status.Equals(clerkFilter.ClerkStatus));
                            clerkStatusID = sts.FirstOrDefault()?.ClerkStatusId;
                        }
                        clerkFilter.ClerkStatusID = clerkStatusID;
                    }

                    Expression<Func<Clerk, bool>> fClerkName = x =>
                        (!string.IsNullOrWhiteSpace(clerkFilter.ClerkName)) ? x.ClerkName.ToLower().Contains(clerkFilter.ClerkName.ToLower()) : true;
                    Expression<Func<Clerk, bool>> fClerkNumber = x =>
                        (clerkFilter.ClerkNumber != null) ? clerkFilter.ClerkNumber == x.Number : true;
                    Expression<Func<Clerk, bool>> fClerkStatusID = x =>
                        (clerkFilter.ClerkStatusID != null && x.ClerkEvents != null) ?
                        ((x.ClerkEvents.Count() > 0) ? clerkFilter.ClerkStatusID == x.ClerkEvents.OrderBy(x => x.EventOccurredDate).Last().ClerkStatusId : false)
                        : true;
                    Expression<Func<Clerk, bool>> fClerkStatusIDImplication = x =>
                        (clerkFilter.ClerkStatusID == null || x.ClerkEvents != null) ? true : false;
                    Expression<Func<Clerk, bool>> fClerkIds = x =>
                        (clerkIds != null) ? clerkIds.Contains(x.ClerkId) : false;

                    List<Expression<Func<Clerk, bool>>> filters = new List<Expression<Func<Clerk, bool>>>();
                    filters.Add(fClerkName);
                    filters.Add(fClerkNumber);
                    filters.Add(fClerkStatusID);
                    filters.Add(fClerkStatusIDImplication);
                    filters.Add(fClerkIds);

                    result = await unitOfWork.Clerks.GetAllFilterAsync(new Repository.Pagination.Filter(paginationFilter.PageNumber, paginationFilter.PageSize),
                                    branchIds,
                                    filter: filters,
                                    includeProperties: includeProperties + $",{IncludeProperties}");
                }

                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                dataDTO = mapper.Map<ClerkGetDTO[]>(result.data).ToList();

                SetClerkStatus(ref dataDTO);
                SetBranchToClerk(ref dataDTO);

                if (!IncludeProperties?.Contains(nameof(Clerk.Counters)) ?? true)
                {
                    dataDTO.ForEach(clerk => clerk.CounterGetDTOs.Clear());
                }
                var totalRecords = result.TotalCount;
                _pagedAPIResponse = PaginationHelper.CreatePagedReponse(dataDTO, validFilter, totalRecords, uriService, route);
                throw new ControllerExceptionGetAllSuccess(nameOfObjects);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (ControllerExceptionGetAllSuccess ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _returnResponse.Response(true, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPost(Name = "AddClerkRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddClerkRange([FromBody] IEnumerable<ClerkPutDTO> DataDTOs)
        {
            string info = nameOfObject + ": Function AddClerkRange(ClerkPutDTO DataDTO): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (DataDTOs.Any() == false)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Warning);
                }
                var data = mapper.Map<Clerk[]>(DataDTOs);

                var result = await unitOfWork.Clerks.AddRangeAsync(data);
                if (result is false)
                    throw new ControllerExceptionExceptionAdded(nameOfObject);
                else
                    throw new ControllerExceptionSuccessAdded(nameOfObject);
            }
            catch (ControllerExceptionSuccessAdded ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (ControllerExceptionExceptionAdded ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPut("{ID}", Name = "UpdateClerk")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdateClerk(ClerkPutDTO dataDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdateClerk(ClerkPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (dataDTO is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }
                var d = unitOfWork.Clerks.GetFirstOrDefault(x => x.ClerkId == ID);
                if (d is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                Clerk app = new Clerk();
                CopyClasses.Copy(dataDTO, ref app);
                app.ClerkId = ID;
                var result = unitOfWork.Clerks.Update(app);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    throw new ControllerExceptionSuccessUpdatedByID(nameOfObject, ID);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
            catch (ControllerExceptionSuccessUpdatedByID ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPatch("{ID}", Name = "UpdatePartialClerk")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdatePartialClerk(JsonPatchDocument<ClerkPutDTO> patchDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdatePartialClerk(ClerkPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (patchDTO is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }
                var obj = unitOfWork.Clerks.GetFirstOrDefault(x => x.ClerkId == ID, "", false);
                if (obj is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                var objPutDTO = mapper.Map<ClerkPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Clerk clerkUpdated = mapper.Map<Clerk>(objPutDTO);
                clerkUpdated.ClerkId = ID;

                var result = unitOfWork.Clerks.Update(clerkUpdated);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    throw new ControllerExceptionSuccessUpdatedByID(nameOfObject, ID);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
            catch (ControllerExceptionSuccessUpdatedByID ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpDelete("{ID}", Name = "DeleteClerkByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteClerkByID(int ID)
        {
            string info = nameOfObject + ": Function DeleteClerkByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Clerks.GetFirstOrDefaultAsync(x => x.ClerkId == ID);
                if (entity is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                bool result = await unitOfWork.Clerks.RemoveAsync(entity);

                if (result is false)
                {
                    throw new ControllerExceptionDeleteByID(nameOfObject, ID);
                }
                else
                {
                    throw new ControllerSuccessDeleteSuccessByID(nameOfObject, ID);
                }
            }
            catch (ControllerExceptionDeleteByID ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
            catch (ControllerSuccessDeleteSuccessByID ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        private void SetClerkStatus(ref List<ClerkGetDTO> clerkGetDTOs)
        {
            string exception = nameOfObject + ": Function SetClerkStatus(): ";
            foreach (var clerk in clerkGetDTOs)
            {
                try
                {
                    ClerkEvent? clerkEvent = unitOfWork.ClerkEvents.GetLastClerkEvent(clerk.ClerkId);
                    if(clerkEvent != null)
                    {
                        var e = mapper.Map<ClerkEventGetDTO>(clerkEvent);
                        clerk.ClerkEventCurrentId = e.ClerkStatusId;
                        clerk.ClerkEventCurrentName = e.ClerkStatus_Status;
                    }
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    unitOfWork.LoggerRepo.Add(mapper.Map<Domain.Models.Logger>(new LoggerPutDTO(exception + ex.Message)));
                }
            }
        }

        private void SetBranchToClerk(ref List<ClerkGetDTO> clerkGetDTOs)
        {
            string exception = nameOfObject + ": Function SetBranchToClerk(): ";
            try
            {
                unitOfWork.Clerks.GetBranchIdAndName(ref clerkGetDTOs);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                unitOfWork.LoggerRepo.Add(mapper.Map<Domain.Models.Logger>(new LoggerPutDTO(exception + ex.Message)));
            }
        }
    }
}
