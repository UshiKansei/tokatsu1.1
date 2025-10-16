using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;


namespace TokatsApplication_v1._1
{
    public partial class A1020101 : Form

    {
        ////////////////////////////////////////////////////////////
        //////////////////////////共通変数//////////////////////////
        ////////////////////////////////////////////////////////////

        private int? year_yokohamasouth;
        private int? year_yokohamanorth;
        string unit_yokohamasouth = "";
        string unit_yokohamanorth = "";

        static private string? username = SessionManager.Get<string>("UserName");

        public A1020101()
        {
            InitializeComponent();

            ////////////////////////////////////////////////////////////
            ////////////////////フォーム画面各種調整////////////////////
            ////////////////////////////////////////////////////////////

            //作業領域の取得、リサイズ
            var WorkingArea = Screen.GetWorkingArea(this);
            this.Size = new Size(WorkingArea.Width, WorkingArea.Height);
            this.Location = new Point(WorkingArea.X, WorkingArea.Y);

            ////////////////////////////////////////////////////////////
            //////////各種コンボボックスのアイテムをDBから取得//////////
            ////////////////////////////////////////////////////////////

            //*---------------------------------------------------------
            //年度選択(横浜市南部・横浜市北部)

            //アイテムをクリア
            Panel2_1_Panel1_Tlayout1_Com1.Items.Clear();
            //Panel2_2_Panel1_Tlayout1_Com1.Items.Clear();

            //DB接続文字列
            string cn_accdb = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = \\\\kanseifs\\share\\110000_公共事業部\\110900_公共事業部共有\\@包括業務管理システムTokats\\開発環境\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";

            //SQLクエリを定義
            string qu_year = "SELECT * FROM M_DATE_年次";

            //DB接続開始

            DDTestPanel.AllowDrop = true;
            DDTestPanel.DragEnter += Panel_DragEnter;
            DDTestPanel.DragDrop += Panel_DragDrop;

            //using(OleDbCommand com_year = new OleDbCommand(qu_year,cn_year)) 
            OleDbConnection connection = new OleDbConnection(cn_accdb);
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter(qu_year, connection);
            //connection.Open();
            //OleDbCommand command = new OleDbCommand(qu_year, connection);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);

            foreach (DataRow row in dataTable.Rows)
            {
                string yearstr = Convert.ToString(row["年度"])!;
                Panel2_1_Panel1_Tlayout1_Com1.Items.Add(yearstr);
            }

            Panel2_1_Panel7_Tlayout1_Chklist1.Items.Clear();
            //Panel2_2_Panel7_Tlayout1_Chklist11.Items.Clear();

            string qu_catgry = "SELECT * FROM M_CONST_業務分類";
            //OleDbConnection conn_catgry = new OleDbConnection(cn_accdb);
            OleDbDataAdapter data_catgry = new OleDbDataAdapter(qu_catgry, connection);
            DataTable catTable = new DataTable();
            data_catgry.Fill(catTable);

            if (catTable.Rows.Count == 0)
            {
                MessageBox.Show("該当者項目", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                foreach (DataRow catrow in catTable.Rows)
                {
                    string? catname = Convert.ToString(catrow["分類名"]);
                    if (catname == null)
                    {
                        catname = "分類不明";
                    }
                    else
                    {
                        Panel2_1_Panel7_Tlayout1_Chklist1.Items.Add(catname);
                    }
                }

            }

        }

        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Panel1_Lab11.Text = username;
        }

        private void Panel2_1_Panel1_Tlayout1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Panel2_1_Panel1_Tlayout1_Com1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (int.TryParse(Panel2_1_Panel1_Tlayout1_Com1.SelectedItem?.ToString(), out int value))
            {
                year_yokohamanorth = value;
            }

        }

        private void Panel2_1_Panel8_Panel2_Lab1_DragDrop(object sender, DragEventArgs e)
        {

        }

        private void Panel2_1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void DDTestPanel_DragDrop(object sender, DragEventArgs e)
        {

        }


        private void Panel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
            else
                e.Effect = DragDropEffects.None;
        }

        private void Panel_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            string excelPath = files[0];

            // Excelファイルを読み込む（例：EPPlusを使用）
            using (var package = new OfficeOpenXml.ExcelPackage(new FileInfo(excelPath)))
            {
                var worksheet = package.Workbook.Worksheets[0];
                int rowCount = worksheet.Dimension.Rows;
                int colCount = worksheet.Dimension.Columns;

                // データを配列に収納
                string[,] data = new string[rowCount, colCount];
                for (int row = 1; row <= rowCount; row++)
                {
                    for (int col = 1; col <= colCount; col++)
                    {
                        data[row - 1, col - 1] = worksheet.Cells[row, col].Text;
                    }
                }

                // ここで data を使ってグラフや表示に活用できます
            }
        }

    }
}
