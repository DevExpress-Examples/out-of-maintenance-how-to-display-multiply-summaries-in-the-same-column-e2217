Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid
Imports DevExpress.XtraGrid.Drawing

Namespace MyXtraGrid
	Public Class MyGridView
		Inherits GridView
		Public Sub New()
			Me.New(Nothing)
		End Sub



        Private Shared ReadOnly getFooterCellDisplayTextObject As Object = New Object()

		Public Custom Event GetFooterCellDisplayText As FooterCellCustomDrawEventHandler
			AddHandler(ByVal value As FooterCellCustomDrawEventHandler)
                Events.AddHandler(getFooterCellDisplayTextObject, value)
			End AddHandler
			RemoveHandler(ByVal value As FooterCellCustomDrawEventHandler)
                Events.RemoveHandler(getFooterCellDisplayTextObject, value)
			End RemoveHandler
			RaiseEvent(ByVal sender As System.Object, ByVal e As DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs)
			End RaiseEvent
		End Event

		Protected Overridable Sub RaiseGetFooterCellDisplayText(ByVal e As FooterCellCustomDrawEventArgs)
            Dim handler As FooterCellCustomDrawEventHandler = CType(Me.Events(getFooterCellDisplayTextObject), FooterCellCustomDrawEventHandler)
			If handler IsNot Nothing Then
				handler(Me, e)
			End If
		End Sub

		Public Sub New(ByVal grid As DevExpress.XtraGrid.GridControl)
			MyBase.New(grid)
			AddHandler CustomDrawFooterCell, AddressOf MyGridView_CustomDrawFooterCell
			AddHandler CustomDrawRowFooterCell, AddressOf MyGridView_CustomDrawRowFooterCell
		End Sub



		Public Shared Sub SplitBounds(ByVal originRect As Rectangle, ByVal separatorWidth As Integer, <System.Runtime.InteropServices.Out()> ByRef resultRect1 As Rectangle, <System.Runtime.InteropServices.Out()> ByRef resultRect2 As Rectangle)
			resultRect1 = originRect
			resultRect1.Height = (originRect.Height - separatorWidth) / 2
			resultRect2 = resultRect1
			resultRect2.Y = resultRect1.Bottom + separatorWidth
		End Sub

		Private Sub DrawFooter(ByVal e As FooterCellCustomDrawEventArgs)
			Dim rect1 As Rectangle
			Dim rect2 As Rectangle
			Dim args As GridFooterCellInfoArgs = e.Info
			Dim originalText As String = e.Info.DisplayText
			Dim originalBounds As Rectangle = args.Bounds
			SplitBounds(originalBounds, 2, rect1, rect2)
			args.Bounds = rect1
			e.Painter.DrawObject(args)
			args.Bounds = rect2
			RaiseGetFooterCellDisplayText(e)
			e.Painter.DrawObject(args)
			args.Bounds = originalBounds
			e.Info.DisplayText = originalText
			e.Handled = True
		End Sub
		Private Sub MyGridView_CustomDrawFooterCell(ByVal sender As Object, ByVal e As FooterCellCustomDrawEventArgs)
			DrawFooter(e)
		End Sub

		Private Sub MyGridView_CustomDrawRowFooterCell(ByVal sender As Object, ByVal e As FooterCellCustomDrawEventArgs)
			Dim r As Rectangle = e.Info.Bounds
			r.Height = (CType(ViewInfo, MyGridViewInfo)).GetGroupFooterHeight()
			e.Info.Bounds = r
			DrawFooter(e)
		End Sub


		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property
	End Class
End Namespace
