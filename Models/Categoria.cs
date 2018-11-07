using System.Collections.Generic;

namespace todoListTutorial.Models {
   public class Categoria {
      public Categoria()
      {
          Todos = new List<Todo>();
      }
      public int Id { get; set; }
      public string Nombre { get; set; }
      public ICollection<Todo> Todos { get; set; }
   }
}