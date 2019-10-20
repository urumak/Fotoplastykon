﻿using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Threading.Tasks;

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
        IFilmWatchingsRepository FilmWatchings { get;  }
        IFilmsRepository Films { get; }
        IFilmPeopleRepository FilmPeople { get; }
        IPersonMarksRepository PersonMarks { get; }
        IInformationCommentsRepository InformationComments { get; }
        Task<int> Complete();
    }
}
