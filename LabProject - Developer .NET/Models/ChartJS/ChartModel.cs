namespace LabProject.Models.ChartJS
{
    public class Chart
    {
        public string[] labels { get; set; } = Array.Empty<string>();
        public List<Datasets> datasets { get; set; } = new List<Datasets>();
    }
    public class Datasets
    {
        public string label { get; set; } = "";
        public string[] backgroundColor { get; set; } = Array.Empty<string>();
        public string[] data { get; set; } = Array.Empty<string>();
    }
}
