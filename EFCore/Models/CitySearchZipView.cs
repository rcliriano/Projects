using System;
using System.Collections.Generic;

#nullable disable

namespace Projects.EFCore.Models
{
    public partial class CitySearchZipView
    {
        public long RecordId { get; set; }
        public DateTime? RecordEntryDate { get; set; }
        public DateTime? RecordLastDate { get; set; }
        public string RecordStatus { get; set; }
        public string CityKey { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }
        public string RegionCode { get; set; }
        public string GeoPositionLatitude { get; set; }
        public string GeoPositionLongitude { get; set; }
    }
}
