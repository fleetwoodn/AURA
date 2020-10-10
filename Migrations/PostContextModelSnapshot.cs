﻿// <auto-generated />
using System;
using AURA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AURA.Migrations
{
    [DbContext(typeof(PostContext))]
    partial class PostContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AURA.Models.PostEig", b =>
                {
                    b.Property<int>("EigId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EigAgen")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<int>("EigDigit")
                        .HasColumnType("int");

                    b.Property<string>("EigLoad")
                        .HasColumnType("nvarchar(3)")
                        .HasMaxLength(3);

                    b.Property<string>("EigNote")
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("EigRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("EigZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EigId");

                    b.ToTable("PostEigs");
                });

            modelBuilder.Entity("AURA.Models.PostFiv", b =>
                {
                    b.Property<int>("FivId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FivCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<int>("FivDigit")
                        .HasColumnType("int");

                    b.Property<string>("FivPrio")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)")
                        .HasMaxLength(1);

                    b.Property<string>("FivText")
                        .IsRequired()
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("FivZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FivId");

                    b.ToTable("PostFivs");
                });

            modelBuilder.Entity("AURA.Models.PostFou", b =>
                {
                    b.Property<int>("FouId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FouAddr")
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<int>("FouDigit")
                        .HasColumnType("int");

                    b.Property<string>("FouEmai")
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("FouName")
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("FouNote")
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("FouOrg")
                        .HasColumnType("nvarchar(45)")
                        .HasMaxLength(45);

                    b.Property<string>("FouPhon")
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("FouPost")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("FouZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("FouId");

                    b.ToTable("PostFous");
                });

            modelBuilder.Entity("AURA.Models.PostNin", b =>
                {
                    b.Property<int>("NinId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NinCapt")
                        .IsRequired()
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<int>("NinDigit")
                        .HasColumnType("int");

                    b.Property<string>("NinFile")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NinNote")
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("NinZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("NinId");

                    b.ToTable("PostNins");
                });

            modelBuilder.Entity("AURA.Models.PostOne", b =>
                {
                    b.Property<int>("OneId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("OneAgen")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<string>("OnePart")
                        .IsRequired()
                        .HasColumnType("nvarchar(35)")
                        .HasMaxLength(35);

                    b.Property<string>("OneStag")
                        .IsRequired()
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("OneTitl")
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("OneZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OneId");

                    b.ToTable("PostOnes");
                });

            modelBuilder.Entity("AURA.Models.PostSev", b =>
                {
                    b.Property<int>("SevId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SevAc1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevAc2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevAcf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevAmou")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevChec")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevCust")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SevDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SevDesc")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SevDigit")
                        .HasColumnType("int");

                    b.Property<string>("SevHidd")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevInvo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevNote")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevPart")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevPaym")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevRefe")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevSign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevStage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevStat")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SevZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SevId");

                    b.ToTable("PostSevs");
                });

            modelBuilder.Entity("AURA.Models.PostSix", b =>
                {
                    b.Property<int>("SixId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SixAmou")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("SixDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("SixDeta")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SixDigit")
                        .HasColumnType("int");

                    b.Property<string>("SixNote")
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("SixStat")
                        .HasColumnType("nvarchar(5)")
                        .HasMaxLength(5);

                    b.Property<string>("SixType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SixZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SixId");

                    b.ToTable("PostSixs");
                });

            modelBuilder.Entity("AURA.Models.PostThr", b =>
                {
                    b.Property<int>("ThrId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ThrDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ThrDigit")
                        .HasColumnType("int");

                    b.Property<string>("ThrText")
                        .IsRequired()
                        .HasColumnType("nvarchar(160)")
                        .HasMaxLength(160);

                    b.Property<string>("ThrZero")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ThrId");

                    b.ToTable("PostThrs");
                });

            modelBuilder.Entity("AURA.Models.PostZero", b =>
                {
                    b.Property<string>("Zero")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ZeroAgen")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("ZeroDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Zero");

                    b.ToTable("PostZeros");
                });
#pragma warning restore 612, 618
        }
    }
}
