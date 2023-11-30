using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
namespace CookingRecipeApi.Services
{
    public class AccountService
    {
        private readonly UserRepository _userRepository;
        private readonly TaskItemRepository _taskItemRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public AccountService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _taskItemRepository = new TaskItemRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public object GetProfile(int userId)
        {
            try
            {
                return _userRepository.FindOrFail(userId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Get profile
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        
        //public object GetAccount(int userId)
        //{
        //    try
        //    {
        //        var user = _userRepository.GetEntity(userId);
        //        if(user == null)
        //        {
        //            throw new Exception("User doesn't exist!");
        //        }
        //        return user;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        public object UpdateProfile(int userId, UpdateProfileRequest request)
        {
            try
            {
                var user = _userRepository.FindOrFail(userId);
                if(user == null)
                {
                    throw new Exception("User doesn't exist!");
                }
                if (request.Avatar != null && request.Avatar.FileName != user.Avatar)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\users\\avatars\\" + date + request.Avatar.FileName))
                    {
                        request.Avatar.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    user.Avatar = "users/avatars/" + date + request.Avatar.FileName;
                }
                if (request.Cover != null && request.Cover.FileName != user.Cover)
                {
                    var date = DateTime.UtcNow.ToString("yyyy_MM_dd_HH_mm");
                    using (FileStream fileStream = File.Create(_webHost.WebRootPath + "\\users\\covers\\" + date + request.Cover.FileName))
                    {
                        request.Cover.CopyTo(fileStream);
                        fileStream.Flush();
                    }
                    user.Cover = "users/covers/" + date + request.Cover.FileName;
                }
                user.FirstName = request.FirstName;
                user.LastName = request.LastName;
                user.Title = request.Title;
                user.PhoneNumber = request.PhoneNumber;
                user.Birthday = request.Birthday;
                user.Address = request.Address;
                user.UpdatedDate = DateTime.Now;
                _userRepository.UpdateByEntity(user);
                _userRepository.SaveChange();

                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetUserInf(int userId)
        {
            try
            {
                var user = _userRepository.FindByCondition(row => userId == row.Id).FirstOrDefault();
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
