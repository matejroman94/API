using AutoMapper;
using Common.Exceptions;
using Common;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AppController : ControllerBase
    {
        private readonly ILogger<AppController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "App";
        private readonly string nameOfObjects = "Apps";
        private readonly string nameOfClass = "AppController";

        private string includeProperties = $"{nameof(Domain.Models.App.Roles)}";

        public AppController(
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

            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllApps")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<AppGetDTO>>> GetAllApps()
        {
            string exception = nameOfObject + ": Function GetAllApps(): ";
            List<AppGetDTO> dataDTO = new List<AppGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Apps.GetAllAsync(Repository.Pagination.Filter.AllRecords(), null, includeProperties: includeProperties);
                
                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<AppGetDTO[]>(data.Item1).ToList();
                return Ok(dataDTO);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                logger.LogWarning(exception + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                Logger.Logger.NewOperationLog($"{nameOfClass} GetAll function failed: " + ex.Message, Logger.Logger.Level.Error);
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpGet("{ID}",Name = "GetAppByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppGetDTO>> GetAppByID(int ID)
        {
            string exception = nameOfObject + ": Function GetAppByID(int ID): ";
     
            AppGetDTO dataDTO = new AppGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Apps.GetFirstOrDefaultAsync(x => x.AppId == ID,
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<AppGetDTO>(data);
                return Ok(dataDTO);

            }
            catch (ControllerExceptionNotFoundById ex)
            {
                logger.LogWarning(exception + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpPost(Name = "AddAppRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddAppRange([FromBody]IEnumerable<AppPutDTO> DataDTOs)
        {
            string exception = nameOfObject + ": Function AddAppRange(AppPutDTO DataDTO): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (DataDTOs.Any() == false)
                {
                    return BadRequest();
                }
                var data = mapper.Map<App[]>(DataDTOs);

                var result = await unitOfWork.Apps.AddRangeAsync(data);
                if (result is false)
                    throw new ControllerExceptionExceptionAdded(nameOfObject);
                else
                    return NoContent();
            }
            catch (ControllerExceptionExceptionAdded ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
        }

        [HttpPut("{ID}",Name = "UpdateApp")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateApp(AppPutDTO dataDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdateApp(AppPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (dataDTO is null) return BadRequest();
                var d = unitOfWork.Apps.GetFirstOrDefault(x => x.AppId == ID);
                if (d is null) return NotFound();

                App app = new App();
                CopyClasses.Copy(dataDTO, ref app);
                app.AppId = ID;
                var result = unitOfWork.Apps.Update(app);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    return NoContent();
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{ID}", Name = "UpdatePartialApp")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePartialApp(JsonPatchDocument<AppPutDTO> patchDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdatePartialApp(AppPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (patchDTO is null) return BadRequest();
                var obj = unitOfWork.Apps.GetFirstOrDefault(x => x.AppId == ID, "", false);
                if (obj is null) return NotFound();

                var objPutDTO = mapper.Map<AppPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                App appUpdated = mapper.Map<App>(objPutDTO);
                appUpdated.AppId = ID;

                var result = unitOfWork.Apps.Update(appUpdated);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    return NoContent();
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{ID}",Name = "DeleteAppByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteAppByID(int ID)
        {
            string exception = nameOfObject + ": Function DeleteAppByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Apps.GetFirstOrDefaultAsync(x => x.AppId == ID);
                if (entity is null) return NotFound();

                bool result = await unitOfWork.Apps.RemoveAsync(entity);

                if (result is false) 
                    throw new ControllerExceptionDeleteByID(nameOfObject, ID);

                return NoContent();
            }
            catch (ControllerExceptionDeleteByID ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
