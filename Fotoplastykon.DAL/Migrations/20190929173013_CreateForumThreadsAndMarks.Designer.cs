﻿// <auto-generated />
using System;
using Fotoplastykon.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Fotoplastykon.DAL.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20190929173013_CreateForumThreadsAndMarks")]
    partial class CreateForumThreadsAndMarks
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("YearOfProduction");

                    b.HasKey("Id");

                    b.ToTable("films");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmPageCreation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("FilmId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("FilmId");

                    b.HasIndex("UserId");

                    b.ToTable("film_page_creations");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FirstName");

                    b.Property<string>("PagePublicId");

                    b.Property<int>("Profession");

                    b.Property<string>("Surname");

                    b.HasKey("Id");

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

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PageCreationRequest", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PagePublicId");

                    b.Property<int>("PageType");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("page_creation_requests");
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

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PersonPageCreation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("PersonId");

                    b.Property<long>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PersonId");

                    b.HasIndex("UserId");

                    b.ToTable("person_page_creations");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250);

                    b.Property<string>("FirstName")
                        .HasMaxLength(100);

                    b.Property<bool>("IsAdmin")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValue(false);

                    b.Property<string>("PasswordHash")
                        .HasMaxLength(300);

                    b.Property<string>("Surname")
                        .HasMaxLength(250);

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(100);

                    b.HasKey("Id");

                    b.ToTable("users");
                });

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.FilmPageCreation", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.Film", "Film")
                        .WithMany("PageCreations")
                        .HasForeignKey("FilmId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "User")
                        .WithMany("FilmPageCreations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
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
                        .WithMany("Children")
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

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PageCreationRequest", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "User")
                        .WithMany("CreationRequests")
                        .HasForeignKey("UserId")
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

            modelBuilder.Entity("Fotoplastykon.DAL.Entities.Concrete.PersonPageCreation", b =>
                {
                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.FilmPerson", "Person")
                        .WithMany("PageCreations")
                        .HasForeignKey("PersonId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Fotoplastykon.DAL.Entities.Concrete.User", "User")
                        .WithMany("PersonPageCreations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
