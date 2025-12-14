namespace tariqi.Domain_Layer.Entities
{
    public class Region
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Code { get; set; }
       
        public ICollection<Area> Areas { get; set; } = new List<Area>();
    }
}
