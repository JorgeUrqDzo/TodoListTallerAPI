using System.Collections.Generic;
using System.Linq;
using todoListTutorial.Models;

namespace todoListTutorial {
   public static class DataSeed {

      public static void Initialize (ApplicationDbContext context) {
         // CreateTodos(context);
         // CreateCategories(context);
         CreateCategoriesAndTodos (context);
      }

      private static void CreateTodos (ApplicationDbContext context) {
         if (!context.Todos.Any ()) {
            context.AddRange (new List<Todo> () {
               new Todo () {
                     Nombre = "Hacer tarea de Mate",
                        Descripcion = "Resolver problemas pag. 34",
                        Hecho = false
                  },
                  new Todo () {
                     Nombre = "Comenzar proyecto Programacion",
                        Descripcion = "Crear repositorio en github",
                        Hecho = false
                  }

            });
            context.SaveChanges ();
         }
      }

      private static void CreateCategories (ApplicationDbContext context) {
         if (!context.Categorias.Any ()) {
            context.Categorias.AddRange (new List<Categoria> () {
               new Categoria () {
                     Nombre = "Hogar"
                  },
                  new Categoria () {
                     Nombre = "Escuela"
                  }
            });

            context.SaveChanges ();
         }
      }

      private static void CreateCategoriesAndTodos (ApplicationDbContext context) {
         if (!context.Categorias.Any ()) {
            context.Categorias.AddRange (new List<Categoria> () {
               new Categoria () {
                     Nombre = "Escuela", Todos = new List<Todo> () {
                        new Todo () {
                              Nombre = "Hacer tarea de Mate",
                                 Descripcion = "Resolver problemas pag. 34",
                                 Hecho = false
                           },
                           new Todo () {
                              Nombre = "Comenzar proyecto Programacion",
                                 Descripcion = "Crear repositorio en github",
                                 Hecho = false
                           }
                     }
                  },
                  new Categoria () {
                     Nombre = "Personal", Todos = new List<Todo> () {
                        new Todo () {
                           Nombre = "Ir al GYM",
                              Descripcion = "Hacer cardio 30 min",
                              Hecho = false
                        }
                     }
                  },
                  new Categoria () {
                     Nombre = "Trabajo"
                  },
            });

            context.SaveChanges ();
         }
      }
   }
}