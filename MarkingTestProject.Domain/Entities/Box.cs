namespace MarkingTestProject.Domain.Entities
{
    public class Box : BaseEntity
    {
        public string Code {  get; set; }
        public Pallet Pallet { get; set; }
        public int PalletId { get; set; }
        public ICollection<Bottle> Bottles { get; set; }
    }
}
