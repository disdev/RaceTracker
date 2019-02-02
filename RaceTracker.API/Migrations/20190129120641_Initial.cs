using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RaceTracker.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Checkpoints",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    GeoJson = table.Column<string>(nullable: true),
                    Number = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkpoints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    From = table.Column<string>(nullable: true),
                    Body = table.Column<string>(nullable: true),
                    FromCity = table.Column<string>(nullable: true),
                    FromState = table.Column<string>(nullable: true),
                    FromCountry = table.Column<string>(nullable: true),
                    FromZip = table.Column<string>(nullable: true),
                    Received = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RaceEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    Start = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RaceEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Monitors",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CheckpointId = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monitors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monitors_Checkpoints_CheckpointId",
                        column: x => x.CheckpointId,
                        principalTable: "Checkpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Races",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    Distance = table.Column<decimal>(nullable: false),
                    Unit = table.Column<int>(nullable: false),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    UltraSignupUrl = table.Column<string>(nullable: true),
                    RaceEventId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Races", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Races_RaceEvents_RaceEventId",
                        column: x => x.RaceEventId,
                        principalTable: "RaceEvents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Participants",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Bib = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Region = table.Column<string>(nullable: true),
                    Age = table.Column<string>(nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    RaceId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Participants", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Participants_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Segments",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    RaceId = table.Column<Guid>(nullable: false),
                    Order = table.Column<int>(nullable: false),
                    GeoJson = table.Column<string>(nullable: true),
                    FromCheckpointId = table.Column<Guid>(nullable: true),
                    ToCheckpointId = table.Column<Guid>(nullable: true),
                    Distance = table.Column<double>(nullable: false),
                    TotalDistance = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Segments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Segments_Checkpoints_FromCheckpointId",
                        column: x => x.FromCheckpointId,
                        principalTable: "Checkpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Segments_Races_RaceId",
                        column: x => x.RaceId,
                        principalTable: "Races",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Segments_Checkpoints_ToCheckpointId",
                        column: x => x.ToCheckpointId,
                        principalTable: "Checkpoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Watchers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: true),
                    ParticipantId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Watchers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Watchers_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Checkins",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    When = table.Column<DateTime>(nullable: false),
                    ParticipantId = table.Column<Guid>(nullable: false),
                    SegmentId = table.Column<Guid>(nullable: true),
                    Confirmed = table.Column<bool>(nullable: false),
                    Note = table.Column<string>(nullable: true),
                    MessageId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Checkins", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Checkins_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkins_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Checkins_Segments_SegmentId",
                        column: x => x.SegmentId,
                        principalTable: "Segments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Leaders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ParticipantId = table.Column<Guid>(nullable: false),
                    Status = table.Column<int>(nullable: false),
                    Progress = table.Column<int>(nullable: false),
                    LastCheckinId = table.Column<Guid>(nullable: true),
                    ElapsedTime = table.Column<double>(nullable: false),
                    Checkins = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Leaders_Checkins_LastCheckinId",
                        column: x => x.LastCheckinId,
                        principalTable: "Checkins",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Leaders_Participants_ParticipantId",
                        column: x => x.ParticipantId,
                        principalTable: "Participants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_MessageId",
                table: "Checkins",
                column: "MessageId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_ParticipantId",
                table: "Checkins",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Checkins_SegmentId",
                table: "Checkins",
                column: "SegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaders_LastCheckinId",
                table: "Leaders",
                column: "LastCheckinId");

            migrationBuilder.CreateIndex(
                name: "IX_Leaders_ParticipantId",
                table: "Leaders",
                column: "ParticipantId");

            migrationBuilder.CreateIndex(
                name: "IX_Monitors_CheckpointId",
                table: "Monitors",
                column: "CheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_Participants_RaceId",
                table: "Participants",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Races_RaceEventId",
                table: "Races",
                column: "RaceEventId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_FromCheckpointId",
                table: "Segments",
                column: "FromCheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_RaceId",
                table: "Segments",
                column: "RaceId");

            migrationBuilder.CreateIndex(
                name: "IX_Segments_ToCheckpointId",
                table: "Segments",
                column: "ToCheckpointId");

            migrationBuilder.CreateIndex(
                name: "IX_Watchers_ParticipantId",
                table: "Watchers",
                column: "ParticipantId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Leaders");

            migrationBuilder.DropTable(
                name: "Monitors");

            migrationBuilder.DropTable(
                name: "Watchers");

            migrationBuilder.DropTable(
                name: "Checkins");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Participants");

            migrationBuilder.DropTable(
                name: "Segments");

            migrationBuilder.DropTable(
                name: "Checkpoints");

            migrationBuilder.DropTable(
                name: "Races");

            migrationBuilder.DropTable(
                name: "RaceEvents");
        }
    }
}
