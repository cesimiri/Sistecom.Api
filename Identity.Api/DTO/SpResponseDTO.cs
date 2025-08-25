namespace Identity.Api.DTO
{
    public class SpResponseDTO
    {
        public int Success { get; set; }
        public string Message { get; set; }
        public int? ErrorNumber { get; set; }
        public int? Severity { get; set; }
        public int? State { get; set; }
        public int? ErrorLine { get; set; }
        public string ProcedureName { get; set; }
    }
}
