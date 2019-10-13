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

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class QuizesService : Service, IQuizesService
    {
        public QuizesService(IUnitOfWork unit, IMapper mapper)
            : base(unit, mapper)
        {
        }

        public bool CheckIfQuizExists(long id)
        {
            return Unit.Quizes.Get(id) != null;
        }

        public Quiz GetFull(long id)
        {
            return Unit.Quizes.GetFullQuiz(id);
        }

        public IPaginationResult<Quiz> GetPaginatedList(IPager pager)
        {
            return Unit.Quizes.GetPaginatedList(pager);
        }

        public ResultModel SubmitQuiz(long quizId, long userId, IEnumerable<UserAnswerModel> answers)
        {
            var quiz = Unit.Quizes.GetFullQuiz(quizId);
            var counter = new QuizPointsCounter(quiz, answers);
            var points = counter.CountPoints();

            var score = new QuizScore
            {
                UserId = userId,
                QuizId = quizId,
                Score = points
            };

            Unit.QuizScores.Add(score);
            Unit.Complete();

            return new ResultModel { Points = points, Quiz = Mapper.Map<QuizResultModel>(quiz) };
        }
    }
}
