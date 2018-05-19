using System;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace MyXtraGrid {
	public class MyGridViewInfo : GridViewInfo {
		public MyGridViewInfo(DevExpress.XtraGrid.Views.Grid.GridView gridView) : base(gridView) {}


        protected override int CalcGroupFooterHeight()
        {
            return base.CalcGroupFooterHeight() * 2;
        }


        protected override int CalcFooterCellHeight()
        {
            return base.CalcFooterCellHeight() * 2;
        }


        public int GetGroupFooterHeight()
        {
            return CalcFooterCellHeight();
        }

        public static void SplitBounds(Rectangle originRect, int separatorWidth, out Rectangle resultRect1, out Rectangle resultRect2)
        {
            resultRect1 = originRect;
            resultRect1.Height = (originRect.Height - separatorWidth) / 2;
            resultRect2 = resultRect1;
            resultRect2.Y = resultRect1.Bottom + separatorWidth;
        }
	
	}
}
