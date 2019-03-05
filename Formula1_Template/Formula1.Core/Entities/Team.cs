using Formula1.Core.Contracts;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Formula1.Core.Entities
{
    public class Team : ICompetitor
    {
        public string Name { get; set; }
        public string Nationality { get; set; }

        public override bool Equals(object obj)
        {
            Team other = obj as Team;
            return Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {
            int hash = 49;
            hash = hash * Name.GetHashCode();
            return hash;
        }
    }
}
