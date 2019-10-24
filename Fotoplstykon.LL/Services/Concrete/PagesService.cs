using AutoMapper;
using Fotoplastykon.BLL.DTOs.Pages;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class PagesService : Service, IPagesService
    {
        public PagesService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public async Task<FilmPageDTO> GetFilmPage(string publicId)
        {
            var film = Mapper.Map<FilmPageDTO>(Unit.Films.GetForPage(publicId));
            //pobrać z bazy
            //dokleić plik
            return film;
        }

        public async Task<FilmPersonPageDTO> GetFilmPersonPage(string publicId)
        {
            var person = Mapper.Map<FilmPersonPageDTO>(Unit.FilmPeople.GetForPage(publicId));
            //pobrać z bazy
            //dokleić plik
            return person;
        }

        public async Task<UserPageDTO> GetUserPage(string publicId)
        {
            var user = Mapper.Map<UserPageDTO>(Unit.Users.GetForPage(publicId));
            //pobrać z bazy
            //dokleić plik
            return user;
        }
    }
}
