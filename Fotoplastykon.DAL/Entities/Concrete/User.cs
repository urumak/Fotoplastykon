using System;
using System.Collections.Generic;
using Fotoplastykon.DAL.Attributes;
using Fotoplastykon.DAL.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Fotoplastykon.DAL.Entities.Concrete
{
    public class User : IEntity, IAnonimisationable
    {
        public User()
        {
            InvitedFriends = new HashSet<Friendship>();
            AcceptedFriends = new HashSet<Friendship>();
            InvitationsSent = new HashSet<Invitation>();
            InvitationsReceived = new HashSet<Invitation>();
            ForumThreads = new HashSet<ForumThread>();
            ForumThreadComments = new HashSet<ForumThreadComment>();
            FilmsWatched = new HashSet<FilmWatching>();
            RatedPeople = new HashSet<PersonMark>();
            Scores = new HashSet<QuizScore>();
            Informations = new HashSet<Information>();
            InformationComments = new HashSet<InformationComment>();

        }

        public long Id { get; set; }
        public string PublicId { get; set; }

        [Anonymise]
        public string UserName { get; set; }

        [Anonymise]
        public string FirstName { get; set; }

        [Anonymise]
        public string Surname { get; set; }

        [Anonymise]
        public string PasswordHash { get; set; }

        [Anonymise]
        public string Email { get; set; }

        public bool IsAdmin { get; set; }
        public long? PhotoId { get; set; }
        public DateTime? AnonimisationDate { get; set; }

        public StoredFileInfo Photo { get; set; }
        public ICollection<Friendship> InvitedFriends { get; set; }
        public ICollection<Friendship> AcceptedFriends { get; set; }
        public ICollection<Invitation> InvitationsSent { get; set; }
        public ICollection<Invitation> InvitationsReceived { get; set; }
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
            builder.Property(p => p.PublicId).IsRequired().HasDefaultValue("00000000-0000-0000-0000-000000000000").HasMaxLength(100);
            builder.Property(p => p.UserName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.FirstName).IsRequired().HasMaxLength(100);
            builder.Property(p => p.Surname).IsRequired().HasMaxLength(250);
            builder.Property(p => p.Email).IsRequired().HasMaxLength(250);
            builder.Property(p => p.PasswordHash).HasMaxLength(300);
            builder.Property(p => p.IsAdmin).IsRequired().HasDefaultValue(false);
            builder.HasOne(p => p.Photo).WithMany(p => p.UserPhotos).HasForeignKey(p => p.PhotoId).OnDelete(DeleteBehavior.SetNull);
            builder.ToTable("users");
        }
    }
}
