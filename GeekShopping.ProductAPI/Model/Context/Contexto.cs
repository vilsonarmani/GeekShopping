using Microsoft.EntityFrameworkCore;
namespace GeekShopping.ProductAPI.Model.Context;
public class Contexto : DbContext
{
    public Contexto() { }
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }
    public DbSet<Product>? Products { get; set; }
}

