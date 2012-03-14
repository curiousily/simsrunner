using System.Collections.Generic;
using System.Windows.Input;
using NaughtySpirit.SimsRunner.Domain.DomainObjects;

namespace NaughtySpirit.SimsRunner.Gui
{
    
    public partial class MainSimulationWindow
    {
        private readonly IList<Stock> _stocks = new List<Stock>();
        private readonly IList<Stock> _selectedStocks = new List<Stock>();

        public MainSimulationWindow()
        {
            InitializeComponent();
        }

        private void OnCanvasMouseUpHandler(object sender, MouseButtonEventArgs e)
        {
            if(SelectButton.IsChecked.GetValueOrDefault(false))
            {
                return;
            }

            if(StockButton.IsChecked.GetValueOrDefault(false)) {
            
                var stock = new Stock(e.GetPosition(CanvasBox));
                stock.AddToCanvas(CanvasBox);
                stock.MouseClick += OnStockMouseClickHandler;
                _stocks.Add(stock);
                stock.MouseDoubleClick += DomainObjectClickHandler;
            }

            if (FlowButton.IsChecked.GetValueOrDefault(false) && _selectedStocks.Count == 2)
            {
                var flow = new Flow(_selectedStocks[0], _selectedStocks[1]);
                flow.AddToCanvas(CanvasBox);
                flow.MouseDoubleClick += DomainObjectClickHandler;
                _selectedStocks.Clear();
            }
        }

        private void DomainObjectClickHandler(object sender)
        {
            var editWindow = new EditObjectWindow(sender);
            editWindow.ShowDialog(); 
        }

        private void OnStockMouseClickHandler(object sender)
        {
            if(FlowButton.IsChecked.GetValueOrDefault(false)) {
                _selectedStocks.Add(sender as Stock);
            }
        }
    }
}
