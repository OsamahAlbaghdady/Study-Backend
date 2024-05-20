namespace BackEndStructuer.Entities;

public class MedicalField : BaseEntity<Guid>
{

    public string? Name { get; set; }

    public string? VideoUrl { get; set; }
    
}