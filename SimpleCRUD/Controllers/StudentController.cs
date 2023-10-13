using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using SimpleCRUD.Models.Data;
using SimpleCRUD.Models.Domain;
using SimpleCRUD.Models.ViewModels;

namespace SimpleCRUD.Controllers
{
    public class StudentController : Controller
    {
        private readonly StudentDbContext _studentDbContext;
        public StudentController(StudentDbContext studentDbContext)
        {
            this._studentDbContext = studentDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _studentDbContext.Students.OrderBy(e => e.Roll).ToListAsync();
            return View(students);
        }


        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddStudentViewModel formData)
        {
            var studentObject = new Student()
            {
                Id = Guid.NewGuid(),
                Roll = formData.Roll,
                Name = formData.Name,
                PhoneNumber = formData.PhoneNumber,
                Email = formData.Email,
                DOB = formData.DOB,
                Department = formData.Department,
            };
            await _studentDbContext.Students.AddAsync(studentObject);
            await _studentDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var student = await _studentDbContext.Students.FirstOrDefaultAsync(x => x.Id == id);

            if(student != null)
            {
                var StudentViewModel = new UpdateStudentViewModel()
                {
                    Id = student.Id,
                    Roll = student.Roll,
                    Name = student.Name,
                    PhoneNumber = student.PhoneNumber,
                    Email = student.Email,
                    DOB = student.DOB,
                    Department = student.Department,
                };
                return View(StudentViewModel);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UpdateStudentViewModel model)
        {
            var student = await _studentDbContext.Students.FindAsync(model.Id);

            if(student != null)
            {
                student.Roll = model.Roll; 
                student.Name = model.Name;
                student.PhoneNumber = model.PhoneNumber;
                student.Email = model.Email;
                student.DOB = model.DOB;
                student.Department = model.Department;

                await _studentDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("Edit");
            }
        }

        [HttpGet]

        public async Task<IActionResult> Delete(UpdateStudentViewModel model)
        {
            var student = await _studentDbContext.Students.FindAsync(model.Id);

            if(student != null)
            {
                _studentDbContext.Students.Remove(student);
                await _studentDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
