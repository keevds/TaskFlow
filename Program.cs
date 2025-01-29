using TaskFlow.Data;
using TaskFlow.Models;

class Program
{
    public static void Main(string[] args)
    {
        var taskService = new TaskService(); // Criar uma única instância do serviço

        Console.WriteLine("------- TASK FLOW -------");

        Console.WriteLine("Escolha a opção desejada:");
        Console.WriteLine("\t1 - Criar uma tarefa nova");
        Console.WriteLine("\t2 - Listar todas as tarefas");

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Digite o título da tarefa:");
                var title = Console.ReadLine(); // Convert.ToString() é desnecessário
        
                Console.WriteLine("Digite uma descrição da tarefa:");
                var description = Console.ReadLine();
        
                taskService.AddTask(title, description, false);
                Console.WriteLine("✅ Tarefa adicionada com sucesso!");
                break;
            
            case "2":
                taskService.ListTasks(); // Correção da chamada da função
                break;

            default:
                Console.WriteLine("❌ Opção inválida!");
                break;
        }
    }
}

public class TaskService
{
    public void AddTask(string title, string description, bool status)
    {
        using (var context = new TaskFlowDataContext())
        {
            var task = new TaskItem
            {
                Title = title,
                Description = description,
                Status = status,
                Date = DateTime.Now
            };

            context.TaskItems.Add(task); // Certifique-se de que DbSet está correto
            context.SaveChanges();
        }
    }

    public void ListTasks()
    {
        using (var context = new TaskFlowDataContext())
        {
            var tasks = context.TaskItems.ToList(); // Ajustado para corresponder ao DbSet

            Console.WriteLine("📋 Lista de Tarefas:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"📌 {task.Id}: {task.Title} - {(task.Status ? "✅ Concluído" : "⏳ Pendente")}");
            }
        }
    }
}