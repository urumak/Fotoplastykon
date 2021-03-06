﻿using System.Collections.Generic;
using AutoMapper;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.BLL.Services.Abstract;
using Microsoft.AspNetCore.Identity;
using Fotoplastykon.BLL.DTOs.Users;
using System.Linq;
using System.Threading.Tasks;
using Fotoplastykon.BLL.Helpers;
using Fotoplastykon.DAL.Storage;
using System;
using Fotoplastykon.Tools.InfiniteScroll;
using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.Tools.Pager;
using Fotoplastykon.DAL.Enums;
using System.Linq.Expressions;
using LinqKit;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class UsersService : Service, IUsersService
    {
        private IPasswordHasher<User> Hasher { get; }
        private Anonymiser<User> Anonymiser { get; }
        private IFilesService Files { get; }

        public UsersService(IUnitOfWork unit, IMapper mapper, IPasswordHasher<User> hasher, Anonymiser<User> anonymiser, IFilesService files)
            : base(unit, mapper)
        {
            Hasher = hasher;
            Anonymiser = anonymiser;
            Files = files;
        }

        public async Task<User> Get(long id)
        {
            return await Unit.Users.Get(id);
        }

        public async Task<IPaginationResult<UserListItem>> GetList(IPager pager)
        {
            var predicate = PredicateBuilder.New<User>(i => i.AnonimisationDate == null);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(i => i.FirstName.Contains(pager.Search) || i.Surname.Contains(pager.Search));

            var data = await Unit.Users.GetPaginatedList(
                pager,
                predicate,
                i => i.FirstName,
                OrderDirection.ASC);

            return new PaginationResult<UserListItem>
            {
                Items = Mapper.Map<List<UserListItem>>(data.Items),
                Pager = data.Pager
            };
        }

        public async Task<long> Add(AddUserDTO user, bool isAdmin = false)
        {
            var entity = Mapper.Map<User>(user);
            entity.PublicId = Guid.NewGuid().ToString();
            entity.IsAdmin = isAdmin;

            await Unit.Users.Add(entity);
            await Unit.Complete();

            await SetPassword(entity.Id, user.Password);
            await Unit.Complete();

            return entity.Id;
        }

        public async Task<UserLoginDTO> GetForLoginByUserName(string userName)
        {
            var user = await Unit.Users.GetByUserName(userName);
            return Mapper.Map<UserLoginDTO>(user);
        }

        public async Task<List<User>> Search(string searchString)
        {
            var users = await Unit.Users
                .Find(u => u.UserName.Contains(searchString)
                    || u.FirstName.Contains(searchString)
                    || u.Surname.Contains(searchString));

            return users.ToList();
        }

        public async Task<bool> CheckIfExists(long id)
        {
            return await Unit.Users.Get(id) != null;
        }

        public async Task Anonymise(long id)
        {
            var user = await Unit.Users.Get(id);

            if(user.PhotoId.HasValue) await Files.Remove(user.PhotoId.Value);

            user = Anonymiser.Anonymise(user);
            await Unit.Complete();
        }

        public async Task<UserPageDTO> GetForPage(long id, long principalId)
        {
            var user = Mapper.Map<UserPageDTO>(await Unit.Users.GetForPage(id));
            var invitation = await Unit.Invitations.Get(user.Id, principalId);

            user.IsFriend = await Unit.Friendships.Get(user.Id, principalId) != null;
            user.InvitationSent = invitation != null;
            if(user.InvitationSent) user.IsInvitationSender = invitation.InvitingId == principalId;

            return user;
        }

        public async Task Update(long id, AddUserDTO user, string newPassword)
        {
            var entity = await Unit.Users.Get(id);
            Mapper.Map(user, entity);
            if(!string.IsNullOrEmpty(newPassword)) entity.PasswordHash = Hasher.HashPassword(entity, newPassword);

            await Unit.Complete();
        }

        private async Task<bool> SetPassword(long id, string password)
        {
            var user = await Unit.Users.Get(id);

            if (user == null) return false;

            user.PasswordHash = Hasher.HashPassword(user, password);

            return true;
        }
    }
}
