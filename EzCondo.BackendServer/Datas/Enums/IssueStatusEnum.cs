using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum IssueStatusEnum
	{
		[PgName("open")]
		OPEN,
		[PgName("in_progress")]
		IN_PROGRESS,
		[PgName("resolved")]
		RESOLVED
	}
}
