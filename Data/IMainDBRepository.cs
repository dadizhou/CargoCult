using System.Linq;
using CargoCult.Models;

namespace CargoCult.Data
{
    public interface IMainDBRepository
    {
        IQueryable<Location> Locations { get; }

        void SaveLocation(Location location);

        Location DeleteLocation(long locationID);
    }
}