using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

public class AccountController :
	Infrastructure.ControllerBaseWithDatabaseContext
{
	#region Constructor
	public AccountController
		(Models.DatabaseContext databaseContext,
		Microsoft.Extensions.Configuration.IConfiguration configuration) :
		base(databaseContext: databaseContext)
	{
		Configuration = configuration;
	}
	#endregion /Constructor

	#region Properties
	private Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }
	#endregion /Properties

	#region Action: RegisterAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "Register")]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		RegisterAsync(Dtos.Account.RegisterDto dto)
	{
		// **************************************************
		//var foundedUser =
		//	await
		//	DatabaseContext.Users
		//	.Where(current => current.Username == dto.Username)
		//	.FirstOrDefaultAsync();
		// **************************************************

		// **************************************************
		// کار نمی‌کند EF Core دستور ذیل در
		//var foundedUser =
		//	await
		//	DatabaseContext.Users
		//	.Where(current => string.Compare(current.Username, dto.Username, true) == 0)
		//	.FirstOrDefaultAsync();
		// **************************************************

		// **************************************************
		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.Username.ToLower() == dto.Username.ToLower())
			.FirstOrDefaultAsync();
		// **************************************************

		if (foundedUser != null)
		{
			var errorMessage =
				"Username is already exists!";

			return Problem(detail: errorMessage);
		}

		// **************************************************
		//var newUser =
		//	new Models.User
		//	(username: dto.Username, password: dto.Password)
		//	{
		//		IsActive = true,
		//		IsAdmin = false,
		//	};

		//await DatabaseContext
		//	.AddAsync(entity: newUser);

		//await DatabaseContext
		//	.SaveChangesAsync();

		//var successMessage =
		//	"The user information registered successfully.";

		//return Ok(value: successMessage);
		// **************************************************

		// **************************************************
		//var passwordHash =
		//	Infrastructure.Utility
		//	.GetSha256(text: dto.Password);

		//var newUser =
		//	new Models.User
		//	(username: dto.Username, password: passwordHash)
		//	{
		//		IsActive = true,
		//		IsAdmin = false,
		//	};

		//await DatabaseContext
		//	.AddAsync(entity: newUser);

		//await DatabaseContext
		//	.SaveChangesAsync();

		//var successMessage =
		//	"The user information registered successfully.";

		//return Ok(value: successMessage);
		// **************************************************

		// **************************************************
		//var saltPassword = "Hello";

		//var passwordHash =
		//	Infrastructure.Utility.HashPassword
		//	(password: dto.Password, saltPassword: saltPassword);

		//var newUser =
		//	new Models.User
		//	(username: dto.Username, password: passwordHash)
		//	{
		//		IsActive = true,
		//		IsAdmin = false,
		//	};

		//await DatabaseContext
		//	.AddAsync(entity: newUser);

		//await DatabaseContext
		//	.SaveChangesAsync();

		//var successMessage =
		//	"The user information registered successfully.";

		//return Ok(value: successMessage);
		// **************************************************

		// **************************************************
		var saltPassword =
			Configuration.GetSection(key: "SaltPassword").Value;

		if (string.IsNullOrWhiteSpace(value: saltPassword))
		{
			return Problem();
		}

		var passwordHash =
			Infrastructure.Utility.HashPassword
			(password: dto.Password, saltPassword: saltPassword);

		var newUser =
			new Models.User
			(username: dto.Username, password: passwordHash)
			{
				IsActive = true,
				IsAdmin = false,
			};

		await DatabaseContext
			.AddAsync(entity: newUser);

		await DatabaseContext
			.SaveChangesAsync();

		var successMessage =
			"The user information registered successfully.";

		return Ok(value: successMessage);
		// **************************************************
	}
	#endregion /Action: RegisterAsync()

	#region Action: LoginAsync()
	[Microsoft.AspNetCore.Mvc.HttpPost(template: "Login")]

	[Microsoft.AspNetCore.Mvc.Consumes
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.Produces
		(contentType: System.Net.Mime.MediaTypeNames.Application.Json)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(type: typeof(string),
		statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status500InternalServerError)]

	[Microsoft.AspNetCore.Mvc.ProducesResponseType
		(statusCode: Microsoft.AspNetCore.Http.StatusCodes.Status415UnsupportedMediaType)]

	public async System.Threading.Tasks.Task
		<Microsoft.AspNetCore.Mvc.IActionResult>
		LoginAsync(Dtos.Account.RegisterDto dto)
	{
		var foundedUser =
			await
			DatabaseContext.Users
			.Where(current => current.Username.ToLower() == dto.Username.ToLower())
			.FirstOrDefaultAsync();

		if (foundedUser == null)
		{
			//return Forbid();

			//return StatusCode(statusCode: StatusCodes.Status403Forbidden);

			// پیغام باید گنگ باشد
			var errorMessage =
				"Username and/or password is not correct!";

			return StatusCode(statusCode: Microsoft.AspNetCore.Http
				.StatusCodes.Status403Forbidden, value: errorMessage);
		}

		// **************************************************
		var saltPassword =
			Configuration.GetSection(key: "SaltPassword").Value;

		if (string.IsNullOrWhiteSpace(value: saltPassword))
		{
			return Problem();
		}
		// **************************************************

		var passwordHash =
			Infrastructure.Utility.HashPassword
			(password: dto.Password, saltPassword: saltPassword);

		if (string.Compare(strA: foundedUser.Password,
			strB: passwordHash, ignoreCase: false) != 0)
		{
			// پیغام باید گنگ باشد
			var errorMessage =
				"Username and/or password is not correct!";

			return StatusCode(statusCode: Microsoft.AspNetCore.Http
				.StatusCodes.Status403Forbidden, value: errorMessage);
		}

		return Ok(value: "Valid Token!");

		// **************************************************
		var accessTokenSecurityKey =
			Configuration.GetSection(key: "AccessTokenSecurityKey").Value;

		if (string.IsNullOrWhiteSpace(value: accessTokenSecurityKey))
		{
			return Problem();
		}

		//if (accessTokenSecurityKey.Length <= 16)
		//{
		//	return Problem();
		//}
		// **************************************************

		var userToken =
			Infrastructure.Utility.GetAccessToken
			(accessTokenSecurityKey: accessTokenSecurityKey, user: foundedUser);

		return Ok(value: userToken);
	}
	#endregion /Action: LoginAsync()
}
