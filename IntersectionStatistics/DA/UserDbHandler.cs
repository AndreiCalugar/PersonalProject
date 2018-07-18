using IntersectionStatistics.Controllers;
using IntersectionStatistics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntersectionStatistics.DA
{
    public class UserDbHandler
    {
        public static Boolean sendDataRegister(User user)
        {



            using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=IntersectionStatistics;Integrated Security=SSPI"))
            {

                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("sendDataRegister", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@username", user.username));
                cmd.Parameters.Add(new SqlParameter("@password", user.password));
                cmd.Parameters.Add(new SqlParameter("@first_name", user.first_name));
                cmd.Parameters.Add(new SqlParameter("@last_name", user.last_name));
                // execute the command

                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated > 0)
                {
                    return true;
                }


            }

            return false;
        }

        public static User getUsernamePassword(String username, String password)
        {
            User user = null;

            using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=IntersectionStatistics;Integrated Security=SSPI"))
            {

                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("getUsernameAndPasswordProcedure", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@username", username));
                cmd.Parameters.Add(new SqlParameter("@password", password));
                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                        user = new User();
                        user.id_user = rdr.GetInt32(0);
                        user.username = rdr.GetString(1);
                        user.password = rdr.GetString(2);

                    }
                }

                return user;
            }


        }





        
    }
}
