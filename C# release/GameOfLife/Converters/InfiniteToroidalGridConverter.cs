using GameOfLife.Implement;
using GameOfLife.ViewModels;

namespace GameOfLife.Converters
{
    public static class InfiniteToroidalGridConverter
    {
        public static CellViewModel[,] FillVm(this InfiniteToroidalGrid grid)
        {
            var cvm = new CellViewModel[grid.Rows, grid.Cols];
            for(int i=0; i<grid.Rows; i++)
            {
                for(int j=0; j<grid.Cols; j++)
                {
                    cvm[i, j] = new CellViewModel
                    {
                        IsAlive = grid.Grid[i, j] == Cell.Alive
                    };
                }
            }

            return cvm;
        }

        public static InfiniteToroidalGrid ExtractGrid(this GridViewModel gvm)
        {
            var cells = new Cell[gvm.Rows, gvm.Cols];
            for(int i=0; i<gvm.Rows; i++)
            {
                for(int j=0; j<gvm.Cols; j++)
                {
                    cells[i, j] = gvm.Grid[i, j].IsAlive
                        ? Cell.Alive
                        : Cell.Dead;
                }
            }

            return new InfiniteToroidalGrid(cells);
        }
    }
}
