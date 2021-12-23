using BookShop.Models;
using Microsoft.EntityFrameworkCore;

namespace BookShop.DATA;
public class CategoryDB_Context:DbContext
{
  public DbSet<Category>categories{get;set;}
  public CategoryDB_Context(DbContextOptions options)
     :base(options){} 
}