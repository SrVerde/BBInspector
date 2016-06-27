using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BBS.Control
{
    /// <summary>
    /// Interaction logic for ObjectExplorer.xaml
    /// </summary>
    public partial class ObjectExplorer : UserControl
    {
        public ObjectExplorer()
        {
            InitializeComponent();
        }

        public object Source
        {
            get { return (object)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ObjectSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register("Source", typeof(object), typeof(ObjectExplorer), new PropertyMetadata(null,new PropertyChangedCallback(OnSourceChanged)));


        private static void OnSourceChanged(DependencyObject sender, DependencyPropertyChangedEventArgs args)
        {
            var o = sender as ObjectExplorer;
            if (o != null)
            {
                var hierarchy = new ObjectViewModelHierarchy(args.NewValue);
                o.tvObjectGraph.DataContext = hierarchy;
            }
        }


    }
}
