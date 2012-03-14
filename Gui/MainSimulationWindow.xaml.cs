using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using NaughtySpirit.SimsRunner.Domain.DomainObjects;

namespace NaughtySpirit.SimsRunner.Gui
{
    
    public partial class MainSimulationWindow : Window
    {
        private readonly IList<Stock> _stocks = new List<Stock>();
        private int _stockCount = 0;

        public MainSimulationWindow()
        {
            InitializeComponent();
        }

        private void OnCanvasMouseDownHandler(object sender, MouseButtonEventArgs e)
        {
            if(SelectButton.IsChecked.GetValueOrDefault(false))
            {
                return;
            }
            var stock = new Stock(e.GetPosition(CanvasBox));
            stock.AddToCanvas(CanvasBox);
            _stocks.Add(stock);
            _stockCount++;
            stock.MouseDoubleClick += clickedStock =>
                                          {
                                              var editWindow = new EditObjectWindow(clickedStock);
                                              editWindow.ShowDialog();
                                          };
            
            if(_stockCount == 2)
            {
                var flow = new Flow(stock, _stocks[0]);
                flow.AddToCanvas(CanvasBox);
                _stocks.Clear();
                _stockCount = 0;
            }
        }
    }
}
