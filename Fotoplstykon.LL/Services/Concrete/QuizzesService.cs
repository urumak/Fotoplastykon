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
using LinqKit;
using Microsoft.AspNetCore.Http;
using System.Linq;

namespace Fotoplastykon.BLL.Services.Concrete
{
    public class QuizzesService : Service, IQuizzesService
    {
        private QuizResultGenerator QuizResultGenerator { get; }
        private IFilesService Files { get; }

        public QuizzesService(IUnitOfWork unit, IMapper mapper, QuizResultGenerator quizResultGenerator, IFilesService files)
            : base(unit, mapper)
        {
            QuizResultGenerator = quizResultGenerator;
            Files = files;
        }

        public async Task<bool> CheckIfQuizExists(long id)
        {
            return await Unit.Quizzes.Get(id) != null;
        }

        public async Task<Quiz> GetFull(long id)
        {
            return await Unit.Quizzes.GetFullQuiz(id);
        }

        public async Task<QuizFormModel> Fetch(long id)
        {
            return Mapper.Map<QuizFormModel>(await Unit.Quizzes.GetFullQuiz(id));
        }

        public async Task<IPaginationResult<ListItemModel>> GetPaginatedList(IPager pager)
        {
            var predicate = PredicateBuilder.New<Quiz>(true);

            if (!string.IsNullOrEmpty(pager.Search)) predicate.And(q => q.Name.Contains(pager.Search));

            var data = await Unit.Quizzes.GetPaginatedList(pager, predicate, q => q.Name, OrderDirection.ASC);
            return new PaginationResult<ListItemModel>
            {
                Items = Mapper.Map<List<ListItemModel>>(data.Items),
                Pager = data.Pager
            };
        }

        public async Task Remove(long id)
        {
            await Unit.Quizzes.Remove(id);
            await Unit.Complete();
        }

        public async Task<QuizResultDTO> SubmitQuiz(long quizId, long userId, QuizModel quizModel)
        {
            var quiz = await Unit.Quizzes.GetFullQuiz(quizId);
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

        public async Task ChangePhoto(long quizid, IFormFile file)
        {
            var quiz = await Unit.Quizzes.Get(quizid);

            var photoId = await Files.AddAndReturnId(file, "quizzes\\");
            var oldPhotoId = quiz.PhotoId;

            quiz.PhotoId = photoId;
            await Unit.Complete();

            if (oldPhotoId.HasValue) await Files.Remove(oldPhotoId.Value);
        }


        #region Add()
        public async Task<long> Add(QuizFormModel model)
        {
            var entity = Mapper.Map<Quiz>(model);
            await Unit.Quizzes.Add(entity);
            await Unit.Complete();

            await AddQuestions(entity.Id, model.Questions);

            await Unit.Complete();

            return entity.Id;
        }
        #endregion

        #region Update()
        public async Task Update(long id, QuizFormModel model)
        {
            var entity = await Unit.Quizzes.Get(id);
            Mapper.Map(model, entity);

            var (questionsToAdd, questionsToUpdate, questionsToDelete) = await SortQuestionsToUpdate(id, model.Questions);
            await AddQuestions(id, questionsToAdd);
            await UpdateQuestions(id, questionsToUpdate);
            DeleteQuestions(questionsToDelete);

            await Unit.Complete();
        }

        private async Task<(List<QuestionFormModel>, List<QuestionFormModel>, List<QuizQuestion>)>
            SortQuestionsToUpdate(long quizId, List<QuestionFormModel> questionsModel)
        {
            var questions = await Unit.QuizQuestions.Find(r => r.QuizId == quizId);

            return (questionsModel.Where(x => !questions.Select(r => r.Id).Contains(x.Id)).ToList(),
                questionsModel.Where(x => questions.Select(r => r.Id).Contains(x.Id)).ToList(),
                questions.Where(x => !questionsModel.Select(r => r.Id).Contains(x.Id)).ToList());
        }

        private async Task AddQuestions(long quizId, List<QuestionFormModel> questionsModel)
        {
            foreach (var item in questionsModel)
            {
                var question = Mapper.Map<QuizQuestion>(item);
                question.QuizId = quizId;

                await Unit.QuizQuestions.Add(question);
                await Unit.Complete();

                var answers = Mapper.Map<List<QuizAnswer>>(item.Answers);
                answers.ForEach(i => i.QuestionId = question.Id);
                await Unit.QuizAnswers.AddRange(answers);
                await Unit.Complete();
            }
        }

        private async Task UpdateQuestions(long quizId, List<QuestionFormModel> questionsModel)
        {
            foreach (var item in questionsModel)
            {
                await UpdateQuestion(quizId, item);
            }
        }

        private void DeleteQuestions(List<QuizQuestion> questionsToDelete)
        {
            Unit.QuizQuestions.RemoveRange(questionsToDelete);
        }

        private async Task UpdateQuestion(long quizId, QuestionFormModel model)
        {
            var entity = await Unit.QuizQuestions.Get(model.Id);
            Mapper.Map(model, entity);
            entity.QuizId = quizId;

            var (answersToAdd, answersToUpdate, answersToDelete) = await SortAnswersToUpdate(model.Id, model.Answers);
            await AddAnswers(model.Id, answersToAdd);
            await UpdateAnswers(model.Id, answersToUpdate);
            DeleteAnswers(answersToDelete);
        }

        private async Task<(List<AnswerFormModel>, List<AnswerFormModel>, List<QuizAnswer>)>
            SortAnswersToUpdate(long questionid, List<AnswerFormModel> answersModel)
        {
            var answers = await Unit.QuizAnswers.Find(r => r.QuestionId == questionid);

            return (answersModel.Where(x => !answers.Select(r => r.Id).Contains(x.Id)).ToList(),
                answersModel.Where(x => answers.Select(r => r.Id).Contains(x.Id)).ToList(),
                answers.Where(x => !answersModel.Select(r => r.Id).Contains(x.Id)).ToList());
        }

        private async Task AddAnswers(long questionId, List<AnswerFormModel> answersModel)
        {
            var items = Mapper.Map<List<QuizAnswer>>(answersModel);

            items.ForEach(i => i.QuestionId = questionId);

            await Unit.QuizAnswers.AddRange(items);
            await Unit.Complete();
        }

        private async Task UpdateAnswers(long questionId, List<AnswerFormModel> answersModel)
        {
            var entities = await Unit.QuizAnswers.Find(x => answersModel.Select(m => m.Id).Contains(x.Id));
            var peopleDictionary = answersModel.ToDictionary(x => x.Id, x => x);

            foreach (var entity in entities)
            {
                Mapper.Map(peopleDictionary[entity.Id], entity);
                entity.QuestionId = questionId;
            }
        }

        private void DeleteAnswers(List<QuizAnswer> answesToDelete)
        {
            Unit.QuizAnswers.RemoveRange(answesToDelete);
        }
        #endregion
    }
}
