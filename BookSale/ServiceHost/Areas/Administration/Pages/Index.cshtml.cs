using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace ServiceHost.Areas.Administration.Pages
{
    public class IndexModel : PageModel
    {
        public List<Chart> BarLineDataSet { get; set; }
        public List<Chart> PolarAreaDataSet { get; set; }
        public void OnGet()
        {
            BarLineDataSet = new List<Chart>
            {
                new Chart
                {
                    Label="Apple",
                    Data=new List<int>{100, 110, 120, 160, 210, 175, -10, 25, 98, 200, 120, 170},
                    BackgroundColor=new string[] { "#ffcdb2" },
                    BorderColor="#b5838d"
                },
                new Chart{
                    Label="Samsung",
                    Data=new List<int>{150, 160, 180, 210, 290, 240, -20, 75, 110, 220, 170,225},
                    BackgroundColor=new string[] { "#ffc8dd" },
                    BorderColor="#ffafcc"
                },
                new Chart{
                    Label="LG",
                    Data=new List<int>{20, 10, 25, 60, 110, 1, -50, 15, 28, 110, 70, 90},
                    BackgroundColor=new string[] { "#00776b" },
                    BorderColor="#023e8a"
                }
            };

            PolarAreaDataSet = new List<Chart>
            {
                new Chart
                {
                    Label = "fruit",
                    Data = new List<int> { 100, 110, 120, 160, 210 },
                    BackgroundColor = new[] { "#E76F51", "#F4A261", "#E9C46A", "#2A9D8F", "#264653" },
                    BorderColor = "#b5838d"
                }

            };

        }
    }
    public class Chart
    {
        [JsonProperty(PropertyName = "label")]
        public string Label { get; set; }
        [JsonProperty(PropertyName = "data")]

        public List<int> Data { get; set; }
        [JsonProperty(PropertyName = "backgroundColor")]

        public string[] BackgroundColor { get; set; }
        [JsonProperty(PropertyName = "borderColor")]

        public string BorderColor { get; set; }
    }
}
