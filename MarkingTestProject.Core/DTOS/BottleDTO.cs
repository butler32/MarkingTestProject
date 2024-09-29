using MarkingTestProject.Domain.Entities;
using Newtonsoft.Json;

namespace MarkingTestProject.Core.DTOS
{
    public class BottleDTO
    {
        public int Id { get; set; }
        public string Code { get; set; }
        [JsonIgnore]
        public BoxDTO Box { get; set; }
        [JsonIgnore]
        public int BoxId { get; set; }
    }
}
