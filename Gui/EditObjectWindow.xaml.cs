using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using NaughtySpirit.SimsRunner.Domain;
using NaughtySpirit.SimsRunner.Domain.Attributes;

namespace NaughtySpirit.SimsRunner.Gui
{
    /// <summary>
    /// Interaction logic for EditObjectWindow.xaml
    /// </summary>
    public partial class EditObjectWindow : Window
    {
        private readonly IEditable _editable;

        private IList<Label> _labels = new List<Label>(); 
        private IList<TextBox> _textBoxes = new List<TextBox>(); 

        public EditObjectWindow(IEditable editable)
        {
            InitializeComponent();
            _editable = editable;
            _labels.Add(Label1);
            _labels.Add(Label2);
            _textBoxes.Add(TextBox1);
            _textBoxes.Add(TextBox2);
            TextBox1.Focus();
            SetLabelContents();
        }

        private void SetLabelContents()
        {
            var propertyIndex = 0;
            foreach (var propertyInfo in AllEditableAttributes())
            {
                _labels[propertyIndex].Content = propertyInfo.Name;
                propertyIndex++;
            }
        }

        private IEnumerable<PropertyInfo> AllEditableAttributes()
        {
            var editableType = _editable.GetType();
            return editableType.GetProperties().Where(propertyInfo => Attribute.IsDefined(propertyInfo, typeof(EditableAttribute))).Reverse();
        }
    }
}
