namespace BankSystem.App.Dto;

public class UserDto
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<BuildingDto> Buildings { get; set; }
}