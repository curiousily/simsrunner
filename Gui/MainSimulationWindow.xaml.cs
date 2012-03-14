using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using NaughtySpirit.SimsRunner.Domain;

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
                                              var editWindow = new EditObjectWindow();
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
