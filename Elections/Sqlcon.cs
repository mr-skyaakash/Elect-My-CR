using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Data;

namespace Elections
{
    class SqlCon
    {
        static String connectionString = "Data Source=172.16.210.250,49170;Initial Catalog=Elections;Integrated Security=True";
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter adt;

        public void open_con()
        {           
                con = new SqlConnection(connectionString);
                con.Open();            
        }

        public void close_con()
        {
            con.Close();
            
        }

        public void exec_query(String query)
        {
            cmd = new SqlCommand(query, con);
            //cmd.Connection = con;
            //cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Dispose();
            con.Close();
        }

        public DataSet data_access(String query)
        {
            adt = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            adt.Fill(ds);
            adt.Dispose();
            return ds;
        }

        public void validatelogin()
        {
            open_con();
        }
    }
}
