using System;

namespace DailyTaskBot;

public class EmployeeDailyTask
{
    public int Id { get; set; }
    public string? EmployeeName { get; set; }
    public string? YesterdaysTask { get; set; }
    public string? TodaysTask { get; set; }
    public string? Obstacle { get; set; }
    public DateTime CreatedDate { get; set; }
}
