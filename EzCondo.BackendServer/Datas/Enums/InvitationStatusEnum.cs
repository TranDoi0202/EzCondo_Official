using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum InvitationStatusEnum
	{
		[PgName("pending")]
		PENDING,
		[PgName("accepted")]
		ACCEPTED,
		[PgName("expired")]
		EXPIRED,
		[PgName("revoked")]
		REVOKED
	}
}
