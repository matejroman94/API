using AutoMapper;
using Common.Exceptions;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using VZPStatAPI.Wrappers;
using VZPStatAPI.Services;
using VZPStatAPI.Filter;
using VZPStatAPI.Helpers;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BranchController : ControllerBase
    {
        protected APIResponse _APIResponse;
        protected PagedAPIResponse _pagedAPIResponse;
        protected ReturnResponse<BranchController> _returnResponse;
        private readonly ILogger<BranchController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private readonly IURIService uriService;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Branch";
        private readonly string nameOfObjects = "Branches";
        private readonly string nameOfClass = "BranchController";

        public BranchController(
            ILogger<BranchController> Logger,
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
            _returnResponse = new ReturnResponse<BranchController>(unitOfWork, this.logger, mapper);

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllBranches")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<BranchGetDTO>>> GetAllBranches(string? includeProperties)
        {
            string info = nameOfObject + ": Function GetAllBranches(): ";
            List<BranchGetDTO> dataDTO = new List<BranchGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                (IEnumerable<Branch> data,int TotalCount) result = (Enumerable.Empty<Branch>(),0);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    result = await unitOfWork.Branches.GetAllAsync(Repository.Pagination.Filter.AllRecords(),
                        null, includeProperties);
                }
                else
                {
                    result = await unitOfWork.Branches.GetAllAsync(Repository.Pagination.Filter.AllRecords());
                }

                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<BranchGetDTO[]>(result.data).ToList();
                return Ok(dataDTO);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                logger.LogWarning(info + ex.Message);
                unitOfWork.LoggerRepo.Add(mapper.Map<Domain.Models.Logger>(new LoggerPutDTO(info + ex.Message)));
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(info + ex.Message);
                unitOfWork.LoggerRepo.Add(mapper.Map<Domain.Models.Logger>(new LoggerPutDTO(info + ex.Message)));
                Logger.Logger.NewOperationLog($"{nameOfClass} GetAll function failed: " + ex.Message, Logger.Logger.Level.Error);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("Pagination",Name = "GetAllBranchesPagination")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<PagedAPIResponse>> GetAllBranchesPagination([FromQuery] PaginationFilter paginationFilter, string? includeProperties)
        {
            string info = nameOfObject + ": Function GetAllBranchesPagination(): ";
            List<BranchGetDTO> dataDTO = new List<BranchGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var route = Request.Path.Value ?? string.Empty;
                var validFilter = new PaginationFilter(paginationFilter.PageNumber, paginationFilter.PageSize);
                (IEnumerable<Branch> data, int TotalCount) result = (Enumerable.Empty<Branch>(), 0);
                if (!string.IsNullOrWhiteSpace(includeProperties))
                {
                    result = await unitOfWork.Branches.GetAllAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize),
                        null, includeProperties);
                }
                else
                {
                    result = await unitOfWork.Branches.GetAllAsync(new Repository.Pagination.Filter(validFilter.PageNumber, validFilter.PageSize));
                }

                if (!result.data.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<BranchGetDTO[]>(result.data).ToList();

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

        [HttpGet("{ID}",Name = "GetBranchByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetBranchByID(int ID, string? IncludeProperties)
        {
            string info = nameOfObject + ": Function GetByID(int ID): ";
            BranchGetDTO dataDTO = new BranchGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                Branch? data = null;
                if(!string.IsNullOrWhiteSpace(IncludeProperties))
                {
                    data = await unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.BranchId == ID, IncludeProperties);
                }
                else
                {
                    data = await unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.BranchId == ID);
                }
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<BranchGetDTO>(data);

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
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpPost(Name = "AddBranchRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddBranchRange(IEnumerable<BranchPutDTO> DataDTOs)
        {
            string info = nameOfObject + ": Function Add(BranchPutDTO DataDTO): ";
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
                var data = mapper.Map<Branch[]>(DataDTOs);

                var result = await unitOfWork.Branches.AddRangeAsync(data);
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

        [HttpPut("{ID}",Name = "UpdateBranch")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdateBranch(BranchPutDTO dataDTO, int ID)
        {
            string info = nameOfObject + ": Function Update(BranchPutDTO dataDTO, int ID): ";
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
                var d = unitOfWork.Branches.GetFirstOrDefault(x => x.BranchId == ID);
                if (d is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                d.BranchName = dataDTO.BranchName;
                d.IpAddress = dataDTO.IpAddress;
                d.Port = dataDTO.Port;

                var result = unitOfWork.Branches.Update(d);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);

                _APIResponse.StatusCode = System.Net.HttpStatusCode.OK;
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

        [HttpPatch("{ID}",Name = "UpdatePartialBranch")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdatePartialBranch(JsonPatchDocument<BranchPutDTO> patchDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdatePartialBranch(BranchPutDTO dataDTO, int ID): ";
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
                var branch = unitOfWork.Branches.GetFirstOrDefault(x => x.BranchId == ID,"",false);
                if (branch is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                var branchPutDTO = mapper.Map<BranchPutDTO>(branch);

                patchDTO.ApplyTo(branchPutDTO,ModelState);

                Branch branchUpdated = mapper.Map<Branch>(branchPutDTO);
                branchUpdated.BranchId = ID;

                var result = unitOfWork.Branches.Update(branchUpdated);
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

        [HttpDelete("{ID}",Name = "DeleteBranchByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteBranchByID(int ID)
        {
            string info = nameOfObject + ": Function DeleteByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.BranchId == ID);
                if (entity is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                bool result = await unitOfWork.Branches.RemoveAsync(entity);

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
