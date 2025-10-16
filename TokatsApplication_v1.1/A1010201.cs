using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.DirectoryServices.ActiveDirectory;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static SkiaSharp.HarfBuzz.SKShaper;
using static System.Runtime.InteropServices.JavaScript.JSType;
using ScottPlot;


namespace TokatsApplication_v1._1
{
    public partial class A1010201 : Form
    {

        //OleDbConnection connection = new OleDbConnection();
        //OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
        //OleDbCommand command = new OleDbCommand();
        //DataTable dataTable = new DataTable();

        //string cn_accdb_c = "";
        string cn_accdb_y = "";
        string sql = "";

        private string? username = SessionManager.Get<string>("UserName");
        private string selectedValue = "";
        int fyear = 0;

        string? key1;
        string? key2;


        ////////////////////////////////////////////////////////////
        ////////////////////フォーム画面各種調整////////////////////
        ////////////////////////////////////////////////////////////

        private void A1010201_Load(object? sender, EventArgs e)
        {
            System.Windows.Forms.Screen s = System.Windows.Forms.Screen.FromControl(this);
            int h = s.Bounds.Height;
            int w = s.Bounds.Width;
            this.Size = new System.Drawing.Size(w, h);

        }

        public A1010201()
        {
            InitializeComponent();
            this.Load += new EventHandler(A1010201_Load);


            TLayout1_03_user.Text = username;

            ////////////////////////////////////////////////////////////
            //////////各種コンボボックスのアイテムをDBから取得//////////
            ////////////////////////////////////////////////////////////

            //*---------------------------------------------------------
            //年度選択(横浜市南部・横浜市北部)

            //アイテムをクリア
            Tab1_1_TLayout1_02_CBox_year.Items.Clear();
            //Panel2_2_Panel1_Tlayout1_Com1.Items.Clear();


            //DB接続文字列
            //string cn_accdb = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = K:\\110000_公共事業部\\110900_公共事業部共有\\@包括業務管理システムTokats\\開発環境\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";
            string cn_accdb_c = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source = ";
            cn_accdb_c += "C:\\Users\\821040\\02_DataBase\\01_Common\\User.accdb;";
            cn_accdb_c += "Jet OLEDB:Database Password = 2410803;";

            //connection.ConnectionString += "Jet OLEDB:Database Password = 2410803;";
            //SQLクエリを定義
            string qu_year = "SELECT * FROM M_DATE_年次";


            try
            {

                using (OleDbConnection connection = new OleDbConnection(cn_accdb_c))
                {
                    connection.Open();
                    using (OleDbDataAdapter dataAdapter = new OleDbDataAdapter(qu_year, connection))
                    {
                        DataTable dataTable = new DataTable();
                        dataAdapter.Fill(dataTable);

                        foreach (DataRow row in dataTable.Rows)
                        {
                            //DB側で年度はNotNull＆重複なしにしたが念のため
                            string yearstr = Convert.ToString(row["年度"])!;
                            if (yearstr != "")
                            {
                                Tab1_1_TLayout1_02_CBox_year.Items.Add(yearstr);
                            }
                            else
                            {
                                Tab1_1_TLayout1_02_CBox_year.Items.Add("2999");
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("データベースに接続出来ませんでした#96", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            //年度選択のチェンジイベント(横浜市南部)
            Tab1_1_TLayout1_02_CBox_year.SelectedIndexChanged += Tab1_1_TLayout1_02_CBox_year_SelectedIndexChanged;


            //年度選択のチェンジイベント(横浜市北部)
            //Tab1_2_TLayout1_02_CBox_year.SelectedIndexChanged += Tab1_2_TLayout1_02_CBox_year_SelectedIndexChanged;

            //一覧を表示ボタンのクリックイベント(横浜市南部)
            //Tab1_1_TLayout1_03_Btn_exec.Click += new EventHandler(Tab1_1_TLayout1_03_Btn_exec_Click);


            //一覧を表示ボタンのクリックイベント(横浜市北部)
            //Tab1_2_TLayout1_03_Btn_exec.Click += new EventHandler(Tab1_1_TLayout1_03_Btn_exec_Click);
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void Tab1_1_TLayout1_02_CBox_year_SelectedIndexChanged(object? sender, EventArgs e)
        {

            if (sender is ComboBox comboBox)
            {
                selectedValue = comboBox.SelectedItem?.ToString() ?? "";
            }
            else
            {
                MessageBox.Show("担当者にお問い合わせください", "異常動作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }



        private async void Tab1_1_TLayout1_03_Btn_exec_Click(object? sender, EventArgs e)
        {
            this.Tab1_1_TLayout1_21_DGrid.DataSource = null;

            if (selectedValue != "")
            {
                //DB接続文字列
                //string cn_accdb = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = \\\\kanseifs\\share\\110000_公共事業部\\110900_公共事業部共有\\@包括業務管理システムTokats\\開発環境\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";
                cn_accdb_y = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\821040\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";


                //SQLクエリを定義
                //string sql = "SELECT * FROM V_A1010201_現場進捗一覧表_管渠統合 WHERE 年度= ?; ";

                fyear = Convert.ToInt32(selectedValue);

                //string sql = "SELECT * FROM V_A1010201_現場進捗一覧表_管渠統合 WHERE 年度= " + fyear + ";";
                sql = @"SELECT * FROM ( ";
                sql += "SELECT * FROM V_A1010201_現場進捗一覧表_人孔 ";
                sql += "UNION ALL ";
                sql += "SELECT * FROM V_A1010201_現場進捗一覧表_管渠統合 ";
                sql += "UNION ALL ";
                sql += "SELECT * FROM V_A1010201_現場進捗一覧表_緊急 ";
                sql += " UNION ALL ";
                sql += "SELECT * FROM V_A1010201_現場進捗一覧表_その他 ) ";
                sql += "WHERE 年度= " + fyear + ";";

                DataTable result = await LoadDataAsync(cn_accdb_y, sql);
                Tab1_1_TLayout1_21_DGrid.AutoGenerateColumns = false;
                Tab1_1_TLayout1_21_DGrid.DataSource = result;
                var col = Tab1_1_TLayout1_21_DGrid;

                //Color オブジェクトはScottPlotにもあるので、ここでは明示的に、namespace と親オブジェクトとしてSystem.Drawing.をColorの前に記述する

                for (int i = 1; i < 12; i++)
                {
                    if (i < 4)
                    {
                        col.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;
                    }
                    else if (i < 8)
                    {
                        col.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.Columns[i].HeaderCell.Style.BackColor = System.Drawing.Color.DarkSeaGreen;
                        col.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                    else
                    {
                        col.Columns[i].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
                        col.Columns[i].HeaderCell.Style.BackColor = System.Drawing.Color.Orange;
                        col.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }
                col.EnableHeadersVisualStyles = false;

            }
            else
            {
                MessageBox.Show("年度が指定されていません", "入力不備", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        //private async Task<DataTable> LoadDataAsync(string connection, string query)
        public async Task<DataTable> LoadDataAsync(string connection, string query)
        {
            return await Task.Run(() =>
            {
                var tbl = new DataTable();
                try
                {

                    using (OleDbConnection conn = new OleDbConnection(connection))
                    {
                        conn.Open();

                        using (OleDbCommand cmd = new OleDbCommand(query, conn))
                        {
                            //cmd.CommandText = sql;
                            //cmd.Parameters.AddWithValue("?", fyear);　のようにステートメントでparamを渡したいのだが、
                            //このメソッドは単独クラスにしたい。外部から動的に検索キーを渡す形になるのは少し難しい
                            //あとで考える、検索キーを配列にして渡す、とか、、、、

                            using (OleDbDataAdapter adp = new OleDbDataAdapter(cmd))
                            {

                                adp.Fill(tbl);

                                //Tab1_1_TLayout1_21_DGrid.DataSource = tbl;
                            }
                        }
                    }

                }
                catch
                {
                    MessageBox.Show("データベースに接続出来ませんでした#1250", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                //return先でTab1_1_TLayout1_21_DGrid.DataSource = tbl
                return tbl;
            });
        }

        private void Tab1_1_TLayout1_21_DGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {


        }

        private void PlotGraph(DataTable dt)
        {
            var pltq = Tab1_1_TLayout1_41_formsPlot1.Plot;
            var pltc = Tab1_1_TLayout1_44_formsPlot2.Plot;
            pltq.Clear();
            pltc.Clear();

            List<double> mcodes = new List<double>();
            List<double> ys1 = new List<double>();
            List<double> ys2 = new List<double>();
            List<string> mnames = new List<string>();

            List<double> ys3 = new List<double>();
            List<double> ys4 = new List<double>();

            double planq_cum = 0;
            double accompq_cum = 0;
            double planc_cum = 0;
            double accompc_cum = 0;

            //数量グラフ、金額グラフ、ここでまとめてデータ取り出し　→　配列作成
            foreach (DataRow row in dt.Rows)
            {
                //ここでDBObject(row[])がnullのままだとConvert処理でエラーになる。よってNull判定を行ったうえで各変数にキャストする

                double mcode = row["月次コード"] is DBNull ? 0 : Convert.ToDouble(row["月次コード"]);
                double planq = row["計画数量"] is DBNull ? 0 : Convert.ToDouble(row["計画数量"]);
                double accompq = row["現場数量"] is DBNull ? 0 : Convert.ToDouble(row["現場数量"]);
                string? mname = row["月"] is DBNull ? "" : Convert.ToString(row["月"]);

                double planc = row["計画金額"] is DBNull ? 0 : Convert.ToDouble(row["計画金額"]);
                double accompc = row["実施金額"] is DBNull ? 0 : Convert.ToDouble(row["実施金額"]);

                planq_cum += planq;
                accompq_cum += accompq;
                planc_cum += planc;
                accompc_cum += accompc;

                mcodes.Add(mcode);
                ys1.Add(planq_cum);
                ys2.Add(accompq_cum);
                mnames.Add(mname ?? "");

                ys3.Add(planc_cum);
                ys4.Add(accompc_cum);

            }

            double[] xs = mcodes.ToArray();
            string[] labels = mnames.ToArray();
            double[] yt1 = ys1.ToArray();
            double[] yt2 = ys2.ToArray();

            double[] yt3 = ys3.ToArray();
            double[] yt4 = ys4.ToArray();

            Double xMin = 0;
            Double xMax = 12;
            double yqMin = 0;
            double yqMax = Math.Max(yt1.Max(), yt2.Max()) * 1.1;

            double ycMin = 0;
            double ycMax = Math.Max(yt3.Max(), yt4.Max()) * 1.1;



            pltq.Axes.SetLimits(xMin, xMax, yqMin, yqMax);
            pltc.Axes.SetLimits(xMin, xMax, ycMin, ycMax);

            //数量グラフ描画　累計グラフは累計地を出してAdd.Scatter()する必要あり
            var line1 = pltq.Add.Scatter(xs, yt1);
            line1.LegendText = "計画";
            line1.Color = ScottPlot.Color.FromHex("#87CEEB");
            line1.LineWidth = 4;
            line1.MarkerSize = 4;

            var line2 = pltq.Add.Scatter(xs, yt2);
            line2.LegendText = "実績";
            line2.Color = ScottPlot.Color.FromHex("#4169E1");
            line2.LineWidth = 4;
            line2.MarkerSize = 4;

            pltq.Axes.Bottom.SetTicks(xs, labels); // xs: double[], labels: string[]
            pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("月");
            //Legend は凡例　
            pltq.ShowLegend();
            pltq.Legend.FontName = ScottPlot.Fonts.Detect("計画");
            pltq.Legend.FontName = ScottPlot.Fonts.Detect("実績");

            pltq.Axes.Title.Label.Text = "数量/月間推移";
            pltq.Axes.Bottom.Label.Text = "月";
            pltq.Axes.Left.Label.Text = "数量";

            pltq.Axes.Title.Label.FontName = ScottPlot.Fonts.Detect("数量/月間推移");
            pltq.Axes.Bottom.Label.FontName = ScottPlot.Fonts.Detect("月");
            pltq.Axes.Left.Label.FontName = ScottPlot.Fonts.Detect("数量");

            //金額グラフ
            var line3 = pltc.Add.Scatter(xs, yt3);
            line3.LegendText = "計画";
            line3.Color = ScottPlot.Color.FromHex("#FF7F50");
            line3.LineWidth = 4;
            line3.MarkerSize = 4;

            var line4 = pltc.Add.Scatter(xs, yt4);
            line4.LegendText = "実績";
            line4.Color = ScottPlot.Color.FromHex("#FF4500");
            line4.LineWidth = 4;
            line4.MarkerSize = 4;

            pltc.Axes.Bottom.SetTicks(xs, labels); // xs: double[], labels: string[]
            pltc.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("月");

            pltc.ShowLegend();
            pltc.Legend.FontName = ScottPlot.Fonts.Detect("計画");
            pltc.Legend.FontName = ScottPlot.Fonts.Detect("実績");

            pltc.Axes.Title.Label.Text = "金額/月間推移";
            pltc.Axes.Bottom.Label.Text = "月";
            pltc.Axes.Left.Label.Text = "金額";

            pltc.Axes.Title.Label.FontName = ScottPlot.Fonts.Detect("金額/月間推移");
            pltc.Axes.Bottom.Label.FontName = ScottPlot.Fonts.Detect("月");
            pltc.Axes.Left.Label.FontName = ScottPlot.Fonts.Detect("金額");

            Tab1_1_TLayout1_41_formsPlot1.Refresh();
            Tab1_1_TLayout1_44_formsPlot2.Refresh();
        }


        private async void Tab1_1_TLayout1_31_gfButton_Click(object sender, EventArgs e)
        {
            DataGridViewRow? selRow = Tab1_1_TLayout1_21_DGrid.SelectedRows[0];

            if (selRow is not null)
            {

                key1 = selRow.Cells["分類名"].Value.ToString();
                key2 = selRow.Cells["表記工種"].Value.ToString();

                sql = "";
                sql = "SELECT * FROM (";
                sql += "SELECT * FROM V_A1010201_現場進捗グラフ_人孔 ";
                sql += "UNION ALL ";
                sql += "SELECT * FROM V_A1010201_現場進捗グラフ_管渠統合 ";
                sql += "UNION ALL ";
                sql += "SELECT * FROM V_A1010201_現場進捗グラフ_緊急 ";
                sql += "UNION ALL ";
                sql += "SELECT * FROM V_A1010201_現場進捗グラフ_その他) ";
                sql += "WHERE 年度 = " + fyear + " AND 表記工種名 ='" + key2 + "' AND 分類名 = '" + key1 + "' ORDER BY 月次コード ASC;";

                DataTable graph = await LoadDataAsync(cn_accdb_y, sql);
                Console.WriteLine(graph);
                PlotGraph(graph);

            }
            else
            {
                MessageBox.Show("グラフ表示したい工種を選んでください#1283", "入力不足", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void Tab1_1_TLayout1_05_Btn_hamatop_Click(object sender, EventArgs e)
        {
            Y1010101 y1010101 = new Y1010101();
            y1010101.Show();
            this.Close();
        }
    }
}
