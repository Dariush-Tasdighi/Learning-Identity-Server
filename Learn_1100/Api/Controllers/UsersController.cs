using System.Linq;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class UsersController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public UsersController
		(Models.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	#region Action: GetUsersAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Models.User>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> GetUsersAsync()
	{
		var result =
			await
			DatabaseContext.Users
			.ToListAsync()
			;

		return Ok(value: result);
	}
	#endregion /Action: GetUsersAsync()

	#region Action: CreateUserAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Models.User),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		CreateUserAsync(Models.User user)
	{
		await DatabaseContext
			.AddAsync(entity: user);

		await DatabaseContext
			.SaveChangesAsync();

		return CreatedAtAction
			(actionName: nameof(GetUserAsync).RemoveAsync(),
			routeValues: new { id = user.Id }, value: user);
	}
	#endregion /Action: CreateUserAsync()

	#region Action: GetUserAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Models.User),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> GetUserAsync(int id)
	{
		var user =
			await
			DatabaseContext.Users
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (user == null)
		{
			return NotFound();
		}

		return Ok(value: user);
	}
	#endregion /Action: GetUserAsync()

	#region Action: UpdateUserAsync
	[Microsoft.AspNetCore.Mvc.HttpPut(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		UpdateUserAsync(long id, Models.User user)
	{
		if (id != user.Id)
		{
			var errorMessage =
				$"The {nameof(id)} parameter is not valid!";

			return BadRequest(error: errorMessage);
		}

		var foundedItem =
			await
			DatabaseContext.Users
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (foundedItem == null)
		{
			return NotFound();
		}

		foundedItem.IsAdmin = user.IsAdmin;
		foundedItem.IsActive = user.IsActive;

		foundedItem.Username = user.Username;
		foundedItem.Password = user.Password;
		foundedItem.LastName = user.LastName;
		foundedItem.FirstName = user.FirstName;

		await DatabaseContext
			.SaveChangesAsync();

		return NoContent();
	}
	#endregion /Action: UpdateUserAsync

	#region Action: DeleteUserAsync
	[Microsoft.AspNetCore.Mvc.HttpDelete(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> DeleteUserAsync(long id)
	{
		var user =
			await
			DatabaseContext.Users
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (user == null)
		{
			return NotFound();
		}

		DatabaseContext.Remove(entity: user);

		await DatabaseContext
			.SaveChangesAsync();

		return NoContent();
	}
	#endregion /Action: DeleteUserAsync
}
