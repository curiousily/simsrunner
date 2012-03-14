using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows;
using System.Windows.Controls;
using NaughtySpirit.SimsRunner.Domain.Attributes;

namespace NaughtySpirit.SimsRunner.Gui
{
    /// <summary>
    /// Interaction logic for EditObjectWindow.xaml
    /// </summary>
    public partial class EditObjectWindow
    {
        private readonly object _editObject;

        private readonly IList<Label> _labels = new List<Label>(); 
        private readonly IList<TextBox> _textBoxes = new List<TextBox>(); 

        public EditObjectWindow(object editObject)
        {
            InitializeComponent();
            _editObject = editObject;
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
                var propertyValue = propertyInfo.GetValue(_editObject, null);
                if(propertyValue != null) {
                    _textBoxes[propertyIndex].Text = propertyValue.ToString();
                }
                propertyIndex++;
            }
        }

        private IEnumerable<PropertyInfo> AllEditableAttributes()
        {
            var editableType = _editObject.GetType();
            return editableType.GetProperties().Where(propertyInfo => Attribute.IsDefined(propertyInfo, typeof(EditableAttribute))).Reverse();
        }

        private void OnCancelClickHandler(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void OnSaveClickHandler(object sender, RoutedEventArgs e)
        {
            var propertyIndex = 0;
            foreach (var propertyInfo in AllEditableAttributes())
            {
                propertyInfo.SetValue(_editObject, _textBoxes[propertyIndex].Text, null);
                propertyIndex++;
            }
            Close();
        }
    }
}
