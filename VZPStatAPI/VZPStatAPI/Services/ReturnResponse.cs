using AutoMapper;
using Domain.DataDTO;
using Repository.Interfaces;
using System.IO;
using VZPStatAPI.Controllers;
using VZPStatAPI.Wrappers;

namespace VZPStatAPI.Services
{
    public class ReturnResponse<T> where T : class
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly ILogger<T> logger;
        private readonly IMapper mapper;

        public ReturnResponse(
            IUnitOfWork unitOfWork,
            ILogger<T> logger,
            IMapper mapper
            )
        {
            this.unitOfWork= unitOfWork;
            this.logger= logger;
            this.mapper= mapper;
        }

        public APIResponse Response(bool success, APIResponse aPIResponse,string info, Logger.Logger.Level level, Exception? exception = null)
        {
            if(success)
            {
                aPIResponse.IsSuccess = true;
#if DEBUG
                logger.LogInformation(info + exception?.Message);
#endif
            }
            else
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage= exception?.Message ?? string.Empty;
                aPIResponse.InnerErrorMessage= exception?.InnerException?.Message ?? string.Empty;
                if (level != Logger.Logger.Level.Info)
                {
                    Logger.Logger.NewOperationLog(info + exception?.Message + exception?.InnerException?.Message, level);
                }
#if DEBUG
                if (level == Logger.Logger.Level.Warning)
                {
                    logger.LogWarning(info + exception?.Message);
                }
                else if(level == Logger.Logger.Level.Error)
                {
                    logger.LogError(info + exception?.Message);
                }
                else if (level == Logger.Logger.Level.Info)
                {
                    logger.LogInformation(info + exception?.Message);
                }
#endif
            }

            return aPIResponse;
        }

        public PagedAPIResponse Response(bool success, PagedAPIResponse aPIResponse, string info, Logger.Logger.Level level, Exception? exception = null)
        {
            if (success)
            {
                aPIResponse.IsSuccess = true;
#if DEBUG
                logger.LogInformation(info + exception?.Message);
#endif
            }
            else
            {
                aPIResponse.IsSuccess = false;
                aPIResponse.ErrorMessage = exception?.Message ?? string.Empty;
                aPIResponse.InnerErrorMessage = exception?.InnerException?.Message ?? string.Empty;
                if (level != Logger.Logger.Level.Info)
                {
                    Logger.Logger.NewOperationLog(info + exception?.Message + exception?.InnerException?.Message, level);
                }
#if DEBUG
                if (level == Logger.Logger.Level.Warning)
                {
                    logger.LogWarning(info + exception?.Message);
                }
                else if (level == Logger.Logger.Level.Error)
                {
                    logger.LogError(info + exception?.Message);
                }
                else if (level == Logger.Logger.Level.Info)
                {
                    logger.LogInformation(info + exception?.Message);
                }
#endif
            }

            return aPIResponse;
        }
    }
}
