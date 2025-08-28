using AutoMapper;
using Common.Exceptions;
using Common;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using VZPStatAPI.LDAP;
using VZPStatAPI.FakeData;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ILogger<UserController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "User";
        private readonly string nameOfObjects = "Users";
        private readonly string nameOfClass = "UserController";

        private string includeProperties = $"{nameof(Domain.Models.User.Roles)}," +
                                            $"{nameof(Domain.Models.User.Branches)}";

        public UserController(
            ILogger<UserController> Logger,
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

        [HttpGet(Name = "GetAllUsers")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<UserGetDTO>>> GetAllUsers()
        {
            string exception = nameOfObject + ": Function GetAllUsers(): ";     
            List<UserGetDTO> dataDTO = new List<UserGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Users.GetAllAsync(Repository.Pagination.Filter.AllRecords(),
                    null,includeProperties: includeProperties);


                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<UserGetDTO[]>(data.Item1).ToList();
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID:int}",Name = "GetUserByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<UserGetDTO>> GetUserByID(int ID)
        {
            string exception = nameOfObject + ": Function GetUserByID(int ID): ";
            UserGetDTO dataDTO = new UserGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Users.GetFirstOrDefaultAsync(x => x.UserId == ID,
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                dataDTO = mapper.Map<UserGetDTO>(data);
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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("LDAP/Login/{login}", Name = "GetUserByLogin")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LDAP_response>>> GetUserByLogin(string login)
        {
            string exception = nameOfObject + ": Function GetUserByLogin(string login): ";
            LDAP_response lDAP_Response = new LDAP_response();

            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                LDAP.LDAP_Helper.Helper helper = new LDAP.LDAP_Helper.Helper(configuration, unitOfWork, login);
                lDAP_Response = await helper.Get_LDAP_ResponseAsync();

                lDAP_Response.Success = true;
                return Ok(lDAP_Response);

            }
            catch (ControllerExceptionNotFoundBy ex)
            {
                lDAP_Response.Error = ex.Message;
                logger.LogWarning(exception + ex.Message);
                return NotFound(lDAP_Response);
            }
            catch (Exception ex)
            {
                string msg = ex.Message + ex.InnerException?.Message;
                lDAP_Response.Error = msg;
                logger.LogError(exception + msg);
                return StatusCode(StatusCodes.Status500InternalServerError, lDAP_Response);
            }
        }

        [HttpGet("Test/Login/{login}", Name = "GetUserByLoginTest")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<LDAP_response>>> GetUserByLoginTest(string login)
        {
            string exception = nameOfObject + ": Function GetUserByLoginTest(string login): ";
            UserGetDTO dataDTO = new UserGetDTO();
            LDAP_response lDAP_Response = new LDAP_response();

            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Users.GetFirstOrDefaultAsync(x => x.Login.Equals(login),
                    includeProperties: includeProperties);
                if (data is null)
                    throw new ControllerExceptionNotFoundBy(nameOfObject, login);
                dataDTO = mapper.Map<UserGetDTO>(data);

                LDAP_faker lDAP_Faker = new LDAP_faker();
                var vzpcodesRegions = await unitOfWork.Regions.GetAllAsync(Repository.Pagination.Filter.AllRecords());
                var vzpcodesBranches = await unitOfWork.Branches.GetAllAsync(Repository.Pagination.Filter.AllRecords());

                List<string> vzpCodes = new List<string>();
                foreach (var vbp in vzpcodesRegions.Item1)
                {
                    vzpCodes.Add(vbp.RegionId.ToString());
                }
                foreach (var vbp in vzpcodesBranches.Item1)
                {
                    vzpCodes.Add(vbp.VZP_code);
                }

                var searchResult = lDAP_Faker.GetFakeRolesAndWorkPlaces(vzpCodes);

                LDAP.LDAP_Helper.HelperTest helper = new LDAP.LDAP_Helper.HelperTest(unitOfWork);
                lDAP_Response = await helper.Get_LDAP_ResponseAsync(searchResult);

                lDAP_Response.Success = true;
                return Ok(lDAP_Response);

            }
            catch (ControllerExceptionNotFoundBy ex)
            {
                lDAP_Response.Error = ex.Message;
                logger.LogWarning(exception + ex.Message);
                return NotFound(lDAP_Response);
            }
            catch (Exception ex)
            {
                string msg = ex.Message + ex.InnerException?.Message;
                lDAP_Response.Error = msg;
                logger.LogError(exception + msg);
                return StatusCode(StatusCodes.Status500InternalServerError, lDAP_Response);
            }
        }

        [HttpPost(Name = "AddUser")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddUser(IEnumerable<UserPutDTO> DataDTOs)
        {
            string exception = nameOfObject + ": Function AddUser(UserPutDTO DataDTO): ";
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

                var data = mapper.Map<User[]>(DataDTOs);

                var result = await unitOfWork.Users.AddRangeAsync(data);
                if (result is false)
                    throw new ControllerExceptionExceptionAdded(nameOfObject);

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
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut("{ID}",Name = "UpdateUser")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateUser(UserPutDTO dataDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdateUser(UserPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (dataDTO is null) return BadRequest();
                var d = unitOfWork.Users.GetFirstOrDefault(x => x.UserId == ID);
                if (d is null) return NotFound();

                User user = new User();
                CopyClasses.Copy(dataDTO, ref user);
                user.UserId = ID;
                var result = unitOfWork.Users.Update(user);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);

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

        [HttpPatch("{ID}", Name = "UpdatePartialUser")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePartialUser(JsonPatchDocument<UserPutDTO> patchDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdatePartialUser(UserPutDTO patchDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (patchDTO is null) return BadRequest();
                var obj = unitOfWork.Users.GetFirstOrDefault(x => x.UserId == ID, "", false);
                if (obj is null) return NotFound();

                var objPutDTO = mapper.Map<UserPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                User userUpdated = mapper.Map<User>(objPutDTO);
                userUpdated.UserId = ID;

                var result = unitOfWork.Users.Update(userUpdated);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);

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

        [HttpDelete("{ID}",Name = "DeleteUserByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteUserByID(int ID)
        {
            string exception = nameOfObject + ": Function DeleteUserByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Users.GetFirstOrDefaultAsync(x => x.UserId == ID);
                if (entity is null) NotFound();

                bool result = await unitOfWork.Users.RemoveAsync(entity!);

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
