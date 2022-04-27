using EventNamespace;
using EventDaoNamespace;
using Newtonsoft.Json.Linq;

namespace ConsumerServiceNamespace{
    class EventService
    {
        EventDAO eventDao = new EventDAO();
        public void convertEvent(string eventoString)
        {   
            JObject json = JObject.Parse(eventoString);
            Event novoEvento = new Event(json["id"].ToString(), json["specVersion"].ToString(),json["source"].ToString(),
            json["type"].ToString(),json["subject"].ToString(), json["time"].ToString(), json["correlationId"].ToString(), 
            json["dataContentType"].ToString(), json["data"].ToString());
            eventDao.existById(json["id"].ToString(), novoEvento);             
        }
    }
}