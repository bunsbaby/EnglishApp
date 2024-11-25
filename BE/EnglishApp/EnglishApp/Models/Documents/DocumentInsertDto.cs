namespace EnglishApp.Models.Documents
{
    public class DocumentInsertDto
    {
        public string Name { get; set; }
        public int DocumentSize { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public string DisplayName { get; set; }
    }
}
