using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TokatsApplication_v1._1
{
    internal class DataAsync
    {
        public async Task<DataTable> LoadDataAsync(string connection, string[] statement )
        {
            int elem = statement.Length;
            string query = statement[0];

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
                            for(int i = 1; i< elem; i++)
                            {
                                cmd.Parameters.AddWithValue("?", statement[i] ?? "");
                            }
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
    }
}
