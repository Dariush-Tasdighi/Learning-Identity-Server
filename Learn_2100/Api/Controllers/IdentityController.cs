using System.Linq;

namespace Api.Controllers;

[Microsoft.AspNetCore.Authorization.Authorize]
[Microsoft.AspNetCore.Mvc.Route(template: "identity")]
public class IdentityController : Infrastructure.ControllerBase
{
	public IdentityController() : base()
	{
	}

	[Microsoft.AspNetCore.Mvc.HttpGet]
	public Microsoft.AspNetCore.Mvc.IActionResult Get()
	{
		if (User == null)
		{
			return Problem();
		}

		if (User.Claims == null)
		{
			return Problem();
		}

		var result =
			// User.Claims -> using System.Linq;
			from claim in User.Claims
			select new { claim.Type, claim.Value }
			;

		return new Microsoft.AspNetCore.Mvc.JsonResult(value: result);
	}
}
