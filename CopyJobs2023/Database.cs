using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CopyJobs2023
{
    class Database
    {
        public static SQLiteConnection conn;
        public static SQLiteCommand cmd;
        public static SQLiteDataReader dr;
        public static string path = @"data.db";
        public static string cs = @"URI=file:"+path;


        public static void Create_db()
        {
        
 


            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source="+ path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE backup1 (id INTEGER, dest1 TEXT UNIQUE,  dest2 TEXT, dest3 TEXT , dest4 TEXT , dest5 TEXT UNIQUE, PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();




                    sqlite.Close();

                }
                

            }

        }

        public static void dataadd(string dest1, string dest2, string dest3, string dest4 , string dest5)
        {

            var con = new SQLiteConnection(cs);
            con.Open();
            var cmd = new SQLiteCommand(con);
            try
            {

                cmd.CommandText = "INSERT INTO backup1(dest1,dest2,dest3,dest4,dest5) VALUES(@dest1,@dest2,@dest3,@dest4,@dest5)";

                cmd.Parameters.AddWithValue("@dest1", dest1);
                cmd.Parameters.AddWithValue("@dest2", dest2);
                cmd.Parameters.AddWithValue("@dest3", dest3);
                cmd.Parameters.AddWithValue("@dest4", dest4);
                cmd.Parameters.AddWithValue("@dest5", dest5);


                MessageBox.Show("Ok.");


                cmd.ExecuteNonQuery();

                


            }
            catch (Exception e)
            {



            }

            con.Close();


        }


    }
}
