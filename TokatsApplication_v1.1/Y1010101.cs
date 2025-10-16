using ScottPlot;
using ScottPlot.WinForms;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace TokatsApplication_v1._1
{
    public partial class Y1010101 : Form
    {

        private string? username = SessionManager.Get<string>("UserName");

        string selectedperiod = "";
        string selectedarea = "";
        string selectedsector = "";

        private Panel? popupRead;
        private Panel? popupPrint;
        private Panel? popupFilter;

        string cn_accdb_y = "";
        string queryselect = "";
        string querytable = "";
        string[] sql;
        /*private Button byType;
        private Button byPipe;
        private Button byHole;
        private Button byTech;
        private Button lstInvest;
        private Button gis;
        private Button ETC;
        private Button defect;*/
        public Y1010101()
        {
            InitializeComponent();
            this.Load += new EventHandler(Y1010101_Load);

            TLayout1_03_user.Text = username;
        }


        private async void Y1010101_Load(object? sender, EventArgs e)
        {
            System.Windows.Forms.Screen s = System.Windows.Forms.Screen.FromControl(this);
            int h = s.Bounds.Height;
            int w = s.Bounds.Width;
            this.Size = new System.Drawing.Size(w, h);

            //tab1_1_TL12_CB.Items.Add("南部");
            //tab1_1_TL12_CB.Items.Add("北部");

            cn_accdb_y = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\821040\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";
            sql = new string[] { " SELECT M_CONTROL_画面制御.種類,M_CONTROL_画面制御.名称 FROM M_CONTROL_画面制御 WHERE ((M_CONTROL_画面制御.フォーム) = ?);", "Y1010101" };

            var dataAsync = new DataAsync();
            DataTable result = await dataAsync.LoadDataAsync(cn_accdb_y, sql);

            foreach (DataRow row in result.Rows)
            {
                //DB側で年度はNotNull＆重複なしにしたが念のため
                string type = Convert.ToString(row["種類"])!;
                string opt = Convert.ToString(row["名称"])!;
                if (type != "")
                {
                    if (type == "地域")
                    {

                        tab1_1_TL12_CB.Items.Add(opt);
                    }
                    else if (type == "2期")
                    {
                        tab1_1_TL24_CB.Items.Add(opt);
                    }
                    else if (type == "業種")
                    {
                        tag1_1_tl41_tl11_Cmb.Items.Add(opt);
                    }
                }
                else
                {
                    tab1_1_TL24_CB.Items.Add("-");
                    tab1_1_TL12_CB.Items.Add("-");
                    tag1_1_tl41_tl11_Cmb.Items.Add("-");
                }
            }

            tl1_14_btn1.Text = "▼　閲覧・更新";
            tl1_15_btn2.Text = "▼　出力";
            tl1_16_btn3.Text = "▼　抽出";

        }

        private void tab1_2_tl_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Tab1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }


        private void tab1_1_TL12_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox comboBox)
            {
                selectedarea = comboBox.SelectedItem?.ToString() ?? "";
                if (selectedarea == "南部")
                {
                    cn_accdb_y = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\821040\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";
                }
                else if (selectedarea == "北部")
                {
                    cn_accdb_y = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\821040\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_north.accdb";
                }
                else
                {
                    MessageBox.Show("担当者にお問い合わせください", "異常動作", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("担当者にお問い合わせください", "異常動作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tab1_1_TL24_CB_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox comboBox2)
            {
                selectedperiod = comboBox2.SelectedItem?.ToString() ?? "";
            }
            else
            {
                MessageBox.Show("担当者にお問い合わせください", "異常動作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tab1_1_TL25_graph_btn_Click(object sender, EventArgs e)
        {
            if (selectedarea != "" && selectedperiod != "")
            {
                //cn_accdb_y = "Provider = Microsoft.ACE.OLEDB.12.0; Data Source = C:\\Users\\821040\\02_DataBase\\02_Yokohama\\TOKATS_yokohama_south.accdb";
                string query;
                query = "";
                query += "SELECT 年度,";
                query += "SUM(計画金額) AS 計画金額の合計,";
                query += "SUM(実施金額) AS 実施金額の合計 ";
                query += "FROM (";
                query += " SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別現場金額グラフ_人孔";
                query += " UNION ALL";
                query += " SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別現場金額グラフ_管渠統合";
                query += " UNION ALL";
                query += " SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別現場金額グラフ_緊急";
                query += " UNION ALL";
                query += " SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別現場金額グラフ_その他";
                query += " ) AS 統合結果";
                query += " WHERE 期 = ? GROUP BY 年度 ORDER BY 年度";
                query += ";";

                sql = new string[] { query, selectedperiod };
                var dataAsync = new DataAsync();
                DataTable result = await dataAsync.LoadDataAsync(cn_accdb_y, sql);

                tag1_1_tl37_Grid.AutoGenerateColumns = false;
                tag1_1_tl37_Grid.DataSource = result;

                //PlotGraphA(result);

                if (result.Rows.Count == 0)
                {
                    MessageBox.Show("対象データがありません#171", "データなし", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    PlotGraphA(result, tag1_1_tl32_plotFY);

                    int sumpv = 0;
                    int sumev = 0;

                    foreach (DataRow row in result.Rows)
                    {

                        int pv = 0;
                        int ev = 0;

                        if (row["計画金額の合計"] != DBNull.Value)
                            pv = Convert.ToInt32(row["計画金額の合計"]);

                        if (row["実施金額の合計"] != DBNull.Value)
                            ev = Convert.ToInt32(row["実施金額の合計"]);


                        sumpv += (int)pv;
                        sumev += (int)ev;
                    }

                    int dif = sumev - sumpv;
                    tab1_1_tl22_tl21_pv.Text = sumpv.ToString("C0");
                    tab1_1_tl22_tl22_ev.Text = sumev.ToString("C0");
                    tag1_1_tl22_tl23_LablDif.Text = dif.ToString("C0");

                    PlotGraphB(sumpv, sumev);

                }

            }
            else
            {
                MessageBox.Show("地域と期、いずれも選択してください#152", "異常動作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void tl1_14_btn1_Click(object sender, EventArgs e)
        {
            if (popupRead == null)
            {
                tl1_14_btn1.Text = "参照確認▲";
                // パネルの作成
                popupRead = new Panel();
                popupRead.Size = new Size(180, 180);
                popupRead.BackColor = System.Drawing.Color.White;
                popupRead.BorderStyle = BorderStyle.FixedSingle;

                // 表示位置をボタンの下に設定
                Point buttonLocation = tl1_14_btn1.PointToScreen(Point.Empty);
                Point formLocation = this.PointToScreen(Point.Empty);
                int x = buttonLocation.X - formLocation.X;
                int y = buttonLocation.Y - formLocation.Y + tl1_14_btn1.Height;

                popupRead.Location = new Point(x, y);

                // 他のコントロールにかぶせるためにフォームに直接追加
                this.Controls.Add(popupRead);
                popupRead.BringToFront();

                // パネル内にラベルなど追加可能
                System.Windows.Forms.Button byType = new System.Windows.Forms.Button();
                byType.Size = new Size(167, 35);
                byType.Margin = new Padding(8, 8, 2, 2);
                byType.Text = "工種別数量";
                byType.Dock = DockStyle.Top;
                popupRead.Controls.Add(byType);

                System.Windows.Forms.Button byPipe = new System.Windows.Forms.Button();
                byPipe.Size = new Size(167, 35);
                byPipe.Margin = new Padding(8, 8, 2, 2);
                byPipe.Text = "管渠調査内容";
                byPipe.Dock = DockStyle.Top;
                popupRead.Controls.Add(byPipe);

                System.Windows.Forms.Button byHole = new System.Windows.Forms.Button();
                byHole.Size = new Size(167, 35);
                byHole.Margin = new Padding(8, 8, 2, 2);
                byHole.Text = "人孔調査内容";
                byHole.Dock = DockStyle.Top;
                popupRead.Controls.Add(byHole);

                System.Windows.Forms.Button byTech = new System.Windows.Forms.Button();
                byTech.Size = new Size(167, 35);
                byTech.Margin = new Padding(8, 8, 2, 2);
                byTech.Text = "技術提案事項";
                byTech.Dock = DockStyle.Top;
                popupRead.Controls.Add(byTech);

            }
            else
            {
                tl1_14_btn1.Text = "参照確認▼";
                // パネルが表示されていれば非表示にする
                this.Controls.Remove(popupRead);
                popupRead.Dispose();
                popupRead = null;
            }


        }

        private void tl1_15_btn2_Click(object sender, EventArgs e)
        {
            if (popupPrint == null)
            {
                tl1_15_btn2.Text = "出力▲";
                // パネルの作成
                popupPrint = new Panel();
                popupPrint.Size = new Size(180, 180);
                popupPrint.BackColor = System.Drawing.Color.White;
                popupPrint.BorderStyle = BorderStyle.FixedSingle;

                // 表示位置をボタンの下に設定
                Point buttonLocation = tl1_15_btn2.PointToScreen(Point.Empty);
                Point formLocation = this.PointToScreen(Point.Empty);
                int x = buttonLocation.X - formLocation.X;
                int y = buttonLocation.Y - formLocation.Y + tl1_15_btn2.Height;

                popupPrint.Location = new Point(x, y);

                // 他のコントロールにかぶせるためにフォームに直接追加
                this.Controls.Add(popupPrint);
                popupPrint.BringToFront();

                // パネル内にラベルなど追加可能
                System.Windows.Forms.Button lstInvest = new System.Windows.Forms.Button();
                lstInvest.Size = new Size(167, 35);
                lstInvest.Margin = new Padding(8, 8, 2, 2);
                lstInvest.Text = "調査内容一覧";
                lstInvest.Dock = DockStyle.Top;
                popupPrint.Controls.Add(lstInvest);

                System.Windows.Forms.Button gis = new System.Windows.Forms.Button();
                gis.Size = new Size(167, 35);
                gis.Margin = new Padding(8, 8, 2, 2);
                gis.Text = "GIS用csv";
                gis.Dock = DockStyle.Top;
                popupPrint.Controls.Add(gis);

                System.Windows.Forms.Button ETC = new System.Windows.Forms.Button();
                ETC.Size = new Size(167, 35);
                ETC.Margin = new Padding(8, 8, 2, 2);
                ETC.Text = "その他";
                ETC.Dock = DockStyle.Top;
                popupPrint.Controls.Add(ETC);

            }
            else
            {
                tl1_15_btn2.Text = "出力▼";
                // パネルが表示されていれば非表示にする
                this.Controls.Remove(popupPrint);
                popupPrint.Dispose();
                popupPrint = null;
            }

        }

        private void tl1_16_btn3_Click(object sender, EventArgs e)
        {
            if (popupFilter == null)
            {
                tl1_16_btn3.Text = "抽出▲";
                // パネルの作成
                popupFilter = new Panel();
                popupFilter.Size = new Size(180, 180);
                popupFilter.BackColor = System.Drawing.Color.White;
                popupFilter.BorderStyle = BorderStyle.FixedSingle;

                // 表示位置をボタンの下に設定
                Point buttonLocation = tl1_16_btn3.PointToScreen(Point.Empty);
                Point formLocation = this.PointToScreen(Point.Empty);
                int x = buttonLocation.X - formLocation.X;
                int y = buttonLocation.Y - formLocation.Y + tl1_16_btn3.Height;

                popupFilter.Location = new Point(x, y);

                // 他のコントロールにかぶせるためにフォームに直接追加
                this.Controls.Add(popupFilter);
                popupFilter.BringToFront();

                // パネル内にラベルなど追加可能
                System.Windows.Forms.Button defect = new System.Windows.Forms.Button();
                defect.Size = new Size(167, 35);
                defect.Margin = new Padding(8, 8, 2, 2);
                defect.Text = "各種異常";
                defect.Dock = DockStyle.Top;
                popupFilter.Controls.Add(defect);


            }
            else
            {
                tl1_16_btn3.Text = "抽出▼";
                // パネルが表示されていれば非表示にする
                this.Controls.Remove(popupFilter);
                popupFilter.Dispose();
                popupFilter = null;
            }
        }

        private void PlotGraphA(DataTable dt,FormsPlot view)
        {
            //var pltq = tag1_1_tl32_plotFY.Plot;
            var pltq = view.Plot;
            pltq.Clear();

            IPalette palette = new ScottPlot.Palettes.Category20();

            List<double> ys1 = new List<double>();
            List<double> ys2 = new List<double>();
            List<string> xcode = new List<string>();
            List<double> xs1 = new List<double>();

            double pv = 0;
            double ev = 0;
            double xc = 0;
            string? fy = "";

            //数量グラフ、金額グラフ、ここでまとめてデータ取り出し　→　配列作成
            foreach (DataRow row in dt.Rows)
            {
                //ここでDBObject(row[])がnullのままだとConvert処理でエラーになる。よってNull判定を行ったうえで各変数にキャストする

                pv += row["計画金額の合計"] is DBNull ? 0 : Convert.ToDouble(row["計画金額の合計"]);
                ev += row["実施金額の合計"] is DBNull ? 0 : Convert.ToDouble(row["実施金額の合計"]);
                fy = row["年度"] is DBNull ? "" : Convert.ToString(row["年度"]);
                xc++;

                ys1.Add(pv);
                ys2.Add(ev);
                xcode.Add(fy ?? "");
                xs1.Add(xc);

            }

            string[] xt2 = xcode.ToArray();
            double[] xt1 = xs1.ToArray();
            double[] yt1 = ys1.ToArray();
            double[] yt2 = ys2.ToArray();

            Double xMin = xt1.Min();
            Double xMax = xt1.Max();
            double yqMin = 0;
            double yqMax = Math.Max(yt1.Max(), yt2.Max()) * 1.1;


            pltq.Axes.SetLimits(xMin, xMax, yqMin, yqMax);

            //数量グラフ描画　累計グラフは累計地を出してAdd.Scatter()する必要あり
            var line1 = pltq.Add.Scatter(xt1, yt1);
            line1.LegendText = "計画";
            line1.Color = palette.GetColor(3);
            line1.Smooth = true;
            line1.LineWidth = 4;
            line1.MarkerSize = 4;

            var line2 = pltq.Add.Scatter(xt1, yt2);
            line2.LegendText = "実績";
            line2.Color = palette.GetColor(2);
            line2.LineWidth = 4;
            line2.Smooth = true;
            line2.MarkerSize = 4;

            pltq.Axes.Bottom.SetTicks(xt1, xt2); // xs: double[], labels: string[]
            pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("年度");
            //Legend は凡例　
            pltq.ShowLegend();
            pltq.Legend.FontName = ScottPlot.Fonts.Detect("計画");
            pltq.Legend.FontName = ScottPlot.Fonts.Detect("実績");

            pltq.Axes.Title.Label.Text = "金額/年度推移";
            pltq.Axes.Bottom.Label.Text = "年度";
            pltq.Axes.Left.Label.Text = "金額";

            pltq.Axes.Title.Label.FontName = ScottPlot.Fonts.Detect("金額/年度推移");
            pltq.Axes.Bottom.Label.FontName = ScottPlot.Fonts.Detect("年度");
            pltq.Axes.Left.Label.FontName = ScottPlot.Fonts.Detect("金額");

            view.Refresh();
        }

        private void PlotGraphB(int pv, int ev)
        {

            double[] values = { pv, ev };
            string[] labels = { "計画", "実績" };

            IPalette palette = new ScottPlot.Palettes.Category20();

            var pltq = tag1_1_tl31_plotAll.Plot;
            pltq.Clear();


            var barPlot = pltq.Add.Bars(values);
            //pltq.Add.Bars(values);

            barPlot.Bars[0].FillColor = palette.GetColor(2);

            barPlot.Bars[1].FillColor = palette.GetColor(3);




            var ticks = labels.Select((label, index) => new ScottPlot.Tick(index, label)).ToArray();
            pltq.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);


            Double xMin = -1;
            Double xMax = 2;
            double yqMin = 0;
            double yqMax = Math.Max(pv, ev) * 1.1;


            pltq.Axes.SetLimits(xMin, xMax, yqMin, yqMax);


            //pltq.Title("計画 vs 実績");
            //pltq.Axes.Left.Label.Text = "値";

            pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("計画");
            pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("実績");

            //pltq.ShowLegend();

            pltq.Axes.Title.Label.Text = "計画 vs 実績";
            pltq.Axes.Bottom.Label.Text = selectedperiod + " 合計";
            pltq.Axes.Left.Label.Text = "金額";

            pltq.Axes.Title.Label.FontName = ScottPlot.Fonts.Detect("計画 vs 実績");
            pltq.Axes.Bottom.Label.FontName = ScottPlot.Fonts.Detect("合計");
            pltq.Axes.Left.Label.FontName = ScottPlot.Fonts.Detect("金額");


            tag1_1_tl31_plotAll.Refresh();

        }

        private void PlotGraphC(DataTable dt, FormsPlot view)
        {
            var pltq = view.Plot;
            pltq.Clear();

            IPalette palette = new ScottPlot.Palettes.Category20();

            double[] values;
            string[] labels;

            List<double> ys1 = new List<double>();
            List<double> ys2 = new List<double>();
            List<string> xcode = new List<string>();
            List<double> xs1 = new List<double>();

            double pv = 0;
            double ev = 0;
            double xc = 0;
            string? fy = "";


            foreach (DataRow row in dt.Rows)
            {
                //ここでDBObject(row[])がnullのままだとConvert処理でエラーになる。よってNull判定を行ったうえで各変数にキャストする

                ev = row[1] is DBNull ? 0 : Convert.ToDouble(row[1]);
                fy = row[0] is DBNull ? "" : Convert.ToString(row[0]);
                xc++;

                ys1.Add(ev);
                xcode.Add(fy ?? "");
                xs1.Add(xc);

                pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect(fy);

            }

            labels = xcode.ToArray();
            double[] xt1 = xs1.ToArray();
            values = ys1.ToArray();


            var barPlot = pltq.Add.Bars(values);
            //pltq.Add.Bars(values);

            //var RoyalBluenk = ScottPlot.Color.FromHex("#4169E1");
            //var LightBlue = ScottPlot.Color.FromHex("#ADD8E6");
            //var LightPink =ScottPlot.Color.FromHex("#FFB6C1");
            //var DeepPink = ScottPlot.Color.FromHex("#FF1493");

            int arycnt =labels.Length;

            for (int j = 0; j < arycnt - 1; j++)
            {
                barPlot.Bars[j].FillColor = palette.GetColor(j * 2);
                barPlot.Bars[j].LineWidth = 2;

            }

            var ticks = labels.Select((label, index) => new ScottPlot.Tick(index, label)).ToArray();
            pltq.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);


            Double xMin = -1;
            Double xMax = xc+1;
            double yqMin = 0;
            double yqMax =values.Max() * 1.1;


            pltq.Axes.SetLimits(xMin, xMax, yqMin, yqMax);


            //pltq.Title("計画 vs 実績");
            //pltq.Axes.Left.Label.Text = "値";

            pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("計画");
            pltq.Axes.Bottom.TickLabelStyle.FontName = ScottPlot.Fonts.Detect("実績");

            //pltq.ShowLegend();

            pltq.Axes.Title.Label.Text = "計画 vs 実績";
            pltq.Axes.Bottom.Label.Text = selectedperiod + " 合計";
            pltq.Axes.Left.Label.Text = "金額";

            pltq.Axes.Title.Label.FontName = ScottPlot.Fonts.Detect("計画 vs 実績");
            pltq.Axes.Bottom.Label.FontName = ScottPlot.Fonts.Detect("合計");
            pltq.Axes.Left.Label.FontName = ScottPlot.Fonts.Detect("金額");


            view.Refresh();

        }



        private void tag1_1_tl41_tl11_Cmb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (sender is System.Windows.Forms.ComboBox comboBox)
            {
                selectedsector = comboBox.SelectedItem?.ToString() ?? "";
            }
            else
            {
                MessageBox.Show("担当者にお問い合わせください", "異常動作", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void tag1_1_tl41_tl12_Btn_Click(object sender, EventArgs e)
        {
            string querya ="";
            string queryb ="";
            if (selectedsector == "計画的調査業務")
            {
                querya = "SELECT 年度,SUM(計画金額) AS 計画金額の合計,SUM(実施金額) AS 実施金額の合計 ";
                querya += "FROM (SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別各業務現場進捗グラフ_人孔 ";
                querya +="UNION ALL SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別各業務現場進捗グラフ_管渠統合) AS 統合結果 ";
                querya += "WHERE 期 =? GROUP BY 年度 ORDER BY 年度;";

                queryb = "SELECT 略称,SUM(実施金額) AS 実施金額の合計 ";
                queryb += "FROM (SELECT 期,略称,実施金額 FROM V_Y1010101_期別各JV進捗グラフ_人孔 UNION ALL SELECT 期,略称,実施金額 FROM V_Y1010101_期別各JV進捗グラフ_管渠統合 )";
                queryb += "WHERE 期 = ? GROUP BY 略称 ORDER BY SUM(実施金額) DESC;";

            }
            else if (selectedsector == "緊急対応業務")
            {
                querya = "SELECT 年度,SUM(計画金額) AS 計画金額の合計,SUM(実施金額) AS 実施金額の合計 ";
                querya += "FROM (SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別各業務現場進捗グラフ_緊急 ) AS 統合結果 ";
                querya += "WHERE 期 =? GROUP BY 年度 ORDER BY 年度;";

                queryb = "SELECT 略称,SUM(実施金額) AS 実施金額の合計 ";
                queryb += "FROM (SELECT 期,略称,実施金額 FROM V_Y1010101_期別各JV進捗グラフ_緊急) ";
                queryb += "WHERE 期 =? GROUP BY 略称 ORDER BY SUM(実施金額) DESC;";

            }
            else if (selectedsector == "その他業務")
            {
                querya = "SELECT 年度,SUM(計画金額) AS 計画金額の合計,SUM(実施金額) AS 実施金額の合計 ";
                querya += "FROM (SELECT 期,年度,計画金額,実施金額 FROM V_Y1010101_期別各業務現場進捗グラフ_その他) AS 統合結果 ";
                querya += "WHERE 期 =? GROUP BY 年度 ORDER BY 年度;";

                queryb = "SELECT 略称,SUM(実施金額) AS 実施金額の合計 ";
                queryb += "FROM ( SELECT 期,略称,実施金額 FROM V_Y1010101_期別各JV進捗グラフ_その他) ";
                queryb += "WHERE 期 =? GROUP BY 略称 ORDER BY SUM(実施金額) DESC;";
            }
            else
            {
                MessageBox.Show("業種が選択されていません", "入力異常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            if(selectedsector != null && selectedperiod != null) {
                string[] sql1 = {querya, selectedperiod };
                var dataAsync = new DataAsync();
                DataTable result = await dataAsync.LoadDataAsync(cn_accdb_y, sql1);

                PlotGraphA(result, tag1_1_tl51_Plot);

                string[] sql2 = { queryb, selectedperiod };
                var dataAsync2 = new DataAsync();
                DataTable result2 = await dataAsync2.LoadDataAsync(cn_accdb_y, sql2);

                PlotGraphC(result2, tag1_1_tl61_Plot);

            }
            else
            {
                MessageBox.Show("年度あるいは業種が選択されていません", "入力異常", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }
    }
}
