using AutoMapper;
using Common;
using Common.Exceptions;
using Domain.DataDTO;
using Domain.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repository.Interfaces;
using VZPStat.EventAsByte;
using VZPStatAPI.Helpers;
using VZPStatAPI.Services;
using VZPStatAPI.Wrappers;
using VZPStatAPI.Wrappers.ConfigData;
using static Logger.Logger;

namespace VZPStatAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        protected APIResponse _APIResponse;
        protected ReturnResponse<EventController> _returnResponse;

        private readonly ILogger<EventController> logger;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IConfiguration configuration;
        private ConfigFileLog configFileLog;

        private readonly EventPostProcessing eventPostProcessing;
        private readonly TestConnectionDatabase testConnectionDatabase;

        private readonly string nameOfObject = "Event";
        private readonly string nameOfObjects = "Events";

        public EventController(
            ILogger<EventController> Logger,
            IUnitOfWork UnitOfWork,
            IMapper Mapper,
            IConfiguration Configuration,
            ConfigFileLog configFileLog
            )
        {
            logger = Logger;
            unitOfWork = UnitOfWork;
            mapper = Mapper;
            configuration = Configuration;
            this.configFileLog = configFileLog;
            _APIResponse = new();
            _returnResponse = new ReturnResponse<EventController>(unitOfWork, this.logger, mapper);

            eventPostProcessing = new EventPostProcessing(ref unitOfWork,ref mapper);
            testConnectionDatabase = new TestConnectionDatabase(configuration.GetConnectionString("VZPDatabase"));
        }

        [HttpGet(Name = "GetAllEvents")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EventGetDTO>>> GetAllEvents()
        {
            string exception = nameOfObject + ": Function GetAllEvents(): ";
            List<EventGetDTO> dataDTO = new List<EventGetDTO>();
            try
            {
                var data = await unitOfWork.Events.GetAllAsync(Repository.Pagination.Filter.AllRecords());
                if (!data.Item1.Any())
                {
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                dataDTO = mapper.Map<EventGetDTO[]>(data.Item1).ToList();
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
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("GetLastEventAsBytes/{BranchCode}",Name = "GetEventAsBytes")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EventAsByte>> GetEventAsBytes(string BranchCode)
        {
            string exception = nameOfObject + ": GetLastEventAsBytes(string BranchCode): ";
            EventAsByte eventAsByte = new EventAsByte();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Events.GetLastEventByBranchCodeAsync(BranchCode);
                if (data is null)
                {
                    eventAsByte.msg = "Events are empty!";
                    throw new ControllerExceptionNotFoundAny(nameOfObjects);
                }
                eventAsByte.code = data.EventHex;
                eventAsByte.date = data.DateReceived is null ? string.Empty : data.DateReceived;
                return Ok(eventAsByte);
            }
            catch (ControllerExceptionNotFoundAny ex)
            {
                logger.LogWarning(exception + ex.Message);
                return NotFound(eventAsByte);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);

                eventAsByte.code = string.Empty;
                eventAsByte.date = string.Empty;
                eventAsByte.msg = ex.Message + ex.InnerException?.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, eventAsByte);
            }
        }

        [HttpGet("{ID}", Name = "GetEventByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EventGetDTO>> GetEventByID(int ID)
        {
            string exception = nameOfObject + ": Function GetEventByID(int ID): ";
            EventGetDTO eventDTO = new EventGetDTO();
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var data = await unitOfWork.Events.GetFirstOrDefaultAsync(x => x.EventId == ID);
                if (data is null)
                    throw new ControllerExceptionAddedByID(nameOfObject, ID);
                eventDTO = mapper.Map<EventGetDTO>(data);
                return Ok(eventDTO);

            }
            catch (ControllerExceptionAddedByID ex)
            {
                logger.LogError(exception + ex.Message);
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost(Name = "AddEvent")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddEvent(EventPutDTO DataDTO)
        {
            string exception = nameOfObject + ": Function AddEvent(EventPutDTO DataDTO): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (DataDTO is null)
                {
                    return BadRequest();
                }

                var data = new Event();
                data = mapper.Map<Event>(DataDTO);

                var result = await unitOfWork.Events.AddAsync(data);
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

        [HttpGet("GetActivity/{ActivitityID}",Name = "GetActivity")]
        [ProducesResponseType(statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult<Domain.Models.Activity> GetActivity(int ActivitityID)
        {
            string exception = nameOfObject + ": Function GetActivity(int ActivitityID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                Domain.Models.Activity? entity = unitOfWork.Activities.GetFirstOrDefault(u => u.ActivityId == ActivitityID);
                if (entity == null) return NotFound();

                return Ok(entity);
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("PostActivity/{BranchCode}",Name = "PostActivity")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult PostActivity(ActivityPutDTO activityPutDTO, string BranchCode)
        {
            string exception = nameOfObject + ": Function PostActivity(ActivityPutDTO activityPutDTO, string BranchCode): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var branch = unitOfWork.Branches.GetFirstOrDefault(u => u.VZP_code.Equals(BranchCode));
                if (branch is null) return NotFound("Cannot find branch!");

                var entity = mapper.Map<Domain.Models.Activity>(activityPutDTO);

                branch.Activities.Add(entity);
                bool res = unitOfWork.Branches.Update(branch);
                if (res is false) 
                {
                    throw new Exception("Cannot added to database!");
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPatch("{ID}", Name = "UpdatePartialEvent")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdatePartialEvent(JsonPatchDocument<EventPutDTO> patchDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdatePartialEvent(EventPutDTO patchDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (patchDTO is null) return BadRequest();
                var obj = unitOfWork.Events.GetFirstOrDefault(x => x.EventId == ID, "", false);
                if (obj is null) return NotFound();

                var objPutDTO = mapper.Map<EventPutDTO>(obj);

                patchDTO.ApplyTo(objPutDTO, ModelState);

                Event eventUpdated = mapper.Map<Event>(objPutDTO);
                eventUpdated.EventId = ID;

                var result = unitOfWork.Events.Update(eventUpdated);
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

        [HttpPost("Config/{BranchCode}",Name = "AddConfig")]
        [ProducesResponseType(typeof(APIResponse), statusCode: StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(APIResponse), statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(APIResponse), statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddConfig([FromBody] ConfigEventData Data, string BranchCode)
        {
            string exception = nameOfObject + ": Function AddConfig(ConfigEventData Data, string BranchCode): ";
            try
            {
                string jsonString = JsonConvert.SerializeObject(Data);

                Branch? branch = await unitOfWork.Branches.GetFirstOrDefaultAsync(x => x.VZP_code.Equals(BranchCode));
                if(branch != null)
                {
                    FillActivity(Data.Act, branch.BranchId);
                    FillClerks(Data.Clrk, branch.BranchId);
                    FillCounter(Data.Cntrcfg, branch.BranchId);
                    FillPrinters(Data.Prn, branch.BranchId);

                    // Log only once per day
                    string llog = $"ConfigEventData received for branch {branch.VZP_code}." + $"Data: {jsonString}";
                    if (!configFileLog.ConfigFileContents.Contains(llog))
                    {
                        Logger.Logger.NewOperationLog(llog, Logger.Logger.Level.Info);
                        configFileLog.ConfigFileContents.Add(llog);
                    }
                }
                else
                {
                    Logger.Logger.NewOperationLog($"Branch {BranchCode} cannot be found!", Logger.Logger.Level.Warning);
                    _APIResponse.StatusCode = System.Net.HttpStatusCode.NotFound;
                    return NotFound(_returnResponse.Response(false, _APIResponse, exception, Level.Warning));
                }

                _APIResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
                return Ok(_returnResponse.Response(true, _APIResponse, string.Empty, Level.Info));
            }
            catch (Exception ex)
            {
                _APIResponse.StatusCode = System.Net.HttpStatusCode.InternalServerError;
                return StatusCode(StatusCodes.Status500InternalServerError, _returnResponse.Response(false, _APIResponse, exception, Level.Error, ex));
            }
        }

        [HttpPost("EventAsBytes/{BranchCode}",Name = "AddEventByBranchCode")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<ResultInfo>> AddEventByBranchCode(IEnumerable<EventAsByte> Data, string BranchCode)
        {
            string exception = nameOfObject + ": AddEventByBranchCode(IEnumerable<EventAsByte> Data,string BranchCode): ";
            string exceptionWrogFormat = "Event as bytes was not in a correct format!\n";
            string exceptionOverFlow = "Event as bytes parameter overflow!\n";
            string exceptionContext = "Cannot added to dbContext. Failed: ";
            List<EventPutDTO> dataDTO = new List<EventPutDTO>();
            List<Event> events = new List<Event>();
            try
            {

                if(testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (Data.Any() is false)
                {
                    return BadRequest();
                }

                Branch? branch = await unitOfWork.Branches.GetFirstOrDefaultAsync(u => u.VZP_code.ToLower().Equals(BranchCode.ToLower()));
                if (branch is null) 
                {
                    return NotFound("Cannot find branch!");
                }

                foreach (var eventAsByte in Data)
                {
                    try
                    {
                        EventPutDTO eventPutDTO;
                        EventProcessing._fromCodeToEventPutDTO(eventAsByte, out eventPutDTO);
                        eventPutDTO.BranchId = branch.BranchId;

                        try
                        {
                            bool res = eventPostProcessing.Run(eventAsByte, eventPutDTO);
                            if (res is true)
                            {
                                eventPutDTO.FullyProcessed = true;
                            }
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(exception + "eventPostProcessing: " + ex.Message);
                        }

                        dataDTO.Add(eventPutDTO);
                    }
                    catch (FormatException ex)
                    {
                        Logger.Logger.NewOperationLog(exceptionWrogFormat + ex.Message, Logger.Logger.Level.Warning);
                        logger.LogError(exceptionWrogFormat + ex.Message);
                    }
                    catch (OverflowException ex)
                    {
                        Logger.Logger.NewOperationLog(exceptionOverFlow + ex.Message, Logger.Logger.Level.Warning);
                        logger.LogError(exceptionOverFlow + ex.Message);
                    }
                    catch (Exception ex)
                    {
                        Logger.Logger.NewOperationLog(exception + ex.Message, Logger.Logger.Level.Warning);
                        logger.LogError(exception + ex.Message);
                    }
                }
                events = mapper.Map<Event[]>(dataDTO).ToList();
                bool result = true;
                try
                {
                    bool res = false;
                    events.ForEach(ev =>
                    {
                        res = unitOfWork.Events.Add(ev);
                        if (res is false)
                        {
                            Logger.Logger.NewOperationLog(exception + $" add event to database failed: code: {ev.EventHex},date: {ev.DateReceived}", Logger.Logger.Level.Warning);
                        }
                    });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message;
                    msg += ex.InnerException?.Message;
                    logger.LogError(msg);
                    Logger.Logger.NewOperationLog(msg, Logger.Logger.Level.Warning);
                    result = false;
                }

                if (result is false)
                    throw new ControllerExceptionExceptionAdded(nameOfObjects);
                else
                    throw new ControllerExceptionSuccessAdded(nameOfObjects);
            }
            catch (ControllerExceptionExceptionAdded ex)
            {
                Logger.Logger.NewOperationLog(exception + ex.Message, Logger.Logger.Level.Warning);
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (ControllerExceptionEventAsBytes ex)
            {
                Logger.Logger.NewOperationLog(exception + ex.Message, Logger.Logger.Level.Warning);
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
                Logger.Logger.NewOperationLog(exception + ex.Message, Logger.Logger.Level.Warning);
                logger.LogError(exception + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost("FakeData/{BranchCode}",Name = "AddFakeEvent")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddFakeEvent(string BranchCode)
        {
            string exception = nameOfObject + ": AddFakeEvent(IEnumerable<EventAsByte> Data,string BranchCode): ";
            string exceptionWrogFormat = "Event as bytes was not in a correct format!\n";
            string exceptionOverFlow = "Event as bytes parameter overflow!\n";
            string exceptionContext = "Cannot added to dbContext. Failed: ";
            var faker = new FakeData.Faker();
            List<EventPutDTO> dataDTO = new List<EventPutDTO>();
            List<Event> events = new List<Event>();
            List<EventAsByte> Data;

            if (testConnectionDatabase.IsServerConnected() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception + testConnectionDatabase.Error);
            }

            Branch? branch = unitOfWork.Branches.GetFirstOrDefault(u => u.VZP_code.Equals(BranchCode));
            if (branch is null) return NotFound(exception + "Cannot find branch!");

            for (int i = 1; i < 32; i++)
            {
                if (i == 1)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_01h();

                }
                else if (i == 2)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_81h();

                }
                else if (i == 3)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_41h();

                }
                else if (i == 4)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_C1h();

                }
                else if (i == 5)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_A1h();

                }
                else if (i == 6)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_02h();

                }
                else if (i == 7)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_03h();

                }
                else if (i == 8)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_04h();

                }
                else if (i == 9)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_05h();

                }
                else if (i == 10)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_06h();

                }
                else if (i == 11)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_08h();

                }
                else if (i == 12)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_07h();

                }
                else if (i == 13)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_09h();

                }
                else if (i == 14)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Ah();

                }
                else if (i == 15)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Bh();

                }
                else if (i == 16)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_10h();

                }
                else if (i == 17)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_11h();

                }
                else if (i == 18)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Ch();

                }
                else if (i == 19)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Dh();

                }
                else if (i == 20)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Eh();

                }
                else break;

                dataDTO.Clear();
                events.Clear();
                try
                {
                   
                    if (Data.Any() is false)
                    {
                        return BadRequest();
                    }

                    foreach (var da in Data)
                    {
                        try
                        {
                            EventPutDTO eventPutDTO;
                            EventProcessing._fromCodeToEventPutDTO(da, out eventPutDTO);
                            eventPutDTO.BranchId = branch.BranchId;
                            eventPutDTO.FullyProcessed = true;
                            dataDTO.Add(eventPutDTO);
                        }
                        catch (FormatException ex)
                        {
                            logger.LogError(exceptionWrogFormat + ex.Message);
                        }
                        catch (OverflowException ex)
                        {
                            logger.LogError(exceptionOverFlow + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex.Message);
                        }
                    }

                    events = mapper.Map<Event[]>(dataDTO).ToList();
                    bool result = true;
                    try
                    {
                        bool res = await unitOfWork.Events.AddRangeAsync(events);
                        if (res is false)
                        {
                            throw new Exception("Failed post data to the database. BranchId: " + BranchCode);
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        msg += ex.InnerException?.Message;
                        logger.LogError(msg);
                        result = false;
                    }

                    if (result is false)
                        throw new ControllerExceptionExceptionAdded(nameOfObjects);
                    else
                        throw new ControllerExceptionSuccessAdded(nameOfObjects);
                }
                catch (ControllerExceptionExceptionAdded ex)
                {
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                catch (ControllerExceptionEventAsBytes ex)
                {
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                catch (ControllerExceptionSuccessAdded ex)
                {
                    logger.LogInformation(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return NoContent();
        }

        [HttpPost("FakeDataDatabaseTesting/{BranchCode}",Name = "AddFakeEventToDatabase")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> AddFakeEventToDatabase(string BranchCode)
        {
            string exception = nameOfObject + ": AddFakeEventToDatabase(IEnumerable<EventAsByte> Data,string BranchCode): ";
            string exceptionWrogFormat = "Event as bytes was not in a correct format!\n";
            string exceptionOverFlow = "Event as bytes parameter overflow!\n";
            string exceptionContext = "Cannot added to dbContext. Failed: ";
            var faker = new FakeData.Faker();
            List<EventPutDTO> dataDTO = new List<EventPutDTO>();
            List<Event> events = new List<Event>();
            List<EventAsByte> Data;

            if (testConnectionDatabase.IsServerConnected() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, exception + testConnectionDatabase.Error);
            }

            Branch? branch = unitOfWork.Branches.GetFirstOrDefault(u => u.VZP_code.Equals(BranchCode));
            if (branch is null) return NotFound(exception + "Cannot find branch!");

            for (int i = 1; i < 32; i++)
            {
                if (i == 1)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_01h();

                }
                else if (i == 2)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_81h();

                }
                else if (i == 3)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_41h();

                }
                else if (i == 4)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_C1h();

                }
                else if (i == 5)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_A1h();

                }
                else if (i == 6)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_02h();

                }
                else if (i == 7)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_03h();

                }
                else if (i == 8)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_04h();

                }
                else if (i == 9)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_05h();

                }
                else if (i == 10)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_06h();

                }
                else if (i == 11)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_08h();

                }
                else if (i == 12)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_07h();

                }
                else if (i == 13)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_09h();

                }
                else if (i == 14)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Ah();

                }
                else if (i == 15)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Bh();

                }
                else if (i == 16)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_10h();

                }
                else if (i == 17)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_11h();

                }
                else if (i == 18)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Ch();

                }
                else if (i == 19)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Dh();

                }
                else if (i == 20)
                {
                    Data = (List<EventAsByte>)faker.GetFakeEvents_0Eh();

                }
                else break;

                dataDTO.Clear();
                events.Clear();
                int j = 1;
                try
                {

                    foreach (var da in Data)
                    {
                        try
                        {
                            EventPutDTO eventPutDTO;
                            EventProcessing._fromCodeToEventPutDTO(da, out eventPutDTO);

                            if(i == 6 || i == 7 || i == 8)
                            {
                                var filter = Repository.Pagination.Filter.AllRecords();
                                var clients = unitOfWork.Clients.GetAll(ref filter,
                                    x => x.BranchId == branch.BranchId).ToList();
                                if(clients is not null)
                                {
                                    if (clients.Count > j)
                                        eventPutDTO.ClientOrdinalNumber = clients[j]?.ClientOrdinalNumber;
                                    else continue;
                                }       
                                else { continue; }
                                ++j;
                            }
                            else if (i == 9 || i == 10 || i == 11)
                            {
                                var filter = Repository.Pagination.Filter.AllRecords();
                                var clients = unitOfWork.Clients.GetAll(ref filter,
                                    x => x.BranchId == branch.BranchId).ToList();
                                if (clients is not null)
                                {
                                    if (clients.Count > j)
                                    {
                                        clients[j].ClientStatusId = 1;
                                        unitOfWork.Clients.Update(clients[j]);
                                        eventPutDTO.ClientOrdinalNumber = clients[j]?.ClientOrdinalNumber;
                                    }
                                    else continue;
                                }
                                else { continue; }
                                ++j;
                            }
                            else if (i == 13)
                            {
                                var filter = Repository.Pagination.Filter.AllRecords();
                                var printers = unitOfWork.Printers.GetAll(ref filter,
                                    x => x.BranchId == branch.BranchId).ToList();
                                if (printers is not null)
                                {
                                    if (printers.Count > j)
                                    {
                                        eventPutDTO.PrinterNumber = printers[j]?.Number;
                                    }
                                    else continue;
                                }
                                else { continue; }
                                ++j;
                            }
                            else if (i == 14 || i == 15)
                            {
                                var filter = Repository.Pagination.Filter.AllRecords();
                                var clients = unitOfWork.Clients.GetAll(ref filter,
                                    x => x.BranchId == branch.BranchId).ToList();
                                if (clients is not null)
                                {
                                    if (clients.Count > j)
                                    {
                                        clients[j].ClientStatusId = 1;
                                        unitOfWork.Clients.Update(clients[j]);
                                        eventPutDTO.ClientOrdinalNumber = clients[j]?.ClientOrdinalNumber;
                                    }
                                    else continue;
                                }
                                else { continue; }
                                ++j;
                            }

                            eventPutDTO.BranchId = branch.BranchId;
                            eventPutDTO.FullyProcessed = true;
                            bool res = eventPostProcessing.Run(da, eventPutDTO);
                            if (res is false)
                            {
                                logger.LogError("Failed post processing data. EventAsByte: " + da.code);
                                throw new Exception("Failed post processing data. EventAsByte: " + da);
                            }
                            else
                            {
                                logger.LogInformation("Number: " + i);
                            }
                            if (res is false)
                            {
                                logger.LogError("Failed post processing data. EventAsByte: " + da.code);
                                throw new Exception("Failed post processing data. EventAsByte: " + da);
                            }
                            else
                            {
                                logger.LogInformation("Number: " + i);
                            }
                            dataDTO.Add(eventPutDTO);
                        }
                        catch (FormatException ex)
                        {
                            logger.LogError(exceptionWrogFormat + ex.Message);
                        }
                        catch (OverflowException ex)
                        {
                            logger.LogError(exceptionOverFlow + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            logger.LogError(ex.Message);
                        }
                    }

                    events = mapper.Map<Event[]>(dataDTO).ToList();
                    bool result = true;
                    try
                    {
                        bool res = await unitOfWork.Events.AddRangeAsync(events);
                        if (res is false)
                        {
                            throw new Exception("Failed post data to the database. BranchId: " + branch.BranchId);
                        }
                    }
                    catch (Exception ex)
                    {
                        string msg = ex.Message;
                        msg += ex.InnerException?.Message;
                        logger.LogError(msg);
                        result = false;
                    }

                    if (result is false)
                        throw new ControllerExceptionExceptionAdded(nameOfObjects);
                    else
                        throw new ControllerExceptionSuccessAdded(nameOfObjects);
                }
                catch (ControllerExceptionExceptionAdded ex)
                {
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                catch (ControllerExceptionEventAsBytes ex)
                {
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
                catch (ControllerExceptionSuccessAdded ex)
                {
                    logger.LogInformation(ex.Message);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
            return NoContent();
        }

        [HttpPut("{ID}",Name = "UpdateEvent")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status400BadRequest)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public ActionResult UpdateEvent(EventPutDTO dataDTO, int ID)
        {
            string exception = nameOfObject + ": Function UpdateEvent(EventPutDTO dataDTO, int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                if (dataDTO is null) return BadRequest();
                var d = unitOfWork.Events.GetFirstOrDefault(x => x.EventId == ID);
                if (d is null) return NotFound();

                Event @event = new Event();
                CopyClasses.Copy(dataDTO, ref @event);
                @event.EventId = ID;
                bool result = unitOfWork.Events.Update(@event);
                if (result is false)
                    throw new ControllerExceptionExceptionUpdatedByID(nameOfObject, ID);
                else
                    throw new ControllerExceptionSuccessUpdatedByID(nameOfObject, ID);
            }
            catch (ControllerExceptionExceptionUpdatedByID ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            catch (ControllerExceptionSuccessUpdatedByID ex)
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

        [HttpDelete("{ID}",Name = "DeleteEventByID")]
        [ProducesResponseType(statusCode: StatusCodes.Status204NoContent)]
        [ProducesResponseType(statusCode: StatusCodes.Status404NotFound)]
        [ProducesResponseType(statusCode: StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult> DeleteEventByID(int ID)
        {
            string exception = nameOfObject + ": Function DeleteEventByID(int ID): ";
            try
            {
                if (testConnectionDatabase.IsServerConnected() == false)
                {
                    throw new Exception(testConnectionDatabase.Error);
                }

                var entity = unitOfWork.Events.GetFirstOrDefault(x => x.EventId == ID);
                if (entity is null) throw new ControllerExceptionDeleteByID(nameOfObject, ID);

                bool result = await unitOfWork.Events.RemoveAsync(entity);
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

        private void FillActivity(IEnumerable<Wrappers.ConfigData.ActivityConfig>? Acts, int BranchID)
        {
            if (Acts != null)
            {
                if (Acts.Count() > 0)
                {
                    // Cinnosti zacinaji od nuly
                    // Cinnost 254 volani klienta
                    // Cinnost 255 prelozeni z prepazky
                    int i = 0;
                    foreach(var act in Acts)
                    {
                        var name = act.Name.Replace("\\n"," ");
                        Domain.Models.Activity? activity = unitOfWork.Activities.GetFirstOrDefault(x => x.Number == i && x.BranchId == BranchID);
                        if(activity != null)
                        {
                            if (activity.ActivityName.Equals(name) == false)
                            {
                                activity.ActivityName = name;
                                var res = unitOfWork.Activities.Update(activity);
                                if (res is false)
                                {
                                    Logger.Logger.NewOperationLog($"Cannot update activity from database {activity.Number}: {activity.ActivityName}",Level.Warning);
                                }
                            }
                        }
                        else
                        {
                            AddNewActivity(name, i, BranchID);
                        }
                        ++i;
                    }
                }
            }
        }

        private void FillClerks(IEnumerable<Wrappers.ConfigData.ClerkConfig>? Clerks, int BranchID)
        {
            if (Clerks != null)
            {
                if (Clerks.Count() > 0)
                {
                    // Obsluhujici zacinaji od nuly
                    int i = 0;
                    foreach (var clrk in Clerks)
                    {
                        var name = clrk.Name.Replace("\\\n", " ");
                        Domain.Models.Clerk? clerk = unitOfWork.Clerks.FindClerkByBranchID(i,BranchID);
                        if (clerk != null)
                        {
                            if (clerk.ClerkName.Equals(name) == false)
                            {
                                clerk.ClerkName = name;
                                var res = unitOfWork.Clerks.Update(clerk);
                                if (res is false)
                                {
                                    Logger.Logger.NewOperationLog($"Cannot update clerk from database {clerk.Number}: {clerk.ClerkName}", Level.Warning);
                                }
                            }
                        }
                        ++i;
                    }
                }
            }
        }

        private void FillCounter(IEnumerable<Wrappers.ConfigData.CounterConfig>? Counters, int BranchID)
        {
            if (Counters != null)
            {
                if (Counters.Count() > 0)
                {
                    // Přepážky zacinaji od jednicky
                    int i = 1;
                    foreach (var counterName in Counters)
                    {
                        var name = counterName.Name.Replace("\\\\n", " ");
                        Domain.Models.Counter? counter = unitOfWork.Counters.GetFirstOrDefault(x => x.Number == i && x.BranchId == BranchID);
                        if (counter != null)
                        {
                            if (counter.CounterName.Equals(name) == false)
                            {
                                counter.CounterName = name;
                                var res = unitOfWork.Counters.Update(counter);
                                if (res is false)
                                {
                                    Logger.Logger.NewOperationLog($"Cannot update counter from database {counter.Number}: {counter.CounterName}", Level.Warning);
                                }
                            }
                        }
                        else
                        {
                            AddNewCounter(name,i,BranchID);
                        }
                        ++i;
                    }
                }
            }
        }

        private void FillPrinters(IEnumerable<Wrappers.ConfigData.PrinterConfig>? Printers, int BranchID)
        {
            if (Printers != null)
            {
                if (Printers.Count() > 0)
                {
                    int i = 0;
                    foreach (var printerName in Printers)
                    {
                        var name = printerName.Name.Replace("\\\n", " ");
                        Domain.Models.Printer? printer = unitOfWork.Printers.GetFirstOrDefault(x => x.Number == i && x.BranchId == BranchID);
                        if (printer != null)
                        {
                            if (printer.PrinterName.Equals(name) == false)
                            {
                                printer.PrinterName = name;
                                var res = unitOfWork.Printers.Update(printer);
                                if (res is false)
                                {
                                    Logger.Logger.NewOperationLog($"Cannot update pritner from database {printer.Number}: {printer.PrinterName}", Level.Warning);
                                }
                            }
                        }
                        else
                        {
                            AddNewPrinter(name, i, BranchID);
                        }
                        ++i;
                    }
                }
            }
        }

        private void AddNewActivity(string cntName, int cntNumber, int branchId)
        {
            ActivityPutDTO activityPutDTO = new ActivityPutDTO();
            activityPutDTO.ActivityName = cntName;
            activityPutDTO.Number = cntNumber;
            activityPutDTO.BranchId = branchId;
            var activity = mapper.Map<Domain.Models.Activity>(activityPutDTO);
            var res = unitOfWork.Activities.Add(activity);
            if (res is false)
            {
                Logger.Logger.NewOperationLog($"Cannot add new activity to database {activityPutDTO.Number}: {activityPutDTO.ActivityName}", Level.Warning);
            }
        }

        private void AddNewCounter(string cntName, int cntNumber, int branchId)
        {
            CounterPutDTO counterPutDTO = new CounterPutDTO();
            counterPutDTO.CounterName = cntName;
            counterPutDTO.Number = cntNumber;
            counterPutDTO.BranchId = branchId;
            counterPutDTO.CounterStatusId = 4; 
            var counter = mapper.Map<Domain.Models.Counter>(counterPutDTO);
            var res = unitOfWork.Counters.Add(counter);
            if (res is false)
            {
                Logger.Logger.NewOperationLog($"Cannot add new counter to database {counterPutDTO.Number}: {counterPutDTO.CounterName}", Level.Warning);
            }
        }

        private void AddNewPrinter(string printerName, int printerNumber, int branchId)
        {
            PrinterPutDTO printerPutDTO = new PrinterPutDTO();
            printerPutDTO.PrinterName = printerName;
            printerPutDTO.Number = printerNumber;
            printerPutDTO.BranchId = branchId;
            printerPutDTO.PrinterCurrentStateId = 2;
            printerPutDTO.PrinterPreviousStateId = 2;
            var printer = mapper.Map<Domain.Models.Printer>(printerPutDTO);
            var res = unitOfWork.Printers.Add(printer);
            if (res is false)
            {
                Logger.Logger.NewOperationLog($"Cannot add new printer to database {printerPutDTO.Number}: {printerPutDTO.PrinterName}", Level.Warning);
            }
        }
    }
}
