using AppSchoolsEF.Data;
using AppSchoolsEF.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppSchoolsEF.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolsContext _context;

        public StudentsController(SchoolsContext context)
        {
            _context = context;        
        }

        // GET: StudentsController
        public async Task<IActionResult> Index(string searchString)
        {
            var students = from m in _context.Students select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Studentname!.Contains(searchString));
            }
            return View(await students.ToListAsync());
        }

        // GET: StudentsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }




        // GET: StudentsController/Create
        public ActionResult Create()
        {
            return View();
        }





        // POST: StudentsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        // GET: StudentsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            if (id == null || _context.Students == null)
            {
                return NotFound();
            }

            var student = await _context.Students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return View(student);
        }

        // POST: StudentsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Student student)
        {
            if (id != student.StudentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.StudentId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(student);
        }

        private bool StudentExists(int studentId)
        {
            return _context.Students.Any(e => e.StudentId == studentId);
        }

        // GET: StudentsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FirstOrDefaultAsync(m => m.StudentId == id);
            return View(student);
        }

        // POST: StudentsController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
