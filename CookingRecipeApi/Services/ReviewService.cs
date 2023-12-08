using AutoMapper;
using BanVeXemPhimApi.Common;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Dto;
using CookingRecipeApi.Models;
using CookingRecipeApi.Repositories;
using CookingRecipeApi.Request;
using System.Threading.Tasks;

namespace CookingRecipeApi.Services
{
    public class ReviewService
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly UserRepository _userRepository;
        private readonly FoodRepository _foodRepository;
        private readonly ApiOption _apiOption;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment _webHost;

        public ReviewService(ApiOption apiOption, DatabaseContext databaseContext, IMapper mapper, IWebHostEnvironment webHost)
        {
            _reviewRepository = new ReviewRepository(apiOption, databaseContext, mapper);
            _userRepository = new UserRepository(apiOption, databaseContext, mapper);
            _foodRepository = new FoodRepository(apiOption, databaseContext, mapper);
            _apiOption = apiOption;
            _mapper = mapper;
            _webHost = webHost;
        }

        public object Comment( int userId, CreateCommentRequest request)
        {
            try
            {
                var newComment = _mapper.Map<Review>(request);

                newComment.UserId = userId;
                _reviewRepository.Create(newComment);
                _reviewRepository.SaveChange();
                return newComment;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetCommentList(int foodId)
        {
            try 
            {
                var comment = _reviewRepository.FindByCondition(row => foodId == row.FoodId).ToList();
                List<ReviewDto> commentList = new List<ReviewDto>();
                for (int i = 0; i < comment.Count; i++)
                {
                    var CommentInf = new ReviewDto
                    {
                        Id = comment[i].Id,
                        UserId = comment[i].UserId,
                        FoodId = comment[i].FoodId,
                        Content = comment[i].Content,
                        FirstName = _userRepository.FindByCondition(row => comment[i].UserId == row.Id).FirstOrDefault().FirstName,
                        LastName = _userRepository.FindByCondition(row => comment[i].UserId == row.Id).FirstOrDefault().LastName,
                        Avatar = _userRepository.FindByCondition(row => comment[i].UserId == row.Id).FirstOrDefault().Avatar,
                        CreatedDate = comment[i].CreatedDate,
                    };
                    commentList.Add(CommentInf);
                }
                return commentList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public object DeleteComment(int userId, int CommentId)
        {
            var comment = _reviewRepository.FindByCondition(row => CommentId == row.Id).FirstOrDefault();
            var food = _foodRepository.FindByCondition(row => comment.FoodId == row.Id).FirstOrDefault();
            if (comment == null)
            {
                throw new ValidateError(1001, "Comment dont exist!");
            }
            if (comment.UserId == userId || food.UserId == userId)
            {
                _reviewRepository.DeleteByEntity(comment);
                _reviewRepository.SaveChange();
            }
            else
            {
                throw new Exception("You do not have the right to delete!");
            }

            return comment;
        }
    }
    
}
