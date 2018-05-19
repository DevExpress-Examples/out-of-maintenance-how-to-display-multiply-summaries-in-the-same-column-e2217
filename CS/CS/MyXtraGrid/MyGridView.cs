using System;
using System.Drawing;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Drawing;

namespace MyXtraGrid {
	public class MyGridView : GridView {
		public MyGridView() : this(null) {}



        private static readonly object getFooterCellDisplayText = new object();

        public event FooterCellCustomDrawEventHandler GetFooterCellDisplayText
        {
            add { Events.AddHandler(getFooterCellDisplayText, value); }
            remove { Events.RemoveHandler(getFooterCellDisplayText, value); }
		}

        protected virtual void RaiseGetFooterCellDisplayText(FooterCellCustomDrawEventArgs e)
        {
            FooterCellCustomDrawEventHandler handler = (FooterCellCustomDrawEventHandler)this.Events[getFooterCellDisplayText];
            if (handler != null) handler(this, e);
        }

        public MyGridView(DevExpress.XtraGrid.GridControl grid)
            : base(grid)
        {
            CustomDrawFooterCell += MyGridView_CustomDrawFooterCell;
            CustomDrawRowFooterCell += MyGridView_CustomDrawRowFooterCell;
        }



        public static void SplitBounds(Rectangle originRect, int separatorWidth, out Rectangle resultRect1, out Rectangle resultRect2)
        {
            resultRect1 = originRect;
            resultRect1.Height = (originRect.Height - separatorWidth) / 2;
            resultRect2 = resultRect1;
            resultRect2.Y = resultRect1.Bottom + separatorWidth;
        }

        private void DrawFooter(FooterCellCustomDrawEventArgs e)
        {
            Rectangle rect1;
            Rectangle rect2;
            GridFooterCellInfoArgs args = e.Info;
            string originalText = e.Info.DisplayText;
            Rectangle originalBounds = args.Bounds;
            SplitBounds(originalBounds, 2, out rect1, out rect2);
            args.Bounds = rect1;
            e.Painter.DrawObject(args);
            args.Bounds = rect2;
            RaiseGetFooterCellDisplayText(e);
            e.Painter.DrawObject(args);
            args.Bounds = originalBounds;
            e.Info.DisplayText = originalText;
            e.Handled = true;
        }
        void MyGridView_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            DrawFooter(e);
        }

        void MyGridView_CustomDrawRowFooterCell(object sender, FooterCellCustomDrawEventArgs e)
        {
            Rectangle r = e.Info.Bounds;
            r.Height = ((MyGridViewInfo)ViewInfo).GetGroupFooterHeight();
            e.Info.Bounds = r;
            DrawFooter(e);
        }


		protected override string ViewName { get { return "MyGridView"; } }
	}
}
