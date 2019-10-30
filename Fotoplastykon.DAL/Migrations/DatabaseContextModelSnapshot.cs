﻿// <auto-generated />
using System;
using Fotoplastykon.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fotoplastykon.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.6-servicing-10079")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Film", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PagePublicId")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<long?>("PhotoId");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("YearOfProduction");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.ToTable("films");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("PagePublicId");

                    b.Property<long?>("PhotoId");

                    b.Property<int>("Profession");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.ToTable("film_people");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmWatching", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FilmId");

                    b.Property<int?>("Mark");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("film_watchings");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.ForumThread", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<long>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<long?>("FilmId");

                    b.Property<long?>("PersonId");

                    b.Property<string>("Subject");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("FilmId");

                    b.HasIndex("PersonId");

                    b.ToTable("forum_threads");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.ForumThreadComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<long>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateDeleted");

                    b.Property<long?>("ParentId");

                    b.Property<string>("Subject");

                    b.Property<long>("ThreadId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ParentId");

                    b.HasIndex("ThreadId");

                    b.ToTable("forum_thread_comments");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Friendship", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("InvitedId");

                    b.Property<long>("InvitingId");

                    b.HasKey("Id");

                    b.HasIndex("InvitedId");

                    b.HasIndex("InvitingId");

                    b.ToTable("friendships");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Information", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<long>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("Introduction");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.ToTable("informations");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.InformationComment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<long>("CreatedById");

                    b.Property<DateTime>("DateCreated");

                    b.Property<DateTime?>("DateDeleted");

                    b.Property<long>("InformationId");

                    b.Property<long?>("ParentId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedById");

                    b.HasIndex("ParentId");

                    b.ToTable("information_comments");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Invitation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("InvitedId");

                    b.Property<long>("InvitingId");

                    b.HasKey("Id");

                    b.HasIndex("InvitedId");

                    b.HasIndex("InvitingId");

                    b.ToTable("invitations");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Message", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateCreated");

                    b.Property<string>("MessageText");

                    b.Property<long>("ReceiverId");

                    b.Property<long>("SenderId");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("messages");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PersonInRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CharacterName");

                    b.Property<long>("FilmId");

                    b.Property<long>("PersonId");

                    b.Property<int>("Role");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("PersonId");

                    b.ToTable("people_in_roles");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PersonMark", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Mark");

                    b.Property<long>("PersonId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("UserId");

                    b.ToTable("person_marks");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Quiz", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("quizes");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.QuizAnswer", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AnswerText");

                    b.Property<bool>("IsCorrect");

                    b.Property<long>("QuestionId");

                    b.HasKey("Id");

                    b.HasIndex("QuestionId");

                    b.ToTable("quiz_answers");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.QuizQuestion", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsMultichoice");

                    b.Property<string>("QuestionText");

                    b.Property<long>("QuizId");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.ToTable("quiz_questions");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.QuizScore", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("QuizId");

                    b.Property<int>("Score");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("QuizId");

                    b.HasIndex("UserId");

                    b.ToTable("quiz_scores");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.StoredFileInfo", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AbsolutePath")
                        .IsRequired()
                        .HasMaxLength(1000);

                    b.Property<string>("DisplayName")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("MimeType")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .HasMaxLength(80);

                    b.Property<string>("RelativePath")
                        .HasMaxLength(1000);

                    b.Property<long>("Size");

                    b.Property<string>("UniqueName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("files");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime?>("AnonimisationDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(300);

                    b.Property<long?>("PhotoId");

                    b.Property<string>("PublicId")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(100)
                        .HasDefaultValue("00000000-0000-0000-0000-000000000000");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.HasIndex("PhotoId");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Film", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.StoredFileInfo", "Photo")
                        .WithMany("FilmPhotos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.StoredFileInfo", "Photo")
                        .WithMany("FilmPersonPhotos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmWatching", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.Film", "Film")
                        .WithMany("Watchings")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "User")
                        .WithMany("FilmsWatched")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.ForumThread", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "CreatedBy")
                        .WithMany("ForumThreads")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.Film", "Film")
                        .WithMany("ForumThreads")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", "Person")
                        .WithMany("ForumThreads")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.ForumThreadComment", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "CreatedBy")
                        .WithMany("ForumThreadComments")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.ForumThreadComment", "Parent")
                        .WithMany("Replies")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.ForumThread", "Thread")
                        .WithMany("Comments")
                        .HasForeignKey("ThreadId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Friendship", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "Invited")
                        .WithMany("AcceptedFriends")
                        .HasForeignKey("InvitedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "Inviting")
                        .WithMany("InvitedFriends")
                        .HasForeignKey("InvitingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Information", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "CreatedBy")
                        .WithMany("Informations")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.InformationComment", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "CreatedBy")
                        .WithMany("InformationComments")
                        .HasForeignKey("CreatedById")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.InformationComment", "Parent")
                        .WithMany("Replies")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Invitation", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "Invited")
                        .WithMany("InvitationsReceived")
                        .HasForeignKey("InvitedId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "Inviting")
                        .WithMany("InvitationsSent")
                        .HasForeignKey("InvitingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.Message", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "Receiver")
                        .WithMany("MessagesReceived")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "Sender")
                        .WithMany("MessagesSent")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PersonInRole", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.Film", "Film")
                        .WithMany("PeopleInRoles")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", "Person")
                        .WithMany("Roles")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PersonMark", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", "Person")
                        .WithMany("Marks")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "User")
                        .WithMany("RatedPeople")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.QuizAnswer", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.QuizQuestion", "Question")
                        .WithMany("Answers")
                        .HasForeignKey("QuestionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.QuizQuestion", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.Quiz", "Quiz")
                        .WithMany("Questions")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.QuizScore", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.Quiz", "Quiz")
                        .WithMany("Scores")
                        .HasForeignKey("QuizId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "User")
                        .WithMany("Scores")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.User", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.StoredFileInfo", "Photo")
                        .WithMany("UserPhotos")
                        .HasForeignKey("PhotoId")
                        .OnDelete(DeleteBehavior.SetNull);
                });
#pragma warning restore 612, 618
        }
    }
}
