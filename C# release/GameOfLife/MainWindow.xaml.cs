using System.Drawing;
using System.Reactive.Disposables;
using GameOfLife.ViewModels;
using ReactiveUI;
using Splat;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ReactiveWindow<MainViewModel>
    {
        public MainWindow()
        {
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;

            InitializeComponent();

            ViewModel = Locator.Current.GetService<MainViewModel>();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(
                    ViewModel,
                    vm => vm.GridVm,
                    v => v.gameGridView.ViewModel)
                    .DisposeWith(disposableRegistration);

                this.OneWayBind(
                    ViewModel,
                    vm => vm.GridVm,
                    v => v.gameGridSize.Content,
                    vm => $"{vm.Rows} x {vm.Cols}")
                    .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vm => vm.Tick,
                    v => v.tickButton)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vm => vm.Cycle,
                    v => v.cycleButton)
                    .DisposeWith(disposableRegistration);

                this.BindCommand(
                    ViewModel,
                    vm => vm.Reset,
                    v => v.resetButton)
                    .DisposeWith(disposableRegistration);
            });
        }
    }
}
