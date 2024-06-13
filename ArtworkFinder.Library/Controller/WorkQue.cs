using ArtworkFinder.Library.Exceptions;
using ArtworkFinder.Library.Models;
using System.Collections.Concurrent;

namespace ArtworkFinder.Library.Controllers;

public class WorkQue
{
    private BlockingCollection<Action> _taskQue = new BlockingCollection<Action>();
    public WorkQue()
    {
        // Start the task processor
        Task.Run(() => ProcessQueue());
    }

    public void EnqueueTask(Action task)
    {
        if (task == null) throw new ArgumentNullException(nameof(task));
        _taskQue.Add(task);
    }

    private void ProcessQueue()
    {
        foreach (var task in _taskQue.GetConsumingEnumerable())
        {
            try
            {
                task.Invoke();
            }
            catch (CustomerNotFoundException e)
            {
                Console.WriteLine(e.Message + " " + e.CustomerName);
            }
        }
    }
    public void Finish()
    {
        _taskQue.CompleteAdding();
    }
}
