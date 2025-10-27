using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcExercise.Data;
using MvcExercise.Models;

namespace MvcExercise.Controllers
{
    public class SimpleDataSetsController : Controller
    {
        private readonly MvcExerciseContext _context;

        public SimpleDataSetsController(MvcExerciseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> StatusValues()
        {
            return View(await _context.StatusValues.ToListAsync());
        }

        // GET: SimpleDataSets
        public async Task<IActionResult> Index(string searchString, string searchBy)
        {
            var simpleDataSets = from s in _context.SimpleDataSet select s;
            
            if(!String.IsNullOrEmpty(searchString))
            {
                simpleDataSets = simpleDataSets.Where(s =>
                (searchBy == "searchName" && s.Name!.Contains(searchString))
                || (searchBy == "searchRank" && !String.IsNullOrEmpty(s.Rank) && s.Rank.Contains(searchString))
                || (searchBy == "searchPosition" && !String.IsNullOrEmpty(s.Position) && s.Position.Contains(searchString))
                || (searchBy == "searchPosting" && !String.IsNullOrEmpty(s.Posting) && s.Posting.Contains(searchString))
                );
            }

            return View(await simpleDataSets.ToListAsync());
        }

        // GET: SimpleDataSets/Details/5
        public async Task<IActionResult> Copy(int? id)
        {
            if (id == null || _context.SimpleDataSet == null)
            {
                return NotFound();
            }

            var simpleDataSet = await _context.SimpleDataSet.FirstOrDefaultAsync(m => m.ID == id);
            if (simpleDataSet == null)
            {
                return NotFound();
            }

            return View(simpleDataSet);
        }

        // GET: SimpleDataSets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SimpleDataSets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,Rank,Position,Posting,UpdateTimeStamp")] SimpleDataSet simpleDataSet)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(simpleDataSet);
                    await _context.SaveChangesAsync();
                }
                // TODO: Find a better way to handle a key violation that doesn't require SQLClient?
                catch (DbUpdateException ex)
                  when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    ModelState.AddModelError("Name", "Name must be unique");
                    return View(simpleDataSet);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(simpleDataSet);
        }

        // GET: SimpleDataSets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.SimpleDataSet == null)
            {
                return NotFound();
            }

            var simpleDataSet = await _context.SimpleDataSet.FindAsync(id);
            if (simpleDataSet == null)
            {
                return NotFound();
            }
            return View(simpleDataSet);
        }

        // POST: SimpleDataSets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Rank,Position,Posting,UpdateTimeStamp")] SimpleDataSet simpleDataSet)
        {
            if (id != simpleDataSet.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    //TODO: EF Core appears to have issues applying SQL default on update https://github.com/dotnet/efcore/issues/3955
                    //For a production site, more investigation would be needed. Maybe an overridden SaveChanges() method...
                    simpleDataSet.UpdateTimeStamp = DateTime.Now;

                    _context.Update(simpleDataSet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SimpleDataSetExists(simpleDataSet.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                // TODO: Find a better way to handle a key violation that doesn't require SQLClient?
                catch(DbUpdateException ex)
                  when (ex.InnerException is SqlException sqlEx && (sqlEx.Number == 2601 || sqlEx.Number == 2627))
                {
                    ModelState.AddModelError("Name", "Name must be unique");
                    return View(simpleDataSet);
                }
                return RedirectToAction(nameof(Index));
            }
            return View(simpleDataSet);
        }

        // GET: SimpleDataSets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.SimpleDataSet == null)
            {
                return NotFound();
            }

            var simpleDataSet = await _context.SimpleDataSet
                .FirstOrDefaultAsync(m => m.ID == id);
            if (simpleDataSet == null)
            {
                return NotFound();
            }

            return View(simpleDataSet);
        }

        // POST: SimpleDataSets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.SimpleDataSet == null)
            {
                return Problem("Entity set 'MvcExerciseContext.SimpleDataSet'  is null.");
            }
            var simpleDataSet = await _context.SimpleDataSet.FindAsync(id);
            if (simpleDataSet != null)
            {
                _context.SimpleDataSet.Remove(simpleDataSet);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SimpleDataSetExists(int id)
        {
          return (_context.SimpleDataSet?.Any(e => e.ID == id)).GetValueOrDefault();
        }

    }
}
