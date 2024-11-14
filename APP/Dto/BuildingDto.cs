namespace BankSystem.App.Dto;

public class BuildingDto
{
    public Guid BuildingId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; } // Путь к изображению здания
    public List<SensorDto> Sensors { get; set; } = new List<SensorDto>();
}