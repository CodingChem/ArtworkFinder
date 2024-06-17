using System.Collections.Concurrent;

namespace ArtworkFinder.Library.Controllers;

internal class WorkQue
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
            // Can this trow an exception?
            task.Invoke();
        }
    }
    public void Finish()
    {
        _taskQue.CompleteAdding();
    }
}
