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
    public class RoleController : ControllerBase
    {
        private readonly ILogger<RoleController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Role";
        private readonly string nameOfObjects = "Roles";
        private readonly string nameOfClass = "RoleController";

        private string includeProperties = $"{nameof(Domain.Models.Role.Apps)}," +
                    $"{nameof(Domain.Models.Role.Users)}";

        public RoleController(
            ILogger<RoleController> Logger,
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

        [HttpGet(Name = "GetAllRoles")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RoleGetDTO>>> GetAllRoles()
        {
            string exception = nameOfObject + ": Function GetAllRoles(): ";           
            List<RoleGetDTO> dataDTO = new List<RoleGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Roles.GetAllAsync(Repository.Pagination.Filter.AllRecords(),
                    null,includeProperties: includeProperties);

                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<RoleGetDTO[]>(data.Item1).ToList();
                throw new ControllerExceptionGetAllSuccess(nameOfObjects);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                logger.LogWarning(exception + ex.Message);
                return NotFound(ex.Message);
            }
            catch (ControllerExceptionGetAllSuccess ex)
            {
                logger.LogInformation(ex.Message);
                return Ok(dataDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                Logger.Logger.NewOperationLog($"{nameOfClass} GetAll function failed: " + ex.Message, Logger.Logger.Level.Error);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}",Name = "GetRoleByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RoleGetDTO>> GetRoleByID(int ID)
        {
            string exception = nameOfObject + ": Function GetRoleByID(int ID): ";
            RoleGetDTO dataDTO = new RoleGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Roles.GetFirstOrDefaultAsync(x => x.RoleId == ID,
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<RoleGetDTO>(data);
                throw new ControllerExceptionFoundByIdSuccess(nameOfObject, ID);

            }
            catch (ControllerExceptionNotFoundById ex)
            {
                logger.LogWarning(exception + ex.Message);
                return NotFound(ex.Message);
            }
            catch (ControllerExceptionFoundByIdSuccess ex)
            {
                logger.LogInformation(ex.Message);
                return Ok(dataDTO);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost(Name = "AddRole")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddRole(IEnumerable<RolePutDTO> DataDTOs)
        {
            string exception = nameOfObject + ": Function AddRole(RolePutDTO DataDTO): ";
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
                var data = mapper.Map<Role[]>(DataDTOs);

                var result = await unitOfWork.Roles.AddRangeAsync(data);
                if (result is false)
                    throw new ControllerExceptionExceptionAdded(nameOfObject);
                else
                    throw new ControllerExceptionSuccessAdded(nameOfObject);
            }
            catch (ControllerExceptionExceptionAdded ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (ControllerExceptionSuccessAdded ex)
            {
                logger.LogInformation(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{ID}",Name = "UpdateRole")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateRole(RolePutDTO dataDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdateRole(UserPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (dataDTO is null) return BadRequest();
                var d = unitOfWork.Roles.GetFirstOrDefault(x => x.RoleId == ID);
                if (d is null) return NotFound();

                Role role = new Role();
                CopyClasses.Copy(dataDTO, ref role);
                role.RoleId = ID;
                var result = unitOfWork.Roles.Update(role);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    throw new ControllerExceptionSuccessUpdatedByID(nameOfObject, ID);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (ControllerExceptionSuccessUpdatedByID ex)
            {
                logger.LogInformation(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{ID}", Name = "UpdatePartialRole")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePartialRole(JsonPatchDocument<RolePutDTO> patchDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdatePartialRole(RolePutDTO patchDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (patchDTO is null) return BadRequest();
                var obj = unitOfWork.Roles.GetFirstOrDefault(x => x.RoleId == ID, "", false);
                if (obj is null) return NotFound();

                var objPutDTO = mapper.Map<RolePutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Role roleUpdated = mapper.Map<Role>(objPutDTO);
                roleUpdated.RoleId = ID;

                var result = unitOfWork.Roles.Update(roleUpdated);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    throw new ControllerExceptionSuccessUpdatedByID(nameOfObject, ID);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (ControllerExceptionSuccessUpdatedByID ex)
            {
                logger.LogInformation(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{ID}",Name = "DeleteRoleByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteRoleByID(int ID)
        {
            string exception = nameOfObject + ": Function DeleteRoleByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Roles.GetFirstOrDefaultAsync(x => x.RoleId == ID);
                if (entity is null) return NotFound();

                bool result = await unitOfWork.Roles.RemoveAsync(entity);

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
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (ControllerSuccessDeleteSuccessByID ex)
            {
                logger.LogInformation(ex.Message);
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
