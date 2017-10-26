using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Newtonsoft.Json;
using pubg_database.Services.Cache;

namespace pubg_database.Controllers
{
    public class ValuesController : ApiController
    {
        
        // GET api/values/5
        public object Get(int id=5)
        {
            return WebCacheProvider.Get($"pubg_{id}");
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public int Put(int id, [FromBody]object value)
        {
                Thread.Sleep(15);
            //var s = JsonConvert.SerializeObject(value);
            WebCacheProvider.Add($"pubg_{id}", value);
            return 0;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
