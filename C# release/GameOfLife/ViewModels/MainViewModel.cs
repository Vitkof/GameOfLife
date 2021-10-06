using System.Reactive;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;
using GameOfLife.Implement;
using GameOfLife.Converters;
using ReactiveUI.Fody.Helpers;
using System;

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
            Reset = ReactiveCommand.Create(ResetMethod);
        }

        [Reactive]
        public GridViewModel GridVm { get; set; }
        public ReactiveCommand<Unit,Unit> Tick { get; }
        public ReactiveCommand<Unit,Unit> Reset { get; }

        private void ResetMethod()
        {
            GridVm = new GridViewModel(InfiniteToroidalGrid.Default.FillVm());
        }

        private void TickMethod()
        {
            var newGrid = _rules.Apply(GridVm.ExtractGrid());
            GridVm = new GridViewModel(newGrid.FillVm());
        }
    }
}
