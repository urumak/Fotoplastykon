using AutoMapper;
using Fotoplastykon.BLL.DTOs.Chat;
using Fotoplastykon.BLL.DTOs.Messages;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class ChatService : Service, IChatService
    {
        private IConfiguration Configuration { get; }
        public ChatService(IUnitOfWork unit, IMapper mapper, IConfiguration configuration)
            : base(unit, mapper)
        {
            Configuration = configuration;
        }

        public async Task<IInfiniteScrollResult<MessageDTO>> GetMessages(IInfiniteScroll scroll, long userId, long friendId)
        {
            var data = await Unit.Messages.GetListForInfiniteScroll(
                scroll,
                m => (m.SenderId == userId && m.ReceiverId == friendId) || (m.ReceiverId == userId && m.SenderId == friendId),
                s => s.DateCreated, OrderDirection.DESC);

            return new InfiniteScrollResult<MessageDTO>
            {
                Items = Mapper.Map<List<MessageDTO>>(data.Items),
                Scroll = data.Scroll
            };
        }

        public async Task<MessageDTO> WriteMessage(long userId, Message message)
        {
            message.SenderId = userId;
            message = await Unit.Messages.Add(message);

            var conversation = await Unit.Conversations.Get(userId, message.ReceiverId);
            if (conversation == null)
            {
                await Unit.Conversations.Add(new Conversation
                {
                    FirstUserId = userId,
                    SecondUserId = message.ReceiverId,
                    LastMessageId = message.Id
                });
            }
            else
            {
                conversation.LastMessageId = message.Id;
            }

            await Unit.Complete();

            return Mapper.Map<Message, MessageDTO>(message, a => a.AfterMap((s, d) => d.IsSender = true));
        }

        public async Task<IInfiniteScrollResult<FriendListItemDTO>> GetFriends(IInfiniteScroll scroll, long userId)
        {
            var data = await Unit.Friendships.GetListForInfiniteScroll(scroll, userId);
            return new InfiniteScrollResult<FriendListItemDTO>
            {
                Items = Mapper.Map<List<FriendListItemDTO>>(data.Items),
                Scroll = data.Scroll
            };
        }

        public async Task<List<FriendListItemDTO>> SearchFriends(string searchInput, long userId, int limit = 20)
        {
            return Mapper.Map<List<FriendListItemDTO>>(await Unit.Friendships.SearchForFriends(searchInput, userId, limit));
        }

        public async Task<List<ChatWindowModel>> GetForChatWindows(List<long> friendsIds, long principalId)
        {
            var friends = Mapper.Map<List<ChatWindowModel>>(await Unit.Users.Find(x => friendsIds.Contains(x.Id)));
            
            foreach (var friend in friends ?? new List<ChatWindowModel>())
            {
                var data = await Unit.Messages.GetListForInfiniteScroll(new InfiniteScroll { UnitSize = 20 }, principalId, friend.Id);
                friend.Messages = new InfiniteScrollResult<MessageDTO>
                {
                    Items = new List<MessageDTO>(),
                    Scroll = data.Scroll
                };

                data.Items.ForEach(m => friend.Messages.Items
                         .Add(Mapper.Map<Message, MessageDTO>(m, a => a.AfterMap((s, d) => d.IsSender = s.SenderId == principalId))));
            }

            return friends;
        }

        public async Task<List<long>> GetUnreadMessagesUsersIds(long receiverId)
        {
            var lastUnreadMesssageFromEachFriend = await Unit.Messages.GetLastUnreadMessagesFromEachFriend(receiverId);
            var readings = await Unit.MessagesReadings.GetByReceiverId(receiverId);

            var messagesWithNoHistory = lastUnreadMesssageFromEachFriend
                .Where(m => !readings.Select(r => r.SenderId).Contains(m.SenderId));

            await Unit.MessagesReadings.AddRange(Mapper.Map<List<MessagesReading>>(messagesWithNoHistory));
            await Unit.Complete();

            return lastUnreadMesssageFromEachFriend.Select(x => x.SenderId).ToList();
        }

        public async Task<IInfiniteScrollResult<LastMessage>> GetLastMessages(IInfiniteScroll scroll, long userId)
        {
            var data = await Unit.Conversations.GetLastMessagesForEachFriend(scroll, userId);
            var readings = await Unit.MessagesReadings.GetByReceiverId(userId);

            var items = MapLastMessages(data.Items, readings);

            return new InfiniteScrollResult<LastMessage>
            {
                Items = items,
                Scroll = data.Scroll
            };
        }

        public async Task UpdateLastReadingDate(long senderId, long receiverId)
        {
            var reading = await Unit.MessagesReadings.Get(x => x.SenderId == senderId && x.ReceiverId == receiverId);

            if (reading != null)
            {
                reading.LastReadingDate = DateTime.Now;
            }
            else
            {
                await Unit.MessagesReadings.Add(new MessagesReading
                {
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    LastReadingDate = DateTime.Now
                });
            }

            await Unit.Complete();
        }

        private List<LastMessage> MapLastMessages(List<Conversation> data, List<MessagesReading> readings)
        {
            var items = new List<LastMessage>();

            foreach (var item in data)
            {
                var reading = readings.FirstOrDefault(r => r.SenderId == item.LastMessage.SenderId);
                if(reading != null)
                {
                    items.Add(new LastMessage
                    {
                        FriendId = item.LastMessage.SenderId,
                        SenderId = item.LastMessage.SenderId,
                        DateCreated = item.LastMessage.DateCreated,
                        Id = item.LastMessage.Id,
                        MessageText = item.LastMessage.MessageText,
                        NameAndSurname = item.LastMessage.Sender.FirstName + " " + item.LastMessage.Sender.Surname,
                        PhotoUrl = item.LastMessage.Sender.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + item.LastMessage.Sender.PhotoId : string.Empty,
                        Unread = reading.LastReadingDate < item.LastMessage.DateCreated
                    });
                }
                else
                {
                    items.Add(new LastMessage
                    {
                        FriendId = item.LastMessage.ReceiverId,
                        SenderId = item.LastMessage.SenderId,
                        DateCreated = item.LastMessage.DateCreated,
                        Id = item.LastMessage.Id,
                        MessageText = item.LastMessage.MessageText,
                        NameAndSurname = item.LastMessage.Receiver.FirstName + " " + item.LastMessage.Receiver.Surname,
                        PhotoUrl = item.LastMessage.Receiver.PhotoId.HasValue ? Configuration["Files:PublicEndpoint"] + item.LastMessage.Receiver.PhotoId : string.Empty,
                        Unread = false
                    });
                }
            }

            return items;
        }
    }
}
