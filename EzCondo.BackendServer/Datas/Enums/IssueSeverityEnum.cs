using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum IssueSeverityEnum
	{
		[PgName("low")]
		LOW,
		[PgName("medium")]
		MEDIUM,
		[PgName("high")]
		HIGH,
		[PgName("critical")]
		CRITICAL
	}
}
