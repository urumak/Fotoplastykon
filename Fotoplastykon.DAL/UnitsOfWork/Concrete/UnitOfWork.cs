using Fotoplastykon.DAL.Repositories.Abstract;
using Fotoplastykon.DAL.Repositories.Concrete;
using Fotoplastykon.DAL.UnitsOfWork.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

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
            IQuizScoresRepository quizScores,
            IFilesRepository files,
            IFilmWatchingsRepository filmWatchings,
            IFilmsRepository films,
            IFilmPeopleRepository filmPeople,
            IPersonMarksRepository personMarks,
            IInformationCommentsRepository informationComments,
            IForumThreadsRepository forumThreads,
            IForumThreadCommentsRepository forumThreadComments)
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
            Files = files;
            FilmWatchings = filmWatchings;
            Films = films;
            FilmPeople = filmPeople;
            PersonMarks = personMarks;
            InformationComments = informationComments;
            ForumThreads = forumThreads;
            ForumThreadComments = forumThreadComments;
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
        public IFilesRepository Files { get; }
        public IFilmWatchingsRepository FilmWatchings { get; }
        public IFilmsRepository Films { get; set; }
        public IFilmPeopleRepository FilmPeople { get; }
        public IPersonMarksRepository PersonMarks { get; }
        public IInformationCommentsRepository InformationComments { get; }
        public IForumThreadsRepository ForumThreads { get; }
        public IForumThreadCommentsRepository ForumThreadComments { get; }

        private DbContext Context { get; }

        public async Task<int> Complete()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
