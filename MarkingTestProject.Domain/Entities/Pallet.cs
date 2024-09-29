namespace MarkingTestProject.Domain.Entities
{
    public class Pallet : BaseEntity
    {
        public string Code { get; set; }
        public ICollection<Box> Boxes { get; set; }
    }
}
