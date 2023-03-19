namespace Infrastructure;

[Microsoft.AspNetCore.Mvc.ApiController]
[Microsoft.AspNetCore.Mvc.Route
	(template: Constant.DefaultRouteTemplate)]
public class ControllerBase :
	Microsoft.AspNetCore.Mvc.ControllerBase
{
	#region Constructor
	public ControllerBase() : base()
	{
	}
	#endregion /Constructor
}
