namespace EnglishApp.Models.Students
{
    public class StudentInsertDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int GenderId { get; set; }
        public string Address { get; set; }
        public int ClassId { get; set; }
    }
}
