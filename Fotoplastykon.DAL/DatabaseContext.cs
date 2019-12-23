using Fotoplastykon.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Fotoplastykon.DAL.Entities.Concrete;

namespace Fotoplastykon.DAL
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<User> Users { get; protected set; }
        public virtual DbSet<Film> Films { get; protected set; }
        public virtual DbSet<FilmPerson> FilmPeople { get; protected set; }
        public virtual DbSet<Friendship> Friendships { get; protected set; }
        public virtual DbSet<Invitation> Invitations { get; protected set; }
        public virtual DbSet<PersonInRole> PeopleInRoles { get; protected set; }
        public virtual DbSet<Information> Informations { get; protected set; }
        public virtual DbSet<InformationComment> InformationComments { get; protected set; }
        public virtual DbSet<FilmWatching> FilmWatchings { get; protected set; }
        public virtual DbSet<ForumThread> ForumThreads { get; protected set; }
        public virtual DbSet<ForumThreadComment> ForumThreadComments { get; protected set; }
        public virtual DbSet<Message> Messages { get; protected set; }
        public virtual DbSet<PersonMark> PeopleMarks { get; protected set; }
        public virtual DbSet<Quiz> Quizes { get; protected set; }
        public virtual DbSet<QuizQuestion> QuizQuestions { get; protected set; }
        public virtual DbSet<QuizAnswer> QuizAnswers { get; protected set; }
        public virtual DbSet<QuizScore> QuizScores { get; protected set; }
        public virtual DbSet<StoredFileInfo> Files { get; protected set; }
        public virtual DbSet<SignalRConnection> SignalRConnections { get; protected set; }
        public virtual DbSet<MessagesReading> MessagesReadings { get; protected set; }
        public virtual DbSet<Conversation> Conversations { get; protected set; }
        public virtual DbSet<InvitationNotification> InvitationNotifications { get; protected set; }
        public virtual DbSet<NotificationsReading> NotificationsReadings { get; protected set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Port=3306;Database=fotoplastykon;User=root;Password=root;Charset=utf8mb4;SslMode=None;Connect Timeout=5;AllowPublicKeyRetrieval=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(DatabaseContext).Assembly);
        }
    }
}
