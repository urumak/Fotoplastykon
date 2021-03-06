﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.BLL.DTOs.Users;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.Tools.Pager;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IUsersService
    {
        Task<User> Get(long id);
        Task<long> Add(AddUserDTO user, bool isAdmin = false);
        Task<UserLoginDTO> GetForLoginByUserName(string userName);
        Task<List<User>> Search(string searchString);
        Task<bool> CheckIfExists(long id);
        Task Anonymise(long id);
        Task<UserPageDTO> GetForPage(long id, long principalId);
        Task<IPaginationResult<UserListItem>> GetList(IPager pager);
        Task Update(long id, AddUserDTO user, string newPassword);
    }
}
