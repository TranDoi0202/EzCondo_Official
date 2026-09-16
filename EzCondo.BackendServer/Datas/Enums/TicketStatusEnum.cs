using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum TicketStatusEnum
	{
		[PgName("open")]
		OPEN,
		[PgName("in_progress")]
		IN_PROGRESS,
		[PgName("closed")]
		CLOSED
	}
}
