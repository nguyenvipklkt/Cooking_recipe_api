using AutoMapper;
using CookingRecipeApi.Common;
using CookingRecipeApi.Database;
using CookingRecipeApi.Helpers.SocketHelper;
using CookingRecipeApi.Models;
using CookingRecipeApi.Request;
using CookingRecipeApi.Respositories;
using CookingRecipeApi.Utility;
using System.Net.WebSockets;
using System.Text;

namespace CookingRecipeApi.Repositories
{
    public class NotificationRepository : BaseRespository<Notification>
    {
        private IMapper _mapper;
        private ConnectionManager _connectionManager;
        private List<string> contentList = new List<string>()
        {
            "đã đánh giá bài viết của bạn",
            "đã bình luận bài viết của bạn",
            "đã theo dõi bạn",
            "có bài viết mới",
        };
        public NotificationRepository(ApiOption apiConfig, DatabaseContext databaseContext, IMapper mapper, ConnectionManager connectionManager) : base(apiConfig, databaseContext)
        {
            this._mapper = mapper;
            _connectionManager = connectionManager;
        }

        public async Task<object> CreateNotification(int userId, int reactingUserId, int foodId, int type)
        {
            var newNotification = new Notification();
            newNotification.UserId = userId;
            newNotification.Type = type;
            newNotification.ReactingUserId = reactingUserId;
            newNotification.FoodId = foodId;
            newNotification.Content = contentList[type];
            this.Create(newNotification);
            this.SaveChange();

            var connectionList = _connectionManager.GetAllSockets();
            var connection = default(KeyValuePair<string, WebSocket>);
            foreach (var item in connectionList)
            {
                var user = _connectionManager.GetUsernameBySocket(item.Value);
                if (user == userId.ToString())
                {
                    connection = item;
                }
            }
            if (!connection.Equals(default(KeyValuePair<string, WebSocket>)))
            {
                var socket = connection.Value;
                if (socket.State == WebSocketState.Open)
                {
                    var message = "{\"name\": \"" + newNotification.Content + "\", \"type\": \"Notify\"}";
                    await socket.SendAsync(buffer: new ArraySegment<byte>(array: Encoding.ASCII.GetBytes(message),
                                                                      offset: 0,
                                                                      count: message.Length),
                                       messageType: WebSocketMessageType.Text,
                                       endOfMessage: true,
                                       cancellationToken: CancellationToken.None);
                }
            }

            return newNotification;
        }

    }
}
