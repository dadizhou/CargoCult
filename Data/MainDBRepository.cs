using System.Collections.Generic;
using System.Linq;
using CargoCult.Models;

namespace CargoCult.Data
{
    public class MainDBRepository : IMainDBRepository
    {
        private MainDBContext context;

        public MainDBRepository(MainDBContext c)
        {
            context = c;
        }

        public IQueryable<Location> Locations => context.Locations;
    }
}