﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReservationSystem.Reservations.Infrastructure.EntityFramework;

namespace ReservationSystem.Reservations.Infrastructure.Migrations
{
    [DbContext(typeof(ReservationContext))]
    partial class ReservationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReservationSystem.Base.Services.Outbox.EntityFramework.Entities.OutboxMessage", b =>
                {
                    b.Property<long>("OutboxMessageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssemblyName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FullNameMessageType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("MessageId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("ObjectId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("OccurredOn")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<DateTime?>("SentDate")
                        .IsConcurrencyToken()
                        .HasColumnType("datetime2");

                    b.Property<string>("SerializedMessage")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("OutboxMessageId");

                    b.ToTable("OutboxMessages","Outbox");
                });

            modelBuilder.Entity("ReservationSystem.Reservations.Domain.Reservations.Reservation", b =>
                {
                    b.Property<long>("ReservationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("CreatedDate")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<int>("ReservationStatus")
                        .HasColumnType("int");

                    b.Property<long>("ServiceId")
                        .HasColumnType("bigint");

                    b.Property<byte[]>("Timestamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ReservationId");

                    b.ToTable("Reservations","Reservations");
                });

            modelBuilder.Entity("ReservationSystem.Reservations.Domain.Service.Service", b =>
                {
                    b.Property<long>("ServiceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("CanBeReserved")
                        .HasColumnType("bit");

                    b.HasKey("ServiceId");

                    b.ToTable("Services","Services");
                });

            modelBuilder.Entity("ReservationSystem.Reservations.Domain.Reservations.Reservation", b =>
                {
                    b.OwnsOne("ReservationSystem.Reservations.Domain.AvailableDates.ReservationDate", "ReservationDate", b1 =>
                        {
                            b1.Property<long>("ReservationId")
                                .HasColumnType("bigint");

                            b1.Property<Guid>("DateId")
                                .HasColumnName("DateId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<DateTime>("EndDateTime")
                                .HasColumnName("EndDateTime")
                                .HasColumnType("datetime2");

                            b1.Property<bool>("IsAvailable")
                                .HasColumnName("IsAvailable")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("StartDateTime")
                                .HasColumnName("StartDateTime")
                                .HasColumnType("datetime2");

                            b1.HasKey("ReservationId");

                            b1.ToTable("ReservationDates","Reservations");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });

                    b.OwnsOne("ReservationSystem.Reservations.Domain.Offer.Offer", "Offer", b1 =>
                        {
                            b1.Property<long>("ReservationId")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("bigint")
                                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                            b1.Property<string>("CurrencyCode")
                                .IsRequired()
                                .HasColumnName("CurrencyCode")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<decimal>("Price")
                                .HasColumnName("Price")
                                .HasColumnType("decimal(18,2)");

                            b1.HasKey("ReservationId");

                            b1.ToTable("Reservations");

                            b1.WithOwner()
                                .HasForeignKey("ReservationId");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
