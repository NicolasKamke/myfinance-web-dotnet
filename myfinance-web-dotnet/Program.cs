using Microsoft.OpenApi.Models;
using myfinance_web_dotnet.Infrastructure;

namespace myfinance_web_dotnet
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<MyFinanceDbContext>();

            // Adiciona o Swagger ao serviço
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "API de Exemplo",
                    Version = "v1",
                    Description = "Documentação da API com Swagger",
                    Contact = new OpenApiContact
                    {
                        Name = "Nicolas",
                        Email = "email@exemplo.com",
                        Url = new Uri("https://github.com/seu-repositorio")
                    }
                });
            });

            // Configurar CORS para bloquear todas as origens
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowLocalhost", policy =>
                {
                    // Permite apenas a origem http://localhost:5173
                    policy.WithOrigins("http://localhost:5173")
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API de Exemplo v1");
                    c.RoutePrefix = string.Empty; // Acesso na raiz do servidor
                });

                app.UseCors("AllowLocalhost");
            }

            app.MapControllers();

            app.Run();
        }
    }
}
