using System.Linq;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

/// <summary>
/// Step (1)
/// </summary>
[Microsoft.AspNetCore.Authorization.Authorize]
public class CustomersController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public CustomersController
		(Models.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	#region Action: GetCustomersAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet]

	/// <summary>
	/// Step (1)
	/// </summary>
	[Microsoft.AspNetCore.Authorization.AllowAnonymous]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(System.Collections.Generic.IEnumerable<Models.Customer>),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> GetCustomersAsync()
	{
		var result =
			await
			DatabaseContext.Customers
			.ToListAsync()
			;

		return Ok(value: result);
	}
	#endregion /Action: GetCustomersAsync()

	#region Action: CreateCustomerAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Models.Customer),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	/// <summary>
	/// Step (1)
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		CreateCustomerAsync(Models.Customer customer)
	{
		await DatabaseContext.AddAsync(entity: customer);

		await DatabaseContext
			.SaveChangesAsync();

		return CreatedAtAction
			(actionName: nameof(GetCustomerAsync).RemoveAsync(),
			routeValues: new { id = customer.Id }, value: customer);
	}
	#endregion /Action: CreateCustomerAsync()

	#region Action: GetCustomerAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet(template: "{id}")]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Models.Customer),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	/// <summary>
	/// Step (1)
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> GetCustomerAsync(int id)
	{
		var customer =
			await
			DatabaseContext.Customers
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (customer == null)
		{
			return NotFound();
		}

		return Ok(value: customer);
	}
	#endregion /Action: GetCustomerAsync()

	#region Action: UpdateCustomerAsync
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

	/// <summary>
	/// Step (1)
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		UpdateCustomerAsync(long id, Models.Customer customer)
	{
		if (id != customer.Id)
		{
			var errorMessage =
				$"The {nameof(id)} parameter is not valid!";

			return BadRequest(error: errorMessage);
		}

		var foundedItem =
			await
			DatabaseContext.Customers
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (foundedItem == null)
		{
			return NotFound();
		}

		foundedItem.LastName = customer.LastName;
		foundedItem.FirstName = customer.FirstName;

		await DatabaseContext
			.SaveChangesAsync();

		return NoContent();
	}
	#endregion /Action: UpdateCustomerAsync

	#region Action: DeleteCustomerAsync
	[Microsoft.AspNetCore.Mvc.HttpDelete(template: "{id}")]

	/// <summary>
	/// Step (9)
	/// </summary>
	[Microsoft.AspNetCore.Authorization.Authorize(Roles = "Admin")]

	/// <summary>
	/// Step (9)
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]

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

	/// <summary>
	/// Step (1)
	/// </summary>
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> DeleteCustomerAsync(long id)
	{
		var customer =
			await
			DatabaseContext.Customers
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (customer == null)
		{
			return NotFound();
		}

		DatabaseContext.Remove(entity: customer);

		await DatabaseContext
			.SaveChangesAsync();

		return NoContent();
	}
	#endregion /Action: DeleteCustomerAsync
}
