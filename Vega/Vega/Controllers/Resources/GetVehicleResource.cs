using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Vega.Controllers.Resources
{
    public class GetVehicleResource
    {
        public int Id { get; set; }
        public KeyValuePairResource Model { get; set; }
        public KeyValuePairResource Make { get; set; }
        public bool IsRegistred { get; set; }

        public ContactResource Contact { get; set; }
        public DateTime LastUpdate { get; set; }

        public ICollection<KeyValuePairResource> Features { get; set; }

        public GetVehicleResource()
        {
            Features = new Collection<KeyValuePairResource>();
        }
    }
}
