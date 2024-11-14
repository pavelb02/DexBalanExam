namespace Domain;

public class Building
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public ICollection<Sensor> Sensors { get; set; } = new List<Sensor>();
    public Guid UserId { get; set; } // Foreign key to User
    public User User { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        var building = (Building)obj;
        return Id == building.Id && Address == building.Address;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Address);
    }}