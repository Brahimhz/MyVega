﻿namespace Vega.Controllers.Resources
{
    public class VehicleQueryResource
    {
        public int? MakeId { get; set; }

        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool IsAsc { get; set; }
        public int Page { get; set; }
        public byte PageSize { get; set; }
    }
}
