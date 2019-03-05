using System;
using System.Collections.Generic;
using System.Text;
using Formula1.Core.Contracts;

namespace Formula1.Core.Entities
{
    public class TotalResultDto<T> : IComparable where T : ICompetitor
    {
        public T Competitor { get; set; }
        public int Position { get; set; }
        public int Points { get; set; }

        public int CompareTo(object obj)
        {
            TotalResultDto<T> other = obj as TotalResultDto<T>;
            return Points.CompareTo(other.Points) * (-1);
        }
    }
}
