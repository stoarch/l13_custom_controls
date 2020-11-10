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

namespace L13WPFNetControls 
{
    /// <summary>
    /// Interaction logic for ColorPicker.xaml
    /// </summary>
    public partial class ColorPicker : UserControl
    {
        public static RoutedEvent ColorChangedEvent;

        public static DependencyProperty ColorProperty { get; set; }

        public static DependencyProperty RedProperty { get; set; }
        public static DependencyProperty GreenProperty { get; set; }
        public static DependencyProperty BlueProperty { get; set; }

        public Color Color { get { return (Color)GetValue(ColorProperty); } set {SetValue(ColorProperty, value); } }
        public byte Red { get { return (byte)GetValue(RedProperty); } set { SetValue(RedProperty, value); } }
        public byte Green { get { return (byte)GetValue(GreenProperty); } set { SetValue(GreenProperty, value); } }
        public byte Blue { get { return (byte)GetValue(BlueProperty); } set { SetValue(BlueProperty, value); } }


        static ColorPicker()
        {
            ColorProperty = DependencyProperty.Register("Color",
                typeof(Color), 
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    Colors.Black, 
                    new PropertyChangedCallback(OnColorChanged)));

            RedProperty = DependencyProperty.Register("Red", 
                typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    new PropertyChangedCallback(OnColorRGBChanged)));

            GreenProperty = DependencyProperty.Register("Green", 
                typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    new PropertyChangedCallback(OnColorRGBChanged)));

            BlueProperty = DependencyProperty.Register("Blue", 
                typeof(byte),
                typeof(ColorPicker),
                new FrameworkPropertyMetadata(
                    new PropertyChangedCallback(OnColorRGBChanged)));

            ColorChangedEvent = EventManager.RegisterRoutedEvent(
                "ColorChanged", 
                RoutingStrategy.Bubble, 
                typeof(RoutedPropertyChangedEventHandler<Color>),
                typeof(ColorPicker)); 
        }

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add
            {
                AddHandler(ColorChangedEvent, value);
            }
            remove
            {
                RemoveHandler(ColorChangedEvent, value);
            }
        }

        private static void OnColorRGBChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var picker = (ColorPicker)sender;
            var color = picker.Color;
            if(e.Property == RedProperty)
            {
                color.R = (byte)e.NewValue;
                picker.Color = color;
                return;
            }

            if(e.Property == GreenProperty)
            {
                color.G = (byte)e.NewValue;
                picker.Color = color;
                return;
            }

            if(e.Property == BlueProperty)
            {
                color.B = (byte)e.NewValue;
                picker.Color = color;
                return;
            }
        }

        private static void OnColorChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var newColor = (Color)e.NewValue;
            var oldColor = (Color)e.OldValue;

            var picker = (ColorPicker)sender;
            picker.Red = newColor.R;
            picker.Green = newColor.G;
            picker.Blue = newColor.B;

            var args = new RoutedPropertyChangedEventArgs<Color>(oldColor, newColor);
            args.RoutedEvent = ColorPicker.ColorChangedEvent;
            picker.RaiseEvent(args); 
        }

        public ColorPicker()
        {
            InitializeComponent();
        }
    }
}
