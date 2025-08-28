using AutoMapper;
using Common.Exceptions;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using VZPStatAPI.Filter;
using VZPStatAPI.Helpers;
using VZPStatAPI.Services;
using VZPStatAPI.Wrappers;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoggerController : ControllerBase
    {
        protected APIResponse _APIResponse;
        protected PagedAPIResponse _pagedAPIResponse;
        protected ReturnResponse<LoggerController> _returnResponse;
        private readonly ILogger<LoggerController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IURIService uriService;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Log";
        private readonly string nameOfObjects = "Logs";
        private readonly string nameOfClass = "LoggerController";

        private string includeProperties = string.Empty;

        public LoggerController(
            ILogger<LoggerController> Logger,
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
            _returnResponse = new ReturnResponse<LoggerController>(unitOfWork, this.logger, mapper);

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllLogs")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<PagedAPIResponse>> GetAllLogs([FromQuery] PaginationFilter paginationFilter)
        {
            string info = nameOfObject + ": Function GetAllLogs(): ";
            List<LoggerGetDTO> dataDTO = new List<LoggerGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);

                (IEnumerable<Domain.Models.Logger> data, int TotalCount) result = (Enumerable.Empty<Domain.Models.Logger>(), 0);
                result = await unitOfWork.LoggerRepo.GetAllAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize),
                    null, includeProperties: includeProperties);


                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<LoggerGetDTO[]>(result.data).ToList();
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

        [HttpGet("ByDate", Name = "GetAllLogsByDate")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<PagedAPIResponse>> GetAllLogsByDate([FromQuery] PaginationFilter paginationFilter,
           DateTime DateFrom, DateTime DateTo)
        {
            string info = nameOfObject + ": Function GetAllLogsByDate(): ";
            List<LoggerGetDTO> dataDTO = new List<LoggerGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
                (IEnumerable<Domain.Models.Logger> data, int TotalCount) result = (Enumerable.Empty<Domain.Models.Logger>(), 0);
                result = await unitOfWork.LoggerRepo.GetAllAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize),
                    u => u.CreatedDate >= DateFrom &&
                    u.CreatedDate <= DateTo, includeProperties: includeProperties);


                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<LoggerGetDTO[]>(result.data).ToList();
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

        [HttpGet("{ID}", Name = "GetLogByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        [ResponseCache(CacheProfileName = "Default")]
        public async Task<ActionResult<APIResponse>> GetLogByID(int ID)
        {
            string info = nameOfObject + ": Function GetLogByID(int ID): ";

            LoggerGetDTO dataDTO = new LoggerGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.LoggerRepo.GetFirstOrDefaultAsync(x => x.LoggerId == ID,
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<LoggerGetDTO>(data);
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
                _pagedAPIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        //[HttpPatch("{ID}", Name = "UpdatePartialLog")]
        //[ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        //[ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        //[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        //[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        //public ActionResult<APIResponse> UpdatePartialLog(JsonPatchDocument<LoggerPutDTO> patchDTO, int ID)
        //{
        //    string info = nameOfObject + ": Function UpdatePartialLog(LoggerPutDTO dataDTO, int ID): ";
        //    try
        //    {
        //        if (testConnectionDatabase.IsServerConnected() == false)
        //        {
        //            throw new Exception(testConnectionDatabase.Error);
        //        }

        //        if (patchDTO is null)
        //        {
        //            _APIResponse.StatusCode = System.Net.HttpStatusCode.BadRequest;
        //            return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
        //        }
        //        var obj = unitOfWork.LoggerRepo.GetFirstOrDefault(x => x.LoggerId == ID, "", false);
        //        if (obj is null)
        //        {
        //            _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
        //            return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
        //        }

        //        var objPutDTO = mapper.Map<LoggerPutDTO>(obj);

        //        patchDTO.ApplyTo(objPutDTO, ModelState);

        //        Domain.Models.Logger loggerUpdated = mapper.Map<Domain.Models.Logger>(objPutDTO);
        //        loggerUpdated.LoggerId = ID;

        //        var result = unitOfWork.LoggerRepo.Update(loggerUpdated);
        //        if (result is false)
        //            throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
        //        else
        //            throw new ControllerExceptionSuccessUpdatedByID(nameOfObject, ID);
        //    }
        //    catch (ControllerExceptionExceptionUpdatedByID ex)
        //    {
        //        _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //        return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
        //    }
        //    catch (ControllerExceptionSuccessUpdatedByID ex)
        //    {
        //        _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
        //        return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //        return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
        //    }
        //}

        //[HttpDelete("{ID}", Name = "DeleteLogByID")]
        //[ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        //[ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        //[ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        //public async Task<ActionResult<APIResponse>> DeleteLogByID(int ID)
        //{
        //    string info = nameOfObject + ": Function DeleteLogByID(int ID): ";
        //    try
        //    {
        //        if (testConnectionDatabase.IsServerConnected() == false)
        //        {
        //            throw new Exception(testConnectionDatabase.Error);
        //        }

        //        var entity = await unitOfWork.LoggerRepo.GetFirstOrDefaultAsync(x => x.LoggerId == ID);
        //        if (entity is null)
        //        {
        //            _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
        //            return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
        //        }

        //        bool result = await unitOfWork.LoggerRepo.RemoveAsync(entity);

        //        if (result is false)
        //        {
        //            throw new ControllerExceptionDeleteByID(nameOfObject, ID);
        //        }
        //        else
        //        {
        //            throw new ControllerSuccessDeleteSuccessByID(nameOfObject, ID);
        //        }
        //    }
        //    catch (ControllerExceptionDeleteByID ex)
        //    {
        //        _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //        return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
        //    }
        //    catch (ControllerSuccessDeleteSuccessByID ex)
        //    {
        //        _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
        //        return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
        //    }
        //    catch (Exception ex)
        //    {
        //        _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
        //        return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
        //    }
        //}
    }
}
