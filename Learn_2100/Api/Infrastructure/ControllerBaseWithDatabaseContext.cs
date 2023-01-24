namespace Infrastructure;

public class ControllerBaseWithDatabaseContext : ControllerBase
{
	#region Constructor
	public ControllerBaseWithDatabaseContext
		(Models.DatabaseContext databaseContext) : base()
	{
		DatabaseContext = databaseContext;
	}
	#endregion /Constructor

	#region Properties
	protected Models.DatabaseContext DatabaseContext { get; }
	#endregion /Properties
}
