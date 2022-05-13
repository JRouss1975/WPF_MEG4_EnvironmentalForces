using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
using MathNet.Numerics;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using OxyPlot.Wpf;

namespace WPF_MEG4_EnvironmentalForces
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        public ForceCalculation fc = new ForceCalculation();

        public MainWindow()
        {
            InitializeComponent();
         
            fc.vesselType = VesselType.Tanker;
            fc.loadingCondition = LoadingCondition.Loaded;

            fc.DWT = 280000;
            fc.T = 22.3;
            fc.Wd = 24.5;

            fc.θw = 150;
            fc.h = 20;
            fc.vw = 34;

            fc.θc = 170;
            fc.s = 0;
            fc.vc = 1.03;

            fc.AL = 3160;
            fc.AT = 1130;
            fc.LBP = 325;


            this.DataContext = this.fc;
        }

        private void menuNew_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuOpen_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuSave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuExportImage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void menuExit_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cbVesselType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if ((sender as ComboBox).SelectedItem != null)
            {

                VesselType vt = (VesselType)(sender as ComboBox).SelectedItem;
                if (spBowShape != null)
                {
                    if (vt == VesselType.GasCarrier)
                    {
                        spBowShape.Visibility = Visibility.Collapsed;
                        spTankShape.Visibility = Visibility.Visible;
                        fc.tankShape = TankShape.Prismatic;
                    }

                    if (vt == VesselType.Tanker)
                    {
                        spBowShape.Visibility = Visibility.Visible;
                        spTankShape.Visibility = Visibility.Collapsed;
                        fc.bowShape = BowShape.Cylindrical;
                    }
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(fc.VesselName.ToString() + "---" + fc.vesselType.ToString() + "---" + fc.tankShape.ToString() + "--" + fc.bowShape.ToString() + "--" + fc.loadingCondition.ToString());
        }



    }

    public class SelectAllFocusBehavior
    {
        public static bool GetEnable(FrameworkElement frameworkElement)
        {
            return (bool)frameworkElement.GetValue(EnableProperty);
        }

        public static void SetEnable(FrameworkElement frameworkElement, bool value)
        {
            frameworkElement.SetValue(EnableProperty, value);
        }

        public static readonly DependencyProperty EnableProperty = DependencyProperty.RegisterAttached("Enable", typeof(bool), typeof(SelectAllFocusBehavior), new FrameworkPropertyMetadata(false, OnEnableChanged));

        private static void OnEnableChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var frameworkElement = d as FrameworkElement;
            if (frameworkElement == null) return;

            if (e.NewValue is bool == false) return;

            if ((bool)e.NewValue)
            {
                frameworkElement.GotFocus += SelectAll;
                frameworkElement.PreviewMouseDown += IgnoreMouseButton;
            }
            else
            {
                frameworkElement.GotFocus -= SelectAll;
                frameworkElement.PreviewMouseDown -= IgnoreMouseButton;
            }
        }

        private static void SelectAll(object sender, RoutedEventArgs e)
        {
            var frameworkElement = e.OriginalSource as FrameworkElement;
            if (frameworkElement is TextBox)
                ((System.Windows.Controls.Primitives.TextBoxBase)frameworkElement).SelectAll();
            else if (frameworkElement is PasswordBox)
                ((PasswordBox)frameworkElement).SelectAll();
        }

        private static void IgnoreMouseButton(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var frameworkElement = sender as FrameworkElement;
            if (frameworkElement == null || frameworkElement.IsKeyboardFocusWithin) return;
            e.Handled = true;
            frameworkElement.Focus();
        }
    }
}