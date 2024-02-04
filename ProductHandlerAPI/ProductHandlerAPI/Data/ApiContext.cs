using Microsoft.EntityFrameworkCore;
using ProductHandlerAPI.Models;

namespace ProductHandlerAPI.Data
{
	public class ApiContext : DbContext
	{
		public ApiContext(DbContextOptions<ApiContext> options) : base(options)
		{

		}

		public DbSet<Product> Products { get; set; }
	}
}