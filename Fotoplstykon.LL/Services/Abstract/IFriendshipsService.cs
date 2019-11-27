﻿using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.Tools.InfiniteScroll;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IFriendshipsService
    {
        Task InviteFriend(long userId, long friendId);
        Task AcceptInvitation(long userId, long invitingId);
        Task RemoveFriend(long userId, long friendId);
        Task RemoveInvitation(long userId, long friendId);
        Task<bool> CheckIfFriendshipExist(long firstId, long secondId);
        Task<bool> CheckIfInvitationExist(long firstId, long secondId);
        Task<bool> CheckIfInvitationExistByInvitationRoles(long invitedId, long invitingId);
        Task<IInfiniteScrollResult<LinkedItemDTO>> GetFriends(IInfiniteScroll scroll, long userId);
    }
}
