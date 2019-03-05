using Formula1.Core.Contracts;
using System.ComponentModel.DataAnnotations;

namespace Formula1.Core.Entities
{
    public class Driver : ICompetitor
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Nationality { get; set; }
        public string Name
        {
            get
            {
                return LastName + " " + FirstName;
            }
        }

        public override bool Equals(object obj)
        {
            Driver other = obj as Driver;
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            int hash = 57;
            hash = hash * Name.GetHashCode();
            return hash;
        }

    }
}
