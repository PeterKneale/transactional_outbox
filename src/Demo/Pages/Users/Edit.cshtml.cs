using System.ComponentModel.DataAnnotations;
using Demo.Core.Application.Users.Commands;
using Demo.Core.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Demo.Pages.Users;

public class Edit : PageModel
{
    private readonly IMediator _mediator;

    public Edit(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<IActionResult> OnGetAsync(Guid id)
    {
        var user = await _mediator.Send(new GetUser.Query(id));
        Id = user.Id;
        FirstName = user.FirstName;
        LastName = user.LastName;
        return Page();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }

        await _mediator.Send(new UpdateUserName.Command(Id, FirstName, LastName));
        
        return RedirectToPage(nameof(Index));
    }

    [Required]
    [BindProperty]
    public Guid Id { get; set; }
    
    [Display(Name = "First Name")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string FirstName { get; set; }

    [Display(Name = "Last Name")]
    [Required]
    [BindProperty]
    [StringLength(50)]
    public string LastName { get; set; }
}