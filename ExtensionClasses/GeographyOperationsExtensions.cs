using System;
using Microsoft.Spatial;

namespace CargoCult.ExtensionClasses
{
    public static class GeographyOperationsExtensions
    {
        const double PIx = Math.PI;
        const double R = 6371;

        public static double Radians(double x)
        {
            return x * PIx / 180;
        }

        public static double DistanceBetweenPlaces(double lat1, double lon1, double lat2, double lon2)
        {
            double sLat1 = Math.Sin(Radians(lat1));
            double sLat2 = Math.Sin(Radians(lat2));
            double cLat1 = Math.Cos(Radians(lat1));
            double cLat2 = Math.Cos(Radians(lat2));
            double cLon = Math.Cos(Radians(lon1) - Radians(lon2));

            double cosD = sLat1 * sLat2 + cLat1 * cLat2 * cLon;

            double d = Math.Acos(cosD);

            double dist = R * d;

            return dist;
        }

        // This is the extension method.
        // The first parameter takes the "this" modifier
        // and specifies the type for which the method is defined.
        public static double? Distance(this GeographyPoint operand1, GeographyPoint operand2)
        {
            var result = DistanceBetweenPlaces(operand1.Latitude, operand1.Longitude, operand2.Latitude, operand2.Longitude);
            return result;
        }
    }
}
