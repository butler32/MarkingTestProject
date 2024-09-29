namespace MarkingTestProject.Models
{
    public class CurrentTaskModel
    {
        public string Name {  get; set; }
        public string Gtin { get; set; }
        public string Volume { get; set; }
        public int BoxFormat {  get; set; }
        public int PalletFormat {  get; set; }
    }
}
