using Microsoft.Win32;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
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

        public static readonly DependencyProperty ShowInfoButtonProperty =
            DependencyProperty.Register("ShowInfoButton", typeof(bool), typeof(JsonViewer), new PropertyMetadata(true, OnShowInfoButton));

        public static readonly DependencyProperty ShowLegendButtonProperty =
            DependencyProperty.Register("ShowLegendButton", typeof(bool), typeof(JsonViewer), new PropertyMetadata(true, OnShowLegendButton));

        public static readonly DependencyProperty ShowFileSaveButtonProperty =
            DependencyProperty.Register("ShowFileSaveButton", typeof(bool), typeof(JsonViewer), new PropertyMetadata(false, OnShowFileSaveButton));

        public static readonly DependencyProperty ShowTitleProperty =
            DependencyProperty.Register("ShowTitle", typeof(bool), typeof(JsonViewer), new PropertyMetadata(false, OnShowTitle));

        public static readonly DependencyProperty ShowCollapseExpandButtonsProperty =
            DependencyProperty.Register("ShowCollapseExpandButtons", typeof(bool), typeof(JsonViewer), new PropertyMetadata(true, OnShowCollapseExpandButtons));

        public static readonly DependencyProperty SelectionColorProperty =
            DependencyProperty.Register("SelectionColor", typeof(Color), typeof(JsonViewer), new PropertyMetadata(Colors.Black, OnSelectionColorSet));

        public static readonly DependencyProperty IsExpandedProperty =
            DependencyProperty.Register("IsExpanded", typeof(bool), typeof(JsonViewer), new PropertyMetadata(true, OnIsExpandedSet));

        public static readonly DependencyProperty JsonProperty =
            DependencyProperty.Register("Json", typeof(string), typeof(JsonViewer), new PropertyMetadata(string.Empty, OnJsonSet));

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(JsonViewer));

        public static readonly DependencyProperty LegendStringColorProperty =
            DependencyProperty.Register("LegendStringColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#4e9a06"), OnStringColorSet));

        public static readonly DependencyProperty LegendNumberColorProperty =
            DependencyProperty.Register("LegendNumberColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#ad7fa8"), OnNumberColorSet));

        public static readonly DependencyProperty LegendBooleanColorProperty =
            DependencyProperty.Register("LegendBooleanColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#c4a000"), OnBooleanColorSet));

        public static readonly DependencyProperty LegendNullColorProperty =
            DependencyProperty.Register("LegendNullColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(Brushes.OrangeRed, OnNullColorSet));

        public static readonly DependencyProperty ElementDividerColorProperty =
            DependencyProperty.Register("ElementDividerColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(BrushHelper.HexCodeToSolidColorBrush("#729fcf"), OnElementDividerColorSet));

        public static readonly DependencyProperty ArrayTextProperty =
            DependencyProperty.Register("ArrayText", typeof(string), typeof(JsonViewer), new PropertyMetadata("array", OnArrayTextSet));

        public static readonly DependencyProperty ElementNameColorProperty =
            DependencyProperty.Register("ElementNameColor", typeof(SolidColorBrush), typeof(JsonViewer), new PropertyMetadata(Brushes.Black, OnElementNameColorSet));

        public JsonViewer()
        {
            InitializeComponent();
            DataContext = this;
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
            get { return (SolidColorBrush)GetValue(LegendNullColorProperty); }
            set { SetValue(LegendNullColorProperty, value); }
        }
        public SolidColorBrush ElementDividerColor
        {
            get { return (SolidColorBrush)GetValue(ElementDividerColorProperty); }
            set { SetValue(ElementDividerColorProperty, value); }
        }
        public string ArrayText
        {
            get { return (string)GetValue(ArrayTextProperty); }
            set { SetValue(ArrayTextProperty, value); }
        }
        public SolidColorBrush ElementNameColor
        {
            get { return (SolidColorBrush)GetValue(ElementNameColorProperty); }
            set { SetValue(ElementNameColorProperty, value); }
        }
        public bool IsExpanded
        {
            get { return (bool)GetValue(IsExpandedProperty); }
            set { SetValue(IsExpandedProperty, value); }
        }
        public Color SelectionColor
        {
            get { return (Color)GetValue(SelectionColorProperty); }
            set { SetValue(SelectionColorProperty, value); }
        }
        public bool ShowCollapseExpandButtons
        {
            get { return (bool)GetValue(ShowCollapseExpandButtonsProperty); }
            set { SetValue(ShowCollapseExpandButtonsProperty, value); }
        }
        public bool ShowLegend
        {
            get { return (bool)GetValue(ShowLegendButtonProperty); }
            set { SetValue(ShowLegendButtonProperty, value); }
        }
        public bool ShowInformationButton
        {
            get { return (bool)GetValue(ShowInfoButtonProperty); }
            set { SetValue(ShowInfoButtonProperty, value); }
        }
        public bool ShowFileSaveButton
        {
            get { return (bool)GetValue(ShowFileSaveButtonProperty); }
            set { SetValue(ShowFileSaveButtonProperty, value); }
        }
        public bool ShowTitle
        {
            get { return (bool)GetValue(ShowTitleProperty); }
            set { SetValue(ShowTitleProperty, value); }
        }
        private static void OnJsonSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.Load((string)e.NewValue);
        }
        private static void OnBooleanColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.LegendBooleanColor = ((SolidColorBrush)e.NewValue);
        }
        private static void OnNumberColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.LegendNumberColor = ((SolidColorBrush)e.NewValue);
        }

        private static void OnStringColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.LegendStringColor = ((SolidColorBrush)e.NewValue);
        }

        private static void OnNullColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.LegendNullColor = ((SolidColorBrush)e.NewValue);
        }
        private static void OnElementDividerColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.ElementDividerColor = ((SolidColorBrush)e.NewValue);
        }
        private static void OnArrayTextSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.ArrayText = (string)e.NewValue;
        }
        private static void OnElementNameColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.ElementNameColor = ((SolidColorBrush)e.NewValue);
        }
        private static void OnIsExpandedSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.IsExpanded = (bool)e.NewValue;
        }
        private static void OnSelectionColorSet(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.SelectionColor = (Color)e.NewValue;
        }
        private static void OnShowCollapseExpandButtons(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.expandAllButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            viewer.collapseAllButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            if (!(bool)e.NewValue)
            {
                viewer.informationButton.Margin = new Thickness(0, 5, 5, 5);
            }
            CheckDockPanelVisibility(viewer);
        }
        private static void OnShowLegendButton(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.legendExpander.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            CheckDockPanelVisibility(viewer);
        }
        private static void OnShowFileSaveButton(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.saveButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            CheckDockPanelVisibility(viewer);
        }
        private static void OnShowTitle(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.titleLabel.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            CheckDockPanelVisibility(viewer);
        }
        private static void OnShowInfoButton(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var viewer = (JsonViewer)d;
            viewer.informationButton.Visibility = (bool)e.NewValue ? Visibility.Visible : Visibility.Collapsed;
            CheckDockPanelVisibility(viewer);
        }

        private static void CheckDockPanelVisibility(JsonViewer viewer)
        {
            var condition1 = viewer.informationButton.Visibility == Visibility.Collapsed;
            var condition2 = viewer.titleLabel.Visibility == Visibility.Collapsed;
            var condition3 = viewer.saveButton.Visibility == Visibility.Collapsed;
            var condition4 = viewer.legendExpander.Visibility == Visibility.Collapsed;
            var condition5 = viewer.expandAllButton.Visibility == Visibility.Collapsed;
            var condition6 = viewer.collapseAllButton.Visibility == Visibility.Collapsed;
            if (condition1 & condition2 & condition3 & condition4 & condition5 & condition6)
            {
                viewer.content.Visibility = Visibility.Collapsed;
            }
            else
            {
                viewer.content.Visibility = Visibility.Visible;
            }

        }

        private void Load(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return;
            }
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
            ToggleItems(IsExpanded);
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
            expandAllButton.IsEnabled = !isExpanded;
            collapseAllButton.IsEnabled = isExpanded;
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog
            {
                Filter = "Json file (*.json)|*.json",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };
            if (saveFileDialog.ShowDialog() == true)
            {
                File.WriteAllText(saveFileDialog.FileName, Json);
            }
        }
    }
}