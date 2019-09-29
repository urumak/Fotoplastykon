using System.Collections.Generic;
using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class User : IEntity
    {
        public User()
        {
            FilmPageCreations = new HashSet<FilmPageCreation>();
            InvitedFriends = new HashSet<Friendship>();
            AcceptedFriends = new HashSet<Friendship>();
            InvitationsSent = new HashSet<Invitation>();
            InvitationsReceived = new HashSet<Invitation>();
            PersonPageCreations = new HashSet<PersonPageCreation>();
            CreationRequests = new HashSet<PageCreationRequest>();
            ForumThreads = new HashSet<ForumThread>();
            ForumThreadComments = new HashSet<ForumThreadComment>();
            FilmsWatched = new HashSet<FilmWatching>();
            RatedPeople = new HashSet<PersonMark>();
            Scores = new HashSet<QuizScore>();
            Informations = new HashSet<Information>();
            InformationComments = new HashSet<InformationComment>();
        }

        public long Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public bool IsAdmin { get; set; }

        public ICollection<FilmPageCreation> FilmPageCreations { get; set; }
        public ICollection<PersonPageCreation> PersonPageCreations { get; set; }
        public ICollection<Friendship> InvitedFriends { get; set; }
        public ICollection<Friendship> AcceptedFriends { get; set; }
        public ICollection<Invitation> InvitationsSent { get; set; }
        public ICollection<Invitation> InvitationsReceived { get; set; }
        public ICollection<PageCreationRequest> CreationRequests { get; set; }
        public ICollection<ForumThread> ForumThreads { get; set; }
        public ICollection<ForumThreadComment> ForumThreadComments { get; set; }
        public ICollection<FilmWatching> FilmsWatched { get; set; }
        public ICollection<PersonMark> RatedPeople { get; set; }
        public ICollection<QuizScore> Scores { get; set; }
        public ICollection<Message> MessagesSent { get; set; }
        public ICollection<Message> MessagesReceived { get; set; }
        public ICollection<Information> Informations { get; set; }
        public ICollection<InformationComment> InformationComments { get; set; }
    }

    internal class UserMappings : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FirstName).HasMaxLength(100);
            builder.Property(p => p.Surname).HasMaxLength(250);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(250);
            builder.Property(p => p.PasswordHash).HasMaxLength(300);
            builder.Property(p => p.IsAdmin).IsRequired().HasDefaultValue(false);
            builder.ToTable("users");
        }
    }
}
