using System;
using System.Reactive;
using ReactiveUI;
using GameOfLife.Implement;
using GameOfLife.Converters;
using ReactiveUI.Fody.Helpers;
using System.Threading;

namespace GameOfLife.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        private readonly Rules _rules;
        public MainViewModel(Rules rules)
        {
            _rules = rules;
            GridVm = new GridViewModel(InfiniteToroidalGrid.Default.FillVm());
            Tick = ReactiveCommand.Create(TickMethod);
            Cycle = ReactiveCommand.Create(CycleMethod);
            Reset = ReactiveCommand.Create(ResetMethod);
        }

        [Reactive]
        public GridViewModel GridVm { get; set; }
        public ReactiveCommand<Unit, Unit> Tick { get; }
        public ReactiveCommand<Unit, Unit> Cycle { get; }
        public ReactiveCommand<Unit, Unit> Reset { get; }

        private void ResetMethod()
        {
            GridVm = new GridViewModel(InfiniteToroidalGrid.Default.FillVm());
        }

        private void TickMethod()
        {
            var newGrid = _rules.Apply(GridVm.ExtractGrid());
            GridVm = new GridViewModel(newGrid.FillVm());
        }

        private void CycleMethod()
        {
            for(int i=0; i<10; i++)
            {
                TickMethod();
                Thread.Sleep(300);
            }
        }
    }
}
