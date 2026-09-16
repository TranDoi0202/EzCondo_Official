using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum PayrollStatusEnum
	{
		[PgName("draft")]
		DRAFT,
		[PgName("pending_approval")]
		PENDING_APPROVAL,
		[PgName("processing")]
		PROCESSING,
		[PgName("completed")]
		COMPLETED
	}
}
