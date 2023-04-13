using System;

namespace CreateWhereString
{
    class Program
    {
		#region Help Functions

		public static void WriteError(string msg)
		{
			ConsoleColor currentColor = Console.ForegroundColor;
			Console.ForegroundColor = ConsoleColor.DarkRed;
			Console.WriteLine($"### ERROR ###\n{msg}");
            Console.ForegroundColor = currentColor;
        }

		#endregion
		static void Main(string[] args)
        {
			LoadArtikelInfo(null, "", "Pfeffer");
			LoadArtikelInfo(null, "Oregano", "");
			LoadArtikelInfo("ART_01", "", "Super Artikel");
			LoadArtikelInfo("ART_02", "", null);
			LoadArtikelInfo("ART_03", "Reis", "Premium Reis");

            Console.WriteLine("\n##################################################\n##################################################\n");

			LoadArtikelInfo(null, "", "Pfe%");
			LoadArtikelInfo(null, "%gan%", "");
			LoadArtikelInfo("ART%", "", "Super%");
			LoadArtikelInfo("ART_02", "", null);
			LoadArtikelInfo("ART_03", "Re%", "Premium%");

			Console.WriteLine($"\n\nConitnue with any key...");
			Console.ReadKey();
        }

		private static bool LoadArtikelInfo(string artNr, string artBez, string artBez1)
		{
			string error = null;
			string er = null;
			
			//Models.ArtInfoLocals artInfo = new Models.ArtInfoLocals();
			
			// Where Bedingung zusammenstoepseln (von Controls lesen)
			string whereCondition = null;
			//bool bWhereStarted = false;
			whereCondition = $"WHERE 1=1\r\n";
			string andOr = "AND";

            whereCondition += AddWhereCondition(out er, artNr, "ArtNr", ref andOr);
            error += er;
            whereCondition += AddWhereCondition(out er, artBez, "ArtBez", ref andOr);
            error += er;
            whereCondition += AddWhereCondition(out er, artBez1, "ArtBez1", ref andOr);
            error += er;

			if (!string.IsNullOrEmpty(error))
			{
				WriteError(error);
				return false;
			}

            // Query zusammenstoepseln
            string strQuery = null;
			strQuery = $"SELECT art.art_nr\r\n" +
					   $"      ,art.art_kbez\r\n" +
					   $"      ,art.art_bez1\r\n" +
					   $"      ,art.art_bez2\r\n" +
					   $"      ,art.aid1_cd\r\n" +
					   $"      ,art.aid2_cd\r\n" +
					   $"      ,art.stlk_nr\r\n" +
					   $"      ,art.land_cd art_land_cd \r\n" +
					   $"      ,artzz.land_cd artzz_land_cd\r\n" +
					   $"      ,artzz.ztnr_nr\r\n" +
					   $"      ,ga.ga_nr\r\n" +
					   $"      ,ga.gaa_cd\r\n" +
					   $"      ,ga.ga_bez\r\n" +
					   $"FROM S_ART art\r\n" +
					   $"    LEFT JOIN S_ARTZZ artzz ON art.ART_NR = artzz.ART_NR\r\n" +
					   $"                      AND art.AID1_CD = artzz.AID1_CD\r\n" +
					   $"                      AND art.AID2_CD = artzz.AID2_CD\r\n" +
					   $"    LEFT JOIN S_GA ga      ON art.ART_NR = ga.ART_NR\r\n" +
					   $"                      AND art.AID1_CD = ga.AID1_CD\r\n" +
					   $"                      AND art.AID2_CD = ga.AID2_CD\r\n" +
					   $"{whereCondition}" +
					   $";";

            Console.WriteLine($"\n----------------------------");
            Console.WriteLine(strQuery);
			Console.WriteLine($"----------------------------\n");

			return true;
		}

		private static string AddWhereCondition(out string error, string colValue, string colName, ref string andOr)
		{
			error = null;

			string condition = null;
			if (string.IsNullOrEmpty(colName))
			{
				error += "AddWhereCondition: No ColumnName defined!\r\n";
			}
            if (!string.IsNullOrEmpty(colValue))
            {
                condition += $"{(!colValue.TrimEnd().Contains("%") ? $"    {andOr} {colName} = '{colValue.TrimEnd()}'" : $"    {andOr} {colName} LIKE '{colValue.TrimEnd()}'")}\r\n";
                andOr = "OR";
            }

			return condition;
        }
	}
}
