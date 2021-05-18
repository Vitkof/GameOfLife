using GameOfLife.Interfaces;
using System.Collections;
using System.Collections.Generic;

namespace GameOfLife.Implement
{
    public sealed class InfiniteToroidalGrid : IGrid<Cell>
    {
        public InfiniteToroidalGrid(Cell[,] grid)
        {
            Grid = grid;
        }

        public int Rows => Grid.GetLength(0);

        public int Cols => Grid.GetLength(1);

        public Cell[,] Grid { get; }

        public IEnumerator<Cell> GetEnumerator()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Cols; j++)
                {
                    yield return Grid[i, j];
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();

        public static InfiniteToroidalGrid Default { get; } = new(new Cell[100, 100]);
    }
}