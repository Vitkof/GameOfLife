using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ReactiveUI;
using System.Reactive.Disposables;
using GameOfLife.ViewModels;

namespace GameOfLife
{
    /// <summary>
    /// Interaction logic for MyGridView.xaml
    /// </summary>
    public partial class MyGridView : ReactiveUserControl<GridViewModel>
    {
        public MyGridView()
        {
            InitializeComponent();

            this.WhenActivated(disposableRegistration =>
            {
                this.OneWayBind(
                    ViewModel,
                    vm => vm.Grid,
                    v => v.gameContentControl.Content,
                    ConvertVmToView)
                .DisposeWith(disposableRegistration);
            });
        }

        private object ConvertVmToView(CellViewModel[,] vm)
        {
            var grid = new Grid
            {
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            int rows = vm.GetLength(0);
            int columns = vm.GetLength(1);
            double rowHeight = gameContentControl.ActualHeight / rows;
            double columnWidth = gameContentControl.ActualWidth / columns;
            var rectangleSize = Math.Min(rowHeight, columnWidth);

            for (int row = 0; row < rows; row++)
            {
                grid.RowDefinitions.Add(new RowDefinition());

                for (int column = 0; column < columns; column++)
                {
                    if (row == 0)
                    {
                        grid.ColumnDefinitions.Add(new ColumnDefinition());
                    }

                    var rectangle = CreateRectangle(vm, row, column, rectangleSize, rectangleSize);

                    Grid.SetRow(rectangle, row);
                    Grid.SetColumn(rectangle, column);

                    grid.Children.Add(rectangle);
                }
            }

            return grid;
        }

        private Rectangle CreateRectangle(CellViewModel[,] grid, int row, int column, double height, double width)
        {
            var rectangle = new Rectangle
            {
                VerticalAlignment = VerticalAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                Height = height,
                Width = width,
                DataContext = grid[row, column],
                Stroke = Brushes.White,
                StrokeThickness = 0.5,
            };

            var leftClickBinding = new InputBinding(
                ViewModel.SwitchCellStatus,
                new MouseGesture(MouseAction.LeftClick))
            {
                CommandParameter = (row, column)
            };
            rectangle.InputBindings.Add(leftClickBinding);

            var aliveCellBinding = new Binding
            {
                Path = new PropertyPath("IsAlive"),
                Mode = BindingMode.TwoWay,
                Converter = CellToColorConverter.Create(
                        aliveColor: Brushes.DarkGreen,
                        deadColor: Brushes.LightGray)
            };
            rectangle.SetBinding(Rectangle.FillProperty, aliveCellBinding);

            return rectangle;
        }
    }


    public class CellToColorConverter : IValueConverter
    {
        public SolidColorBrush AliveColor { get; }
        public SolidColorBrush DeadColor { get; }

        private CellToColorConverter(
            SolidColorBrush aliveColor,
            SolidColorBrush deadColor)
        {
            AliveColor = aliveColor;
            DeadColor = deadColor;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => value is bool isAlive
                && isAlive
                ? AliveColor
                : DeadColor;
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => value is SolidColorBrush brush && brush == AliveColor;

        public static CellToColorConverter Create(
            SolidColorBrush aliveColor,
            SolidColorBrush deadColor)
            => new(aliveColor, deadColor);
    }
}
