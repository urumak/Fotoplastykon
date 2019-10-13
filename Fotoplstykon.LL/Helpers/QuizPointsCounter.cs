using Fotoplastykon.BLL.Models.Quizes;
using Fotoplastykon.DAL.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fotoplastykon.BLL.Helpers
{
    public class QuizPointsCounter
    {
        private Dictionary<long, List<long>> UserAnswersDictionary { get; }
        private Dictionary<long, List<long>> CorrectAnswersDictionary { get; }

        public QuizPointsCounter(Quiz quiz, IEnumerable<UserAnswerModel> answers)
        {
            UserAnswersDictionary = answers.GroupBy(a => a.QuestionId)
                .Select(g => new
                {
                    g.Key,
                    Value = g.Select(c => c.AnswerId)
                })
                .ToDictionary(x => x.Key, x => x.Value.ToList());

            CorrectAnswersDictionary = quiz.Questions.ToDictionary(q => q.Id, q => q.Answers.Select(a => a.Id).ToList());
        }

        public int CountPoints()
        {
            var points = 0;

            foreach (var question in UserAnswersDictionary)
            {
                var correct = CorrectAnswersDictionary[question.Key];
                if (question.Value.Count == correct.Count && question.Value.All(correct.Contains)) points++;
            }

            return points;
        }
    }
}
