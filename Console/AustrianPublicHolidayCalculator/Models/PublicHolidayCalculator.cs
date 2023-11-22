namespace AustrianPublicHolidayCalculator.Models
{
    public static class PublicHolidayCalculator
    {
        /// <summary>
        /// Gets the Easter Date of the given year.
        /// </summary>
        /// <param name="year">year to calculate</param>
        /// <returns>The easter date</returns>
        public static DateTime EasterDate(int year)
        {
            int k = year / 100;
            int m = 15 + (3 * k + 3) / 4 - (8 * k + 13) / 25;
            int s = 2 - (3 * k + 3) / 4;
            int a = year % 19;
            int d = (19 * a + m) % 30;
            int r = (d + a / 11) / 29;
            int og = 21 + d - r;
            int sz = 7 - (year + year / 4 + s) % 7;
            int oe = 7 - (og - sz) % 7;
            int os = og + oe;

            if (os > 31)
            {
                return new DateTime(year, 4, os - 31);
            }
            else
            {
                return new DateTime(year, 3, os);
            }
        }

        /// <summary>
        /// Gets the public holidays of austria.
        /// </summary>
        /// <param name="year">year</param>
        /// <returns>List of the austrians public holiday for the given year</returns>
        public static List<PublicHoliday> GetAustriaPublicHolidays(int year, eFederalState federalState)
        {
            DateTime easterDate = EasterDate(year);

            List<PublicHoliday> publicHolidays = new List<PublicHoliday>()
            {
                new PublicHoliday("Neujahr", new DateTime(year, 1, 1), eFederalState.All),
                new PublicHoliday("Heiligen drei Könige", new DateTime(year, 1,6), eFederalState.All),
                new PublicHoliday("Ostersonntag", easterDate, eFederalState.All),
                new PublicHoliday("Ostermontag", easterDate.AddDays(1), eFederalState.All),
                new PublicHoliday("Staatsfeiertag", new DateTime(year, 5, 1), eFederalState.All),
                new PublicHoliday("Christi Himmelfahrt", easterDate.AddDays(39), eFederalState.All),
                new PublicHoliday("Pfingstsonntag", easterDate.AddDays(49), eFederalState.All),
                new PublicHoliday("Pfingstmontag", easterDate.AddDays(50), eFederalState.All),
                new PublicHoliday("Fronleichnam", easterDate.AddDays(60), eFederalState.All),
                new PublicHoliday("Mariä Himmelfahrt", new DateTime(year, 8, 15), eFederalState.All),
                new PublicHoliday("Nationalfeiertag", new DateTime(year, 10, 26), eFederalState.All),
                new PublicHoliday("Allerheiligen", new DateTime(year, 11, 1), eFederalState.All),
                new PublicHoliday("Mariä Empfängnis", new DateTime(year, 12, 8), eFederalState.All),
                new PublicHoliday("Christtag", new DateTime(year, 12, 25), eFederalState.All),
                new PublicHoliday("Stefanitag", new DateTime(year, 12, 26), eFederalState.All)
            };

            //switch (federalState)
            //{
            //    case eFederalState.All:
            //        publicHolidays.Add(new PublicHoliday("Stefanitag (Carinthia, Styria, Tyrol, Vorarlberg)", new DateTime(year, 3, 19), eFederalState.All));
            //        publicHolidays.Add(new PublicHoliday("Liebstattsonntag (UpperAustria)", easterDate.AddDays(-63), eFederalState.All));
            //        break;
            //    case eFederalState.Burgenland:
            //        break;
            //    case eFederalState.Carinthia:
            //        publicHolidays.Add(new PublicHoliday("Stefanitag (Carinthia)", new DateTime(year, 3, 19), eFederalState.All));
            //        break;
            //    case eFederalState.LowerAustria:
            //        break;
            //    case eFederalState.UpperAustria:
            //        publicHolidays.Add(new PublicHoliday("Liebstattsonntag (UpperAustria)", easterDate.AddDays(-63), eFederalState.All));
            //        break;
            //    case eFederalState.Salzburg:
            //        break;
            //    case eFederalState.Styria:
            //        publicHolidays.Add(new PublicHoliday("Stefanitag (Styria)", new DateTime(year, 3, 19), eFederalState.All));
            //        break;
            //    case eFederalState.Tyrol:
            //        publicHolidays.Add(new PublicHoliday("Stefanitag (Tyrol)", new DateTime(year, 3, 19), eFederalState.All));
            //        break;
            //    case eFederalState.Vorarlberg:
            //        publicHolidays.Add(new PublicHoliday("Stefanitag (Vorarlberg)", new DateTime(year, 3, 19), eFederalState.All));
            //        break;
            //    case eFederalState.Vienna:
            //        break;
            //    default:
            //        break;
            //}

            return publicHolidays.OrderBy(x => x.HolidayDate).ToList();
        }
    }
}