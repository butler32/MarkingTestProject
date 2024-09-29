using MarkingTestProject.Domain.Entities;
using Newtonsoft.Json;

namespace MarkingTestProject.Core.DTOS
{
    public class BoxDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        public PalletDTO Pallet { get; set; }
        [JsonIgnore]
        public int PalletId { get; set; }
        public ICollection<BottleDTO> Bottles { get; set; }
    }
}
