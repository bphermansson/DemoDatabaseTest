using Microsoft.EntityFrameworkCore;
using StudentWebApi.Controllers;
using StudentWebApi.Data;
using StudentWebApi.Model;

namespace StudentWebApi.Test
{
	public class UnitTest1:IDisposable
	{
		private static DbContextOptions<StudentWebApiContext> dbContextOptions = new DbContextOptionsBuilder<StudentWebApiContext>()
			.UseInMemoryDatabase(databaseName:"StudentDb")
			.Options;

		StudentWebApiContext context;
        public UnitTest1()
        {
            context = new StudentWebApiContext(dbContextOptions);
			context.Database.EnsureCreated();
			SeedDatabase();
        }

		private void SeedDatabase()
		{
			List<Student> students = new List<Student>()
			{ 
				new Student()
				{
					Id=1,
					Name="Nils"
				},
				new Student()
				{
					Id=2,
					Name="Pelle"
				}
			};
			context.Student.AddRange(students);
			context.SaveChanges();
		}

		public void Dispose()
		{
			context.Database.EnsureDeleted();
		}

		[Fact]
		public void Test1()
		{
			Student student;
			student = context.Student.Find(1);
			Assert.Equal("Nils", student.Name);
		}
	}
}