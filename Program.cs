using TaskFlow.Data;
using TaskFlow.Models;

namespace TaskFlow;

internal static class Program
{
    public static void Main(string[] args)
    {
        
        var taskService = new TaskService();

        while (true)
        {
            Console.WriteLine("------- TASK FLOW -------");

            Console.WriteLine("Escolha a opção desejada:");
            Console.WriteLine("\t1 - Criar uma tarefa nova");
            Console.WriteLine("\t2 - Listar todas as tarefas");
            Console.WriteLine("\t3 - Concluir uma tarefa");
            Console.WriteLine("\t4 - Limpar todas as tarefas concluídas");
            Console.WriteLine("\t5 - Sair");

            var option = Console.ReadLine();
            
            switch (option)
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

                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        taskService.CheckTask(id);
                        Console.WriteLine("✅ Tarefa concluída com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("❌ ID inválido!");
                    }
                    break;
                
                case "4":
                    taskService.DeleteTask();
                    Console.WriteLine("✅ As tarefas concluídas foram deletadas");
                    break;
                
                case "5":
                    Console.WriteLine("🔚 Encerrando programa...");
                    return;
                
                default:
                    Console.WriteLine("❌ Opção inválida!");
                    break;
            }
            Console.WriteLine("\n Digite qualquer tecla para continuar...");
            Console.ReadKey();
        }
    }
}

public class TaskService
{
    public void AddTask(string? title, string? description, bool status)
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

    public void CheckTask(int id)
    {
        using (var context = new TaskFlowDataContext())
        {
            var task = context.TaskItems.FirstOrDefault(x => x.Id == id);
            task.Status = true;
            
            context.SaveChanges();
        }
    }

    public void DeleteTask()
    {
        using (var context = new TaskFlowDataContext())
        {
            var task = context.TaskItems.FirstOrDefault(x => x.Status == true);
            context.TaskItems.Remove(task);
            
            context.SaveChanges();
        }
    }
}