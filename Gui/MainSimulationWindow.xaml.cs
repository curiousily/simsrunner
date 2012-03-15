using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using NaughtySpirit.SimsRunner.Domain.DomainObjects;
using NaughtySpirit.SimsRunner.Domain.Services.Simulation;

namespace NaughtySpirit.SimsRunner.Gui
{
    
    public partial class MainSimulationWindow
    {
        public delegate void EnableSelectionHandler();
        public event EnableSelectionHandler EnableSelection;

        public delegate void DisableSelectionHandler();
        public event DisableSelectionHandler DisableSelection;

        private readonly IList<BaseDomainObject> _flows = new List<BaseDomainObject>();
        private readonly IList<Stock> _selectedStocks = new List<Stock>();     

        public MainSimulationWindow()
        {
            InitializeComponent();
        }

        public void OnEnableSelection()
        {
            var handler = EnableSelection;
            if (handler != null) handler();
        }

        public void OnDisableSelection()
        {
            var handler = DisableSelection;
            if (handler != null) handler();
        }

        private void OnCanvasClickHandler(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            if(ShouldSelect())
            {
                return;
            }

            if(ShouldAddStock())
            {
                var midPoint = mouseButtonEventArgs.GetPosition(CanvasBox);
                AddStock(midPoint);
            }

            if (ShouldAddFlow())
            {
                AddFlow();
            }
        }

        private void AddStock(Point midPoint)
        {
            var stock = new Stock(midPoint);
            stock.AddToCanvas(CanvasBox);
            stock.MouseClick += OnStockMouseClickHandler;
            stock.MouseDoubleClick += DomainObjectClickHandler;
            EnableSelection += stock.OnEnableSelectionHandler;
            DisableSelection += stock.OnDisableSelectionHandler;
//            _flows.Add(stock);            
        }

        private void AddFlow()
        {
            var flow = new Flow(_selectedStocks[0], _selectedStocks[1]);
            flow.AddToCanvas(CanvasBox);
            flow.MouseDoubleClick += DomainObjectClickHandler;
            EnableSelection += flow.OnEnableSelectionHandler;
            DisableSelection += flow.OnDisableSelectionHandler;
            _flows.Add(flow);
            _selectedStocks.Clear();
        }

        private bool ShouldSelect()
        {
            return SelectButton.IsChecked.GetValueOrDefault(false);
        }

        private bool ShouldAddStock()
        {
            return StockButton.IsChecked.GetValueOrDefault(false);
        }

        private bool ShouldAddFlow()
        {
            return FlowButton.IsChecked.GetValueOrDefault(false) && _selectedStocks.Count == 2;
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

        private void OnRunClickHandler(object sender, RoutedEventArgs routedEventArgs)
        {
            var formula = "";
            foreach(var flow in _flows)
            {
                formula += flow.GetFormula();
            }
            MessageBox.Show("Formula: " + formula);
            var time = Int32.Parse(TimeBox.Text);
            var step = Int32.Parse(StepBox.Text);
            ISimulation simulation = new EulerSimulation(formula, time, step);
            simulation.Run();
            MessageBox.Show("Simulation Complete!");
        }

        private void OnSelectClickHandler(object sender, RoutedEventArgs e)
        {
            OnEnableSelection();
        }

        private void OnToolSelectHandler(object sender, RoutedEventArgs e)
        {
            OnDisableSelection();
        }
    }
}
