﻿// <auto-generated />
using System;
using Library;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Library.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20240109031503_readerChange")]
    partial class readerChange
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.14");

            modelBuilder.Entity("AuthorAndComposition", b =>
                {
                    b.Property<int>("AuthorId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompositionId")
                        .HasColumnType("INTEGER");

                    b.HasKey("AuthorId", "CompositionId");

                    b.HasIndex("CompositionId");

                    b.ToTable("AuthorAndComposition");
                });

            modelBuilder.Entity("CompositionsAndPublishers", b =>
                {
                    b.Property<int>("PlaceOfPublicationId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompostionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PublishingHouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("PlaceOfPublicationId", "CompostionId");

                    b.HasIndex("CompostionId");

                    b.HasIndex("PublishingHouseId");

                    b.ToTable("CompositionsAndPublishers");
                });

            modelBuilder.Entity("GivenExamplers", b =>
                {
                    b.Property<int>("ReaderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ExamplerId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DateOfIssue")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("DateOfReturn")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Returned")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReaderId", "ExamplerId");

                    b.HasIndex("ExamplerId");

                    b.ToTable("GivenExamplers");
                });

            modelBuilder.Entity("Library.Entities.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("MiddleName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Library.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Library.Entities.Composition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("AmountInLibrary")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("TypeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("TypeId");

                    b.ToTable("Compositions");
                });

            modelBuilder.Entity("Library.Entities.Exampler", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CompositionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfRack")
                        .HasColumnType("INTEGER");

                    b.Property<int>("NumberOfShelf")
                        .HasColumnType("INTEGER");

                    b.Property<int>("StorageId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CompositionId");

                    b.HasIndex("StorageId");

                    b.ToTable("Examplers");
                });

            modelBuilder.Entity("Library.Entities.InterlibrarySubscription", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("ArrivalDate")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfOrder")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("NameOfLibrary")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ReaderId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("ReaderId");

                    b.ToTable("InterlibrarySubscriptions");
                });

            modelBuilder.Entity("Library.Entities.MenuInfo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("NameOfFunc")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("ParentId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("MenuInfo");
                });

            modelBuilder.Entity("Library.Entities.Penalty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("NumOfSuspensionDays")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Sum")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Penalties");
                });

            modelBuilder.Entity("Library.Entities.PlaceOfPublication", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("PublishingHouseId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("PublishingHouseId");

                    b.ToTable("PlacesOfPublications");
                });

            modelBuilder.Entity("Library.Entities.PublishingHouse", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("DaysOfIssuance")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("PermissionOnIssuance")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("PublishingHouses");
                });

            modelBuilder.Entity("Library.Entities.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("PereregistrationDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ReaderTicket")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("RegistrationDate")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Readers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Reader");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Library.Entities.Storage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Storages");
                });

            modelBuilder.Entity("Library.Entities.TypeOfComposition", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("TypesOfComposition");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.PropertiesOfTypes.Degree", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.PropertiesOfTypes.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("FacultyId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.PropertiesOfTypes.Faculty", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Faculties");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.PropertiesOfTypes.Position", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.PropertiesOfTypes.Rank", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Ranks");
                });

            modelBuilder.Entity("Library.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ReadersAndPenaltys", b =>
                {
                    b.Property<int>("ReaderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PenaltyId")
                        .HasColumnType("INTEGER");

                    b.Property<DateOnly>("DateOfEnding")
                        .HasColumnType("TEXT");

                    b.Property<DateOnly>("DateOfGetting")
                        .HasColumnType("TEXT");

                    b.HasKey("ReaderId", "PenaltyId");

                    b.HasIndex("PenaltyId");

                    b.ToTable("ReadersAndPenaltys");
                });

            modelBuilder.Entity("UserInfo", b =>
                {
                    b.Property<int>("MenuInfoId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Delete")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Edit")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Read")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("Write")
                        .HasColumnType("INTEGER");

                    b.HasKey("MenuInfoId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserInfo");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.Employee", b =>
                {
                    b.HasBaseType("Library.Entities.Reader");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PositionId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.HasDiscriminator().HasValue("Employee");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.Professor", b =>
                {
                    b.HasBaseType("Library.Entities.Reader");

                    b.Property<int>("DegreeId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("DepartmentId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("PositionId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RankId")
                        .HasColumnType("INTEGER");

                    b.HasIndex("DegreeId");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("PositionId");

                    b.HasIndex("RankId");

                    b.ToTable("Readers", t =>
                        {
                            t.Property("DepartmentId")
                                .HasColumnName("Professor_DepartmentId");

                            t.Property("PositionId")
                                .HasColumnName("Professor_PositionId");
                        });

                    b.HasDiscriminator().HasValue("Professor");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.Student", b =>
                {
                    b.HasBaseType("Library.Entities.Reader");

                    b.Property<int>("FacultyId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasIndex("FacultyId");

                    b.HasDiscriminator().HasValue("Student");
                });

            modelBuilder.Entity("AuthorAndComposition", b =>
                {
                    b.HasOne("Library.Entities.Author", "Author")
                        .WithMany("AuthorsAndCompositions")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.Composition", "Composition")
                        .WithMany("AuthorsAndCompositions")
                        .HasForeignKey("CompositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Composition");
                });

            modelBuilder.Entity("CompositionsAndPublishers", b =>
                {
                    b.HasOne("Library.Entities.Composition", "Composition")
                        .WithMany("CompositionsAndPublishers")
                        .HasForeignKey("CompostionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.PlaceOfPublication", "PlaceOfPublication")
                        .WithMany("CompositionsAndPublishers")
                        .HasForeignKey("PlaceOfPublicationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.PublishingHouse", "PublishingHouse")
                        .WithMany("CompositionsAndPublishers")
                        .HasForeignKey("PublishingHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Composition");

                    b.Navigation("PlaceOfPublication");

                    b.Navigation("PublishingHouse");
                });

            modelBuilder.Entity("GivenExamplers", b =>
                {
                    b.HasOne("Library.Entities.Exampler", "Exampler")
                        .WithMany("GivenExamplers")
                        .HasForeignKey("ExamplerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.Reader", "Reader")
                        .WithMany("GivenExamplers")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Exampler");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("Library.Entities.Composition", b =>
                {
                    b.HasOne("Library.Entities.TypeOfComposition", "Type")
                        .WithMany("Compositions")
                        .HasForeignKey("TypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Type");
                });

            modelBuilder.Entity("Library.Entities.Exampler", b =>
                {
                    b.HasOne("Library.Entities.Composition", "Composition")
                        .WithMany("Examplers")
                        .HasForeignKey("CompositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.Storage", "Storage")
                        .WithMany("Examplers")
                        .HasForeignKey("StorageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Composition");

                    b.Navigation("Storage");
                });

            modelBuilder.Entity("Library.Entities.InterlibrarySubscription", b =>
                {
                    b.HasOne("Library.Entities.Reader", "Reader")
                        .WithMany("InterlibrarySubscriptions")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("Library.Entities.PlaceOfPublication", b =>
                {
                    b.HasOne("Library.Entities.PublishingHouse", "PublishingHouse")
                        .WithMany("PlaceOfPublications")
                        .HasForeignKey("PublishingHouseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("PublishingHouse");
                });

            modelBuilder.Entity("Library.Entities.Reader", b =>
                {
                    b.HasOne("Library.Entities.Category", "Category")
                        .WithMany("Readers")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.PropertiesOfTypes.Department", b =>
                {
                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("ReadersAndPenaltys", b =>
                {
                    b.HasOne("Library.Entities.Penalty", "Penalty")
                        .WithMany("ReadersAndPenaltys")
                        .HasForeignKey("PenaltyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.Reader", "Reader")
                        .WithMany("ReadersAndPenaltys")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Penalty");

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("UserInfo", b =>
                {
                    b.HasOne("Library.Entities.MenuInfo", "MenuInfo")
                        .WithMany("UserInfos")
                        .HasForeignKey("MenuInfoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.User", "User")
                        .WithMany("UserInfos")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MenuInfo");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.Employee", b =>
                {
                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Position");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.Professor", b =>
                {
                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Degree", "Degree")
                        .WithMany()
                        .HasForeignKey("DegreeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Rank", "Rank")
                        .WithMany()
                        .HasForeignKey("RankId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Degree");

                    b.Navigation("Department");

                    b.Navigation("Position");

                    b.Navigation("Rank");
                });

            modelBuilder.Entity("Library.Entities.TypeOfReader.Student", b =>
                {
                    b.HasOne("Library.Entities.TypeOfReader.PropertiesOfTypes.Faculty", "Faculty")
                        .WithMany()
                        .HasForeignKey("FacultyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Faculty");
                });

            modelBuilder.Entity("Library.Entities.Author", b =>
                {
                    b.Navigation("AuthorsAndCompositions");
                });

            modelBuilder.Entity("Library.Entities.Category", b =>
                {
                    b.Navigation("Readers");
                });

            modelBuilder.Entity("Library.Entities.Composition", b =>
                {
                    b.Navigation("AuthorsAndCompositions");

                    b.Navigation("CompositionsAndPublishers");

                    b.Navigation("Examplers");
                });

            modelBuilder.Entity("Library.Entities.Exampler", b =>
                {
                    b.Navigation("GivenExamplers");
                });

            modelBuilder.Entity("Library.Entities.MenuInfo", b =>
                {
                    b.Navigation("UserInfos");
                });

            modelBuilder.Entity("Library.Entities.Penalty", b =>
                {
                    b.Navigation("ReadersAndPenaltys");
                });

            modelBuilder.Entity("Library.Entities.PlaceOfPublication", b =>
                {
                    b.Navigation("CompositionsAndPublishers");
                });

            modelBuilder.Entity("Library.Entities.PublishingHouse", b =>
                {
                    b.Navigation("CompositionsAndPublishers");

                    b.Navigation("PlaceOfPublications");
                });

            modelBuilder.Entity("Library.Entities.Reader", b =>
                {
                    b.Navigation("GivenExamplers");

                    b.Navigation("InterlibrarySubscriptions");

                    b.Navigation("ReadersAndPenaltys");
                });

            modelBuilder.Entity("Library.Entities.Storage", b =>
                {
                    b.Navigation("Examplers");
                });

            modelBuilder.Entity("Library.Entities.TypeOfComposition", b =>
                {
                    b.Navigation("Compositions");
                });

            modelBuilder.Entity("Library.Entities.User", b =>
                {
                    b.Navigation("UserInfos");
                });
#pragma warning restore 612, 618
        }
    }
}
