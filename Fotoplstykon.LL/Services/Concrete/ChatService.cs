using AutoMapper;
using Fotoplastykon.BLL.DTOs.Chat;
using Fotoplastykon.BLL.DTOs.Messages;
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

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class ChatService : Service, IChatService
    {
        public ChatService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {

        }

        public async Task<IInfiniteScrollResult<Message>> GetMessages(IInfiniteScroll scroll, long userId, long friendId)
        {
            return await Unit.Messages.GetListForInfiniteScroll(
                scroll,
                m => (m.SenderId == userId && m.ReceiverId == friendId) || (m.ReceiverId == userId && m.SenderId == friendId),
                s => s.DateCreated, OrderDirection.DESC);
        }

        public async Task<MessageDTO> WriteMessage(long userId, Message message)
        {
            message.SenderId = userId;
            message = await Unit.Messages.Add(message);
            await Unit.Complete();

            return Mapper.Map<Message, MessageDTO>(message, a => a.AfterMap((s, d) => d.IsSender = true));
        }

        public async Task<IInfiniteScrollResult<ChatListItemDTO>> GetFriends(IInfiniteScroll scroll, long userId)
        {
            var data = await Unit.Friendships.GetListForInfiniteScroll(scroll, userId);
            return new InfiniteScrollResult<ChatListItemDTO>
            {
                Items = Mapper.Map<List<ChatListItemDTO>>(data.Items),
                Scroll = data.Scroll
            };
        }

        public async Task<List<ChatListItemDTO>> SearchFriends(string searchInput, long userId, int limit = 20)
        {
            return Mapper.Map<List<ChatListItemDTO>>(await Unit.Friendships.SearchForFriends(searchInput, userId, limit));
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

        public async Task<IInfiniteScrollResult<MessageDTO>> GetLastMessages(IInfiniteScroll scroll, long receiverId)
        {
            var data = await Unit.Messages.GetLastMessagesFromEachFriend(scroll, receiverId);
            return new InfiniteScrollResult<MessageDTO>
            {
                Items = Mapper.Map<List<MessageDTO>>(data.Items),
                Scroll = data.Scroll
            };
        }

        public async Task UpdateLastReadingDate(long senderId, long receiverId)
        {
            var reading = await Unit.MessagesReadings.Get(x => x.SenderId == senderId && x.ReceiverId == receiverId);
            reading.LastReadingDate = DateTime.Now;
            await Unit.Complete();
        }
    }
}
