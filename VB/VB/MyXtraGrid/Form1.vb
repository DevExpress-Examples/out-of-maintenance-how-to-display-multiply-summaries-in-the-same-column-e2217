Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.Utils.Menu

Namespace MyXtraGrid
	Partial Public Class Form1
		Inherits Form

		Private Function CreateTable(ByVal RowCount As Integer) As DataTable
			Dim tbl As New DataTable()
			tbl.Columns.Add("Name", GetType(String))
			tbl.Columns.Add("ID", GetType(Integer))
			tbl.Columns.Add("Number", GetType(Integer))
			tbl.Columns.Add("Date", GetType(DateTime))
			For i As Integer = 0 To RowCount - 1
				tbl.Rows.Add(New Object() { String.Format("Name{0}", i Mod 4), i, 3 - i, DateTime.Now.AddDays(i) })
			Next i
			Return tbl
		End Function


		Public Sub New()
			InitializeComponent()
			myGridControl1.DataSource = CreateTable(20)
		End Sub

		Private myFooterText As String = "Now!"

		Private Sub myGridView1_GetFooterCellDisplayText(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs) Handles myGridView1.GetFooterCellDisplayText
			e.Info.DisplayText = myFooterText
		End Sub

		Private Sub myGridView1_ShowGridMenu(ByVal sender As Object, ByVal e As DevExpress.XtraGrid.Views.Grid.GridMenuEventArgs) Handles myGridView1.ShowGridMenu
			If e.MenuType <> DevExpress.XtraGrid.Views.Grid.GridMenuType.Summary Then
				Return
			End If
			If e.Menu Is Nothing Then
				Return
			End If
			Dim subItem As New DXSubMenuItem()
			subItem.Caption = "Item 2"
			Dim myItem As New DXMenuItem("Now!")
			AddHandler myItem.Click, AddressOf myItem_Click
			subItem.Items.Add(myItem)
			e.Menu.Items.Add(subItem)
		End Sub

		Private Sub myItem_Click(ByVal sender As Object, ByVal e As EventArgs)
			myFooterText = DateTime.Now.ToString()
			myGridView1.UpdateSummary()
		End Sub

	End Class
End Namespace