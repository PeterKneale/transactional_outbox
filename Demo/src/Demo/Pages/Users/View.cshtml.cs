using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Users;

public class View : PageModel
{
    private readonly IMediator _mediator;

    public View(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var user = await _mediator.Send(new GetUser.Query(id));
        Id = id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        return Page();
    }
    
    public Guid Id { get; set; }
    
    [Display(Name = "First Name")]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    public string LastName { get; set; }
}