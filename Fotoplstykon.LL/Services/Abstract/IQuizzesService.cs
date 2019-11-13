using Fotoplastykon.BLL.DTOs.Quizzes;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
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
    }
}
