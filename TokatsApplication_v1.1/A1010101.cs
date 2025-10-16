using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace TokatsApplication_v1._1
{
    public partial class A1010101 : Form
    {
        //static private string? username = null;
        //static public string GetUserName() { return username; }

        public A1010101()
        {
            InitializeComponent();

            ////////////////////////////////////////////////////////////
            ////////////////////フォーム画面各種調整////////////////////
            ////////////////////////////////////////////////////////////

            //開発環境のDPIに固定
            this.AutoScaleDimensions = new SizeF(96F, 96F);

            //作業領域の取得、リサイズ
            var WorkingArea = Screen.GetWorkingArea(this);
            this.Size = new Size(WorkingArea.Width, WorkingArea.Height);
            this.Location = new Point(WorkingArea.X, WorkingArea.Y);
        }

        private void Panel3_Panel3_But1_Click(object sender, EventArgs e)
        {
            string inputID = Panel3_Panel1_Text1.Text;
            string inputPW = Panel3_Panel2_Text1.Text;

            var login = new LogIn
            {
                UserID = inputID,
                UserPW = inputPW
            };


            var context = new ValidationContext(login);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(login, context, results, true);


            if (!isValid)
            {
                foreach (var error in results)
                {
                    MessageBox.Show(error.ErrorMessage, "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                auth(inputID, inputPW);
            }

        }


        private void auth(string username, string password)
        {
            //DB接続コネクタ
            OleDbConnection connection = new OleDbConnection();
            OleDbDataAdapter dataAdapter = new OleDbDataAdapter();
            OleDbCommand command = new OleDbCommand();
            DataTable dataTable = new DataTable();


            //string provider = "Provider = Microsoft.ACE.OLEDB.12.0;";
            //string filePath = "Data Source = K:\\110000_公共事業部\\110900_公共事業部共有\\@包括業務管理システムTokats\\開発環境\\02_DataBase\\01_Common\\User.accdb;";

            connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source = ";
            //connection.ConnectionString += "K:\\110000_公共事業部\\110900_公共事業部共有\\@包括業務管理システムTokats\\開発環境\\02_DataBase\\01_Common\\User.accdb;";
            connection.ConnectionString += "C:\\Users\\821040\\02_DataBase\\01_Common\\User.accdb;";
            connection.ConnectionString += "Jet OLEDB:Database Password = 2410803;";

            /*connection.ConnectionString = "Provider = Microsoft.ACE.OLEDB.12.0;Data Source = "
            + "C:\\Users\\821040\\02_DataBase\\01_Common\\User.accdb;";*/



            string sql = "SELECT Users.UserID, Users.Name, Users.RoginID, Users.Password FROM Users WHERE (((Users.RoginID)= ? ) AND ((Users.Password)= ? )); ";

            try
            {

                command.Connection = connection;
                command.CommandText = sql;
                command.Parameters.AddWithValue("?", username);
                command.Parameters.AddWithValue("?", password);
                connection.Open();

                using (OleDbDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string name = string.Format("{0}", reader["Name"]);
                        string roginID = string.Format("{0}", reader["RoginID"]);
                        string pword = string.Format("{0}", reader["Password"]);
                        //MessageBox.Show("該当者あり", "成功", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        SessionManager.Set("UserName", name);
                        SessionManager.Set("RoginID", roginID);

                        A1010201 a1010201 = new A1010201();
                        a1010201.Show();


                    }
                    else
                    {
                        MessageBox.Show("該当者なし", "失敗", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

            }
            catch
            {
                MessageBox.Show("データベースに接続出来ませんでした", "入力エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
