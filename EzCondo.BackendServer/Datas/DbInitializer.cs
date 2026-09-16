using Microsoft.EntityFrameworkCore;

namespace EzCondo.BackendServer.Datas
{
	public sealed class DbInitializer
	{
		private readonly ApplicationDbContext _dbContext;

		public DbInitializer(ApplicationDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public Task SeedAsync(CancellationToken cancellationToken = default)
		{
			// Keep migration and future seed orchestration in one startup boundary.
			return _dbContext.Database.MigrateAsync(cancellationToken);
		}
	}
}
