﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ejercicio.Web.Data;
using Ejercicio.Web.Models;

namespace Ejercicio.Web.Controllers
{
    public class BarriosController : Controller
    {
        private readonly EjercicioDbContext _context;

        public BarriosController(EjercicioDbContext context)
        {
            _context = context;
        }

        // GET: Barrios
        public async Task<IActionResult> Index()
        {
            return View(await _context.Barrios.ToListAsync());
        }

        // GET: Barrios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }

            return View(barrio);
        }

        // GET: Barrios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Barrios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Barrio barrio)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Add(barrio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(barrio);
        }

        // GET: Barrios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var barrio = await _context.Barrios.FindAsync(id);
            if (barrio == null)
            {
                return NotFound();
            }
            return View(barrio);
        }

        // POST: Barrios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Barrio barrio)
        {
            if (id != barrio.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(barrio);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbUpdateException)
                {
                    if (dbUpdateException.InnerException.Message.Contains("duplicate"))
                    {
                        ModelState.AddModelError(string.Empty, "hay un registro con el mismo nombre.");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty,
                       dbUpdateException.InnerException.Message);
                    }
                }
                catch (Exception exception)
                {
                    ModelState.AddModelError(string.Empty, exception.Message);
                }
            }
            return View(barrio);
        }

        // GET: Barrios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Barrio barrio = await _context.Barrios
            .FirstOrDefaultAsync(m => m.Id == id);
            if (barrio == null)
            {
                return NotFound();
            }
            _context.Barrios.Remove(barrio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BarrioExists(int id)
        {
            return _context.Barrios.Any(e => e.Id == id);
        }
    }
}
