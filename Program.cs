using TaskFlow.Data;
using TaskFlow.Models;

class Program
{
    public static void Main(string[] args)
    {
        var taskService = new TaskService();

        Console.WriteLine("------- TASK FLOW -------");

        Console.WriteLine("Escolha a opção desejada:");
        Console.WriteLine("\t1 - Criar uma tarefa nova");
        Console.WriteLine("\t2 - Listar todas as tarefas");
        Console.WriteLine("\t3 - Concluir uma tarefa");

        switch (Console.ReadLine())
        {
            case "1":
                Console.WriteLine("Digite o título da tarefa:");
                var title = Console.ReadLine();
        
                Console.WriteLine("Digite uma descrição da tarefa:");
                var description = Console.ReadLine();
        
                taskService.AddTask(title, description, false);
                Console.WriteLine("✅ Tarefa adicionada com sucesso!");
                break;
            
            case "2":
                taskService.ListTasks();
                break;
            
            case "3":
                Console.WriteLine("Digite o número do ID da tarefa:");
                var id = Convert.ToInt32(Console.ReadLine());
                
                taskService.CheckTask(id);
                Console.WriteLine("✅ Tarefa concluída com sucesso!");
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

            context.TaskItems.Add(task);
            context.SaveChanges();
        }
    }

    public void ListTasks()
    {
        using (var context = new TaskFlowDataContext())
        {
            var tasks = context.TaskItems.ToList();

            Console.WriteLine("📋 Lista de Tarefas:");
            foreach (var task in tasks)
            {
                Console.WriteLine($"📌 {task.Id}: {task.Title} - {(task.Status ? "✅ Concluído" : "⏳ Pendente")}");
            }
        }
    }

    public void CheckTask(int Id)
    {
        using (var context = new TaskFlowDataContext())
        {
            var task = context.TaskItems.FirstOrDefault(x => x.Id == Id);
            task.Status = true;
            
            context.SaveChanges();
        }
    }
}