namespace TrainMaster.Domain.Dto
{
    public class UserCreateUpdateDto
    {
        public int Id { get; set; }
        public string? Cpf { get; set; }
        public string? Email { get; set; }
    }
}