namespace AustrianPublicHolidayCalculator.Models
{
    /// <summary>
    /// Federal States / Bundeslaender from austria
    /// </summary>
    public enum eFederalState
    {
        All = 0,
        Burgenland = 1,
        Carinthia = 2,
        LowerAustria = 3,
        UpperAustria = 4,
        Salzburg = 5,
        Styria = 6,
        Tyrol = 7,
        Vorarlberg = 8,
        Vienna = 9
    }

    public class PublicHoliday
    {
        #region Properties

        /// <summary>
        /// Name of the holiday
        /// </summary>
        public string HolidayName { get; set; }

        /// <summary>
        /// Date of the holiday
        /// </summary>
        public DateTime HolidayDate { get; set; }

        public eFederalState FederalState { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Default Constructor
        /// </summary>
        public PublicHoliday()
        {
            
        }

        /// <summary>
        /// Full-Constructor
        /// </summary>
        public PublicHoliday(string holidayName, DateTime holidayDate, eFederalState federalState)
        {
            HolidayName = holidayName;
            HolidayDate = holidayDate;

        }

        #endregion
    }
}
