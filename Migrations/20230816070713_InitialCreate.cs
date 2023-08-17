using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TechyRecruit.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RecruitModel",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Recruiter = table.Column<string>(type: "TEXT", nullable: false),
                    OpeningDetails = table.Column<string>(type: "TEXT", nullable: false),
                    CandidateName = table.Column<string>(type: "TEXT", nullable: false),
                    ReceivedDate = table.Column<string>(type: "TEXT", nullable: true),
                    ContactNumber = table.Column<string>(type: "TEXT", nullable: false),
                    Email = table.Column<string>(type: "TEXT", nullable: false),
                    Company = table.Column<string>(type: "TEXT", nullable: false),
                    TotalExperience = table.Column<string>(type: "TEXT", nullable: false),
                    RelevantExperience = table.Column<string>(type: "TEXT", nullable: false),
                    CCTC = table.Column<decimal>(type: "TEXT", nullable: false),
                    ECTC = table.Column<decimal>(type: "TEXT", nullable: false),
                    CurrentLocation = table.Column<string>(type: "TEXT", nullable: false),
                    PreferredLocation = table.Column<string>(type: "TEXT", nullable: false),
                    NoticePeriodOrLastWorkingDay = table.Column<string>(type: "TEXT", nullable: false),
                    HoldingOfferOrPackageAmount = table.Column<string>(type: "TEXT", nullable: false),
                    ExperienceInCloudPlatforms = table.Column<string>(type: "TEXT", nullable: false),
                    ExperienceInLeadHandling = table.Column<string>(type: "TEXT", nullable: false),
                    ExperienceInPerformanceTesting = table.Column<string>(type: "TEXT", nullable: false),
                    ContractRoleRequired = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecruitModel", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RecruitModel");
        }
    }
}
