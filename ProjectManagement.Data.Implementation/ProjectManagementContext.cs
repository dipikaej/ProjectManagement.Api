using Microsoft.EntityFrameworkCore;
using ProjectManagement.Entities;
using System;

namespace ProjectManagement.Data
{
    public class ProjectManagementContext : DbContext
    {
        public ProjectManagementContext(DbContextOptions options) : base(options)
        {

        }

        public void SeedInitialData()
        {
            User testUser1 = new User
            {
                FirstName = "Test",
                LastName = "User1",
                Email = "testuser1@test.com",
            };
            User.Add(testUser1);
            User testUser2 = new User
            {
                FirstName = "Test",
                LastName = "User2",
                Email = "testuser2@gmail.com"
            };
            User.Add(testUser2);

            Project testProject1 = new Project { Name = "TestProject1", CreatedOn = DateTime.Now, Detail = "This is Test project 1" };
            Project testProject2 = new Project { Name = "TestProject2", CreatedOn = DateTime.Now, Detail = "This is Test project 2" };

            Project.Add(testProject1);
            Project.Add(testProject2);
            this.SaveChanges();
        }

        public DbSet<User> User { get; set; }

        public DbSet<Project> Project { get; set; }

        public DbSet<Task> Task { get; set; }
    }
}
