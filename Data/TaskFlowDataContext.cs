using Microsoft.EntityFrameworkCore;
using TaskFlow.Models;

namespace TaskFlow.Data;
public class TaskFlowDataContext : DbContext
{
    public DbSet<TaskItem> TaskItems { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        options.UseSqlServer("Server=localhost,1433;Database=TaskFlow;User ID=sa;Password=1q2w3e4r@#$;Encrypt=False");
    }
}