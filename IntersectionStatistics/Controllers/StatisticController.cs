using IntersectionStatistics.DA;
using IntersectionStatistics.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace IntersectionStatistics.Controllers
{
    public class StatisticController : ApiController
    {


        [HttpGet]
        [ActionName("getstatistic")]
        public IHttpActionResult getStatistic ([FromBody]Statistic statistic)
        {
            List<Statistic> stat = StatisticDbHandler.getStatistic();


            if (stat != null)
            {
                return Ok(stat);
            }
            else
            {
                return Ok(new { success = false, message = "error can't retrieve statistic " });
            }
        }


        [HttpPost]
        [ActionName("getstatisticAfterDate")]
        public IHttpActionResult getStatisticAfterDate([FromBody]StatisticAfterDateModel datestat )
        {
            List<Statistic> stat = StatisticDbHandler.getStatisticAfterDateProcedure( datestat.firstdate ,datestat.lastdate);


            if (stat != null)
            {
                return Ok(stat);
            }
            else
            {
                return Ok(new { success = false, message = "error can't retrieve statistic " });
            }
        }



        [HttpPost]
        [ActionName("getcollectedData")]
        public IHttpActionResult getcollectedData([FromBody]Statistic myresponse)
        {
            Boolean op = StatisticDbHandler.getcollectedData(myresponse);
            if (op == false)
            {
                return Ok(new { success = false, message = "Could not add data" });
            }

            return Ok(new { success = true });
        }
    }
}