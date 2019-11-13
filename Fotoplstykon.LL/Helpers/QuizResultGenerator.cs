using AutoMapper;
using Fotoplastykon.BLL.DTOs.Quizzes;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.BLL.Helpers
{
    public class QuizResultGenerator
    {
        private IMapper Mapper { get; }
        private Dictionary<long, List<long>> CorrectAnswersDictionary { get; set; }
        private Dictionary<long, List<long>> UserAnswers { get; set; }
        private QuizModel UserQuiz { get; set; }
        private Quiz Quiz { get; set; }

        public QuizResultGenerator(IMapper mapper)
        {
            Mapper = mapper;
        }

        public QuizResultDTO GetResult(Quiz quiz, QuizModel userAnswers)
        {
            Init(quiz, userAnswers);

            return new QuizResultDTO
            {
                Points = CountPoints(),
                Name = Quiz.Name,
                Questions = CheckAnswers()
            };
        }

        private void Init(Quiz quiz, QuizModel userQuiz)
        {
            CorrectAnswersDictionary = quiz.Questions.ToDictionary(q => q.Id, q => q.Answers.Where(x => x.IsCorrect).Select(a => a.Id).ToList());
            UserAnswers = userQuiz.Questions.ToDictionary(q => q.Id, q => q.Answers.Where(x => x.IsSelected).Select(a => a.Id).ToList());
            UserQuiz = userQuiz;
            Quiz = quiz;
        }

        private int CountPoints()
        {
            var points = 0;

            foreach (var question in UserAnswers)
            {
                var correct = CorrectAnswersDictionary[question.Key];
                if (question.Value.Count == correct.Count && question.Value.All(correct.Contains)) points++;
            }

            return points;
        }

        private List<QuestionResultDTO> CheckAnswers()
        {
            var questions = Mapper.Map<List<QuestionResultDTO>>(UserQuiz.Questions);

            foreach(var question in questions)
            {
                question.Answers
                    .Where(a => CorrectAnswersDictionary[question.Id].Contains(a.Id))
                    .ToList()
                    .ForEach(a => a.IsCorrect = true);
            }

            return questions;
        }
    }
}
