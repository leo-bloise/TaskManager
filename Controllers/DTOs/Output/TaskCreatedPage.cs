using TaskManager.Models.Entities;

namespace TaskManager.Controllers.DTOs.Output;

using System.Collections.Generic;

public class TaskCreatedPage : Page<TaskCreatedResponse>
{
    public TaskCreatedPage(List<TaskCreatedResponse> data, int page, int size, int totalItems, int totalPage) : base(data, page, size, totalItems, totalPage)
    {
    }
    public static Page<TaskCreatedResponse> Adapt(Page<Models.Entities.Task> taskPage)
    {
        var data = taskPage.Data;
        var dataAdapted = data.Select((p) => TaskCreatedResponse.Adapt(p)).ToList();
        return new TaskCreatedPage(dataAdapted, taskPage.PageNumber, taskPage.PageSize, taskPage.TotalItems, taskPage.TotalPages);
    }
}