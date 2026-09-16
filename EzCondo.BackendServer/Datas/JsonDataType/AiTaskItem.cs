namespace EzCondo.BackendServer.Datas.JsonDataType
{
	public class AiTaskItem
	{
		public int StepId { get; set; } //Thứ tự bước
		public required string Title { get; set; }
		public required string Instruction { get; set; } //Hướng dẫn thực thi
		public required string TargetRole { get; set; } //Bộ phận chịu trách nhiệm chính
		public List<int> Dependency { get; set; } = new(); //Danh sách StepId bắt buộc hoàn thành trước
		public List<string> AcceptanceCriteria { get; set; } = new(); //Các tiêu chí nghiệm thu
		public int EstimatedMinute { get; set; } //Khung thời gian tối đa cho phép
	}
}
