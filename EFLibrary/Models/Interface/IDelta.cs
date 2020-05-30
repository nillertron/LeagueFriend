namespace EFLibrary.Models
{
    public interface IDelta
    {
        int Id { get; set; }
        string Period { get; set; }
        double Value { get; set; }
    }
}