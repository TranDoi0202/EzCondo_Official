using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum BillingCycleEnum
	{
		[PgName("monthly")]
		MONTHLY,
		[PgName("yearly")]
		YEARLY
	}
}
