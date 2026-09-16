namespace EzCondo.BackendServer.Datas.Entities
{
	public class Function
	{
		public required string Id { get; set; }
		public required string Name { get; set; }
		public string? ParentId { get; set; }
		public string? Url { get; set; }

		public Function? Parent { get; set; }

		public ICollection<Function> Children { get; set; } = new List<Function>();
	}
}
