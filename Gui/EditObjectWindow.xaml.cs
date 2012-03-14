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
using System.Windows.Shapes;

namespace NaughtySpirit.SimsRunner.Gui
{
    /// <summary>
    /// Interaction logic for EditObjectWindow.xaml
    /// </summary>
    public partial class EditObjectWindow : Window
    {
        public EditObjectWindow()
        {
            InitializeComponent();
            NameBox.Focus();
        }
    }
}
