using System;
namespace Projects.Models
{
    public class PostDTOCityDetailsModel
    {
        public string CityKey { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string ZipCode { get; set; }
        public string CountryCode { get; set; }

    }

    public class PostDTOCityForecastsModel
    {
        public int RecordID { get; set; }
        public string HeadlineDate { get; set; }
        public string Headline { get; set; }
        public string HeadlineCategory { get; set; }
        public string ForecastDate { get; set; }
        public string TemparatureMax { get; set; }
        public string TemparatureMin { get; set; }
        public string DayConditions { get; set; }
        public string isDayPrecipitation { get; set; }
        public string DayPrecipitationType { get; set; } 
        public string DayPrecipitationIntensity { get; set; }
        public string NightConditions { get; set; }
        public string isNightPrecipitation { get; set; }
        public string NightPrecipitationType { get; set; }
        public string NightPrecipitationIntensity { get; set; }

    }
}
