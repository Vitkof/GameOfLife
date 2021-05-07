using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Interfaces
{
    public interface IRules<TGrid, TCell>
        where TGrid : IGrid<TCell>
    {
        TGrid Apply(TGrid oldGrid);
    }
}
