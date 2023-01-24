using System.Linq;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class CustomersController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public CustomersController
		(Models.DatabaseContext databaseContext) : base(databaseContext: databaseContext)
	{
	}
	#endregion /Constructor

	// **************************************************
	// Step (1)
	// **************************************************
	/// <summary>
	/// Swagger Error!
	/// </summary>
	//public System.Collections.Generic
	//	.IEnumerable<Models.Customer> GetCustomers()
	//{
	//	var result =
	//		new System.Collections.Generic.List<Models.Customer>();

	//	result.Add(item: new Models.Customer
	//		(firstName: "Dariush", lastName: "Tasdighi"));

	//	result.Add(item: new Models.Customer
	//		(firstName: "Ali Reza", lastName: "Mohammadi"));

	//	return result;

	//	//var result =
	//	//	DatabaseContext.Customers
	//	//	.ToList()
	//	//	;

	//	//return result;
	//}
	// **************************************************

	// **************************************************
	// Step (2)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]
	//public System.Collections.Generic
	//	.IEnumerable<Models.Customer> GetCustomers()
	//{
	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return result;
	//}
	// **************************************************

	// **************************************************
	// Step (3)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]
	//public System.Collections.Generic
	//	.IEnumerable<Models.Customer> GetCustomers()
	//{
	//	//if (DatabaseContext.Customers == null)
	//	//{
	//	//	//var errorMessage =
	//	//	//	$"Entity set 'DatabaseContext.Customers' is null!";

	//	//	var errorMessage =
	//	//		$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//	//	return Problem
	//	//		(detail: errorMessage);
	//	//}

	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return result;
	//}
	// **************************************************

	// **************************************************
	// Step (4)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]
	//public Microsoft.AspNetCore.Mvc.IActionResult GetCustomers()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (5)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]
	//public Microsoft.AspNetCore.Mvc.ActionResult GetCustomers()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (6)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]
	//public Microsoft.AspNetCore.Mvc.ActionResult
	//	<System.Collections.Generic.IEnumerable<Models.Customer>> GetCustomers()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (7)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]

	////[Microsoft.AspNetCore.Mvc.ProducesResponseType
	////	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//public Microsoft.AspNetCore.Mvc.ActionResult
	//	<System.Collections.Generic.IEnumerable<Models.Customer>> GetCustomers()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		//return Problem();

	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (8)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(System.Collections.Generic.IEnumerable<Models.Customer>),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//public Microsoft.AspNetCore.Mvc.IActionResult GetCustomers()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	var result =
	//		DatabaseContext.Customers
	//		.ToList()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (9)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(System.Collections.Generic.IEnumerable<Models.Customer>),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//public async System.Threading.Tasks.Task
	//	<Microsoft.AspNetCore.Mvc.IActionResult> GetCustomersAsync()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	//var result =
	//	//	DatabaseContext.Customers
	//	//	// ToList() -> using System.Linq;
	//	//	.ToList()
	//	//	;

	//	var result =
	//		await
	//		DatabaseContext.Customers
	//		// ToListAsync() -> using Microsoft.EntityFrameworkCore;
	//		.ToListAsync()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (10)
	// **************************************************
	//[Microsoft.AspNetCore.Mvc.HttpGet]

	//[Microsoft.AspNetCore.Mvc.Consumes
	//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	//[Microsoft.AspNetCore.Mvc.Produces
	//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(System.Collections.Generic.IEnumerable<Models.Customer>),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//public async System.Threading.Tasks.Task
	//	<Microsoft.AspNetCore.Mvc.IActionResult> GetCustomersAsync()
	//{
	//	if (DatabaseContext.Customers == null)
	//	{
	//		var errorMessage =
	//			$"Entity set '{DatabaseContext}.{DatabaseContext.Customers}' is null!";

	//		return Problem
	//			(detail: errorMessage);
	//	}

	//	var result =
	//		await
	//		DatabaseContext.Customers
	//		.ToListAsync()
	//		;

	//	return Ok(value: result);
	//}
	// **************************************************

	// **************************************************
	// Step (11)
	// **************************************************
	#region Action: GetCustomersAsync()
	[Microsoft.AspNetCore.Mvc.HttpGet]

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
	// **************************************************

	#region Action: CreateCustomerAsync()
	// PostCustomer -> CreateCustomerAsync

	[Microsoft.AspNetCore.Mvc.HttpPost]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(Models.Customer),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(Models.Customer),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status201Created)]

	// New: Customer Validation!
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	// New: Customer is null!
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		CreateCustomerAsync(Models.Customer customer)
	{
		//DatabaseContext
		//	.Customers.Add(entity: customer);

		// AddAsync() -> using Microsoft.EntityFrameworkCore;
		//await DatabaseContext
		//	.Customers.AddAsync(entity: customer);

		//DatabaseContext.Add(entity: customer);

		await DatabaseContext.AddAsync(entity: customer);

		//DatabaseContext.SaveChanges();

		// SaveChangesAsync() -> using Microsoft.EntityFrameworkCore;
		await DatabaseContext
			.SaveChangesAsync();

		//return Ok(value: customer);

		//return CreatedAtAction(actionName: "GetCustomer",
		//	routeValues: new { id = customer.Id }, value: customer);

		// Note: Routing Error!
		//return CreatedAtAction(actionName: nameof(GetCustomerAsync),
		//	routeValues: new { id = customer.Id }, value: customer);

		//return CreatedAtAction
		//	(actionName: nameof(GetCustomerAsync).Replace("Async", string.Empty),
		//	routeValues: new { id = customer.Id }, value: customer);

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

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(type: typeof(string),
	//	statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	// New: /Customers/aa
	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult> GetCustomerAsync(int id)
	{
		//var customer =
		//	DatabaseContext.Customers
		//	.Find(keyValues: id);

		//var customer =
		//	await
		//	DatabaseContext.Customers
		//	.FindAsync(keyValues: id);

		//var customer =
		//	DatabaseContext.Customers
		//	.Where(current => current.Id == id)
		//	.FirstOrDefault();

		var customer =
			await
			DatabaseContext.Customers
			.Where(current => current.Id == id)
			.FirstOrDefaultAsync();

		if (customer == null)
		{
			// از دست بدهیم TraceId دقت کنید که نوشتن دستورات ذیل باعث می‌شود که
			//var errorMessage =
			//	$"There is not any customer with this id: {id}!";

			//return NotFound
			//	(value: errorMessage);

			return NotFound();
		}

		return Ok(value: customer);
	}
	#endregion /Action: GetCustomerAsync()

	//#region Action: UpdateCustomerAsync
	////PutCustomer -> UpdateCustomerAsync

	//[Microsoft.AspNetCore.Mvc.HttpPut(template: "{id}")]

	//[Microsoft.AspNetCore.Mvc.Consumes
	//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	//[Microsoft.AspNetCore.Mvc.Produces
	//	(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status404NotFound)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status405MethodNotAllowed)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	//[Microsoft.AspNetCore.Mvc.ProducesResponseType
	//	(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	//public async System.Threading.Tasks.Task
	//	<Microsoft.AspNetCore.Mvc.IActionResult>
	//	UpdateCustomerAsync(long id, Models.Customer customer)
	//{
	//	if (id != customer.Id)
	//	{
	//		var errorMessage =
	//			$"The {nameof(id)} parameter is not valid!";

	//		return BadRequest(error: errorMessage);
	//	}

	//	DatabaseContext.Entry(entity: customer).State =
	//		Microsoft.EntityFrameworkCore.EntityState.Modified;

	//	try
	//	{
	//		await DatabaseContext
	//			.SaveChangesAsync();
	//	}
	//	catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
	//	{
	//		var result =
	//			await
	//			CheckCustomerExistsAsync(id: id);

	//		if (result == false)
	//		{
	//			return NotFound();
	//		}
	//		else
	//		{
	//			throw;
	//		}
	//	}

	//	return NoContent();
	//}
	//#endregion /Action: UpdateCustomerAsync

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

	// https://localhost:7283/api/Customers/1 -> https://localhost:7283/api/Customers
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

		//return Ok(value: foundedItem);

		return NoContent();
	}
	#endregion /Action: UpdateCustomerAsync

	#region Action: DeleteCustomerAsync
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

		//DatabaseContext.Customers
		//	.Remove(entity: customer);

		DatabaseContext.Remove(entity: customer);

		await DatabaseContext
			.SaveChangesAsync();

		return NoContent();
	}
	#endregion /Action: DeleteCustomerAsync

	#region Method: CheckCustomerExists() 
	private async
		System.Threading.Tasks.Task<bool>
		CheckCustomerExistsAsync(long id)
	{
		var result =
			await
			DatabaseContext.Customers
			.Where(current => current.Id == id)
			.AnyAsync();

		return result;
	}

	//[Microsoft.AspNetCore.Mvc.NonAction]
	//public async
	//	System.Threading.Tasks.Task<bool>
	//	CheckCustomerExistsAsync(long id)
	//{
	//	var result =
	//		await
	//		DatabaseContext.Customers
	//		.Where(current => current.Id == id)
	//		.AnyAsync();

	//	return result;
	//}
	#endregion /Method: CheckCustomerExists()
}
