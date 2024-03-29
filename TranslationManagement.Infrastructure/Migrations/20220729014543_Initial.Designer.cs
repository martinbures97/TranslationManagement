﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using TranslationManagement.Infrastructure;

#nullable disable

namespace TranslationManagement.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220729014543_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.7");

            modelBuilder.Entity("TranslationManagement.Application.Entities.TranslationJob.TranslationJobEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerName")
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("OriginalContent")
                        .IsRequired()
                        .HasMaxLength(2000)
                        .HasColumnType("TEXT");

                    b.Property<double>("Price")
                        .HasColumnType("REAL");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TranslatedContent")
                        .HasColumnType("TEXT");

                    b.Property<string>("TranslatorId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TranslatorId");

                    b.ToTable("TranslationJobs");

                    b.HasData(
                        new
                        {
                            Id = "753abc48-2fbf-4023-83b6-6a27fccf4b32",
                            CustomerName = "Netflix",
                            IsDeleted = false,
                            OriginalContent = "Text to translate",
                            Price = 0.17000000000000001,
                            Status = 0
                        },
                        new
                        {
                            Id = "24f08dff-2385-45c1-afba-252b650fd666",
                            CustomerName = "Microsoft",
                            IsDeleted = false,
                            OriginalContent = "Text to translate",
                            Price = 0.17000000000000001,
                            Status = 1,
                            TranslatorId = "0d2c4f16-2dd9-438d-b333-da1c3c8189e6"
                        },
                        new
                        {
                            Id = "8f423273-6e07-433d-bd32-495e2d4bb2b3",
                            CustomerName = "Xiaomi",
                            IsDeleted = false,
                            OriginalContent = "Text to translate",
                            Price = 0.17000000000000001,
                            Status = 2,
                            TranslatorId = "0d2c4f16-2dd9-438d-b333-da1c3c8189e6"
                        },
                        new
                        {
                            Id = "f787b8d2-bd78-4797-9943-d6464c027c2c",
                            CustomerName = "Apple",
                            IsDeleted = true,
                            OriginalContent = "Text to translate",
                            Price = 0.17000000000000001,
                            Status = 0
                        });
                });

            modelBuilder.Entity("TranslationManagement.Application.Entities.Translator.TranslatorEntity", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("CreditCardNumber")
                        .IsRequired()
                        .HasMaxLength(19)
                        .HasColumnType("TEXT");

                    b.Property<int>("HourlyRate")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Translators");

                    b.HasData(
                        new
                        {
                            Id = "a4022e95-cc1d-4fa4-b5fd-fe5690a75521",
                            CreditCardNumber = "4590181712697982",
                            HourlyRate = 500,
                            IsDeleted = false,
                            Name = "Mylie Ritter",
                            Type = 0
                        },
                        new
                        {
                            Id = "0d2c4f16-2dd9-438d-b333-da1c3c8189e6",
                            CreditCardNumber = "4590182781315688",
                            HourlyRate = 1000,
                            IsDeleted = false,
                            Name = "Evica Johansson",
                            Type = 1
                        },
                        new
                        {
                            Id = "99b8b395-17aa-448b-9ee6-68838b5a267b",
                            CreditCardNumber = "4590181640630931",
                            HourlyRate = 150,
                            IsDeleted = true,
                            Name = "Kerstin Bazhaev",
                            Type = 0
                        });
                });

            modelBuilder.Entity("TranslationManagement.Application.Entities.TranslationJob.TranslationJobEntity", b =>
                {
                    b.HasOne("TranslationManagement.Application.Entities.Translator.TranslatorEntity", "Translator")
                        .WithMany("Jobs")
                        .HasForeignKey("TranslatorId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Translator");
                });

            modelBuilder.Entity("TranslationManagement.Application.Entities.Translator.TranslatorEntity", b =>
                {
                    b.Navigation("Jobs");
                });
#pragma warning restore 612, 618
        }
    }
}
