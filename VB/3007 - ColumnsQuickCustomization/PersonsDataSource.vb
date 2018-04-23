Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Collections.ObjectModel
Imports System.ComponentModel

Namespace _3007___ColumnsQuickCustomization
	Public Class PersonsViewModel
		Private persons_Renamed As ObservableCollection(Of Person)

		Public Sub New()
			Dim Names As New List(Of String) (New String() {"Alex", "Alice", "Tony", "Den", "Andrew", "John", "Donald", "Brian", "Effy", "Lisa", "Matthew"})
			persons_Renamed = New ObservableCollection(Of Person)()
			For i As Integer = 0 To 9
				persons_Renamed.Add(New Person(Names(i), "Last name " & i, i, 170 + i, 75 + i))
			Next i
		End Sub

		Public ReadOnly Property ThemeNames() As List(Of String)
			Get
				Return New List(Of String) (New String() {"DeepBlue", "Office2007Blue", "Office2007Silver", "Office2007Black", "LightGray"})
			End Get
		End Property
		Public ReadOnly Property Persons() As ObservableCollection(Of Person)
			Get
				Return persons_Renamed
			End Get
		End Property
	End Class

	Public Class Person
		Implements INotifyPropertyChanged, IDataErrorInfo
		Private Const lastNamePropertyName As String = "LastName"
		Private Const firstNamePropertyName As String = "FirstName"
		Private Const agePropertyName As String = "Age"
		Private Const heightPropertyName As String = "Height"
		Private Const weightPtropertyName As String = "Weight"

		Private firstName_Renamed As String
		Private lastName_Renamed As String
		Private age_Renamed As Integer
		Private height_Renamed As Integer
		Private weight_Renamed As Integer
		Private Validator As New PersonPropertiesValidator()

		Public Sub New(ByVal firstName As String, ByVal lastName As String, ByVal age As Integer, ByVal height As Integer, ByVal weight As Integer)
			Me.FirstName = firstName
			Me.LastName = lastName
			Me.Age = age
			Me.Weight = weight
			Me.Height = height
		End Sub

		Public Property FirstName() As String
			Get
				Return firstName_Renamed
			End Get
			Set(ByVal value As String)
				Validator.IsNameValid(value, firstNamePropertyName)
				firstName_Renamed = value
				RaisePropertyChanged(firstNamePropertyName)
			End Set
		End Property

		Public Property LastName() As String
			Get
				Return lastName_Renamed
			End Get
			Set(ByVal value As String)
				Validator.IsNameValid(value, lastNamePropertyName)
				lastName_Renamed = value
				RaisePropertyChanged(lastNamePropertyName)
			End Set
		End Property

		Public Property Age() As Integer
			Get
				Return age_Renamed
			End Get
			Set(ByVal value As Integer)
				Validator.IsAgeValid(value, agePropertyName)
				age_Renamed = value
				RaisePropertyChanged(agePropertyName)
			End Set
		End Property

		Public Property Height() As Integer
			Get
				Return height_Renamed
			End Get
			Set(ByVal value As Integer)
				height_Renamed = value
				RaisePropertyChanged(heightPropertyName)
			End Set
		End Property

		Public Property Weight() As Integer
			Get
				Return weight_Renamed
			End Get
			Set(ByVal value As Integer)
				weight_Renamed = value
				RaisePropertyChanged(weightPtropertyName)
			End Set
		End Property

		#Region "INotifyPropertyChanged members"
		Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

		Private Sub RaisePropertyChanged(ByVal propertyName As String)
			RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
		End Sub
		#End Region

		#Region "IDataErrorInfo members"
		Public ReadOnly Property [Error]() As String Implements IDataErrorInfo.Error
			Get
				Return Validator.DataError
			End Get
		End Property

		Default Public ReadOnly Property Item(ByVal columnName As String) As String Implements IDataErrorInfo.Item
			Get
				If Validator.DataErrors.ContainsKey(columnName) Then
					Return Validator.DataErrors(columnName)
				Else
					Return Nothing
				End If
			End Get
		End Property
		#End Region
	End Class

	Public Class PersonPropertiesValidator
		Private dataError_Renamed As String = ""
		Private dataErrors_Renamed As New Dictionary(Of String, String)()

		Public Function IsNameValid(ByVal value As String, ByVal propertyName As String) As Boolean
			If String.IsNullOrEmpty(value) Then
				dataErrors_Renamed(propertyName) = "Full name is required."
				Return False
			Else
				ClearPropertyErrors(propertyName)
				Return True
			End If
		End Function

		Public Function IsAgeValid(ByVal value As Integer, ByVal propertyName As String) As Boolean
			If value <= 0 Then
				dataErrors_Renamed(propertyName) = "Age validation failed."
				Return False
			Else
				ClearPropertyErrors(propertyName)
				Return True
			End If
		End Function

		Public ReadOnly Property DataError() As String
			Get
				Return dataError_Renamed
			End Get
		End Property

		Public ReadOnly Property DataErrors() As Dictionary(Of String, String)
			Get
				Return dataErrors_Renamed
			End Get
		End Property

		Private Sub ClearPropertyErrors(ByVal propertyName As String)
			If dataErrors_Renamed.ContainsKey(propertyName) Then
				dataErrors_Renamed.Remove(propertyName)
			End If
		End Sub
	End Class
End Namespace
