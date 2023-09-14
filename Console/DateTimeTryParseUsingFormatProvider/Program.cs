namespace DateTimeTryParseUsingFormatProvider
{
	internal class Program
	{
		static void Main()
		{
			string dateString = "2023-08-29";
			DateTime date;

			// create IFormatProvider
			//IFormatProvider formatProvider = new System.Globalization.CultureInfo("en-US");
			//IFormatProvider formatProvider = new System.Globalization.CultureInfo("de-DE");

			// DateTime.TryParse using IFormatProvider
			//if (DateTime.TryParse(dateString, formatProvider, System.Globalization.DateTimeStyles.None, out date))
			if (DateTime.TryParse(dateString, new System.Globalization.CultureInfo("de-DE"), System.Globalization.DateTimeStyles.None, out date))
			{
				Console.WriteLine("Date was successfully parsed: " + date.ToString("yyyy-MM-dd"));
			}
			else
			{
				Console.WriteLine("It was not possible to parse the date.");
			}

			Console.WriteLine("\n\nContinue with any key...");
			Console.ReadKey();
		}
	}
}