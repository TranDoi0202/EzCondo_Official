using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ApartmentStatusEnum
	{
		[PgName("available")]
		AVAILABLE,
		[PgName("occupied")]
		OCCUPIED,
		[PgName("maintenance")]
		MAINTENANCE
	}
}
