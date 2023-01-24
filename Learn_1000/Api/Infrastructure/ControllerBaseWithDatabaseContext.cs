namespace Infrastructure;

public class ControllerBaseWithDatabaseContext : ControllerBase
{
	#region Constructor
	public ControllerBaseWithDatabaseContext
		(Models.DatabaseContext databaseContext) : base()
	{
		//_context = databaseContext;

		DatabaseContext = databaseContext;
	}
	#endregion /Constructor

	#region Properties
	//private readonly Models.DatabaseContext _context;

	protected Models.DatabaseContext DatabaseContext { get; }
	#endregion /Properties
}
