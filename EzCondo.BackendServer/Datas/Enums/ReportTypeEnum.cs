using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ReportTypeEnum
	{
		[PgName("profit")]
		PROFIT,
		[PgName("demo_graphic")]
		DEMO_GRAPHIC,
		[PgName("infrastructure")]
		INFRASTRUCTURE,
		[PgName("employee")]
		EMPLOYEE
	}
}
