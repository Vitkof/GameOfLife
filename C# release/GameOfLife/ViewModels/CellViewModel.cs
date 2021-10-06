using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using System.Diagnostics;

namespace GameOfLife.ViewModels
{
    [DebuggerDisplay("{IsAlive, nq}")]
    public class CellViewModel : ReactiveObject
    {
        [Reactive]
        public bool IsAlive { get; set; }
    }
}