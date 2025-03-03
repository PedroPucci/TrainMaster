using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;
using TrainMaster.Application.UnitOfWork;
using TrainMaster.Extensions.SwaggerDocumentation;
using TrainMaster.Infrastracture.Connections;
using TrainMaster.Infrastracture.Repository.RepositoryUoW;
using TrainMaster.Infrastracture.Security.Cryptography;
using TrainMaster.Infrastracture.Security.Token.Access;

namespace TrainMaster.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                opt.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Api TrainMaster",
                    Description = @"
                        - Sistema TrainMaster, uma plataforma de gerenciamento de cursos online para empresas.
                        ",
                });

                opt.OperationFilter<CustomOperationDescriptions>();
            });

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseNpgsql(config.GetConnectionString("WebApiDatabase"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("CorsPolicy", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200");
                });
            });

            services.AddScoped<IRepositoryUoW, RepositoryUoW>();
            services.AddScoped<TokenService>();
            services.AddScoped<BCryptoAlgorithm>();
            services.AddScoped<IUnitOfWorkService, UnitOfWorkService>();

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            return services;
        }
    }
}
