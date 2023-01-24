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
}
