﻿// <auto-generated />
using System;
using AURA.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AURA.Migrations
{
    [DbContext(typeof(PostContext))]
    [Migration("20201223062404_initialPost")]
    partial class initialPost
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AURA.Models.Agents", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AuraId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("AuraRole")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("nvarchar(60)")
                        .HasMaxLength(60);

                    b.Property<string>("NickName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("TaxId")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("Agents");
                });

            modelBuilder.Entity("AURA.Models.AgentsAddress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostCode")
                        .IsRequired()
                        .HasColumnType("nvarchar(20)")
                        .HasMaxLength(20);

                    b.Property<string>("StreetAddress")
                        .IsRequired()
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgentsAddresses");
                });

            modelBuilder.Entity("AURA.Models.AgentsEmail", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EmailAddress")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgentsEmails");
                });

            modelBuilder.Entity("AURA.Models.AgentsPayment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentDetail")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PaymentType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgentsPayments");
                });

            modelBuilder.Entity("AURA.Models.AgentsPhone", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CarrierName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CountryCode")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmailText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgentsPhones");
                });

            modelBuilder.Entity("AURA.Models.AgentsRemarks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RemarkSubject")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RemarkText")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgentsRemarks");
                });

            modelBuilder.Entity("AURA.Models.AgentsRoles", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AgentRole")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Notes")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PayRate")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PayType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("AgentsRoles");
                });

            modelBuilder.Entity("AURA.Models.BookAccounts", b =>
                {
                    b.Property<int>("AccountId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AccountName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.HasKey("AccountId");

                    b.ToTable("BookAccounts");
                });

            modelBuilder.Entity("AURA.Models.FractionList", b =>
                {
                    b.Property<string>("FractionId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<string>("FractionDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("FractionId");

                    b.ToTable("FractionLists");
                });

            modelBuilder.Entity("AURA.Models.Journal", b =>
                {
                    b.Property<string>("InvoiceNumber")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Ac1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Ac2")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Acf")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Check")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ForeignKey")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("InvoiceDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsHidden")
                        .HasColumnType("bit");

                    b.Property<bool>("IsPayment")
                        .HasColumnType("bit");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PartyId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PostId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Reference")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Sign")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Stage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("InvoiceNumber");

                    b.ToTable("Journals");
                });

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

                    b.Property<decimal>("EigLoad")
                        .HasColumnType("decimal(18,2)")
                        .HasMaxLength(10);

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
                        .HasColumnType("nvarchar(2000)")
                        .HasMaxLength(2000);

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

                    b.Property<decimal>("SevAmou")
                        .HasColumnType("decimal(18,2)");

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

                    b.Property<decimal>("SixAmou")
                        .HasColumnType("decimal(18,2)");

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

            modelBuilder.Entity("AURA.Models.ProductList", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("AccountNumber")
                        .HasColumnType("int");

                    b.Property<DateTime>("EntryDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FractionId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("ListPrice")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ProductDescription")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProductId");

                    b.ToTable("ProductList");
                });
#pragma warning restore 612, 618
        }
    }
}
