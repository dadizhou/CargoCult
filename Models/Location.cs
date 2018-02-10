using Microsoft.Spatial;

namespace CargoCult.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public GeographyPoint Position {get;set;}
    }
}
