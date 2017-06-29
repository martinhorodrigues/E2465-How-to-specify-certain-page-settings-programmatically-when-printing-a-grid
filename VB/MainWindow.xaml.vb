Imports Microsoft.VisualBasic
Imports System
Imports System.ComponentModel
Imports System.Data
Imports System.Windows
Imports DevExpress.Xpf.Core
Imports DevExpress.Xpf.Printing

Namespace WpfPrintGridPageSettings
	Partial Public Class MainWindow
		Inherits DXWindow
		Public Sub New()
			InitializeComponent()
		End Sub

		Private Sub Window_Loaded(ByVal sender As Object, ByVal e As RoutedEventArgs)
			gridControl1.Columns.Clear()
			gridControl1.DataSource = TabletDataSet.CreateData().Tables(0)
		End Sub

		Private Sub button1_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			Dim link As New VisualDataNodeLink(CType(gridControl1.View, DevExpress.Xpf.Grid.TableView), "My Document")

			link.PaperKind = System.Drawing.Printing.PaperKind.A5
			link.Landscape = True

			link.ShowPrintPreviewDialog(Me)
		End Sub
	End Class

	#Region "Data for the Grid"
	Public Class TabletDataSet
		Inherits DataSet
		Private Const m_columns As Integer = 10
		Private Const m_rows As Integer = 10

		Public Sub New()
			MyBase.New()
			Dim table As New DataTable("table")

			DataSetName = "ManualDataSet"

			For i As Integer = 0 To m_columns - 1
				table.Columns.Add("Column" & i.ToString(), GetType(Int32))
			Next i

			Tables.AddRange(New DataTable() { table })
		End Sub

		Public Shared Function CreateData() As TabletDataSet
			Dim ds As New TabletDataSet()
			Dim table As DataTable = ds.Tables("table")

			For i As Integer = 0 To m_rows - 1
				Dim row(m_columns - 1) As Object

				For j As Integer = 0 To m_columns - 1
					row(j) = i * m_columns + j
				Next j

				table.Rows.Add(row)
			Next i

			Return ds
		End Function

		#Region "Disable Serialization for Tables and Relations"
		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Shadows ReadOnly Property Tables() As DataTableCollection
			Get
				Return MyBase.Tables
			End Get
		End Property

		<DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)> _
		Public Shadows ReadOnly Property Relations() As DataRelationCollection
			Get
				Return MyBase.Relations
			End Get
		End Property
		#End Region ' Disable Serialization for Tables and Relations
	End Class
	#End Region ' Data for the Grid

End Namespace
