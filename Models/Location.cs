using Microsoft.Spatial;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoCult.Models
{
    public class Location
    {
        public int LocationID { get; set; }
        public string Name { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [NotMapped]
        public GeographyPoint Position => GeographyPoint.Create(Latitude, Longitude);

    }
}
