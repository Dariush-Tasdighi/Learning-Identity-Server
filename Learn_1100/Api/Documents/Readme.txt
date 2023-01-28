**************************************************
In Folder: Models

	Create File: User.cs
	Update File: DatabaseContext.cs

		public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
**************************************************

**************************************************
In Folder: Controllers

	Create File: UsersController.cs based on CustomersController.cs

		Replace All: customer to user (Match Case)
		Replace All: Customer to User (Match Case)
		Replace All: Customers to Users (Match Case)

		Update Method: UpdateCustomerAsync
**************************************************

**************************************************
Create Folder:

	Dtos

	Create Folder:

		Account

		Create Files:

			LoginDto
			RegisterDto
**************************************************

**************************************************
In Folder: Infrastructure

	Update File: Utility.cs

		HashPassword()
**************************************************

**************************************************
In Controllers Folder:

	Create File: AccountController.cs

		POST Verb: RegisterAsync()
**************************************************

**************************************************
In Controllers Folder:

	Create File: AccountController.cs

		LoginAsync (POST)
**************************************************

**************************************************
In Folder: Infrastructure

	Update File: Utility.cs

		GetAccessToken()
**************************************************

**************************************************
Check JWT Access Token in Site:

	https://jwt.io
	https://JsonWebToken.io

	https://jwt.ms
	https://token.dev/
	http://calebb.net/
	https://www.jstoolset.com/jwt
**************************************************
