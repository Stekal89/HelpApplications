using System.Data;

namespace WPFMVVMMultiSelect.Models
{
    public class TestData
    {
        private readonly Dictionary<int, string> _firstNames = new Dictionary<int, string>()
        {
            { 1, "John" },
            { 2, "Jane" },
            { 3, "Michael" },
            { 4, "Sarah" },
            { 5, "David" },
            { 6, "Emily" },
            { 7, "Daniel" },
            { 8, "Emma" },
            { 9, "Matthew" },
            { 10, "Olivia" },
            { 11, "Joshua" },
            { 12, "Sophia" },
            { 13, "Andrew" },
            { 14, "Isabella" },
            { 15, "James" },
            { 16, "Mia" },
            { 17, "Christopher" },
            { 18, "Charlotte" },
            { 19, "Joseph" },
            { 20, "Amelia" },
            { 21, "Samuel" },
            { 22, "Harper" },
            { 23, "Benjamin" },
            { 24, "Evelyn" },
            { 25, "Alexander" },
            { 26, "Abigail" },
            { 27, "William" },
            { 28, "Ella" },
            { 29, "Ethan" },
            { 30, "Avery" },
            { 31, "Jacob" },
            { 32, "Scarlett" },
            { 33, "Logan" },
            { 34, "Grace" },
            { 35, "Lucas" },
            { 36, "Chloe" },
            { 37, "Jackson" },
            { 38, "Victoria" },
            { 39, "Aiden" },
            { 40, "Riley" },
            { 41, "Henry" },
            { 42, "Aria" },
            { 43, "Sebastian" },
            { 44, "Lily" },
            { 45, "Jack" },
            { 46, "Aubrey" },
            { 47, "Owen" },
            { 48, "Zoey" },
            { 49, "Gabriel" },
            { 50, "Penelope" }
        };

        private readonly Dictionary<int, string> _lastNames = new Dictionary<int, string>()
        {
            { 1, "Smith" }, 
            { 2, "Johnson" }, 
            { 3, "Williams" }, 
            { 4, "Brown" }, 
            { 5, "Jones" },
            { 6, "Garcia" }, 
            { 7, "Miller" }, 
            { 8, "Davis" }, 
            { 9, "Rodriguez" }, 
            { 10, "Martinez" },
            { 11, "Hernandez" }, 
            { 12, "Lopez" }, 
            { 13, "Gonzalez" }, 
            { 14, "Wilson" }, 
            { 15, "Anderson" },
            { 16, "Thomas" }, 
            { 17, "Taylor" }, 
            { 18, "Moore" }, 
            { 19, "Jackson" }, 
            { 20, "Martin" },
            { 21, "Lee" }, 
            { 22, "Perez" }, 
            { 23, "Thompson" }, 
            { 24, "White" }, 
            { 25, "Harris" },
            { 26, "Sanchez" }, 
            { 27, "Clark" }, 
            { 28, "Ramirez" }, 
            { 29, "Lewis" }, 
            { 30, "Robinson" },
            { 31, "Walker" }, 
            { 32, "Young" }, 
            { 33, "Allen" }, 
            { 34, "King" }, 
            { 35, "Wright" },
            { 36, "Scott" }, 
            { 37, "Torres" },
            { 38, "Nguyen" }, 
            { 39, "Hill" }, 
            { 40, "Flores" },
            { 41, "Green" }, 
            { 42, "Adams" }, 
            { 43, "Nelson" }, 
            { 44, "Baker" }, 
            { 45, "Hall" },
            { 46, "Rivera" }, 
            { 47, "Campbell" }, 
            { 48, "Mitchell" }, 
            { 49, "Carter" }, 
            { 50, "Roberts" }
        };

        public DataTable Table { get; set; }

        public TestData()
        {
            Table = CreateRandomTestData(500);
        }

        public DataTable CreateRandomTestData(int numberOfRows)
        {
            DataTable table = new DataTable();
            table.Columns.Add("Id", typeof(int));
            table.Columns.Add("LastName", typeof(string));
            table.Columns.Add("FirstName", typeof(string));
            table.Columns.Add("BirthDate", typeof(DateTime));
            table.Columns.Add("Age", typeof(int));

            Random random = new Random();
            for (int i = 0; i < numberOfRows; i++)
            {
                DataRow row = table.NewRow();
                row["Id"] = i + 1;
                row["LastName"] = _lastNames[random.Next(1, 51)];
                row["FirstName"] = _firstNames[random.Next(1, 51)];
                DateTime birthDate = DateTime.Now.AddYears(-random.Next(1, 100));
                row["BirthDate"] = birthDate;
                row["Age"] = DateTime.Now.Year - birthDate.Year;
                table.Rows.Add(row);
            }

            return table;
        }
    }
}
