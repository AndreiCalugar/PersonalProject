using IntersectionStatistics.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace IntersectionStatistics.DA
{
    public class StatisticDbHandler
    {


        public static List<Statistic> getStatistic()
        {
            List<Statistic> stat = new ArrayList<Statistic>();
            Statistic statistic = null;  

            using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=IntersectionStatistics;Integrated Security=SSPI"))
            {

                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("getStatistic", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

             



                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {
                       
                        statistic = new Statistic();
                        statistic.id_stat = rdr.GetInt32(0);
                        statistic.id_lap = rdr.GetInt32(1);
                        statistic.date = rdr.GetDateTime(2);
                        statistic.decibels = rdr.GetDouble(3);
                        statistic.gas = rdr.GetDouble(4);
                        statistic.counter = rdr.GetInt32(5);

                        stat.Add(statistic);
                    }
                }
            }

                return stat ;
            
        }




        public static List<Statistic> getStatisticAfterDateProcedure(DateTime firstdate, DateTime lastdate)
        {
            List<Statistic> stat = new ArrayList<Statistic>();
            Statistic statistic = null;

            using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=IntersectionStatistics;Integrated Security=SSPI"))
            {

                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("getStatisticAfterDateProcedure", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@firstdate", firstdate.Date));
                cmd.Parameters.Add(new SqlParameter("@lastdate", lastdate.Date));



                // execute the command
                using (SqlDataReader rdr = cmd.ExecuteReader())
                {

                    // iterate through results, printing each to console
                    while (rdr.Read())
                    {

                        statistic = new Statistic();
                        statistic.id_stat = rdr.GetInt32(0);
                        statistic.id_lap = rdr.GetInt32(1);
                        statistic.date = rdr.GetDateTime(2);
                        statistic.decibels = rdr.GetDouble(3);
                        statistic.gas = rdr.GetDouble(4);
                        statistic.counter = rdr.GetInt32(5);

                        stat.Add(statistic);
                    }
                }
            }

            return stat;

        }



        public static Boolean getcollectedData(Statistic myresponse)
        {



            using (SqlConnection conn = new SqlConnection("Server=(local);DataBase=IntersectionStatistics;Integrated Security=SSPI"))
            {

                conn.Open();

                // 1.  create a command object identifying the stored procedure
                SqlCommand cmd = new SqlCommand("getcollectedData", conn);

                // 2. set the command object so it knows to execute a stored procedure
                cmd.CommandType = CommandType.StoredProcedure;

                // 3. add parameter to command, which will be passed to the stored procedure
                cmd.Parameters.Add(new SqlParameter("@date", myresponse.date));
                cmd.Parameters.Add(new SqlParameter("@decibels",myresponse.decibels));
                cmd.Parameters.Add(new SqlParameter("@gas", myresponse.gas));
                cmd.Parameters.Add(new SqlParameter("@counter", myresponse.counter));
                // execute the command

                int rowsUpdated = cmd.ExecuteNonQuery();
                if (rowsUpdated > 0)
                {
                    return true;
                }


            }

            return false;
        }
    }
}