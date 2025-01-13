using LinkApp.Data;
using LinkApp.Data.Abstractions;
using LinkApp.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LinkApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<LinkAppDbContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString(nameof(LinkAppDbContext))));

            builder.Services.AddScoped<ILinksRepository, LinksRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader()
            );

            app.Run();
        }
    }
}
