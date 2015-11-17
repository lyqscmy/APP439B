using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace APP439B.Model
{
    public class Database
    {
        # region Constructor

        public Database()
        {

        }

        # endregion // Constructor

        String str;
        static SqlConnection myConn = new SqlConnection("Data Source=(LocalDB)\\v11.0;AttachDbFilename=\"C:\\Users\\admin\\documents\\visual studio 2013\\Projects\\APP439B\\APP439B\\userInfo.mdf\";Integrated Security=True;Connect Timeout=10");

        /*str = "CREATE DATABASE MyDatabase ON PRIMARY " +
            "(NAME = MyDatabase_Data, " +
            "FILENAME = 'C:\\MyDatabaseData.mdf', " +
            "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
            "LOG ON (NAME = MyDatabase_Log, " +
            "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
            "SIZE = 1MB, " +
            "MAXSIZE = 5MB, " +
            "FILEGROWTH = 10%)";*/

        //SqlCommand myCommand = new SqlCommand(str, myConn);

        public void Connect()
        {
            try
            {
                myConn.Open();
                //myCommand.ExecuteNonQuery();
                
                MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        public void InsertUserInfo()
        {
            string query = "INSERT INTO UserInfo (ID, Date, Person, Video)";
            query += " VALUES (@ID, @Date, @Person, @Video)";

            SqlCommand myCommand = new SqlCommand(query, myConn);
            int id = Database.GetHighestID("ID", "UserInfo");
            id += 1;
            //myCommand.Parameters.AddWithValue("@ID", DBNull.Value);
            myCommand.Parameters.AddWithValue("@ID", id);
            myCommand.Parameters.AddWithValue("@Date", "2015.10.15");
            myCommand.Parameters.AddWithValue("@Person", "Kailun");
            //myCommand.Parameters.AddWithValue("@EnvID", DBNull.Value);
            //myCommand.Parameters.AddWithValue("@DopplerID", DBNull.Value);
            myCommand.Parameters.AddWithValue("@Video", DBNull.Value);
            myCommand.ExecuteNonQuery();
        }

        public void InsertEnvironment()
        {
            string query = "INSERT INTO Environment (EnvID, WindSpeed, WindDirection, Temperature, Humidity)";
            query += " VALUES (@EnvID, @WindSpeed, @WindDirection, @Temperature, @Humidity)";

            SqlCommand myCommand = new SqlCommand(query, myConn);
            int id = Database.GetHighestID("EnvID", "Environment");
            id += 1;
            //myCommand.Parameters.AddWithValue("@ID", DBNull.Value);
            myCommand.Parameters.AddWithValue("@EnvID", id);
            myCommand.Parameters.AddWithValue("@WindSpeed", 18.2);
            myCommand.Parameters.AddWithValue("@WindDirection", "East");
            //myCommand.Parameters.AddWithValue("@EnvID", DBNull.Value);
            //myCommand.Parameters.AddWithValue("@DopplerID", DBNull.Value);
            myCommand.Parameters.AddWithValue("@Temperature", 25.0);
            myCommand.Parameters.AddWithValue("@Humidity", 2.3);
            myCommand.ExecuteNonQuery();
        }

        public void ReadAll()
        {
            try
            {
                SqlDataReader myReader = null;
                SqlCommand myCommand = new SqlCommand("select * from UserInfo", myConn);
                myReader = myCommand.ExecuteReader();
                string allInfo;
                while (myReader.Read())
                {
                    allInfo = myReader["ID"].ToString() + "\t" + myReader["Date"].ToString() + "\t" + myReader["EnvID"].ToString();
                    MessageBox.Show(allInfo, "MyProgram", MessageBoxButton.OK, MessageBoxImage.Information);
                    //Console.WriteLine(myReader["Column1"].ToString());
                    //Console.WriteLine(myReader["Column2"].ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void Disconnect()
        {
            if (myConn.State == ConnectionState.Open)
            {
                myConn.Close();
            }
        }

        public static int GetHighestID(String nameID, String tableName)
        {
            int maxId;
            string max = "SELECT MAX(" + nameID + ") FROM " + tableName;
            SqlCommand myCommand = new SqlCommand(max, myConn);
            if (myCommand.ExecuteScalar() == DBNull.Value)
            {
                maxId = 0;
            }
            else{
            maxId = Convert.ToInt32(myCommand.ExecuteScalar());
            }
            return maxId;
        }

        public void DeleteAll(String tableName)
        {
            string deleteAll = "DELETE FROM " + tableName;
            SqlCommand myCommand = new SqlCommand(deleteAll, myConn);
            myCommand.ExecuteNonQuery();
        }
    }
}
