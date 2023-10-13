namespace SimpleCRUD.Models.ViewModels
{
    public class UpdateStudentViewModel
    {
        public Guid Id { get; set; }
        public int Roll { get; set; }
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime DOB { get; set; }
        public string Department { get; set; }
    }
}
