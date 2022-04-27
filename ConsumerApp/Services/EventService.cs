using EventNamespace;
using EventDaoNamespace;
using Newtonsoft.Json.Linq;

namespace ConsumerServiceNamespace{
    class EventService
    {
        public static void convertEvent(string eventoString)
        {   
            JObject json = JObject.Parse(eventoString);
            Event novoEvento = new Event(json["id"].ToString(), json["specVersion"].ToString(),json["source"].ToString(),
            json["type"].ToString(),json["subject"].ToString(), json["time"].ToString(), json["correlationId"].ToString(), 
            json["dataContentType"].ToString(), json["data"].ToString());
            EventDAO.existById(json["id"].ToString(), novoEvento);
             
        }
    }
}