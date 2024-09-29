using MarkingTestProject.Domain.Entities;

namespace MarkingTestProject.Core.DTOS
{
    public class PalletDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public ICollection<BoxDTO> Boxes { get; set; }
    }
}
