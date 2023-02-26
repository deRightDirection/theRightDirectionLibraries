using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using theRightDirection.WPF.Controls.Json;

namespace theRightDirection.WPF.Controls
{
    /// <summary>
    /// Interaction logic for JsonViewer.xaml
    /// </summary>
    public partial class JsonViewer : UserControl
    {
        public static readonly DependencyProperty JsonProperty = DependencyProperty.Register("Json", typeof(string), typeof(JsonViewer), new PropertyMetadata(string.Empty, OnJsonSet));
        public static readonly DependencyProperty ElementNameColorProperty = DependencyProperty.Register("ElementNameColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(Brushes.Black, OnElementNameColorSet));
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(string), typeof(JsonViewer), new PropertyMetadata(null, OnTitleSet));
        public static readonly DependencyProperty SelectionColorProperty = DependencyProperty.Register("SelectionColor", typeof(Color), typeof(JsonViewer), new PropertyMetadata(Colors.Black, OnSelectionColorSet));

        public JsonViewer()
        {
            InitializeComponent();
            DataContext = this;
        }

        public Color SelectionColor
        {
            get { return (Color)GetValue(SelectionColorProperty); }
            set { SetValue(SelectionColorProperty, value); }
        }

        public SolidColorBrush ElementNameColor
        {
            get { return (SolidColorBrush)GetValue(ElementNameColorProperty); }
            set { SetValue(ElementNameColorProperty, value); }
        }

        public string Json
        {
            get { return (string)GetValue(JsonProperty); }
            set { SetValue(JsonProperty, value); }
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        private static void OnSelectionColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.SelectionColor = (Color)e.NewValue;
        }

        private static void OnJsonSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var json = (string)e.NewValue;
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            var jsonLoader = new JsonLoader();
            var viewer = (JsonViewer)d;
            viewer.boompje.Items.Clear();
            var item = jsonLoader.LoadJsonToTreeView((string)e.NewValue);
            item[0].Name = viewer.Title;
            item[0].Type = TypeOfJsonElement.FirstElement;
            item[0].ShowDivider = false;
            viewer.boompje.Items.Add(item);
        }

        private static void OnElementNameColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.ElementNameColor = (SolidColorBrush)e.NewValue;
        }

        private static void OnTitleSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.Title = (string)e.NewValue;
        }
    }
}