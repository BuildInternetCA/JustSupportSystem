namespace JustSupportSystem.DTO
{
    public class UITableData
    {
        public UITableData()
        {
            Headers = new Dictionary<int, string>();
            Rows = new List<Dictionary<string, string>>();
        }
        public Dictionary<int, string>? Headers { get; set; }
        public List<Dictionary<string, string>>? Rows { get; set; }
    }
}
