using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Threading.Tasks;

namespace CookingRecipeApi.Services
{
    public class FollowService
    {
        private readonly FollowRepository _followRepository;
        private readonly UserRepository _userRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public FollowService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _followRepository = new FollowRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object GetFollowUser(int userId)
        {
            try
            {
                var follow = _followRepository.FindByCondition(row => userId == row.UserId).ToList();
                if (follow == null)
                {
                    return "Nobody follow!";
                }
                return follow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetFollower(int userId)
        {
            try
            {
                var follower = _followRepository.FindByCondition(row => userId == row.FollowingUserId).ToList();
                
                return follower;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object Follow(int userId, FollowRequest request)
        {
            try
            {
                var checkFollow = _followRepository.FindByCondition(row => userId == row.UserId && request.FollowingUserId == row.FollowingUserId).FirstOrDefault();
                if (checkFollow != null)
                {
                    throw new ValidateError(1001, "You followed!");

                }
                var newFollow = _mapper.Map<Follow>(request);

                if (request.FollowingUserId == userId)
                {
                    throw new Exception("Follow invalid!");
                }
                newFollow.UserId = userId;
                _followRepository.Create(newFollow);
                _followRepository.SaveChange();
                return newFollow;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object UnFollow(int userId, int followingUserId)
        {
            var follow = _followRepository.FindByCondition(row => followingUserId == row.FollowingUserId && userId == row.UserId).FirstOrDefault();
            if (follow == null)
            {
                throw new ValidateError(1001, "Follow dont exist!");
            }
            _followRepository.DeleteByEntity(follow);
            _followRepository.SaveChange();
            return follow;
        }
    }
    
}
