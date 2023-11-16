using AutoMapper;
using Broker.Data;
using Broker.Dtos;
using Broker.Models;
using Broker.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;


namespace Broker
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public class MappingProfile : Profile
        {
            public MappingProfile()
            {
                CreateMap<Banco, BancoDto>()
                    .ForMember(dto => dto.estado, opt => opt.MapFrom(src => src.estado.descripcion))
                    .ForMember(dto => dto.cuenta, opt => opt.MapFrom(src => src.cuenta.numero));

                CreateMap<BancoDto, Banco>()
                    .ForMember(dest => dest.estado, opt => opt.Ignore()) // Ignora la propiedad de navegación para evitar problemas de seguimiento de Entity Framework
                    .ForMember(dest => dest.cuenta, opt => opt.Ignore());
                CreateMap<BancoDtoAgregar, Banco>()
                    .ForMember(dest => dest.estado, opt => opt.Ignore()) // Ignora la propiedad de navegación para evitar problemas de seguimiento de Entity Framework
                    .ForMember(dest => dest.cuenta, opt => opt.Ignore());
            }
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContext<ApiDb>(options => options.UseNpgsql(Configuration.GetConnectionString("PostgreSQLConnection")));

            services.AddScoped<BancoService>();
            services.AddScoped<TransaccionService>();
            services.AddScoped<CuentaService>();

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                policy => policy
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());

            });

            services.AddControllers();
            // Configuración de AutoMapper
            services.AddAutoMapper(typeof(Startup));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Broker", Version = "v1" });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Broker v1"));
            }  // interfaz grafica de swagger: https://localhost:5001/swagger/index.html

            app.UseCors("AllowAllOrigins");

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
