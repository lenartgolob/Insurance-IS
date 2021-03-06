using web.Models;
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace web.Data
{
    public static class DbInitializer
    {
        public static void Initialize(InsuranceContext context)
        {
            context.Database.EnsureCreated();

            // Look for any students.
            if (context.Insured.Any())
            {
                return;   // DB has been seeded
            }

            var insured = new Insured[]
            {
            new Insured{FirstMidName="Carson",LastName="Alexander"},
            new Insured{FirstMidName="Meredith",LastName="Alonso"},
            new Insured{FirstMidName="Arturo",LastName="Anand"},
            new Insured{FirstMidName="Gytis",LastName="Barzdukas"},
            new Insured{FirstMidName="Yan",LastName="Li"},
            new Insured{FirstMidName="Peggy",LastName="Justice"},
            new Insured{FirstMidName="Laura",LastName="Norman"},
            new Insured{FirstMidName="Nino",LastName="Olivetto"}
            };
            foreach (Insured i in insured)
            {
                context.Insured.Add(i);
            }
            context.SaveChanges();

        //      var instructors = new Instructor[]
        //     {
        //         new Instructor { FirstMidName = "Kim",     LastName = "Abercrombie",
        //             HireDate = DateTime.Parse("1995-03-11") },
        //         new Instructor { FirstMidName = "Fadi",    LastName = "Fakhouri",
        //             HireDate = DateTime.Parse("2002-07-06") },
        //         new Instructor { FirstMidName = "Roger",   LastName = "Harui",
        //             HireDate = DateTime.Parse("1998-07-01") },
        //         new Instructor { FirstMidName = "Candace", LastName = "Kapoor",
        //             HireDate = DateTime.Parse("2001-01-15") },
        //         new Instructor { FirstMidName = "Roger",   LastName = "Zheng",
        //             HireDate = DateTime.Parse("2004-02-12") }
        //     };

        //     foreach (Instructor i in instructors)
        //     {
        //         context.Instructors.Add(i);
        //     }
        //     context.SaveChanges();

        //     var departments = new Department[]
        //     {
        //         new Department { Name = "English",     Budget = 350000,
        //             StartDate = DateTime.Parse("2007-09-01"),
        //             InstructorID  = instructors.Single( i => i.LastName == "Abercrombie").ID },
        //         new Department { Name = "Mathematics", Budget = 100000,
        //             StartDate = DateTime.Parse("2007-09-01"),
        //             InstructorID  = instructors.Single( i => i.LastName == "Fakhouri").ID },
        //         new Department { Name = "Engineering", Budget = 350000,
        //             StartDate = DateTime.Parse("2007-09-01"),
        //             InstructorID  = instructors.Single( i => i.LastName == "Harui").ID },
        //         new Department { Name = "Economics",   Budget = 100000,
        //             StartDate = DateTime.Parse("2007-09-01"),
        //             InstructorID  = instructors.Single( i => i.LastName == "Kapoor").ID }
        //     };

        //     foreach (Department d in departments)
        //     {
        //         context.Departments.Add(d);
        //     }
        //     context.SaveChanges();

        //     var courses = new Course[]
        //     {
        //         new Course {CourseID = 1050, Title = "Chemistry",      Credits = 3,
        //             DepartmentID = departments.Single( s => s.Name == "Engineering").DepartmentID
        //         },
        //         new Course {CourseID = 4022, Title = "Microeconomics", Credits = 3,
        //             DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
        //         },
        //         new Course {CourseID = 4041, Title = "Macroeconomics", Credits = 3,
        //             DepartmentID = departments.Single( s => s.Name == "Economics").DepartmentID
        //         },
        //         new Course {CourseID = 1045, Title = "Calculus",       Credits = 4,
        //             DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
        //         },
        //         new Course {CourseID = 3141, Title = "Trigonometry",   Credits = 4,
        //             DepartmentID = departments.Single( s => s.Name == "Mathematics").DepartmentID
        //         },
        //         new Course {CourseID = 2021, Title = "Composition",    Credits = 3,
        //             DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
        //         },
        //         new Course {CourseID = 2042, Title = "Literature",     Credits = 4,
        //             DepartmentID = departments.Single( s => s.Name == "English").DepartmentID
        //         },
        //     };

        //     foreach (Course c in courses)
        //     {
        //         context.Courses.Add(c);
        //     }
        //     context.SaveChanges();

        //     var officeAssignments = new OfficeAssignment[]
        //     {
        //         new OfficeAssignment {
        //             InstructorID = instructors.Single( i => i.LastName == "Fakhouri").ID,
        //             Location = "Smith 17" },
        //         new OfficeAssignment {
        //             InstructorID = instructors.Single( i => i.LastName == "Harui").ID,
        //             Location = "Gowan 27" },
        //         new OfficeAssignment {
        //             InstructorID = instructors.Single( i => i.LastName == "Kapoor").ID,
        //             Location = "Thompson 304" },
        //     };

        //     foreach (OfficeAssignment o in officeAssignments)
        //     {
        //         context.OfficeAssignments.Add(o);
        //     }
        //     context.SaveChanges();

        //     var courseInstructors = new CourseAssignment[]
        //     {
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Kapoor").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Harui").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Zheng").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Fakhouri").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Harui").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
        //             },
        //         new CourseAssignment {
        //             CourseID = courses.Single(c => c.Title == "Literature" ).CourseID,
        //             InstructorID = instructors.Single(i => i.LastName == "Abercrombie").ID
        //             },
        //     };

        //     foreach (CourseAssignment ci in courseInstructors)
        //     {
        //         context.CourseAssignments.Add(ci);
        //     }
        //     context.SaveChanges();

        //     var enrollments = new Enrollment[]
        //     {
        //         new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Alexander").ID,
        //             CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID,
        //             Grade = Grade.A
        //         },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Alexander").ID,
        //             CourseID = courses.Single(c => c.Title == "Microeconomics" ).CourseID,
        //             Grade = Grade.C
        //             },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Alexander").ID,
        //             CourseID = courses.Single(c => c.Title == "Macroeconomics" ).CourseID,
        //             Grade = Grade.B
        //             },
        //             new Enrollment {
        //                 StudentID = students.Single(s => s.LastName == "Alonso").ID,
        //             CourseID = courses.Single(c => c.Title == "Calculus" ).CourseID,
        //             Grade = Grade.B
        //             },
        //             new Enrollment {
        //                 StudentID = students.Single(s => s.LastName == "Alonso").ID,
        //             CourseID = courses.Single(c => c.Title == "Trigonometry" ).CourseID,
        //             Grade = Grade.B
        //             },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Alonso").ID,
        //             CourseID = courses.Single(c => c.Title == "Composition" ).CourseID,
        //             Grade = Grade.B
        //             },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Anand").ID,
        //             CourseID = courses.Single(c => c.Title == "Chemistry" ).CourseID
        //             },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Anand").ID,
        //             CourseID = courses.Single(c => c.Title == "Microeconomics").CourseID,
        //             Grade = Grade.B
        //             },
        //         new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Barzdukas").ID,
        //             CourseID = courses.Single(c => c.Title == "Chemistry").CourseID,
        //             Grade = Grade.B
        //             },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Li").ID,
        //             CourseID = courses.Single(c => c.Title == "Composition").CourseID,
        //             Grade = Grade.B
        //             },
        //             new Enrollment {
        //             StudentID = students.Single(s => s.LastName == "Justice").ID,
        //             CourseID = courses.Single(c => c.Title == "Literature").CourseID,
        //             Grade = Grade.B
        //             }
        //     };

        //     foreach (Enrollment e in enrollments)
        //     {
        //         var enrollmentInDataBase = context.Enrollments.Where(
        //             s =>
        //                     s.Student.ID == e.StudentID &&
        //                     s.Course.CourseID == e.CourseID).SingleOrDefault();
        //         if (enrollmentInDataBase == null)
        //         {
        //             context.Enrollments.Add(e);
        //         }
        //     }
        //     context.SaveChanges();

        //     var roles = new IdentityRole[] {
        //         new IdentityRole{Id="1", Name="Administrator"},
        //         new IdentityRole{Id="2", Name="Manager"},
        //         new IdentityRole{Id="3", Name="Staff"}
        //     };

        //     foreach (IdentityRole r in roles)
        //     {
        //         context.Roles.Add(r);
        //     }

        //     var user = new ApplicationUser
        //     {
        //         FirstName = "Bob",
        //         LastName = "Dilon",
        //         City = "Ljubljana",
        //         Email = "bob@example.com",
        //         NormalizedEmail = "XXXX@EXAMPLE.COM",
        //         UserName = "bob@example.com",
        //         NormalizedUserName = "bob@example.com",
        //         PhoneNumber = "+111111111111",
        //         EmailConfirmed = true,
        //         PhoneNumberConfirmed = true,
        //         SecurityStamp = Guid.NewGuid().ToString("D")
        //     };


        //     if (!context.Users.Any(u => u.UserName == user.UserName))
        //     {
        //         var password = new PasswordHasher<ApplicationUser>();
        //         var hashed = password.HashPassword(user,"Testni123!");
        //         user.PasswordHash = hashed;
        //         context.Users.Add(user);
                
        //     }

        //     context.SaveChanges();
            

        //     var UserRoles = new IdentityUserRole<string>[]
        //     {
        //         new IdentityUserRole<string>{RoleId = roles[0].Id, UserId=user.Id},
        //         new IdentityUserRole<string>{RoleId = roles[1].Id, UserId=user.Id},
        //     };

        //     foreach (IdentityUserRole<string> r in UserRoles)
        //     {
        //         context.UserRoles.Add(r);
        //     }


        //     context.SaveChanges();
        }
    }
}