namespace JustSupportSystem.DTO
{
    public class CompanyViewDTO
    {
        public CompanyViewDTO()
        {
            CompanyId = 0;
            CompanyName = string.Empty;
            CompanyAddress = string.Empty;
            
        }
        public long CompanyId { get; set; }
        public string CompanyCode { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public int UserCount { get; set; }
        public int TicketCount { get; set; }
    }

    public class CompanyViewDetailsDTO : CompanyViewDTO
    {
        public string? Notes { get; set; }
        public string? DefaultAgent { get; set; }
        public UITable UserTable {get;set;}
        public UITable CustomFields { get; set; }
    }
}
