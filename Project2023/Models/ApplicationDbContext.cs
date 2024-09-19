using Microsoft.EntityFrameworkCore;
namespace Project2023.Models
{
    public class ApplicationDbContext:DbContext
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProjectGroup>().HasKey(col => new {
                col.StudentId,
                col.ProjectId,

            });

            modelBuilder.Entity<ProjectGroup>().HasOne(tbl => tbl.Project_ProjectGroup)
                .WithMany(tbl => tbl.ProjectGroups).HasForeignKey(col => col.ProjectId);

            modelBuilder.Entity<ProjectGroup>().HasOne(tbl => tbl.Project_Student)
                .WithMany(tbl => tbl.ProjectStudents).HasForeignKey(col => col.StudentId);

            base.OnModelCreating(modelBuilder);

        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Dept> depts { get; set; }
        public DbSet<Course> courses { get; set; }
        public DbSet<Project> projects { get; set; }
        public DbSet<ProjectAssessment> ProjectAssessments { get; set; }
        public DbSet<ProjectCommDecision> ProjectCommDecisions { get; set; }
        public DbSet<ProjectDissDate> ProjectDissDates { get; set; }
        public DbSet<ProjectGroup> ProjectGroups { get; set; }
        public DbSet<ProjectWeek> ProjectWeeks { get; set; }
        public DbSet<Rating> Ratings { get; set; }



    }

}
