using Microsoft.Spatial;
using System.ComponentModel.DataAnnotations.Schema;

namespace CargoCult.Models
{
    public class Location
    {
        public long LocationID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        [NotMapped]
        public GeographyPoint Position => GeographyPoint.Create(Latitude, Longitude);

    }
}