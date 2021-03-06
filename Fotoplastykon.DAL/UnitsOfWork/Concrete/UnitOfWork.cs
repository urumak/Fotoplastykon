﻿using Fotoplastykon.DAL.Repositories.Abstract;
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
            IInvitationsRepository invitations, 
            IFriendshipsRepository friendships,
            IInformationsRepository informations,
            IQuizzesRepository quizes,
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
            IForumThreadCommentsRepository forumThreadComments,
            IMessagesRepository messages,
            ISignalRConnectionsRepository signalRConnections,
            IMessagesReadingsRepository messagesReadings,
            IConversationsRepository conversations,
            IInvitationNotificationsRepository invitationNotifications,
            INotificationsReadingsRepository notificationsReadings,
            IPeopleInRolesRepository peopleInRoles)
        {
            Context = context;
            Users = users;
            Friendships = friendships;
            Invitations = invitations;
            Informations = informations;
            Quizzes = quizes;
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
            Messages = messages;
            SignalRConnections = signalRConnections;
            MessagesReadings = messagesReadings;
            Conversations = conversations;
            InvitationNotifications = invitationNotifications;
            NotificationsReadings = notificationsReadings;
            PeopleInRoles = peopleInRoles;
        }

        public IUsersRepository Users { get; }
        public IInvitationsRepository Invitations { get; }
        public IFriendshipsRepository Friendships { get; }
        public IQuizzesRepository Quizzes { get; }
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
        public IMessagesRepository Messages { get; }
        public ISignalRConnectionsRepository SignalRConnections { get; }
        public IMessagesReadingsRepository MessagesReadings { get; }
        public IConversationsRepository Conversations { get; }
        public IInvitationNotificationsRepository InvitationNotifications { get; }
        public INotificationsReadingsRepository NotificationsReadings { get; }

        public IPeopleInRolesRepository PeopleInRoles { get; }

        private DbContext Context { get; }

        public async Task<int> Complete()
        {
            return await Context.SaveChangesAsync();
        }
    }
}
