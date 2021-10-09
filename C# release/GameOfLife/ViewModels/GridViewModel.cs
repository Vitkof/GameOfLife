using ReactiveUI;
using System.Diagnostics;
using System.Reactive;

namespace GameOfLife.ViewModels
{
    public class GridViewModel : ReactiveObject
    {
        public CellViewModel[,] Grid { get; }
        public int Rows { get; }
        public int Cols { get; }
        public ReactiveCommand<(int, int), Unit> SwitchCellStatus { get; }
        public ReactiveCommand<(int, int)[], Unit> SelectSetCells { get; }


        public GridViewModel(CellViewModel[,] grid)
        {
            Grid = grid;
            Rows = grid.GetLength(0);
            Cols = grid.GetLength(1);
            SwitchCellStatus = ReactiveCommand.Create<(int w, int h), Unit>(SwitchMethod);
            SelectSetCells = ReactiveCommand.Create<(int w, int h)[], Unit>(SelectSet);
        }
       

        private Unit SwitchMethod((int w, int h) t)
        {
            bool prevStatus = Grid[t.w, t.h].IsAlive;
            Grid[t.w, t.h].IsAlive = !prevStatus;
            Debug.WriteLine($"Width: {t.w}. Height: {t.h}. " +
                            $"Previous: {prevStatus}. " +
                            $"Current: {Grid[t.w, t.h].IsAlive}");
            return Unit.Default;
        }

        private Unit SelectSet((int w, int h)[] arg)
        {
            throw new System.NotImplementedException();
        }
    }
}