namespace EzCondo.BackendServer.Datas.JsonDataType
{
	public class FeatureList
	{
		//tính năng vận hành
		public bool HasResidentManager { get; set; } //quản lý cư dân
		public bool HasWorkTaskManager { get; set; } //phân công công việc
		public bool HasBasicReport { get; set; } //báo cáo cơ bản

		//nhóm tính năng nâng cao
		public bool HasVnpayIntegration { get; set; } //tích hợp VNPAY
		public bool SmartAttendence { get; set; } //chấm công bằng QR
		public bool HasAdvanceAnalytics { get; set; } //báo cáo lợi nhuận, thống kê phức tạp

		//tính năng AI
		public bool HasAiAssistant { get; set; }

		//giới hạn dung lượng lưu file đính kèm
		public int MaxStorageGb { get; set; }
	}
}
