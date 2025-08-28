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

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ActivityController : ControllerBase
    {
        protected APIResponse _APIResponse;
        protected ReturnResponse<AppController> _returnResponse;
        private readonly ILogger<AppController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Activity";
        private readonly string nameOfObjects = "Activities";
        private readonly string nameOfClass = "ActivityController";

        private string includeProperties = $"{nameof(Domain.Models.Activity.Branch)}";

        public ActivityController(
            ILogger<AppController> Logger,
            IUnitOfWork UnitOfWork,
            IMapper Mapper,
            IConfiguration Configuration
            )
        {
            logger = Logger;
            unitOfWork = UnitOfWork;
            mapper = Mapper;
            configuration = Configuration;
            _APIResponse = new();
            _returnResponse = new ReturnResponse<AppController>(unitOfWork,logger,mapper);

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllActivites")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllActivites()
        {
            string info = nameOfObject + ": Function GetAllActivites(): ";
            List<ActivityGetDTO> dataDTO = new List<ActivityGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Activities.GetAllAsync(Repository.Pagination.Filter.AllRecords(),null,includeProperties: includeProperties);

                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<ActivityGetDTO[]>(data.Item1).ToList();
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
                _APIResponse.Result = dataDTO;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("ActivitiesDistinctNames",Name = "GetAllActivitesDistinctNames")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetAllActivitesDistinctNames()
        {
            string info = nameOfObject + ": Function GetAllActivites(): ";
            List<ActivityGetDTO> dataDTO = new List<ActivityGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Activities.GetAllAsync(Repository.Pagination.Filter.AllRecords(), null,includeProperties: includeProperties);
                data.Item1 = data.Item1.GroupBy(x => x.ActivityName.ToLower()).Select(group => group.First());

                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<ActivityGetDTO[]>(data.Item1).ToList();
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
                _APIResponse.Result = dataDTO;
                return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Info, ex);
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return _returnResponse.Response(false, _APIResponse, info, Logger.Logger.Level.Error, ex);
            }
        }

        [HttpGet("{ID}", Name = "GetActivityByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> GetActivityByID(int ID)
        {
            string info = nameOfObject + ": Function GetActivityByID(int ID): ";

            ActivityGetDTO dataDTO = new ActivityGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Activities.GetFirstOrDefaultAsync(x => x.ActivityId == ID,
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<ActivityGetDTO>(data);
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

        [HttpPost(Name = "AddActivityRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddActivityRange([FromBody] IEnumerable<ActivityPutDTO> DataDTOs)
        {
            string info = nameOfObject + ": Function AddActivityRange(ActivityPutDTO DataDTO): ";
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
                var data = mapper.Map<Activity[]>(DataDTOs);

                var result = await unitOfWork.Activities.AddRangeAsync(data);
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

        [HttpPut("{ID}", Name = "UpdateActivity")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdateActivity(ActivityPutDTO dataDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdateActivity(ActivityPutDTO dataDTO, int ID): ";
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
                var d = unitOfWork.Activities.GetFirstOrDefault(x => x.ActivityId == ID);
                if (d is null) 
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                Activity app = new Activity();
                CopyClasses.Copy(dataDTO, ref app);
                app.ActivityId = ID;
                var result = unitOfWork.Activities.Update(app);
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

        [HttpPatch("{ID}", Name = "UpdatePartialActivity")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<APIResponse> UpdatePartialActivity(JsonPatchDocument<ActivityPutDTO> patchDTO, int ID)
        {
            string info = nameOfObject + ": Function UpdatePartialActivity(ActivityPutDTO dataDTO, int ID): ";
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
                var obj = unitOfWork.Activities.GetFirstOrDefault(x => x.ActivityId == ID, "", false);
                if (obj is null)
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                var objPutDTO = mapper.Map<ActivityPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Activity activityUpdated = mapper.Map<Activity>(objPutDTO);
                activityUpdated.ActivityId = ID;

                var result = unitOfWork.Activities.Update(activityUpdated);
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

        [HttpDelete("{ID}", Name = "DeleteActivityByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> DeleteActivityByID(int ID)
        {
            string info = nameOfObject + ": Function DeleteActivityByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Activities.GetFirstOrDefaultAsync(x => x.ActivityId == ID);
                if (entity is null) 
                {
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return _returnResponse.Response(true, _APIResponse, info, Logger.Logger.Level.Warning);
                }

                bool result = await unitOfWork.Activities.RemoveAsync(entity);

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
