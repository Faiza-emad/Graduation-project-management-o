using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project2023.Migrations
{
    public partial class step1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "depts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DeptName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HOD = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_depts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicationUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    DoctorName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserType = table.Column<int>(type: "int", nullable: false),
                    HOD = table.Column<bool>(type: "bit", nullable: false),
                    DeptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApplicationUsers_depts_DeptId",
                        column: x => x.DeptId,
                        principalTable: "depts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "courses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    CourseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DeptId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_courses_depts_DeptId",
                        column: x => x.DeptId,
                        principalTable: "depts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Fax = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WorkNature = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Objective = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Target = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Summary = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Decision = table.Column<int>(type: "int", nullable: true),
                    StudyYear = table.Column<int>(type: "int", nullable: true),
                    StudySem = table.Column<int>(type: "int", nullable: true),
                    PImage = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    percentage = table.Column<int>(type: "int", nullable: true),
                    Closed = table.Column<int>(type: "int", nullable: true),
                    CourseId = table.Column<int>(type: "int", nullable: true),
                    DoctorId = table.Column<int>(type: "int", nullable: true),
                    Student1 = table.Column<int>(type: "int", nullable: true),
                    Student2 = table.Column<int>(type: "int", nullable: true),
                    Student3 = table.Column<int>(type: "int", nullable: true),
                    Student4 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_projects_ApplicationUsers_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projects_ApplicationUsers_Student1",
                        column: x => x.Student1,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projects_ApplicationUsers_Student2",
                        column: x => x.Student2,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projects_ApplicationUsers_Student3",
                        column: x => x.Student3,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projects_ApplicationUsers_Student4",
                        column: x => x.Student4,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_projects_courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "courses",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectAssessments",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    PQ1 = table.Column<int>(type: "int", nullable: true),
                    PQ2 = table.Column<int>(type: "int", nullable: true),
                    PQ3 = table.Column<int>(type: "int", nullable: true),
                    PQ4 = table.Column<int>(type: "int", nullable: true),
                    PQ5 = table.Column<int>(type: "int", nullable: true),
                    PQ6 = table.Column<int>(type: "int", nullable: true),
                    PQ7 = table.Column<int>(type: "int", nullable: true),
                    DoctorApproved = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectAssessments", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectAssessments_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectCommDecisions",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    AssDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Q1 = table.Column<int>(type: "int", nullable: true),
                    Q2 = table.Column<int>(type: "int", nullable: true),
                    Q3 = table.Column<int>(type: "int", nullable: true),
                    Q4 = table.Column<int>(type: "int", nullable: true),
                    Q5 = table.Column<int>(type: "int", nullable: true),
                    Q6 = table.Column<int>(type: "int", nullable: true),
                    Q7 = table.Column<int>(type: "int", nullable: true),
                    Q8 = table.Column<int>(type: "int", nullable: true),
                    S1 = table.Column<int>(type: "int", nullable: true),
                    S2 = table.Column<int>(type: "int", nullable: true),
                    S3 = table.Column<int>(type: "int", nullable: true),
                    S4 = table.Column<int>(type: "int", nullable: true),
                    S5 = table.Column<int>(type: "int", nullable: true),
                    S6 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectCommDecisions", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectCommDecisions_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectDissDates",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    GradDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradDay = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GradPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Sup1 = table.Column<int>(type: "int", nullable: true),
                    Sup2 = table.Column<int>(type: "int", nullable: true),
                    Sup3 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectDissDates", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectDissDates_ApplicationUsers_Sup1",
                        column: x => x.Sup1,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectDissDates_ApplicationUsers_Sup2",
                        column: x => x.Sup2,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectDissDates_ApplicationUsers_Sup3",
                        column: x => x.Sup3,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProjectDissDates_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectGroups",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    PQ1 = table.Column<int>(type: "int", nullable: true),
                    PQ2 = table.Column<int>(type: "int", nullable: true),
                    PQ3 = table.Column<int>(type: "int", nullable: true),
                    PQ4 = table.Column<int>(type: "int", nullable: true),
                    PQ5 = table.Column<int>(type: "int", nullable: true),
                    DoctorGrade = table.Column<int>(type: "int", nullable: true),
                    committeGrade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectGroups", x => new { x.StudentId, x.ProjectId });
                    table.ForeignKey(
                        name: "FK_ProjectGroups_ApplicationUsers_StudentId",
                        column: x => x.StudentId,
                        principalTable: "ApplicationUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectGroups_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectWeeks",
                columns: table => new
                {
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    Week1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date1 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date2 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date3 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date4 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date5 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date6 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date7 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date8 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date9 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date10 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date11 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Week12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date12 = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    percentage1 = table.Column<int>(type: "int", nullable: false),
                    percentage2 = table.Column<int>(type: "int", nullable: false),
                    percentage3 = table.Column<int>(type: "int", nullable: false),
                    percentage4 = table.Column<int>(type: "int", nullable: false),
                    percentage5 = table.Column<int>(type: "int", nullable: false),
                    percentage6 = table.Column<int>(type: "int", nullable: false),
                    percentage7 = table.Column<int>(type: "int", nullable: false),
                    percentage8 = table.Column<int>(type: "int", nullable: false),
                    percentage9 = table.Column<int>(type: "int", nullable: false),
                    percentage10 = table.Column<int>(type: "int", nullable: false),
                    percentage11 = table.Column<int>(type: "int", nullable: false),
                    percentage12 = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectWeeks", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_ProjectWeeks_projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApplicationUsers_DeptId",
                table: "ApplicationUsers",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_courses_DeptId",
                table: "courses",
                column: "DeptId");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDissDates_Sup1",
                table: "ProjectDissDates",
                column: "Sup1");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDissDates_Sup2",
                table: "ProjectDissDates",
                column: "Sup2");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectDissDates_Sup3",
                table: "ProjectDissDates",
                column: "Sup3");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectGroups_ProjectId",
                table: "ProjectGroups",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_CourseId",
                table: "projects",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_DoctorId",
                table: "projects",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_projects_Student1",
                table: "projects",
                column: "Student1");

            migrationBuilder.CreateIndex(
                name: "IX_projects_Student2",
                table: "projects",
                column: "Student2");

            migrationBuilder.CreateIndex(
                name: "IX_projects_Student3",
                table: "projects",
                column: "Student3");

            migrationBuilder.CreateIndex(
                name: "IX_projects_Student4",
                table: "projects",
                column: "Student4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectAssessments");

            migrationBuilder.DropTable(
                name: "ProjectCommDecisions");

            migrationBuilder.DropTable(
                name: "ProjectDissDates");

            migrationBuilder.DropTable(
                name: "ProjectGroups");

            migrationBuilder.DropTable(
                name: "ProjectWeeks");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "ApplicationUsers");

            migrationBuilder.DropTable(
                name: "courses");

            migrationBuilder.DropTable(
                name: "depts");
        }
    }
}
