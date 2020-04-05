using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using APBD_tutorial_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial_3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

      
        [HttpGet]
        public IActionResult GetStudent(string orderBy)
        {

            var students = new List<Student>();
            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19103;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = @"Select s.FirstName, s.LastName, s.BirthDate, st.Name as Studies, e.Semester " +
                                          @"from Student s " +
                                          @"join Enrollment e on e.IdEnrollment = s.IdEnrollment " +
                                          @"join Studies st on st.IdStudy = e.IdStudy;";

                    connection.Open();
                    var response = command.ExecuteReader();
                    while (response.Read())
                    {
                        var st = new Student
                        {
                            FirstName = response["FirstName"].ToString(),
                            LastName = response["LastName"].ToString(),
                            Studies = response["Studies"].ToString(),
                            BirthDate = DateTime.Parse(response["BirthDate"].ToString()),
                            Semester = int.Parse(response["Semester"].ToString())
                        };

                        students.Add(st);
                    }



                }
            }

            return Ok(students);
        }

        [HttpGet]
        public IActionResult GetSemester(string idStudent)
        {

            var list = new List<Enrollment>();
            using (var connection = new SqlConnection(@"Data Source=db-mssql;Initial Catalog=s19103;Integrated Security=True"))
            {
                using (var command = new SqlCommand())
                {
                    command.Connection = connection;
                    command.CommandText = "SELECT e.IdEnrollment, e.IdStudy, e.Semester, e.StartDate FROM Enrollment e JOIN Student s ON e.IdEnrollment = s.IdEnrollment WHERE s.IndexNumber = @id;";
                    command.Parameters.AddWithValue("id", idStudent);

                    connection.Open();
                    var response = command.ExecuteReader();
                    while (response.Read())
                    {
                        var e = new Enrollment
                        {
                           IdEnrollment = int.Parse(response["IdEnrollment"].ToString()),
                           IdStudy = int.Parse(response["IdStudy"].ToString()),
                           Semester = int.Parse(response["Semester"].ToString()),
                           StartDate = DateTime.Parse(response["StartDate"].ToString())
                        };

                        list.Add(e);
                    }



                }
            }

            return Ok(list);
        }














    }
}