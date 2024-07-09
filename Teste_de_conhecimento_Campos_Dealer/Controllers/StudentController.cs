using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using Teste_de_conhecimento_Campos_Dealer.Data;
using Teste_de_conhecimento_Campos_Dealer.Models;
using Teste_de_conhecimento_Campos_Dealer.Models.Entities;

namespace Teste_de_conhecimento_Campos_Dealer.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppBdContext bdContext;
        public StudentController(AppBdContext bdContext) 
        {
            this.bdContext = bdContext;
        }
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel viewModel)
        {
            var student = new Student
            {
                Name = viewModel.Name,
                Email = viewModel.Email,
                Phone = viewModel.Phone,
                Subscribed = viewModel.Subscribed
            };
            await bdContext.Students.AddAsync(student);
            await bdContext.SaveChangesAsync();
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> List()
        {
            var students = await bdContext.Students.ToListAsync();
            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await bdContext.Students.FindAsync(id);
            return View(student);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student viewModel)
        {
            var student = await bdContext.Students.FindAsync(viewModel.Id);
            if(student is not null)
            {
                student.Name = viewModel.Name;
                student.Email = viewModel.Email;
                student.Phone = viewModel.Phone;
                student.Subscribed = viewModel.Subscribed;
                await bdContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Student viewModel)
        {
            var student = await bdContext.Students.AsNoTracking().FirstOrDefaultAsync(x => x.Id == viewModel.Id);  
            
            if (student is not null)
            {
                bdContext.Students.Remove(viewModel);
                await bdContext.SaveChangesAsync();
            }
            return RedirectToAction("List", "Student");
        }
    }
}
