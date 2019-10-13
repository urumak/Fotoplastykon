using Fotoplastykon.BLL.Models.Quizes;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;

namespace Fotoplastykon.BLL.Services.Abstract
{
    public interface IQuizesService
    {
        Quiz GetFull(long id);
        IPaginationResult<Quiz> GetPaginatedList(IPager pager);
        bool CheckIfQuizExists(long id);
        ResultModel SubmitQuiz(long quizId, long userId, IEnumerable<UserAnswerModel> answers);
    }
}
