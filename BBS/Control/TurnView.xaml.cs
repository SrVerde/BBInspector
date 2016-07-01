using BBS.Models;
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
    /// Interaction logic for TurnView.xaml
    /// </summary>
    public partial class TurnView : UserControl
    {
        public TurnView()
        {
            InitializeComponent();
        }



        public Turn Turn
        {
            get { return (Turn)GetValue(TurnProperty); }
            set { SetValue(TurnProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Turn.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TurnProperty =
            DependencyProperty.Register("Turn", typeof(Turn), typeof(TurnView), new PropertyMetadata(null));



    }
}
