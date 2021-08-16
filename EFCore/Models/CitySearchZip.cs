using System;
using System.Collections.Generic;

#nullable disable

namespace Projects.EFCore.Models
{
    public partial class CitySearchZip
    {
        public long RecordId { get; set; }
        public DateTime? RecordEntryDate { get; set; }
        public DateTime? RecordLastDate { get; set; }
        public string RecordStatus { get; set; }
        public string Json { get; set; }
    }
}
