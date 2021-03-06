﻿using Fotoplastykon.DAL.Repositories.Abstract;
using System;
using System.Threading.Tasks;

namespace Fotoplastykon.DAL.UnitsOfWork.Abstract
{
    public interface IUnitOfWork
    {
        IUsersRepository Users { get; }
        IInvitationsRepository Invitations { get; }
        IFriendshipsRepository Friendships { get; }
        IInformationsRepository Informations { get; }
        IQuizzesRepository Quizzes { get; }
        IQuizQuestionsRepository QuizQuestions { get; }
        IQuizAnswersRepository QuizAnswers { get; }
        IQuizScoresRepository QuizScores { get; }
        IFilesRepository Files { get; }
        IFilmWatchingsRepository FilmWatchings { get;  }
        IFilmsRepository Films { get; }
        IFilmPeopleRepository FilmPeople { get; }
        IPersonMarksRepository PersonMarks { get; }
        IInformationCommentsRepository InformationComments { get; }
        IForumThreadsRepository ForumThreads { get; }
        IForumThreadCommentsRepository ForumThreadComments { get; }
        IMessagesRepository Messages { get; }
        ISignalRConnectionsRepository SignalRConnections { get; }
        IMessagesReadingsRepository MessagesReadings { get; }
        IConversationsRepository Conversations { get; }
        IInvitationNotificationsRepository InvitationNotifications { get; }
        INotificationsReadingsRepository NotificationsReadings { get; }
        IPeopleInRolesRepository PeopleInRoles { get; }
        Task<int> Complete();
    }
}
