using System.Collections.Generic;

namespace Postcodes.API.Testing.Models
{

    public class Geolocation
    {
        public double longitude { get; set; }
        public double latitude { get; set; }
        public int limit { get; set; }
        public int radius { get; set; }
    }

    public class Geolocations
    {
        public List<Geolocation> geolocations { get; set; }
    }

}
