namespace CompleteMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialDBScripts : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Course",
                c => new
                    {
                        CourseId = c.Int(nullable: false),
                        Title = c.String(maxLength: 50),
                        Credits = c.Int(nullable: false),
                        DepartmentID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CourseId)
                .ForeignKey("dbo.Department", t => t.DepartmentID, cascadeDelete: true)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.Department",
                c => new
                    {
                        DepartmentId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                        Budget = c.Decimal(nullable: false, storeType: "money"),
                        StartDate = c.DateTime(nullable: false),
                        InstructorID = c.Int(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                        Administrator_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.DepartmentId)
                .ForeignKey("dbo.Instructor", t => t.Administrator_UserId)
                .Index(t => t.Administrator_UserId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false, maxLength: 50),
                        IsActive = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        LastName = c.String(nullable: false, maxLength: 50),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        Age = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.OfficeAssignment",
                c => new
                    {
                        InstructorId = c.Int(nullable: false),
                        Location = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.InstructorId)
                .ForeignKey("dbo.Instructor", t => t.InstructorId)
                .Index(t => t.InstructorId);
            
            CreateTable(
                "dbo.Role",
                c => new
                    {
                        RoleId = c.Int(nullable: false, identity: true),
                        RoleName = c.String(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.RoleId);
            
            CreateTable(
                "dbo.Enrollment",
                c => new
                    {
                        EnrollmentId = c.Int(nullable: false, identity: true),
                        CourseId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        Grade = c.Int(),
                        Student_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.EnrollmentId)
                .ForeignKey("dbo.Course", t => t.CourseId, cascadeDelete: true)
                .ForeignKey("dbo.Student", t => t.Student_UserId)
                .Index(t => t.CourseId)
                .Index(t => t.Student_UserId);
            
            CreateTable(
                "dbo.InstructorCourse",
                c => new
                    {
                        Instructor_UserId = c.Int(nullable: false),
                        Course_CourseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Instructor_UserId, t.Course_CourseId })
                .ForeignKey("dbo.Instructor", t => t.Instructor_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Course", t => t.Course_CourseId, cascadeDelete: true)
                .Index(t => t.Instructor_UserId)
                .Index(t => t.Course_CourseId);
            
            CreateTable(
                "dbo.UserRole",
                c => new
                    {
                        User_UserId = c.Int(nullable: false),
                        Role_RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.User_UserId, t.Role_RoleId })
                .ForeignKey("dbo.User", t => t.User_UserId, cascadeDelete: true)
                .ForeignKey("dbo.Role", t => t.Role_RoleId, cascadeDelete: true)
                .Index(t => t.User_UserId)
                .Index(t => t.Role_RoleId);
            
            CreateTable(
                "dbo.Instructor",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        InstructorId = c.Int(nullable: false),
                        HireDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Student",
                c => new
                    {
                        UserId = c.Int(nullable: false),
                        StudentId = c.Int(nullable: false),
                        EnrollmentDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.User", t => t.UserId)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Student", "UserId", "dbo.User");
            DropForeignKey("dbo.Instructor", "UserId", "dbo.User");
            DropForeignKey("dbo.Course", "DepartmentID", "dbo.Department");
            DropForeignKey("dbo.Department", "Administrator_UserId", "dbo.Instructor");
            DropForeignKey("dbo.Enrollment", "Student_UserId", "dbo.Student");
            DropForeignKey("dbo.Enrollment", "CourseId", "dbo.Course");
            DropForeignKey("dbo.UserRole", "Role_RoleId", "dbo.Role");
            DropForeignKey("dbo.UserRole", "User_UserId", "dbo.User");
            DropForeignKey("dbo.OfficeAssignment", "InstructorId", "dbo.Instructor");
            DropForeignKey("dbo.InstructorCourse", "Course_CourseId", "dbo.Course");
            DropForeignKey("dbo.InstructorCourse", "Instructor_UserId", "dbo.Instructor");
            DropIndex("dbo.Student", new[] { "UserId" });
            DropIndex("dbo.Instructor", new[] { "UserId" });
            DropIndex("dbo.UserRole", new[] { "Role_RoleId" });
            DropIndex("dbo.UserRole", new[] { "User_UserId" });
            DropIndex("dbo.InstructorCourse", new[] { "Course_CourseId" });
            DropIndex("dbo.InstructorCourse", new[] { "Instructor_UserId" });
            DropIndex("dbo.Enrollment", new[] { "Student_UserId" });
            DropIndex("dbo.Enrollment", new[] { "CourseId" });
            DropIndex("dbo.OfficeAssignment", new[] { "InstructorId" });
            DropIndex("dbo.Department", new[] { "Administrator_UserId" });
            DropIndex("dbo.Course", new[] { "DepartmentID" });
            DropTable("dbo.Student");
            DropTable("dbo.Instructor");
            DropTable("dbo.UserRole");
            DropTable("dbo.InstructorCourse");
            DropTable("dbo.Enrollment");
            DropTable("dbo.Role");
            DropTable("dbo.OfficeAssignment");
            DropTable("dbo.User");
            DropTable("dbo.Department");
            DropTable("dbo.Course");
        }
    }
}
