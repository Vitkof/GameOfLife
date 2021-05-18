using GameOfLife.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GameOfLife.Implement
{
    public sealed class Rules : IRules<InfiniteToroidalGrid, Cell>
    {
        public static Rules Instance { get; } = new();
        public InfiniteToroidalGrid Apply(InfiniteToroidalGrid oldGrid)
        {
            var upGrid = new Cell[oldGrid.Rows, oldGrid.Cols];

            for(int i=0; i<oldGrid.Rows; i++)
            {
                for(int j=0; j<oldGrid.Cols; j++)
                {
                    upGrid[i, j] = ComputeCellStatus(i, j, oldGrid);
                }
            }
            return new InfiniteToroidalGrid(upGrid);
        }

        private Cell ComputeCellStatus(int i, int j, InfiniteToroidalGrid currentGrid)
        {
            var neighbours = GetNeighbours(i, j, currentGrid);
            var aliveNeighbours = neighbours.Count(neigh => neigh == Cell.Alive);
            var currCell = currentGrid.Grid[i, j];

            var upCell = currCell switch
            {
                Cell.Dead when aliveNeighbours == 3 => Cell.Alive,
                Cell.Alive when aliveNeighbours==2 || aliveNeighbours==3 => Cell.Alive,
                _ => Cell.Dead,
            };

            return upCell;
        }

        private IEnumerable<Cell> GetNeighbours(int i, int j, InfiniteToroidalGrid currentGrid)
        {
            List<Cell> lst = new List<Cell>();
            (sbyte, sbyte)[] sides = { (-1, 0), (1, 0), (0, -1), (0, 1),
                                       (-1,-1), (-1,1), (1, -1), (1, 1)};
            foreach (var (di, dj) in sides)
            {
                var normalizedI = Normalize(i + di, currentGrid.Rows);
                var normalizedJ = Normalize(j + dj, currentGrid.Cols);
                lst.Add(currentGrid.Grid[normalizedI, normalizedJ]);
            }
            return lst;

            /*for(sbyte di=-1; di<=1; di++)
            {
                for(sbyte dj=-1; dj<=1; dj++)
                {
                    if (di == 0 && dj == 0) continue;

                    var normalizedI = Normalize(i + di, currentGrid.Rows);
                    var normalizedJ = Normalize(j + dj, currentGrid.Cols);

                    yield return currentGrid.Grid[normalizedI, normalizedJ];
                }
            }*/
        }

        private int Normalize(int v, int rows)
        {
            return (v + rows) % rows;
        }

    }
}
