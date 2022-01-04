using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Core.CustomEntities;
using SocialMedia.Core.Interfaces;
using SocialMedia.Core.Services;
using SocialMedia.Infraestructure.Data;
using SocialMedia.Infraestructure.Filters;
using SocialMedia.Infraestructure.Interfaces;
using SocialMedia.Infraestructure.Repositories;
using SocialMedia.Infraestructure.Services;
using System;

namespace SocialMedia.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());   //busca en toda la solucion, cuales son los profiles que se estan haciendo para registrarlos

            services.AddControllers(options =>
            {
                options.Filters.Add<GlobalExceptionFilter>();
            }).AddNewtonsoftJson(options =>
            { 
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
                options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;            //especifica que si tenemos un valor null, lo ignore y no lo muestre
            }).ConfigureApiBehaviorOptions(options => {       //con esto des habilitamos la decoracion [ApiControler]
            
            });

            services.Configure<PaginationOptions>(Configuration.GetSection("Pagination"));       //este string "Pagination debe coinsidir con el colocado en el appsettings (en .Configure tambien aplica un .Singleton que crea un solo objeto para todo el programa)
            
            services.AddDbContext<SocialMediaContext>(Options =>
                Options.UseSqlServer(Configuration.GetConnectionString("SocialMedia"))     //usaremos sql server y la cadena de conexion para acceder esta en el GetConectionString("SocialMedia")
            );
            
            services.AddTransient<IPostService, PostService>();
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddTransient<IUnitOfWork, UnitOfWork>();           //Trasient crea una instncia nueva cada vez que se hace un request
            services.AddSingleton<IUriService>(provider =>              //Singleton indica que se creará una unica instancia para toda la aplicacion, no es necesario crear instancias cada vez que se haga un request
            {
                var accesor = provider.GetRequiredService<IHttpContextAccessor>();      //obtener acceso al contexto http que esta generando la app
                var request = accesor.HttpContext.Request;          //obtener el request del cliente
                var absoluteUri = string.Concat(request.Scheme, "://", request.Host.ToUriComponent());       //obtener la url del request hecho, (dentro del Concat() va la url estructurada), request.Schema devuelve el estandar por el que el usuario hizo el request, ya se Http o Https
                return new UriService(absoluteUri);         //retorna nueva instancia de UriService
            });     

            services.AddSwaggerGen(doc =>
            {
                doc.SwaggerDoc("v1.0", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Social MEdia API", Version = "V1.0" });
            });

            //Acregamos el ValidationFilter al midelWork para que las ejecuciones pasen x este filtro
            services.AddMvc(options =>              //a�adimos compativilidad con MVC
            {
                options.Filters.Add<ValidationFilter>();
            }).AddFluentValidation(options =>
            {
                options.RegisterValidatorsFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1.0/swagger.json", "Social Media API v1.0");          //colocamos url para que nuestro archivo swagger se vea con interfaz grafica, despues se coloca un nombre en este caso (Social Media API)
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
