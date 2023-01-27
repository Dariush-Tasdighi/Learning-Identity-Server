namespace RazorPages.Pages
{
	public class PrivacyModel :
		Microsoft.AspNetCore.Mvc.RazorPages.PageModel
	{
		public PrivacyModel
			(Microsoft.Extensions.Logging.ILogger<IndexModel> logger) : base()
		{
			Logger = logger;
		}

		private Microsoft.Extensions.Logging.ILogger<IndexModel> Logger { get; }

		public void OnGet()
		{
		}
	}
}
