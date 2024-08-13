using SocialMediaApp.Application.Common.Models;

namespace SocialMediaApp.Application.TodoLists.Queries.GetTodos;
public class TodosVm
{
    public IReadOnlyCollection<LookupDto> PriorityLevels { get; init; } = Array.Empty<LookupDto>();

    public IReadOnlyCollection<TodoListDto> Lists { get; init; } = Array.Empty<TodoListDto>();
}
