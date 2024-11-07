using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CEPDI.TECHTEST.Api.DATA;
using CEPDI.TECHTEST.MODELS;
using System.Net;

namespace CEPDI.TECHTEST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class usuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public usuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/usuarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<usuarios>>> Getusuarios()
        {
            return await _context.usuarios.ToListAsync();
        }

        // GET: api/usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<usuarios>> Getusuarios(int id)
        {
            var usuarios = await _context.usuarios.FindAsync(id);

            if (usuarios == null)
            {
                return NotFound();
            }

            return usuarios;
        }

        // PUT: api/usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Putusuarios(int id, usuarios usuarios)
        {
            //Validación de contraseña: 
            //un carácter especial, mayúsculas, minúsculas, números y una longitud de 8 caracteres como mínimo.
            string sPassword = usuarios.password;
            bool contains = sPassword.Any(char.IsLetterOrDigit)
                            && sPassword.Any(char.IsUpper)
                            && sPassword.Any(char.IsLower)
                            && sPassword.Any(char.IsDigit)
                            && sPassword.Length >= 8;

            if (contains)
            {
                if (id != usuarios.idUsuario)
                {
                    return BadRequest();
                }

                _context.Entry(usuarios).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!usuariosExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            else
            {
                return NotFound("Contraseña invalida");
            }

            return NoContent();
        }

        // POST: api/usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<usuarios>> Postusuarios(usuarios usuarios)
        {
            //Validación de contraseña: 
            //un carácter especial, mayúsculas, minúsculas, números y una longitud de 8 caracteres como mínimo.
            string sPassword = usuarios.password;
            bool contains = sPassword.Any(char.IsLetterOrDigit)                            
                            && sPassword.Any(char.IsUpper)
                            && sPassword.Any(char.IsLower) 
                            && sPassword.Any(char.IsDigit)
                            && sPassword.Length>=8;

            if (contains)
            {
                _context.usuarios.Add(usuarios);
                await _context.SaveChangesAsync();
                return CreatedAtAction("Getusuarios", new { id = usuarios.idUsuario }, usuarios);
            }
            else {
                return NotFound("Contraseña invalida: La contraseña debe contener un carácter especial, mayúsculas, minúsculas, números y una longitud de 8 caracteres como mínimo.");
            }
            
        }

        // DELETE: api/usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deleteusuarios(int id)
        {
            usuarios usuario = _context.usuarios.FirstOrDefault(q=>q.idUsuario==id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool usuariosExists(int id)
        {
            return _context.usuarios.Any(e => e.idUsuario == id);
        }
    }
}
