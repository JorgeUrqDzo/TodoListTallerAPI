using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoListTutorial.Models;

namespace todoListTutorial.Controllers {
   [Route ("api/[controller]")]
   public class CategoriasController : Controller {
      private readonly ApplicationDbContext _context;

      public CategoriasController (ApplicationDbContext context) {
         _context = context;
      }

      // GET api/categorias
      [HttpGet ("")]
      public ActionResult<IEnumerable<Categoria>> Gets () {
         return _context.Categorias.ToList ();
      }

      // GET api/categorias/5
      [HttpGet ("{id}", Name = "categoriaCreada")]
      public ActionResult<Categoria> GetById (int id) {
         var categoria = _context.Categorias.Include(c => c.Todos).FirstOrDefault (c => c.Id == id);

         if (categoria == null) {
            return NotFound ();
         }

         return Ok (categoria);
      }

      // POST api/categorias
      [HttpPost ("")]
      public IActionResult Post ([FromBody] Categoria newCategoria) {
         if (ModelState.IsValid) {
            _context.Categorias.Add (newCategoria);
            _context.SaveChanges ();
            return new CreatedAtRouteResult ("categoriaCreada", new { Id = newCategoria.Id }, newCategoria);
         }

         return BadRequest (ModelState);
      }

      // PUT api/categorias/5
      [HttpPut ("{id}")]
      public IActionResult Put (int id, [FromBody] Categoria updateCategoria) {
         if(id != updateCategoria.Id){
            return BadRequest();
         }
         _context.Entry(updateCategoria).State = EntityState.Modified;
         _context.SaveChanges();
         return Ok(updateCategoria);
      }

      // DELETE api/categorias/5
      [HttpDelete ("{id}")]
      public IActionResult DeleteById (int id) { 
         var categoriaEncontrado = _context.Categorias.FirstOrDefault( c => c.Id == id);
         if(categoriaEncontrado == null ){
            return NotFound();
         }
         _context.Categorias.Remove(categoriaEncontrado);
         _context.SaveChanges();
         return Ok(categoriaEncontrado);
      }
   }
}