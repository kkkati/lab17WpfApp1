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

namespace lab17WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для ColorIndicator.xaml
    /// </summary>
    public partial class ColorIndicator : UserControl
    {
        public static DependencyProperty ColorProperty;
        public static DependencyProperty RedProperty;
        public static DependencyProperty GreenProperty;
        public static DependencyProperty BlueProperty;
        static ColorIndicator()
        {
            // Регистрация свойств зависимости
            ColorProperty = DependencyProperty.Register("Color", typeof(ColorIndicator), typeof(ColorIndicator),
                new FrameworkPropertyMetadata(Colors.Black, new PropertyChangedCallback(OnColorChanged)));
            RedProperty = DependencyProperty.Register("Red", typeof(byte), typeof(ColorIndicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            GreenProperty = DependencyProperty.Register("Green", typeof(byte), typeof(ColorIndicator),
                new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));
            BlueProperty = DependencyProperty.Register("Blue", typeof(byte), typeof(ColorIndicator),
                 new FrameworkPropertyMetadata(new PropertyChangedCallback(OnColorRGBChanged)));

            // Регистрация маршрутизируемого события
            ColorChangedEvent = EventManager.RegisterRoutedEvent("ColorChanged", RoutingStrategy.Bubble,
            typeof(RoutedPropertyChangedEventHandler<Color>), typeof(ColorIndicator));
        }
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }
        public byte Red
        {
            get { return (byte)GetValue(RedProperty); }
            set { SetValue(RedProperty, value); }
        }
        public byte Green
        {
            get { return (byte)GetValue(GreenProperty); }
            set { SetValue(GreenProperty, value); }
        }
        public byte Blue
        {
            get { return (byte)GetValue(BlueProperty); }
            set { SetValue(BlueProperty, value); }

        }
        private static void OnColorRGBChanged(DependencyObject sender,
            DependencyPropertyChangedEventArgs e)
        {
            ColorIndicator colorIndicator = (ColorIndicator)sender;
            Color color = colorIndicator.Color;
            if (e.Property == RedProperty)
                color.R = (byte)e.NewValue;
            else if (e.Property == GreenProperty)
                color.G = (byte)e.NewValue;
            else if (e.Property == BlueProperty)
                color.B = (byte)e.NewValue;

            colorIndicator.Color = color;
        }
        private static void OnColorChanged(DependencyObject sender,
      DependencyPropertyChangedEventArgs e)
        {
            Color newColor = (Color)e.NewValue;
            ColorIndicator colorpicker = (ColorIndicator)sender;
            colorpicker.Red = newColor.R;
            colorpicker.Green = newColor.G;
            colorpicker.Blue = newColor.B;
        }
        public static readonly RoutedEvent ColorChangedEvent;
        

        public event RoutedPropertyChangedEventHandler<Color> ColorChanged
        {
            add { AddHandler(ColorChangedEvent, value); }
            remove { RemoveHandler(ColorChangedEvent, value); }
        }
        public ColorIndicator()
        {
            InitializeComponent();
        }
    }
}
