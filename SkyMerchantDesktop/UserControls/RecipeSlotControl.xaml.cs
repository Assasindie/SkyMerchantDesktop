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

namespace SkyMerchantDesktop.UserControls
{
    /// <summary>
    /// Interaction logic for RecipeSlotControl.xaml
    /// </summary>
    public partial class RecipeSlotControl : UserControl
    {
        public RecipeSlotControl()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty RecipeNameProperty =
            DependencyProperty.Register(
                nameof(RecipeName),
                typeof(string),
                typeof(RecipeSlotControl));

        public static readonly DependencyProperty QuantityNameProperty =
            DependencyProperty.Register(
                nameof(Quantity),
                typeof(string),
                typeof(RecipeSlotControl));

        public string RecipeName
        {
            get { return (string)GetValue(RecipeNameProperty); }
            set { SetValue(RecipeNameProperty, value); }
        }


        public string Quantity
        {
            get { return (string)GetValue(QuantityNameProperty); }
            set { SetValue(QuantityNameProperty, value); }
        }
    }
}
