using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Database_First_Approach_Entity_Framework.Models;

namespace Database_First_Approach_Entity_Framework.Controllers
{
    public class EmployeeDetailsController : Controller
    {
        private readonly DotNetTrainingContext _context;

        public EmployeeDetailsController(DotNetTrainingContext context)
        {
            _context = context;
        }

        // GET: EmployeeDetails
        public async Task<IActionResult> Index()
        {
              return _context.EmployeeDetails != null ? 
                          View(await _context.EmployeeDetails.ToListAsync()) :
                          Problem("Entity set 'DotNetTrainingContext.EmployeeDetails'  is null.");
        }

        // GET: EmployeeDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeDetail = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmployeeDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmpId,Name,Address,Age,Salary,WorkType")] EmployeeDetail employeeDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(employeeDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail == null)
            {
                return NotFound();
            }
            return View(employeeDetail);
        }

        // POST: EmployeeDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("EmpId,Name,Address,Age,Salary,WorkType")] EmployeeDetail employeeDetail)
        {
            if (id != employeeDetail.EmpId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employeeDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeDetailExists(employeeDetail.EmpId))
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
            return View(employeeDetail);
        }

        // GET: EmployeeDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.EmployeeDetails == null)
            {
                return NotFound();
            }

            var employeeDetail = await _context.EmployeeDetails
                .FirstOrDefaultAsync(m => m.EmpId == id);
            if (employeeDetail == null)
            {
                return NotFound();
            }

            return View(employeeDetail);
        }

        // POST: EmployeeDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.EmployeeDetails == null)
            {
                return Problem("Entity set 'DotNetTrainingContext.EmployeeDetails'  is null.");
            }
            var employeeDetail = await _context.EmployeeDetails.FindAsync(id);
            if (employeeDetail != null)
            {
                _context.EmployeeDetails.Remove(employeeDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeDetailExists(int id)
        {
          return (_context.EmployeeDetails?.Any(e => e.EmpId == id)).GetValueOrDefault();
        }
    }
}
