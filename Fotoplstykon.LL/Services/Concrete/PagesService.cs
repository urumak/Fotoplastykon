using AutoMapper;
using Fotoplastykon.BLL.DTOs.Pages;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.Enums;
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
            return Mapper.Map<FilmPageDTO>(await Unit.Films.GetForPage(publicId));
        }

        public async Task<FilmPersonPageDTO> GetFilmPersonPage(string publicId)
        {
            return  Mapper.Map<FilmPersonPageDTO>(await Unit.FilmPeople.GetForPage(publicId));
        }

        public async Task<UserPageDTO> GetUserPage(string publicId)
        {
            return Mapper.Map<UserPageDTO>(await Unit.Users.GetForPage(publicId));
        }
    }
}
