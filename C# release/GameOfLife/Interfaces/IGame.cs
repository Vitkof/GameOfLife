using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IGame<TGrid, TRules, TCell> : IEnumerable<TGrid>
        where TGrid : IGrid<TCell>
        where TRules : IRules
    {
        TGrid Initial { get; }
    }
}
