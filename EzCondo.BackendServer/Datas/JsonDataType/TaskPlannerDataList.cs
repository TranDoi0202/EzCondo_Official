namespace EzCondo.BackendServer.Datas.JsonDataType
{
	public class TaskPlannerDataList
	{
		public int TotalEstimatedMinutes { get; set; } //Tổng thười gian dự kiến hoàn thành
		public required string SeverityLevel { get; set; } //Mức độ nghiêm trọng
		public List<string> RequiredRole { get; set; } = new(); //Danh sách bộ phận phải tham gia
		public List<AiTaskItem> Task { get; set; } = new(); //Danh sách công việc AI đã phân tích
	}
}
