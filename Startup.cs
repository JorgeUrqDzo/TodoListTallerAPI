using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using todoListTutorial.Models;

namespace todoListTutorial {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            services.AddDbContext<ApplicationDbContext> (options => options.UseInMemoryDatabase ("todoDb"));
            services.AddMvc ()
                .SetCompatibilityVersion (CompatibilityVersion.Version_2_1)
                .AddJsonOptions (ConfigureJson);

        }

        private void ConfigureJson (MvcJsonOptions obj) {
            obj.SerializerSettings.ReferenceLoopHandling  = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env, ApplicationDbContext context) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                app.UseHsts ();
            }
            app.UseCors (builder =>
                builder.WithOrigins ("*")
                .AllowAnyHeader ()
            );

            app.UseHttpsRedirection ();
            app.UseMvc ();

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