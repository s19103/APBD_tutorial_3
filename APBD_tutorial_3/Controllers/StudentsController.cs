using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APBD_tutorial_3.DAL;
using APBD_tutorial_3.Models;
using Microsoft.AspNetCore.Mvc;

namespace APBD_tutorial_3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {

        private readonly IDbService _dbService;
        
        public StudentsController(IDbService dbService)
        {
            _dbService = dbService;
        }

        [HttpGet]
        public IActionResult GetStudents(string orderBy)
        {
            return Ok(_dbService.GetStudents());
        }









       

        [HttpPost]
        public IActionResult CreateStudent(Student student)
        {
            //... add to database
            //... generate index number
            student.IndexNumber = $"s{new Random().Next(1,20000)}";
            return Ok(student);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStudent(int id)
        {
            if (id < 10)
            {
                return Ok("Update complete");
            }

            return NotFound("Error");
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            if (id < 10)
            {
                return Ok("Delete Completed");
            }

            return NotFound("Error");
        }

    }
}