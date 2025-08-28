using AutoMapper;
using Common.Exceptions;
using Common;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Microsoft.IdentityModel.Tokens;
using VZPStatAPI.Wrappers;
using VZPStatAPI.Services;
using VZPStatAPI.Filter;
using System.Linq.Expressions;
using VZPStatAPI.Helpers;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CounterController : ControllerBase
    {
        protected APIResponse _APIResponse;
        protected PagedAPIResponse _pagedAPIResponse;
        protected ReturnResponse<CounterController> _returnResponse;
        private readonly ILogger<CounterController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IURIService uriService;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Counter";
        private readonly string nameOfObjects = "Counters";
        private readonly string nameOfClass = "CounterController";

        private string includeProperties = $"{nameof(Domain.Models.Counter.Branch)}," +
            $"{nameof(Domain.Models.Counter.CounterStatus)}";

        public CounterController(
            ILogger<CounterController> Logger,
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
            _APIResponse = new();
            _pagedAPIResponse = new(null, 1, 0);
            _returnResponse = new ReturnResponse<CounterController>(unitOfWork, this.logger, mapper);

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllCounters")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedAPIResponse>> GetAllCounters([FromQuery] PaginationFilter paginationFilter, string? CounterName, string? IncludeProperties, string? BranchIds)
        {
            string info = nameOfObject + ": Function GetAllCounters(): ";
            List<CounterGetDTO> dataDTO = new List<CounterGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if(string.IsNullOrWhiteSpace(BranchIds))
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                IEnumerable<int> branchIds = Enumerable.Empty<int>();
                string[] brnchIds = BranchIds.Split(',');
                branchIds = Array.ConvertAll(brnchIds, s => int.Parse(s));

                Expression<Func<Counter, bool>> fCounterName = x =>
                    (!string.IsNullOrWhiteSpace(CounterName)) ? x.CounterName.ToLower().Contains(CounterName.ToLower()) : true;
                Expression<Func<Counter, bool>> fBranchIds = x =>
                    (branchIds.Count() > 0) ? branchIds.Contains(x.BranchId) : true;

                List<Expression<Func<Counter, bool>>> filters = new List<Expression<Func<Counter, bool>>>();
                filters.Add(fCounterName);
                filters.Add(fBranchIds);

                (IEnumerable<Counter> data, int TotalCount) result = (Enumerable.Empty<Counter>(), 0);
                result = await unitOfWork.Counters.GetAllFilterAsync(new Repository.Pagination.Filter(paginationFilter.PageNumber,paginationFilter.PageSize),
                        filter: filters,
                        includeProperties: includeProperties + $",{IncludeProperties}");

                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                dataDTO = mapper.Map<CounterGetDTO[]>(result.data).ToList();
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

        [HttpGet("{ID}", Name = "GetCounterByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetCounterByID(int ID, [FromQuery] string? IncludeProperties)
        {
            string info = nameOfObject + ": Function GetCounterByID(int ID): ";

            CounterGetDTO dataDTO = new CounterGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Counters.GetFirstOrDefaultAsync(x => x.CounterId == ID,
                    includeProperties: includeProperties + $",{IncludeProperties}");
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<CounterGetDTO>(data);
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


        [HttpGet("CounterStatus", Name = "GetCounterStatus")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetCounterStatus([FromQuery] int? CounterStatusId, string? branchIds, string? includeProperties)
        {
            string info = nameOfObject + ": Function GetCounterStatus(): ";
            List<CounterStatus> data = new List<CounterStatus>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var counterStatus = await unitOfWork.Counters.GetCounterStatusReasonAsync((CounterStatusId != null) ? (x => x.CounterStatusId == CounterStatusId) : null,
                                                                     (!string.IsNullOrWhiteSpace(includeProperties)) ? includeProperties : string.Empty);
                data = counterStatus.ToList();

                if (!string.IsNullOrWhiteSpace(branchIds))
                {
                    string[] brnchIds = branchIds.Split(',');
                    IEnumerable<int> branchInts = Array.ConvertAll(brnchIds, s => int.Parse(s));
                    data.ForEach(x => x.Counters = x.Counters.Where(counter => branchInts.Contains(counter.BranchId)).ToList());
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

        [HttpPost("Filter", Name = "GetAllFilteredCounters")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedAPIResponse>> GetAllFilteredCounters(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] string? IncludeProperties,
            [FromBody] CounterFilter? counterFilter)
        {
            string info = nameOfObject + ": Function GetAllFilteredCounters(): ";
            List<CounterGetDTO> dataDTO = new List<CounterGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                (IEnumerable<Counter> data, int TotalCount) result = (Enumerable.Empty<Counter>(), 0);
                if (counterFilter != null)
                {
                    var branchIds = Common.Common.IfEmptyReturnNull(counterFilter.BranchIds);
                    var counterIds = Common.Common.IfEmptyReturnNull(counterFilter.CounterIds);

                    Expression<Func<Counter, bool>> fCounterName = x =>
                        (!string.IsNullOrWhiteSpace(counterFilter.CounterName)) ? x.CounterName.ToLower().Contains(counterFilter.CounterName.ToLower()) : true;
                    Expression<Func<Counter, bool>> fCounterNumber = x =>
                        (counterFilter.CounterNumber != null) ? counterFilter.CounterNumber == x.Number : true;
                    Expression<Func<Counter, bool>> fCounterStatusID = x =>
                        (counterFilter.CounterStatusID != null) ? counterFilter.CounterStatusID == x.CounterStatusId : true;
                    Expression<Func<Counter, bool>> fCounterStatus = x =>
                        (!string.IsNullOrWhiteSpace(counterFilter.CounterStatus) && x.CounterStatus != null) ? x.CounterStatus.Status.Equals(counterFilter.CounterStatus) : true;
                    Expression<Func<Counter, bool>> fCounterStatusImplication = x =>
                        (counterFilter.CounterStatus == null || x.CounterStatus != null) ? true : false;
                    Expression<Func<Counter, bool>> fBranchIds = x =>
                        (branchIds != null) ? branchIds.Contains(x.BranchId) : false;
                    Expression<Func<Counter, bool>> fCounterIds = x =>
                        (counterIds != null) ? counterIds.Contains(x.CounterId) : false;

                    List<Expression<Func<Counter, bool>>> filters = new List<Expression<Func<Counter, bool>>>();
                    filters.Add(fCounterName);
                    filters.Add(fCounterNumber);
                    filters.Add(fCounterStatusID);
                    filters.Add(fCounterStatus);
                    filters.Add(fCounterStatusImplication);
                    filters.Add(fBranchIds);
                    filters.Add(fCounterIds);

                    result = await unitOfWork.Counters.GetAllFilterAsync(new Repository.Pagination.Filter(paginationFilter.PageNumber, paginationFilter.PageSize),
                                    filter: filters,
                                    includeProperties: includeProperties + $",{IncludeProperties}");
                }

                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                dataDTO = mapper.Map<CounterGetDTO[]>(result.data).ToList();
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

        [HttpPost(Name = "AddCounterRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddCounterRange([FromBody] IEnumerable<CounterPutDTO> DataDTOs)
        {
            string info = nameOfObject + ": Function AddCounterRange(CounterPutDTO DataDTO): ";
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
                var data = mapper.Map<Counter[]>(DataDTOs);

                var result = await unitOfWork.Counters.AddRangeAsync(data);
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

        [HttpPut("{ID}", Name = "UpdateCounter")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdateCounter(CounterPutDTO dataDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdateCounter(CounterPutDTO dataDTO, int ID): ";
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
                var d = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == ID);
                if (d is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                Counter counter = new Counter();
                CopyClasses.Copy(dataDTO, ref counter);
                counter.CounterId = ID;
                var result = unitOfWork.Counters.Update(counter);
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

        [HttpPatch("{ID}", Name = "UpdatePartialCounter")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdatePartialCounter(JsonPatchDocument<CounterPutDTO> patchDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdatePartialCounter(CounterPutDTO dataDTO, int ID): ";
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
                var obj = unitOfWork.Counters.GetFirstOrDefault(x => x.CounterId == ID, "", false);
                if (obj is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                var objPutDTO = mapper.Map<CounterPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Counter counterUpdated = mapper.Map<Counter>(objPutDTO);
                counterUpdated.CounterId = ID;

                var result = unitOfWork.Counters.Update(counterUpdated);
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

        [HttpDelete("{ID}", Name = "DeleteCounterByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteCounterByID(int ID)
        {
            string info = nameOfObject + ": Function DeleteCounterByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Counters.GetFirstOrDefaultAsync(x => x.CounterId == ID);
                if (entity is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                bool result = await unitOfWork.Counters.RemoveAsync(entity);

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
    }
}
