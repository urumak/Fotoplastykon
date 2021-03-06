﻿using Fotoplastykon.BLL.DTOs.Chat;
using Fotoplastykon.BLL.DTOs.Messages;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IChatService
    {
        Task<IInfiniteScrollResult<MessageDTO>> GetMessages(IInfiniteScroll scroll, long userId, long friendId);
        Task<MessageDTO> WriteMessage(long userId, Message message);
        Task<IInfiniteScrollResult<FriendListItemDTO>> GetFriends(IInfiniteScroll scroll, long userId);
        Task<List<ChatWindowModel>> GetForChatWindows(List<long> friendsIds, long principalId);
        Task<List<FriendListItemDTO>> SearchFriends(string searchInput, long userId, int limit = 20);
        Task<List<long>> GetUnreadMessagesUsersIds(long receiverId);
        Task<IInfiniteScrollResult<LastMessage>> GetLastMessages(IInfiniteScroll scroll, long receiverId);
        Task UpdateLastReadingDate(long senderId, long receiverId);
    }
}
