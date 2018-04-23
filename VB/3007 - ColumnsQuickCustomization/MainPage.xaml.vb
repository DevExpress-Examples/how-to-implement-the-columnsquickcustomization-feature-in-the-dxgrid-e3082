Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Net
Imports System.Windows
Imports System.Windows.Controls
Imports System.Windows.Documents
Imports System.Windows.Input
Imports System.Windows.Media
Imports System.Windows.Media.Animation
Imports System.Windows.Shapes
Imports System.Windows.Controls.Primitives
Imports DevExpress.Xpf.Core.Native
Imports DevExpress.Xpf.Grid
Imports DevExpress.Xpf.Editors
Imports System.Collections.Specialized

Namespace _3007___ColumnsQuickCustomization
	Partial Public Class MainPage
		Inherits UserControl

		Private popup As Popup
		Private columnsInfoContainer As GridColumnsInfoContainer

		Public Sub New()
			InitializeComponent()

			grid.AddHandler(GridControl.MouseLeftButtonUpEvent, New MouseButtonEventHandler(AddressOf grid_MouseLeftButtonUp), True)
			AddHandler grid.Columns.CollectionChanged, AddressOf Columns_CollectionChanged
		End Sub

		Private Sub Columns_CollectionChanged(ByVal sender As Object, ByVal e As NotifyCollectionChangedEventArgs)
			columnsInfoContainer = New GridColumnsInfoContainer(grid)
		End Sub

		Private Sub grid_MouseLeftButtonUp(ByVal sender As Object, ByVal e As MouseEventArgs)
			If popup Is Nothing Then
				initializePopup()
			End If
			Dim header As FrameworkElement = LayoutHelper.FindParentObject(Of IndicatorColumnHeader)(TryCast(e.OriginalSource, DependencyObject))
			If header IsNot Nothing Then

				Dim pos As Point = e.GetPosition(Me)
				popup.VerticalOffset = pos.Y
				popup.HorizontalOffset = pos.X
				popup.IsOpen = True
			End If
		End Sub

		Private Sub initializePopup()
			popup = New Popup()
			popup.IsOpen = False
			popup.Child = New ListBox() With {.ItemsSource = columnsInfoContainer.Columns, .ItemTemplate = CType(Me.Resources("columnCustomizerItemTemplate"), DataTemplate)}

			AddHandler popup.Child.MouseLeave, AddressOf child_MouseLeave
		End Sub

		Private Sub child_MouseLeave(ByVal sender As Object, ByVal e As MouseEventArgs)
			CType((CType(sender, FrameworkElement)).Parent, Popup).IsOpen = False
		End Sub

	End Class

	Public Class GridColumnsInfoContainer
		Private columns_Renamed As List(Of GridColumnInfo)

		Public Sub New(ByVal grid As GridControl)
			columns_Renamed = New List(Of GridColumnInfo)()
			For Each column As GridColumn In grid.Columns
				columns_Renamed.Add(New GridColumnInfo(column))
			Next column
		End Sub

		Public ReadOnly Property Columns() As List(Of GridColumnInfo)
			Get
				Return columns_Renamed
			End Get
		End Property
	End Class

	Public Class GridColumnInfo
		Private column_Renamed As GridColumn

		Public Sub New(ByVal column As GridColumn)
			Me.column_Renamed = column
		End Sub

		Public Property Column() As GridColumn
			Get
				Return column_Renamed
			End Get
			Set(ByVal value As GridColumn)
				column_Renamed = value
			End Set
		End Property

		Public Property IsVisible() As Boolean
			Get
				Return column_Renamed.Visible
			End Get
			Set(ByVal value As Boolean)
				column_Renamed.Visible = value
			End Set
		End Property

		Public Property FieldName() As String
			Get
				Return column_Renamed.FieldName
			End Get
			Set(ByVal value As String)
				column_Renamed.FieldName = value
			End Set
		End Property
	End Class
End Namespace
