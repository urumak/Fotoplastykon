using Fotoplastykon.BLL.DTOs.Quizzes;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IQuizzesService
    {
        Task<Quiz> GetFull(long id);
        Task<IPaginationResult<ListItemModel>> GetPaginatedList(IPager pager);
        Task<bool> CheckIfQuizExists(long id);
        Task<QuizResultDTO> SubmitQuiz(long quizId, long userId, QuizModel quizModel);
        Task<QuizFormModel> Fetch(long id);
        Task ChangePhoto(long quizid, IFormFile file);
        Task Remove(long id);
        Task Update(long id, QuizFormModel model);
    }
}
