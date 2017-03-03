using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HoveyTech.Data.EfCore.Tests.TestContext;

namespace HoveyTech.Data.EfCore.Tests.Migrations
{
    [DbContext(typeof(TestDbContext))]
    [Migration("20170303042812_TestGuidObject")]
    partial class TestGuidObject
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("HoveyTech.Data.EfCore.Tests.TestContext.TestGuidObject", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.ToTable("TestGuidObject");
                });

            modelBuilder.Entity("HoveyTech.Data.EfCore.Tests.TestContext.TestObject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTimeOffset>("CreatedOn");

                    b.Property<Guid>("TestGuidObjectId");

                    b.Property<string>("Text");

                    b.HasKey("Id");

                    b.HasIndex("TestGuidObjectId");

                    b.ToTable("TestObject");
                });

            modelBuilder.Entity("HoveyTech.Data.EfCore.Tests.TestContext.TestObject", b =>
                {
                    b.HasOne("HoveyTech.Data.EfCore.Tests.TestContext.TestGuidObject", "TestGuidObject")
                        .WithMany("TestObjects")
                        .HasForeignKey("TestGuidObjectId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
