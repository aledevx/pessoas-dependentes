using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using pessoa_dependentes_serverside.Data;
using pessoa_dependentes_serverside.Models;

namespace pessoa_dependentes_serverside.Controllers
{
    public class DependentesController : Controller
    {
        private readonly Context _context;

        public DependentesController(Context context)
        {
            _context = context;
        }

        // GET: Dependentes
        public async Task<IActionResult> ListaDependentes(Guid? id)
        {
            var dependente = await _context.Dependentes.Where(d => d.PessoaId == id).ToListAsync();
            return Json(dependente);
        }

        // GET: Dependentes/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dependente = await _context.Dependentes
                .Include(d => d.Pessoa)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dependente == null)
            {
                return NotFound();
            }

            return View(dependente);
        }

        // GET: Dependentes/Create


        // POST: Dependentes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Dependente dependente)
        {
            if (ModelState.IsValid)
            {
                dependente.Id = Guid.NewGuid();
                _context.Add(dependente);
                await _context.SaveChangesAsync();
                return Ok();
            }
            return Json(dependente);
        }

        // GET: Dependentes/Edit/5
        // public async Task<IActionResult> Edit(Guid? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var dependente = await _context.Dependentes.FindAsync(id);
        //     if (dependente == null)
        //     {
        //         return NotFound();
        //     }
        //     ViewData["PessoaId"] = new SelectList(_context.Pessoas, "Id", "Nome", dependente.PessoaId);
        //     return View(dependente);
        // }

        // POST: Dependentes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var dependente = await _context.Dependentes.FindAsync(id);
            if (id != dependente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dependente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DependenteExists(dependente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(dependente);
            }
            return Json(dependente);
        }

        // POST: Dependentes/Delete/5
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            var dependente = await _context.Dependentes.FindAsync(id);
            _context.Dependentes.Remove(dependente);
            await _context.SaveChangesAsync();
            return Ok();
        }

        private bool DependenteExists(Guid id)
        {
            return _context.Dependentes.Any(e => e.Id == id);
        }

        public async Task<IActionResult> EditGet(Guid? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var dependente = await _context.Dependentes.FindAsync(id);

            if(dependente == null)
            {
                return NotFound();
            }

            return Json(dependente);
        }
    }
}
