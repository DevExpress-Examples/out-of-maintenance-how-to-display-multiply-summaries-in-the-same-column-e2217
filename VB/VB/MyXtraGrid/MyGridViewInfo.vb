Imports Microsoft.VisualBasic
Imports System
Imports System.Drawing
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo

Namespace MyXtraGrid
	Public Class MyGridViewInfo
		Inherits GridViewInfo
		Public Sub New(ByVal gridView As DevExpress.XtraGrid.Views.Grid.GridView)
			MyBase.New(gridView)
		End Sub


		Protected Overrides Function CalcGroupFooterHeight() As Integer
			Return MyBase.CalcGroupFooterHeight() * 2
		End Function


		Protected Overrides Function CalcFooterCellHeight() As Integer
			Return MyBase.CalcFooterCellHeight() * 2
		End Function


		Public Function GetGroupFooterHeight() As Integer
			Return CalcFooterCellHeight()
		End Function

		Public Shared Sub SplitBounds(ByVal originRect As Rectangle, ByVal separatorWidth As Integer, <System.Runtime.InteropServices.Out()> ByRef resultRect1 As Rectangle, <System.Runtime.InteropServices.Out()> ByRef resultRect2 As Rectangle)
			resultRect1 = originRect
			resultRect1.Height = (originRect.Height - separatorWidth) / 2
			resultRect2 = resultRect1
			resultRect2.Y = resultRect1.Bottom + separatorWidth
		End Sub

	End Class
End Namespace
