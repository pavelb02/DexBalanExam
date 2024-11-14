namespace BankSystem.App.Dto
{
    public class SensorDto
    {
        public Guid SensorId { get; set; }
        public string Name { get; set; }  // Название датчика
        public string Term { get; set; }  // Температура
        public int Charge { get; set; }
        public int Water { get; set; }
        public string ImagePath { get; set; }  // Путь к изображению датчика
        public DateTime MeasurementDate { get; set; }  // Дата замера
        public CoordinateDto Coordinate { get; set; }  // Координаты датчика

        public DateTime CreatedAt { get; set; }  // Дата создания записи
        public DateTime UpdatedAt { get; set; }  // Дата последнего обновления записи
    }

    public class CoordinateDto
    {
        public int X { get; set; }
        public int Y { get; set; }
    }
}