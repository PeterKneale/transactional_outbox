using Demo.Core.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Users;

public class Index : PageModel
{
    private readonly IMediator _mediator;

    public Index(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task OnGetAsync()
    {
        Data = await _mediator.Send(new ListUsers.Query());
    }

    public ListUsers.Result Data { get; private set; }
}