using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IGrid<TCell> : IEnumerable<TCell>
    {
        public int Rows { get; }
        public int Cols { get; }
    }
}
