using Fotoplastykon.DAL.Repositories.Abstract;
using System;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        IFilmPageCreationsRepository FilmPagesCreations { get; }
        IInvitationsRepository Invitations { get; }
        IFriendshipsRepository Friendships { get; }
        IPersonPageCreationsRepository PersonPagesCreations { get; }
        IInformationsRepository Informations { get; }
        IQuizesRepository Quizes { get; }
        IQuizQuestionsRepository QuizQuestions { get; }
        IQuizAnswersRepository QuizAnswers { get; }
        IQuizScoresRepository QuizScores { get; }
        IFilesRepository Files { get; }
        int Complete();
    }
}
