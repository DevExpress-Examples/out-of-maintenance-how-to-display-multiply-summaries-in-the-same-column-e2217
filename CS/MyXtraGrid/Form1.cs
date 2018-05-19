using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.Utils.Menu;

namespace MyXtraGrid {
    public partial class Form1 : Form {

        private DataTable CreateTable(int RowCount)
        {
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Name", typeof(string));
            tbl.Columns.Add("ID", typeof(int));
            tbl.Columns.Add("Number", typeof(int));
            tbl.Columns.Add("Date", typeof(DateTime));
            for (int i = 0; i < RowCount; i++)
                tbl.Rows.Add(new object[] { String.Format("Name{0}", i % 4), i, 3 - i, DateTime.Now.AddDays(i) });
            return tbl;
        }
        

        public Form1() {
            InitializeComponent();
            myGridControl1.DataSource = CreateTable(20);
        }

        string myFooterText = "Now!";

        private void myGridView1_GetFooterCellDisplayText(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
        {
            e.Info.DisplayText = myFooterText;
        }

        private void myGridView1_ShowGridMenu(object sender, DevExpress.XtraGrid.Views.Grid.GridMenuEventArgs e)
        {
            if (e.MenuType != DevExpress.XtraGrid.Views.Grid.GridMenuType.Summary) return;
            if (e.Menu == null) return;
            DXSubMenuItem subItem = new DXSubMenuItem();
            subItem.Caption = "Item 2";
            DXMenuItem myItem = new DXMenuItem("Now!");
            myItem.Click += myItem_Click;
            subItem.Items.Add(myItem);
            e.Menu.Items.Add(subItem);
        }

        void myItem_Click(object sender, EventArgs e)
        {
            myFooterText = DateTime.Now.ToString();
            myGridView1.UpdateSummary();
        }

    }
}