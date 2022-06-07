using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Connection_XAMPP_MySQL.Model
{
    public class Person
    {
        #region Properties

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }
        
        #endregion

            #region Constructors

        public Person()
        {

        }

        public Person(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }

        #endregion

        #region Methods


     


        #endregion
    }
}
