using System;

namespace EnglishApp.Models.Documents
{
    public class DocumentDto
    {
        public string Name { get; set; }
        public int DocumentSize { get; set; }
        public string Description { get; set; }
        public string FileName { get; set; }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string DisplayName { get; set; }
    }
}
