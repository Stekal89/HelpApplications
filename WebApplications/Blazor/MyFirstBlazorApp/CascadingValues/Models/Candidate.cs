namespace CascadingValues.Models
{
    /// <summary>
    /// ################################
    /// ### Cascading values by type ###
    /// ### For example              ###
    /// ################################
    /// </summary>
    public class Candidate
    {
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public Models.Address Address { get; set; }
    }
}
