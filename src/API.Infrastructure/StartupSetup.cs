using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Verdant.API.Infrastructure.Data;

namespace Verdant.API.Infrastructure;

public static class StartupSetup
{
  public static void AddDbContext(this IServiceCollection services, string connectionString) =>
      services.AddDbContext<AppDbContext>(options =>
          options.UseSqlite(connectionString)); // will be created in web project root
}
