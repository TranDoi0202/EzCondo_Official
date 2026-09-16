using NpgsqlTypes;

namespace EzCondo.BackendServer.Datas.Enums
{
	public enum ResidentTypeEnum
	{
		[PgName("owner")]
		OWNER,
		[PgName("tenant")]
		TENANT,
		[PgName("family_member")]
		FAMILY_MEMBER
	}
}
