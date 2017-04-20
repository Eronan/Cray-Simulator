using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Cray_Simulator
{
    static class Program
    {
        const string sqliteDBFile = @".\vgCards.db";
        static string[] sqlQueries = new string[2] { "select * from Main", "select * from Custom" };

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            DataTable Main = new DataTable();

            foreach (string str in sqlQueries)
            {
                using (SQLiteConnection cnn = new SQLiteConnection("datasource=" + sqliteDBFile))
                {
                    try
                    {
                        DataTable dt = new DataTable();
                        SQLiteCommand cmd = new SQLiteCommand(str, cnn);
                        cnn.Open();

                        SQLiteDataReader reader = cmd.ExecuteReader();
                        dt.Load(reader);
                        Main.Merge(dt);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                    finally
                    {
                        if (cnn != null) cnn.Close();
                    }
                }

            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new DeckBuilder(Main));
        }
    }
}
