using Newtonsoft.Json;

namespace todoListTutorial.Models {
   public class Todo {
      public int Id { get; set; }
      public int CategoriaId { get; set; }
      public string Nombre { get; set; }
      public string Descripcion { get; set; }
      public bool Hecho { get; set; }

      [JsonIgnore]
      public Categoria Categoria { get; set; }
   }
}