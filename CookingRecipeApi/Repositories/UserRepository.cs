using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Respositories;
using CookingRecipeApi.Utility;

namespace CookingRecipeApi.Repositories
{
    public class UserRepository : BaseRespository<User>
    {
        private IMapper _mapper;
        public UserRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
        }

        /// <summary>
        /// UserLogin function. That return User by user login request
        /// </summary>
        /// <param name="userLoginRequest"></param>
        /// <returns></returns>
        public User UserLogin(LoginRequest userLoginRequest)
        {
            try
            {
                var passwordByMD5 = UtilityFunction.CreateMD5(userLoginRequest.Password);
                return Model.Where(row => row.Email == userLoginRequest.Email && row.Password == passwordByMD5).FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        ///// <summary>
        ///// Check user register
        ///// </summary>
        ///// <param name="user"></param>
        ///// <returns></returns>
        //public bool CheckUserRegister(User user)
        //{
        //    try
        //    {
        //        var userlist = Model.Where(row => row.Username == user.Username || row.NumberPhone == user.NumberPhone).ToList();
        //        if(userlist.Count > 0)
        //        {
        //            return false;
        //        }
        //        return true;
        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        
    }
}
