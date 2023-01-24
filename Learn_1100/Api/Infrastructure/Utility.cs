namespace Infrastructure;

public static class Utility : object
{
	static Utility()
	{
	}

	public static string RemoveAsync(this string value)
	{
		if (string.IsNullOrWhiteSpace(value: value))
		{
			return value;
		}

		var result =
			value.Replace
			(oldValue: "Async", newValue: string.Empty);

		return result;
	}

	private static byte[] HashPassword
		(string password, byte[] saltPasswordBytes)
	{
		using var hmac =
			new System.Security.Cryptography
			.HMACSHA512(key: saltPasswordBytes);

		var passwordBytes =
			System.Text.Encoding
			.UTF8.GetBytes(s: password);

		var result =
			hmac.ComputeHash(buffer: passwordBytes);

		return result;
	}

	public static string HashPassword
		(string password, string saltPassword)
	{
		var saltPasswordBytes =
			System.Text.Encoding
			.UTF8.GetBytes(s: saltPassword);

		var passwordHashBytes =
			HashPassword(password: password,
			saltPasswordBytes: saltPasswordBytes);

		var result =
			System.Text.Encoding.UTF8
			.GetString(bytes: passwordHashBytes);

		return result;
	}

	public static string GetAccessToken
		(string accessTokenSecurityKey, Models.User user)
	{
		var claims =
			new System.Collections.Generic
			.List<System.Security.Claims.Claim>
			{
				//new System.Security.Claims.Claim(type: "Name", user.Username),

				new System.Security.Claims.Claim
					(type: System.Security.Claims.ClaimTypes.Name, user.Username),
			};

		var accessTokenSecurityKeyBytes =
			System.Text.Encoding.UTF8.GetBytes(s: accessTokenSecurityKey);

		var symmetricSecurityKey =
			new Microsoft.IdentityModel.Tokens
			.SymmetricSecurityKey(key: accessTokenSecurityKeyBytes);

		var algorithm =
			Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha512Signature;

		var signingCredentials =
			new Microsoft.IdentityModel.Tokens
			.SigningCredentials(key: symmetricSecurityKey, algorithm: algorithm);

		var expires =
			System.DateTime.Now.AddDays(value: 1);

		var jwtSecurityToken =
			new System.IdentityModel.Tokens.Jwt.JwtSecurityToken
			(claims: claims,
			expires: expires,
			signingCredentials: signingCredentials);

		var jwtSecurityTokenHandler =
			new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

		var result =
			jwtSecurityTokenHandler.WriteToken(token: jwtSecurityToken);

		return result;
	}
}
