namespace Application.Dto
{
    public class AuthorDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime DateOfRegistration { get; set; }
        public List<PhotoDto> Photos { get; set; }
        public List<TextDto> Texts { get; set; }
    }
}
