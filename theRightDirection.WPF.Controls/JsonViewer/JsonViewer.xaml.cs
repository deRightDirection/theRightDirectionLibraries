using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using theRightDirection.Library;
namespace theRightDirection.WPF.Xaml.Controls.JsonViewer
{
    /// <summary>
    /// Interaction logic for JsonViewer.xaml
    /// </summary>
    public partial class JsonViewer : UserControl
    {
        private const GeneratorStatus Generated = GeneratorStatus.ContainersGenerated;
        private DispatcherTimer _timer;

        public static readonly DependencyProperty JsonProperty =
            DependencyProperty.Register("Json", typeof(string), typeof(JsonViewer), new PropertyMetadata(null, OnJsonSet));

        public static readonly DependencyProperty LegendStringColorProperty =
            DependencyProperty.Register("LegendStringColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#4e9a06"), OnStringColorSet));

        public static readonly DependencyProperty LegendNumberColorProperty =
            DependencyProperty.Register("LegendNumberColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#ad7fa8"), OnNumberColorSet));

        public static readonly DependencyProperty LegendBooleanColorProperty =
            DependencyProperty.Register("LegendBooleanColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#c4a000"), OnBooleanColorSet));

        public static readonly DependencyProperty LegendNullColorProperty =
            DependencyProperty.Register("LegendNullColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(Brushes.OrangeRed, OnNullColorSet));
 
        public JsonViewer()
        {
            InitializeComponent();
        }
        public string Json
        {
            get { return (string)GetValue(JsonProperty); }
            set { SetValue(JsonProperty, value);}
        }
        public SolidColorBrush LegendStringColor
        {
            get { return (SolidColorBrush)GetValue(LegendStringColorProperty); }
            set { SetValue(LegendStringColorProperty, value); }
        }
        public SolidColorBrush LegendNumberColor
        {
            get { return (SolidColorBrush)GetValue(LegendNumberColorProperty); }
            set { SetValue(LegendNumberColorProperty, value); }
        }
        public SolidColorBrush LegendBooleanColor
        {
            get { return (SolidColorBrush)GetValue(LegendBooleanColorProperty); }
            set { SetValue(LegendBooleanColorProperty, value); }
        }
        public SolidColorBrush LegendNullColor
        {
            get { return (SolidColorBrush)GetValue(JsonProperty); }
            set { SetValue(JsonProperty, value); }
        }

        private static void OnJsonSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.Load((string)e.NewValue);
        }
        private static void OnBooleanColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.legendBoolean.Foreground = ((SolidColorBrush)e.NewValue);
        }
        private static void OnNumberColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.legendNumber.Foreground = ((SolidColorBrush)e.NewValue);
        }

        private static void OnStringColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.legendString.Foreground = ((SolidColorBrush)e.NewValue);
        }

        private static void OnNullColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.legendNull.Foreground = ((SolidColorBrush)e.NewValue);
        }


        private void Load(string json)
        {
            JsonTreeView.ItemsSource = null;
            JsonTreeView.Items.Clear();

            var children = new List<JToken>();

            try
            {
                var token = JToken.Parse(json);

                if (token != null)
                {
                    children.Add(token);
                }

                JsonTreeView.ItemsSource = children;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the JSON string:\r\n" + ex.Message);
            }
        }

        private void JValue_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount != 2)
                return;

            var tb = sender as TextBlock;
            if (tb != null)
            {
                Clipboard.SetText(tb.Text);
            }
        }

        private void ExpandAll(object sender, RoutedEventArgs e)
        {
            ToggleItems(true);
        }

        private void CollapseAll(object sender, RoutedEventArgs e)
        {
            ToggleItems(false);
        }

        private void ShowDoubleClickLabel(object sender, RoutedEventArgs e)
        {
            dblClickLabel.SetCurrentValue(Label.VisibilityProperty, Visibility.Visible);
        }

        private void ToggleItems(bool isExpanded)
        {
            if (JsonTreeView.Items.IsEmpty)
                return;

            var prevCursor = Cursor;
            Cursor = Cursors.Wait;
            _timer = new DispatcherTimer(TimeSpan.FromMilliseconds(500), DispatcherPriority.Normal, delegate
            {
                ToggleItems(JsonTreeView, JsonTreeView.Items, isExpanded);
                _timer.Stop();
                Cursor = prevCursor;
            }, Application.Current.Dispatcher);
            _timer.Start();
        }

        private void ToggleItems(ItemsControl parentContainer, ItemCollection items, bool isExpanded)
        {
            var itemGen = parentContainer.ItemContainerGenerator;
            if (itemGen.Status == Generated)
            {
                Recurse(items, isExpanded, itemGen);
            }
            else
            {
                itemGen.StatusChanged += delegate
                {
                    Recurse(items, isExpanded, itemGen);
                };
            }
        }

        private void Recurse(ItemCollection items, bool isExpanded, ItemContainerGenerator itemGen)
        {
            if (itemGen.Status != Generated)
                return;

            foreach (var item in items)
            {
                var tvi = itemGen.ContainerFromItem(item) as TreeViewItem;
                tvi.IsExpanded = isExpanded;
                ToggleItems(tvi, tvi.Items, isExpanded);
            }
        }
    }
}