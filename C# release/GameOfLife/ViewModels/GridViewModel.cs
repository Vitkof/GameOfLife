using ReactiveUI;
using System.Diagnostics;
using System.Reactive;
using GameOfLife.Implement;
using GameOfLife.Converters;

namespace GameOfLife.ViewModels
{
    public class GridViewModel : ReactiveObject
    {
        public GridViewModel(CellViewModel[,] grid)
        {
            Grid = grid;
            Rows = grid.GetLength(0);
            Cols = grid.GetLength(1);
            SwitchCellStatus = ReactiveCommand.Create<(int width, int height), Unit>(SwitchMethod);
        }

        public CellViewModel[,] Grid { get; }
        public int Rows { get; }
        public int Cols { get; }
        public ReactiveCommand<(int,int),Unit> SwitchCellStatus { get; }

        private Unit SwitchMethod((int w,int h) t)
        {
            var prevStatus = Grid[t.h, t.w].IsAlive;
            Grid[t.h, t.w].IsAlive = !prevStatus;
            Debug.WriteLine($"Height: {t.h}. Width: {t.w}. Previous: {prevStatus}. " +
                $"Current: {Grid[t.h,t.w].IsAlive}");
            return Unit.Default;
        }
    }
}