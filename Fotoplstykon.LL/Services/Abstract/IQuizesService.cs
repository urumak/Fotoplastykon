using Fotoplastykon.BLL.DTOs.Quizes;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IQuizesService
    {
        Task<Quiz> GetFull(long id);
        Task<IPaginationResult<Quiz>> GetPaginatedList(IPager pager);
        Task<bool> CheckIfQuizExists(long id);
        Task<ResultDTO> SubmitQuiz(long quizId, long userId, IEnumerable<UserAnswerDTO> answers);
    }
}
