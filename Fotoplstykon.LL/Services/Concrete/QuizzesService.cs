using AutoMapper;
using Fotoplastykon.BLL.Helpers;
using Fotoplastykon.BLL.DTOs.Quizzes;
using Fotoplastykon.BLL.Services.Abstract;
using Fotoplastykon.DAL.Entities.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Fotoplastykon.Tools.Pager;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Fotoplastykon.DAL.Enums;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class QuizzesService : Service, IQuizzesService
    {
        private QuizResultGenerator QuizResultGenerator { get; }
        public QuizzesService(IUnitOfWork unit, IMapper mapper, QuizResultGenerator quizResultGenerator)
            : base(unit, mapper)
        {
            QuizResultGenerator = quizResultGenerator;
        }

        public async Task<bool> CheckIfQuizExists(long id)
        {
            return await Unit.Quizes.Get(id) != null;
        }

        public async Task<Quiz> GetFull(long id)
        {
            return await Unit.Quizes.GetFullQuiz(id);
        }

        public async Task<IPaginationResult<ListItemModel>> GetPaginatedList(IPager pager)
        {
            var data = await Unit.Quizes.GetPaginatedList(pager, q => q.Name, OrderDirection.ASC);
            return new PaginationResult<ListItemModel>
            {
                Items = Mapper.Map<List<ListItemModel>>(data.Items),
                Pager = data.Pager
            };
        }

        public async Task<QuizResultDTO> SubmitQuiz(long quizId, long userId, QuizModel quizModel)
        {
            var quiz = await Unit.Quizes.GetFullQuiz(quizId);
            var result = QuizResultGenerator.GetResult(quiz, quizModel);
            var score = Unit.QuizScores.Get(userId, quizId);

            if (score == null)
            {
                await Unit.QuizScores.Add(new QuizScore
                {
                    UserId = userId,
                    QuizId = quizId,
                    Score = result.Points
                });
                await Unit.Complete();
            }

            return result;
        }
    }
}
