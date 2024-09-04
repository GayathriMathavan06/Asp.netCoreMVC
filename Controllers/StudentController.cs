using Microsoft.AspNetCore.Mvc;
using MVCCoreDemo.Models;

namespace MVCCoreDemo.Controllers  
{  
    public class StudentController : Controller  
    {  
        StudentDataAccessLayer objstudent = new StudentDataAccessLayer();  
  
        public IActionResult Index()  
        {  
            List<Student> lststudent = new List<Student>();  
            lststudent = objstudent.GetAllStudent().ToList();  
  
            return View(lststudent);  
        }  
        // GET: Student/Create
public IActionResult Create()
{
    return View();
}

// POST: Student/Create
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Create(Student student)
{
    if (ModelState.IsValid)
    {
        objstudent.AddStudent(student); // Assuming AddStudent is a method to add a student
        return RedirectToAction(nameof(Index));
    }
    return View(student);
}

// GET: Student/Edit/5
public IActionResult Edit(int id)
{
    Student student = objstudent.GetStudentById(id); // Assuming GetStudentById retrieves a student by ID
    if (student == null)
    {
        return NotFound();
    }
    return View(student);
}

// POST: Student/Edit/5
[HttpPost]
[ValidateAntiForgeryToken]
public IActionResult Edit(int id, Student student)
{
    if (id != student.StudId)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        objstudent.UpdateStudent(student); // Assuming UpdateStudent updates a student's details
        return RedirectToAction(nameof(Index));
    }
    return View(student);
}


// GET: Student/Details/5
public IActionResult Details(int id)
{
    Student student = objstudent.GetStudentById(id); // Assuming GetStudentById retrieves a student by ID
    if (student == null)
    {
        return NotFound();
    }
    return View(student);
}

// GET: Student/Delete/5
public IActionResult Delete(int id)
{
    Student student = objstudent.GetStudentById(id); // Assuming GetStudentById retrieves a student by ID
    if (student == null)
    {
        return NotFound();
    }
    return View(student);
}

// POST: Student/Delete/5
[HttpPost, ActionName("Delete")]
[ValidateAntiForgeryToken]
public IActionResult DeleteConfirmed(int id)
{
    objstudent.DeleteStudent(id); // Assuming DeleteStudent removes a student by ID
    return RedirectToAction(nameof(Index));
}


     }  
}