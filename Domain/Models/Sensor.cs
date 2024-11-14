namespace Domain;

public class Sensor
{
    public Guid Id { get; set; }
    public Coordinate Coordinate { get; set; }
    public string Name { get; set; }
    public string Term { get; set; }
    public int Charge { get; set; }
    public int Water { get; set; }
    public string ImagePath { get; set; }
    public DateTime MeasurementDate { get; set; }
    
    public Guid BuildingId { get; set; }
    public Building Building { get; set; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var sensor = (Sensor)obj;
        return Id == sensor.Id && Name == sensor.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
}