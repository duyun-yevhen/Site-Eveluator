﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebCrawler.Model;

namespace WebCrawler.Model.Migrations
{
    [DbContext(typeof(WebCrawlerDbContext))]
    [Migration("20210623095144_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebCrawler.Model.PerformanceTest", b =>
                {
                    b.Property<int>("TestId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<string>("SiteUrl")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("TestId");

                    b.ToTable("PerformanceTests");
                });

            modelBuilder.Entity("WebCrawler.Model.UrlResponseTime", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("InSitePage")
                        .HasColumnType("bit");

                    b.Property<bool>("InSitemap")
                        .HasColumnType("bit");

                    b.Property<int>("ResponseTime")
                        .HasColumnType("int");

                    b.Property<int>("TestId")
                        .HasColumnType("int");

                    b.Property<string>("Url")
                        .HasMaxLength(2048)
                        .HasColumnType("nvarchar(2048)");

                    b.HasKey("Id");

                    b.HasIndex("TestId");

                    b.ToTable("UrlResponseTimes");
                });

            modelBuilder.Entity("WebCrawler.Model.UrlResponseTime", b =>
                {
                    b.HasOne("WebCrawler.Model.PerformanceTest", "Test")
                        .WithMany("UrlResponseTimes")
                        .HasForeignKey("TestId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Test");
                });

            modelBuilder.Entity("WebCrawler.Model.PerformanceTest", b =>
                {
                    b.Navigation("UrlResponseTimes");
                });
#pragma warning restore 612, 618
        }
    }
}
