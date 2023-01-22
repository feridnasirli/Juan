using Microsoft.EntityFrameworkCore;

namespace Juan.Models
{
	public class JuanContext:DbContext
	{
		public JuanContext(DbContextOptions options):base(options){}
		public DbSet<Slider> Sliders { get; set; }
		public DbSet<Shoe> Shoes { get; set; }

	}
}
