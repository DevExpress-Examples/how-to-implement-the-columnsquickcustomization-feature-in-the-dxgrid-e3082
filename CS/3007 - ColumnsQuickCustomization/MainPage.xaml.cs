using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Controls.Primitives;
using DevExpress.Xpf.Core.Native;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using System.Collections.Specialized;

namespace _3007___ColumnsQuickCustomization {
    public partial class MainPage : UserControl {

        Popup popup;
        GridColumnsInfoContainer columnsInfoContainer;

        public MainPage() {
            InitializeComponent();

            grid.AddHandler(GridControl.MouseLeftButtonUpEvent, new  MouseButtonEventHandler(grid_MouseLeftButtonUp), true);
            grid.Columns.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Columns_CollectionChanged);
        }

        void Columns_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
            columnsInfoContainer = new GridColumnsInfoContainer(grid);
        }

        private void grid_MouseLeftButtonUp(object sender, MouseEventArgs e) {
            if (popup == null) {
                initializePopup();
            }
            FrameworkElement header = LayoutHelper.FindParentObject<IndicatorColumnHeader>(e.OriginalSource as DependencyObject);
            if (header != null) {

                Point pos = e.GetPosition(this);
                popup.VerticalOffset = pos.Y;
                popup.HorizontalOffset = pos.X;
                popup.IsOpen = true;
            }
        }

        void initializePopup() {
            popup = new Popup();
            popup.IsOpen = false;
            popup.Child = new ListBox() {
                ItemsSource = columnsInfoContainer.Columns,
                ItemTemplate = (DataTemplate)this.Resources["columnCustomizerItemTemplate"]
            };

            popup.Child.MouseLeave += new MouseEventHandler(child_MouseLeave);
        }

        private void child_MouseLeave(object sender, MouseEventArgs e) {
            ((Popup)((FrameworkElement)sender).Parent).IsOpen = false;
        }

    }
    
    public class GridColumnsInfoContainer {
        List<GridColumnInfo> columns;

        public GridColumnsInfoContainer(GridControl grid) {
            columns = new List<GridColumnInfo>();
            foreach (GridColumn column in grid.Columns) {
                columns.Add( new GridColumnInfo(column));
            }
        }

        public List<GridColumnInfo> Columns {
            get {
                return columns;
            }
        }
    }

    public class GridColumnInfo {
        GridColumn column;

        public GridColumnInfo(GridColumn column) {
            this.column = column;
        }

        public GridColumn Column {
            get {
                return column;
            }
            set {
                column = value;
            }
        }

        public bool IsVisible {
            get {
                return column.Visible;
            }
            set {
                column.Visible = value;
            }
        }

        public string FieldName {
            get {
                return column.FieldName;
            }
            set {
                column.FieldName = value;
            }
        }
    }
}
