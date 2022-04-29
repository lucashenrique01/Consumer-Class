using System;
using EventNamespace;
using EventDaoNamespace;
using Newtonsoft.Json.Linq;

namespace ConsumerServiceNamespace{
    class EventService
    {
        EventDAO eventDao = new EventDAO();
        public void convertEvent(string eventString)
        {   
            JObject json = JObject.Parse(eventString);
            Event newEvent = new Event(json["id"].ToString(), json["specVersion"].ToString(),json["source"].ToString(),
            json["type"].ToString(),json["subject"].ToString(), json["time"].ToString(), json["correlationId"].ToString(), 
            json["dataContentType"].ToString(), json["data"].ToString());
            eventDao.existById(json["id"].ToString(), newEvent);
            if(!eventDao.exists(eventDao.getCount())){
                    eventDao.insertEvent(newEvent);
                }else {
                    Console.WriteLine("Evento já consumido");
                }             
        }
    }
}