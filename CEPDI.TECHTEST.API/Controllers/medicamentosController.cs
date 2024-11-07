using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CEPDI.TECHTEST.Api.DATA;
using CEPDI.TECHTEST.MODELS;

namespace CEPDI.TECHTEST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class medicamentosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public medicamentosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/medicamentos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<medicamentos>>> Getmedicamentos()
        {
            //Se usa include para obtener el objeto formasfarmaceuticas por la Foreign key
            return await _context.medicamentos.Include("formasfarmaceuticas").ToListAsync();
        }

        // GET: api/medicamentos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<medicamentos>> Getmedicamentos(int id)
        {
            var medicamentos = _context.medicamentos.Include("formasfarmaceuticas").FirstOrDefault(q=>q.idmedicamento==id);

            if (medicamentos == null)
            {
                return NotFound();
            }

            return medicamentos;
        }

        // PUT: api/medicamentos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putmedicamentos(int id, medicamentos medicamentos)
        {
            if (id != medicamentos.idmedicamento)
            {
                return BadRequest();
            }

            _context.Entry(medicamentos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!medicamentosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/medicamentos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<medicamentos>> Postmedicamentos(medicamentos medicamentos)
        {
            _context.medicamentos.Add(medicamentos);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getmedicamentos", new { id = medicamentos.idmedicamento }, medicamentos);
        }

        // DELETE: api/medicamentos/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletemedicamentos(int id)
        {
            medicamentos medicamento = _context.medicamentos.FirstOrDefault(q => q.idmedicamento == id);
            if (medicamento == null)
            {
                return NotFound();
            }

            _context.medicamentos.Remove(medicamento);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool medicamentosExists(int id)
        {
            return _context.medicamentos.Any(e => e.idmedicamento == id);
        }
    }
}
