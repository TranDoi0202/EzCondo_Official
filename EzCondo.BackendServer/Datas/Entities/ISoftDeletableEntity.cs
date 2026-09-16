namespace EzCondo.BackendServer.Datas.Entities
{
	public interface ISoftDeletableEntity
	{
		bool IsDeleted { get; set; }
		DateTime? DeletedAt { get; set; }
	}
}
