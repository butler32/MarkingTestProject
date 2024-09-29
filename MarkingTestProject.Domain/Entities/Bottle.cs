namespace MarkingTestProject.Domain.Entities
{
    public class Bottle : BaseEntity
    {
        public string Code { get; set; }
        public Box Box { get; set; }
        public int BoxId { get; set; }
    }
}
