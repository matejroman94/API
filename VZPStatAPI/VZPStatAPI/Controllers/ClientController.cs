using AutoMapper;
using Common.Exceptions;
using Common;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using VZPStatAPI.Wrappers;
using VZPStatAPI.Services;
using VZPStatAPI.Filter;
using VZPStatAPI.Helpers;
using System.Linq.Expressions;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        protected APIResponse _APIResponse;
        protected PagedAPIResponse _pagedAPIResponse;
        protected ReturnResponse<ClientController> _returnResponse;
        private readonly ILogger<ClientController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IURIService uriService;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Client";
        private readonly string nameOfObjects = "Clients";
        private readonly string nameOfClass = "ClientController";

        private string includeProperties = $"{nameof(Domain.Models.Client.Branch)}," +
            $"{nameof(Domain.Models.Client.Clerk)}," +
            $"{nameof(Domain.Models.Client.Activity)}," +
            $"{nameof(Domain.Models.Client.Printer)}," +
            $"{nameof(Domain.Models.Client.Counter)}," +
            $"{nameof(Domain.Models.Client.ClientStatus)}," +
            $"{nameof(Domain.Models.Client.ClientDone)}";

        public ClientController(
            ILogger<ClientController> Logger,
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
            _returnResponse = new ReturnResponse<ClientController>(unitOfWork, this.logger, mapper);

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllClients")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<PagedAPIResponse>> GetAllClients([FromQuery] PaginationFilter paginationFilter)
        {
            string info = nameOfObject + ": Function GetAllClients(): ";
            List<ClientGetDTO> dataDTO = new List<ClientGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                (IEnumerable<Client> data, int TotalCount) result = (Enumerable.Empty<Client>(), 0);
                result = await unitOfWork.Clients.GetAllAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize),
                    null, includeProperties: includeProperties);


                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<ClientGetDTO[]>(result.data).ToList();
                var totalRecords = result.TotalCount;
                _pagedAPIResponse = PaginationHelper.CreatePagedReponse(dataDTO, validFilter, totalRecords, uriService, route);

                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _returnResponse.Response(true, _pagedAPIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("ByDate", Name = "GetAllClientsByDate")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<PagedAPIResponse>> GetAllClientsByDate([FromQuery] PaginationFilter paginationFilter,
            DateTime DateFrom, DateTime DateTo)
        {
            string info = nameOfObject + ": Function GetAllClientsByDate(): ";
            List<ClientGetDTO> dataDTO = new List<ClientGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
                (IEnumerable<Client> data, int TotalCount) result = (Enumerable.Empty<Client>(), 0);
                result = await unitOfWork.Clients.GetAllAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize),
                    u => u.EventOccurredDate >= DateFrom.Date &&
                    u.EventOccurredDate <= DateTo.Date, includeProperties: includeProperties);


                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<ClientGetDTO[]>(result.data).ToList();
                var totalRecords = result.TotalCount;
                _pagedAPIResponse = PaginationHelper.CreatePagedReponse(dataDTO, validFilter, totalRecords, uriService, route);

                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _returnResponse.Response(true, _pagedAPIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("{ID}", Name = "GetClientByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<APIResponse>> GetClientByID(int ID)
        {
            string info = nameOfObject + ": Function GetClientByID(int ID): ";

            ClientGetDTO dataDTO = new ClientGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.ClientId == ID,
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<ClientGetDTO>(data);

                _APIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _APIResponse.Result = dataDTO;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionNotFoundById ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("ClientStatus", Name = "GetClientStatus")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetClientStatus([FromQuery] int? ClientStatus, string? branchIds, string? includeProperties)
        {
            string info = nameOfObject + ": Function GetClientStatus(): ";
            List<ClientStatus> data = new List<ClientStatus>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                Expression<Func<ClientStatus, bool>> fClientStatusId = x =>
                    (ClientStatus != null) ? x.ClientStatusId == ClientStatus : true;

                List<Expression<Func<ClientStatus, bool>>> filters = new List<Expression<Func<ClientStatus, bool>>>();
                filters.Add(fClientStatusId);

                var clientStatuses = await unitOfWork.Clients.GetClientStatusAsync(filters,
                                                                     (!string.IsNullOrWhiteSpace(includeProperties)) ? includeProperties : string.Empty);
                data = clientStatuses.ToList();

                if (!string.IsNullOrWhiteSpace(branchIds))
                {
                    string[] brnchIds = branchIds.Split(',');
                    IEnumerable<int> branchInts = Array.ConvertAll(brnchIds, s => int.Parse(s));
                    data.ForEach(x => x.Clients = x.Clients.Where(client => branchInts.Contains(client.BranchId)).ToList());
                }

                if (!data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                _APIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _APIResponse.Result = data;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("ClientDoneReason", Name = "GetClientDoneReason")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetClientDoneReason([FromQuery] int? ClientDoneStatus, string? branchIds, string? includeProperties)
        {
            string info = nameOfObject + ": Function GetClientDoneReason(): ";
            List<Reason> data = new List<Reason>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var clientStatuses = await unitOfWork.Clients.GetClientDoneReasonAsync((ClientDoneStatus != null) ? (x => x.ReasonId == ClientDoneStatus) : null,
                                                                     (!string.IsNullOrWhiteSpace(includeProperties)) ? includeProperties : string.Empty);
                data = clientStatuses.ToList();

                if (!string.IsNullOrWhiteSpace(branchIds))
                {
                    string[] brnchIds = branchIds.Split(',');
                    IEnumerable<int> branchInts = Array.ConvertAll(brnchIds, s => int.Parse(s));
                    data.ForEach(x => x.Clients = x.Clients.Where(client => branchInts.Contains(client.BranchId)).ToList());
                }

                if (!data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }

                _APIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                _APIResponse.Result = data;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPost("Filter",Name = "GetAllFilteredClients")]
        [ProducesResponseType(typeof(PagedAPIResponse), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(PagedAPIResponse), statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(PagedAPIResponse), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(PagedAPIResponse), statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<PagedAPIResponse>> GetAllFilteredClients(
            [FromQuery] PaginationFilter paginationFilter,
            [FromBody] ClientFilter? clientFilter
            )
        {
            string info = nameOfObject + ": Function GetAllFilteredClients(): ";
            List<ClientGetDTO> dataDTO = new List<ClientGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                (IEnumerable<Client> data, int TotalCount) result = (Enumerable.Empty<Client>(), 0);
                if (clientFilter != null)
                {
                    var branchIds = Common.Common.IfEmptyReturnNull(clientFilter.BranchIds);
                    var counterIds = Common.Common.IfEmptyReturnNull(clientFilter.CounterIds);
                    var clekIds = Common.Common.IfEmptyReturnNull(clientFilter.ClerksIds);

                    Expression<Func<Client, bool>> fDateFrom = x =>
                    (clientFilter.DateFrom != null && x.EventOccurredDate != null) ? ((DateTime)x.EventOccurredDate).Date >= ((DateTime)clientFilter.DateFrom).Date : true;
                    Expression<Func<Client, bool>> fDateTo = x =>
                    (clientFilter.DateTo != null && x.EventOccurredDate != null) ? ((DateTime)x.EventOccurredDate).Date <= ((DateTime)clientFilter.DateTo).Date : true;
                    Expression<Func<Client, bool>> fOrdinalNumber = x =>
                    (clientFilter.OrdinalNumber != null) ? x.ClientOrdinalNumber == clientFilter.OrdinalNumber : true;
                    Expression <Func<Client, bool>> fOnline = x =>
                    (clientFilter.Online != null) ? x.Priority == (bool)clientFilter.Online : true;
                    Expression<Func<Client, bool>> fReason = x =>
                    (clientFilter.Reason != null && x.ClientDone != null) ? x.ClientDone.Description.Equals(clientFilter.Reason) : true;
                    Expression<Func<Client, bool>> fReasonImplication = x =>
                    (clientFilter.Reason == null || x.ClientDone != null) ? true : false;
                    Expression<Func<Client, bool>> fClientStatus = x =>
                    (clientFilter.ClientStatus != null && x.ClientStatus != null) ? x.ClientStatus.Status.Equals(clientFilter.ClientStatus) : true;
                    Expression<Func<Client, bool>> fClientStatusImplication = x =>
                    (clientFilter.ClientStatus == null || x.ClientStatus != null) ? true : false;
                    Expression<Func<Client, bool>> fActivityName = x =>
                    (clientFilter.Activity != null && x.Activity != null) ? x.Activity.ActivityName.ToLower().Equals(clientFilter.Activity.ToLower()) : true;
                    Expression<Func<Client, bool>> fActivityNameImplication = x =>
                    (clientFilter.Activity == null || x.Activity != null) ? true : false;
                    Expression<Func<Client, bool>> fBranchIds = x =>
                    (branchIds != null) ? branchIds.Contains(x.BranchId) : false;
                    Expression<Func<Client, bool>> fCounterIds = (x =>
                    ((counterIds != null) ? counterIds.Contains(x.CounterId) : false)
                        ||
                    ((x.CounterId == null) ? true : false)); 
                    Expression <Func<Client, bool>> fClekIds = (x =>
                    ((clekIds != null) ? clekIds.Contains(x.ClerkId) : false)
                        ||
                    ((x.ClerkId == null) ? true : false));

                    List <Expression<Func<Client, bool>>> filters = new List<Expression<Func<Client, bool>>>();
                    filters.Add(fDateFrom);
                    filters.Add(fDateTo);
                    filters.Add(fOnline);
                    filters.Add(fOrdinalNumber);
                    filters.Add(fReason);
                    filters.Add(fReasonImplication);
                    filters.Add(fClientStatus);
                    filters.Add(fClientStatusImplication);
                    filters.Add(fActivityName);
                    filters.Add(fActivityNameImplication);
                    filters.Add(fBranchIds);
                    filters.Add(fCounterIds);
                    filters.Add(fClekIds);

                    result = await unitOfWork.Clients.GetAllFilterAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize),
                        filters, includeProperties: includeProperties);
                }


                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<ClientGetDTO[]>(result.data).ToList();
                var totalRecords = result.TotalCount;
                _pagedAPIResponse = PaginationHelper.CreatePagedReponse(dataDTO, validFilter, totalRecords, uriService, route);
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.OK;
                return _returnResponse.Response(true, _pagedAPIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _pagedAPIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPost(Name = "AddClientRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddClientRange([FromBody] IEnumerable<ClientPutDTO> DataDTOs)
        {
            string info = nameOfObject + ": Function AddClientRange(ClientPutDTO DataDTO): ";
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
                var data = mapper.Map<Client[]>(DataDTOs);

                var result = await unitOfWork.Clients.AddRangeAsync(data);
                if (result is false)
                    throw new ControllerExceptionExceptionAdded(nameOfObject);

                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
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

        [HttpPut("{ID}", Name = "UpdateClient ")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdateClient(ClientPutDTO dataDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdateClient(ClientPutDTO dataDTO, int ID): ";
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
                var d = unitOfWork.Clients.GetFirstOrDefault(x => x.ClientId == ID);
                if (d is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                Client client = new Client();
                CopyClasses.Copy(dataDTO, ref client);
                client.ClientId = ID;
                var result = unitOfWork.Clients.Update(client);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);

                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
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

        [HttpPatch("{ID}", Name = "UpdatePartialClient")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdatePartialClient(JsonPatchDocument<ClientPutDTO> patchDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdatePartialClient(ClientPutDTO dataDTO, int ID): ";
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
                var obj = unitOfWork.Clients.GetFirstOrDefault(x => x.ClientId == ID, "", false);
                if (obj is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                var objPutDTO = mapper.Map<ClientPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Client clientUpdated = mapper.Map<Client>(objPutDTO);
                clientUpdated.ClientId = ID;

                var result = unitOfWork.Clients.Update(clientUpdated);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);

                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
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

        [HttpDelete("{ID}", Name = "DeleteClientByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteClientByID(int ID)
        {
            string info = nameOfObject + ": Function DeleteClientByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Clients.GetFirstOrDefaultAsync(x => x.ClientId == ID);
                if (entity is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                bool result = await unitOfWork.Clients.RemoveAsync(entity);

                if (result is false)
                    throw new ControllerExceptionDeleteByID(nameOfObject, ID);

                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info);
            }
            catch (ControllerExceptionDeleteByID ex)
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
    }
}
