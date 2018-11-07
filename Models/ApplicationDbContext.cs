using Microsoft.EntityFrameworkCore;

namespace todoListTutorial.Models {
   public class ApplicationDbContext : DbContext {
      public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) { }

      public DbSet<Categoria> Categorias { get; set; }
      public DbSet<Todo> Todos { get; set; }
   }
}