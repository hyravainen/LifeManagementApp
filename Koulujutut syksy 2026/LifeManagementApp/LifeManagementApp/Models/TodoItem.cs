namespace LifeManagementApp.Models;

public class TodoItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public int Priority { get; set; } = 3; // 1-5, 5 = tärkein
    public DateTime DueDate { get; set; } = DateTime.Today;
    public bool IsDone { get; set; }
}
