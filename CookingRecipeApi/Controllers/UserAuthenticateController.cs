using AutoMapper;
using AutoMapper.Configuration;
using CookingRecipeApi.Common;
using CookingRecipeApi.Controllers;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Request;
using CookingRecipeApi.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace CookingRecipeApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAuthenticateController : BaseApiController<UserAuthenticateController>
    {
        private readonly UserAuthenticateService _userAuthenticateService;
        public UserAuthenticateController(DatabaseContext databaseContext, IMapper mapper, ApiOption apiConfig)
        {
            _userAuthenticateService = new UserAuthenticateService(apiConfig, databaseContext, mapper);
        }

        /// <summary>
        /// User login
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserLogin")]
        public MessageData UserLogin(LoginRequest request)
        {
            try
            {
                var res = _userAuthenticateService.UserLogin(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// User register account
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("UserRegister")]
        public MessageData UserRegister(UserRegisterRequest request)
        {
            try
            {
                var res = _userAuthenticateService.UserRegister(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// change password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("ChangePassword")]
        public MessageData ChangePassword(ChangePasswordRequest request)
        {
            try
            {
                var res = _userAuthenticateService.ChangePassword(UserId, request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// forgot password
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("ForgotPassword")]
        public MessageData ForgotPassword(string username)
        {
            try
            {
                var res = _userAuthenticateService.ForgotPassword(username);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// Verify OTP
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("VerifyOTP")]
        public MessageData VerifyOTP(VerifyOTPRequest request)
        {
            try
            {
                var res = _userAuthenticateService.VerifyOTP(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public MessageData ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var res = _userAuthenticateService.ResetPassword(request);
                return new MessageData { Data = res, Status = 1 };
            }
            catch (Exception ex)
            {
                return NG(ex);
            }
        }
    }
}
