<!-- default file list -->
*Files to look at*:

* [MainPage.xaml](./CS/3007 - ColumnsQuickCustomization/MainPage.xaml) (VB: [MainPage.xaml](./VB/3007 - ColumnsQuickCustomization/MainPage.xaml))
* [MainPage.xaml.cs](./CS/3007 - ColumnsQuickCustomization/MainPage.xaml.cs) (VB: [MainPage.xaml](./VB/3007 - ColumnsQuickCustomization/MainPage.xaml))
* [PersonsDataSource.cs](./CS/3007 - ColumnsQuickCustomization/PersonsDataSource.cs) (VB: [PersonsDataSource.vb](./VB/3007 - ColumnsQuickCustomization/PersonsDataSource.vb))
<!-- default file list end -->
# How to implement the ColumnsQuickCustomization feature in the DXGrid


<p>This example shows how to implement the ColumnsQuickCustomization feature in the DXGrid for Silverlight. The main idea is to handle the MouseDown event on the GridControl and dynamically create and open a popup if a click occurs inside the IndicatorColumnHeader.</p>


<h3>Description</h3>

<p>The main idea is to handle the MouseDown event on the GridControl and dynamically create and open a popup, if a click occurs inside the IndicatorColumnHeader. The popup contains a ListBox with the list of columns in the grid. Due to Silverlight specificity, it is impossible to bind the ListBox directly to the grid&#39;s columns collection. That&#39;s why it is necessary to create a wrapper for this collection that will provide access to a column&#39;s properties and serve as the ItemsSource for popup ListBox. There is a <strong>GridColumnsInfoContainer</strong> in this example, serving a columns wrapper.</p>

<br/>


