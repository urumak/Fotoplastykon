using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using System;

namespace Fotoplastykon.DAL.UnitsOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(DatabaseContext context, 
            IUsersRepository users, 
            IFilmPageCreationsRepository filmPagesCreations, 
            IInvitationsRepository invitations, 
            IFriendshipsRepository friendships,
            IPersonPageCreationsRepository personPagesCreations,
            IInformationsRepository informations,
            IQuizesRepository quizes,
            IQuizQuestionsRepository quizQuestions,
            IQuizAnswersRepository quizAnswers,
            IQuizScoresRepository quizScores)
        {
            Context = context;
            Users = users;
            FilmPagesCreations = filmPagesCreations;
            Friendships = friendships;
            Invitations = invitations;
            PersonPagesCreations = personPagesCreations;
            Informations = informations;
            Quizes = quizes;
            QuizQuestions = quizQuestions;
            QuizAnswers = quizAnswers;
            QuizScores = quizScores;
        }

        public IUsersRepository Users { get; }
        public IFilmPageCreationsRepository FilmPagesCreations { get; }
        public IInvitationsRepository Invitations { get; }
        public IFriendshipsRepository Friendships { get; }
        public IPersonPageCreationsRepository PersonPagesCreations { get; }
        public IQuizesRepository Quizes { get; }
        public IQuizQuestionsRepository QuizQuestions { get; }
        public IQuizAnswersRepository QuizAnswers { get; }
        public IQuizScoresRepository QuizScores { get; }
        public IInformationsRepository Informations { get; }
        private DbContext Context { get; }

        public int Complete()
        {
            return Context.SaveChanges();
        }
    }
}
