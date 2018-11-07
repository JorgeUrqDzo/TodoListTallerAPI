using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoListTutorial.Models;

namespace todoListTutorial.Controllers {
   [Route ("api/categorias/{categoriaId}/[controller]")]
   public class TodosController : Controller {
      private readonly ApplicationDbContext _context;
      public TodosController (ApplicationDbContext context) {
         _context = context;
      }

      // GET api/todos
      [HttpGet ("")]
      public ActionResult<IEnumerable<Todo>> Gets (int categoriaId) {
         var todos = _context.Todos.Where (t => t.CategoriaId == categoriaId).ToList ();
         return Ok (todos);
      }

      // GET api/todos/5
      [HttpGet ("{id}", Name = "getTodo")]
      public ActionResult<Todo> GetById (int id, int categoriaId) {
         var todo = _context.Todos.FirstOrDefault (t => t.CategoriaId == categoriaId && t.Id == id);

         if (todo == null) {
            return NotFound ();
         }

         return todo;
      }

      // POST api/todos
      [HttpPost ("")]
      public IActionResult Post ([FromBody] Todo newTodo, int categoriaId) {
         if (ModelState.IsValid) {

            newTodo.CategoriaId = categoriaId;
            _context.Todos.Add (newTodo);
            _context.SaveChanges ();
            return new CreatedAtRouteResult ("getTodo", new { id = newTodo.Id }, newTodo);
         }

         return BadRequest (ModelState);
      }

      // PUT api/todos/5
      [HttpPut ("{id}")]
      public IActionResult Put (int id, [FromBody] Todo updateTodo, int categoriaId) {

         if (id != updateTodo.Id) {
            return BadRequest ();
         }
         updateTodo.CategoriaId = categoriaId;
         _context.Entry (updateTodo).State = EntityState.Modified;
         _context.SaveChanges ();
         return Ok (updateTodo);
      }

      // DELETE api/todos/5
      [HttpDelete ("{id}")]
      public IActionResult DeleteById (int id, int categoriaId) {
         var todo = _context.Todos.FirstOrDefault (t => t.Id == id && t.CategoriaId == categoriaId);
         if (todo == null) {
            return NotFound ();
         }
         _context.Remove (todo);
         _context.SaveChanges ();
         return Ok (todo);
      }
   }
}