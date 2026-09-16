using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ReportPeriodEnum
	{
		[PgName("daily")]
		DAILY,
		[PgName("weekly")]
		WEEKLY,
		[PgName("monthly")]
		MONTHLY,
		[PgName("quarterly")]
		QUARTERLY,
		[PgName("yearly")]
		YEARLY
	}
}
