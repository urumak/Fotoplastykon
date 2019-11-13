namespace Fotoplastykon.BLL.DTOs.Quizzes
{
    public class AnswerResultDTO
    {
        public long Id { get; set; }
        public string AnswerText { get; set; }
        public bool IsCorrect { get; set; }
        public bool IsSelected { get; set; }
    }
}