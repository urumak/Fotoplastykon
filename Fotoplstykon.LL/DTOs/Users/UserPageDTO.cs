using Fotoplastykon.BLL.DTOs.Shared;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Fotoplastykon.BLL.DTOs.Users
{
    public class UserPageDTO
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PhotoUrl { get; set; }
        public bool IsFriend { get; set; }
        public bool InvitationSent { get; set; }
        public List<RankModel> WatchedFilms { get; set; }
        public List<RankModel> RatedPeople { get; set; }
    }
}
