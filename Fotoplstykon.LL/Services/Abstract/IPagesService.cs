using Fotoplastykon.BLL.DTOs.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IPagesService
    {
        Task<FilmPageDTO> GetFilmPage(string publicId);
        Task<FilmPersonPageDTO> GetFilmPersonPage(string publicId);
        Task<UserPageDTO> GetUserPage(string publicId);
    }
}
