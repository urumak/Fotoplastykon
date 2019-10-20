using AutoMapper;
using Fotoplastykon.BLL.Helpers;
using Fotoplastykon.BLL.Models.Quizes;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class QuizesService : Service, IQuizesService
    {
        public QuizesService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public async Task<bool> CheckIfQuizExists(long id)
        {
            return await Unit.Quizes.Get(id) != null;
        }

        public async Task<Quiz> GetFull(long id)
        {
            return await Unit.Quizes.GetFullQuiz(id);
        }

        public async Task<IPaginationResult<Quiz>> GetPaginatedList(IPager pager)
        {
            return await Unit.Quizes.GetPaginatedList(pager);
        }

        public async Task<ResultModel> SubmitQuiz(long quizId, long userId, IEnumerable<UserAnswerModel> answers)
        {
            var quiz = await Unit.Quizes.GetFullQuiz(quizId);
            var counter = new QuizPointsCounter(quiz, answers);
            var points = counter.CountPoints();

            var score = new QuizScore
            {
                UserId = userId,
                QuizId = quizId,
                Score = points
            };

            await Unit.QuizScores.Add(score);
            await Unit.Complete();

            return new ResultModel { Points = points, Quiz = Mapper.Map<QuizResultModel>(quiz) };
        }
    }
}
