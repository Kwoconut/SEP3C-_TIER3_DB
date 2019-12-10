﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SEP3_TIER3.Database;

namespace SEP3_TIER3.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SEP3_TIER3.Model.Edge", b =>
                {
                    b.Property<int>("EdgeId")
                        .HasColumnType("int");

                    b.Property<int>("FromNodeIndex")
                        .HasColumnType("int");

                    b.Property<int>("Length")
                        .HasColumnType("int");

                    b.Property<int>("ToNodeIndex")
                        .HasColumnType("int");

                    b.HasKey("EdgeId");

                    b.ToTable("Edges");
                });

            modelBuilder.Entity("SEP3_TIER3.Model.FlightPlan", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ArrivalTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("Delay")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DepartureTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("EndLocation")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StartLocation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("FlightPlans");
                });

            modelBuilder.Entity("SEP3_TIER3.Model.Node", b =>
                {
                    b.Property<int>("NodeId")
                        .HasColumnType("int")
                        .HasMaxLength(20);

                    b.Property<int>("DistanceFromSource")
                        .HasColumnType("int");

                    b.Property<bool>("IsVisited")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PositionXCoordinate")
                        .HasColumnType("float");

                    b.Property<double>("PositionYCoordinate")
                        .HasColumnType("float");

                    b.Property<bool>("isGroundNode")
                        .HasColumnType("bit");

                    b.HasKey("NodeId");

                    b.HasIndex("PositionXCoordinate", "PositionYCoordinate");

                    b.ToTable("GroundNodes");
                });

            modelBuilder.Entity("SEP3_TIER3.Model.NodeEdge", b =>
                {
                    b.Property<int>("EdgeId")
                        .HasColumnType("int");

                    b.Property<int>("NodeId")
                        .HasColumnType("int");

                    b.HasKey("EdgeId", "NodeId");

                    b.HasIndex("NodeId");

                    b.ToTable("NodeEdges");
                });

            modelBuilder.Entity("SEP3_TIER3.Model.Plane", b =>
                {
                    b.Property<string>("CallSign")
                        .HasColumnType("nvarchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Company")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("FlightPlanId")
                        .HasColumnType("int");

                    b.Property<string>("Model")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("PositionXCoordinate")
                        .HasColumnType("float");

                    b.Property<double>("PositionYCoordinate")
                        .HasColumnType("float");

                    b.Property<string>("Status")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("CallSign");

                    b.HasIndex("FlightPlanId");

                    b.HasIndex("PositionXCoordinate", "PositionYCoordinate");

                    b.ToTable("Planes");
                });

            modelBuilder.Entity("SEP3_TIER3.Model.Position", b =>
                {
                    b.Property<double>("XCoordinate")
                        .HasColumnType("float");

                    b.Property<double>("YCoordinate")
                        .HasColumnType("float");

                    b.HasKey("XCoordinate", "YCoordinate");

                    b.ToTable("Positions");
                });

            modelBuilder.Entity("SEP3_TIER3.Model.Node", b =>
                {
                    b.HasOne("SEP3_TIER3.Model.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionXCoordinate", "PositionYCoordinate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SEP3_TIER3.Model.NodeEdge", b =>
                {
                    b.HasOne("SEP3_TIER3.Model.Edge", "Edge")
                        .WithMany("NodeEdges")
                        .HasForeignKey("EdgeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SEP3_TIER3.Model.Node", "GroundNode")
                        .WithMany("NodeEdges")
                        .HasForeignKey("NodeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SEP3_TIER3.Model.Plane", b =>
                {
                    b.HasOne("SEP3_TIER3.Model.FlightPlan", "FlightPlan")
                        .WithMany()
                        .HasForeignKey("FlightPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SEP3_TIER3.Model.Position", "Position")
                        .WithMany()
                        .HasForeignKey("PositionXCoordinate", "PositionYCoordinate")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
