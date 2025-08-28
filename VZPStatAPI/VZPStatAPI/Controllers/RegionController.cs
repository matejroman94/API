using AutoMapper;
using Common;
using Common.Exceptions;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Repository.Interfaces;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegionController : ControllerBase
    {
        private readonly ILogger<RegionController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;

        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Region";
        private readonly string nameOfObjects = "Regions";

        public RegionController(
            ILogger<RegionController> Logger,
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

        [HttpGet(Name = "GetAllRegions")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<RegionGetDTO>>> GetAllRegions()
        {
            string exception = nameOfObject + ": Function GetAllRegions(): ";
            List<RegionGetDTO> dataDTO = new List<RegionGetDTO>();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Regions.GetAllAsync(Repository.Pagination.Filter.AllRecords(),
                    null,includeProperties: $"{nameof(Domain.Models.Region.Branches)}");
                
                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<RegionGetDTO[]>(data.Item1).ToList();
                return Ok(dataDTO);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                logger.LogWarning(exception + ex.Message);
                Logger.Logger.NewOperationLog("RegionController GetAll function failed: " + ex.Message, Logger.Logger.Level.Warning);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                Logger.Logger.NewOperationLog("RegionController GetAll function failed: " + ex.Message, Logger.Logger.Level.Error);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{ID}",Name = "GetRegionByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<RegionGetDTO>> GetRegionByID(int ID)
        {
            string exception = nameOfObject + ": Function GetRegionByID(int ID): ";
            RegionGetDTO categoryDTO = new RegionGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Regions.GetFirstOrDefaultAsync(x => x.RegionId == ID,
                    includeProperties: $"{nameof(Region.Branches)}");
                if (data is null)
                    throw new ControllerExceptionNotFoundById(nameOfObject, ID);
                categoryDTO = mapper.Map<RegionGetDTO>(data);
                return Ok(categoryDTO);

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

        [HttpPost(Name = "AddRegionRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddRegionRange(IEnumerable<RegionPutDTO> DataDTOs)
        {
            string exception = nameOfObject + ": Function AddRegionRange(RegionPutDTO DataDTO): ";
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

                var data = mapper.Map<Region[]>(DataDTOs);

                var result = await unitOfWork.Regions.AddRangeAsync(data);
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

        [HttpPut("{ID}",Name = "UpdateRange")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateRange(RegionPutDTO dataDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdateRange(RegionPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (dataDTO is null) return BadRequest();
                var d = unitOfWork.Regions.GetFirstOrDefault(x => x.RegionId == ID);
                if (d is null) return NotFound();

                Region region = new Region();
                CopyClasses.Copy(dataDTO, ref region);
                region.RegionId = ID;
                var result = unitOfWork.Regions.Update(region);
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

        [HttpPatch("{ID}", Name = "UpdatePartialRegion")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePartialRegion(JsonPatchDocument<RegionPutDTO> patchDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdatePartialRegion(RegionPutDTO patchDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (patchDTO is null) return BadRequest();
                var obj = unitOfWork.Regions.GetFirstOrDefault(x => x.RegionId == ID, "", false);
                if (obj is null) return NotFound();

                var objPutDTO = mapper.Map<RegionPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Region regionUpdated = mapper.Map<Region>(objPutDTO);
                regionUpdated.RegionId = ID;

                var result = unitOfWork.Regions.Update(regionUpdated);
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

        [HttpDelete("{ID}",Name = "DeleteRegionByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteRegionByID(int ID)
        {
            string exception = nameOfObject + ": Function DeleteRegionByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = await unitOfWork.Regions.GetFirstOrDefaultAsync(x => x.RegionId == ID);
                if (entity is null) return NotFound();

                bool result = await unitOfWork.Regions.RemoveAsync(entity);

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
