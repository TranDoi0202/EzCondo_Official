using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ReportStatusEnum
	{
		[PgName("submitted")]
		SUBMITTED,
		[PgName("reviewed")]
		REVIEWED,
		[PgName("rejected")]
		REJECTED
	}
}
