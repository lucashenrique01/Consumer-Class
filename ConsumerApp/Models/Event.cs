namespace EventNamespace
{
    public class Event {
        public string Id {get; set;}
        public string SpecVersion {get; set;}
        public string Source {get; set;}
        public string Type {get;set;}
        public string Subject {get;set;}
        public string Time {get;set;}
        public string CorrelationID {get;set;}
        public string DataContentType {get;set;}
        public string Data {get;set;}

        public Event(string id, string specVersion, string source, string type, string subject, string time, string correlationid,
        string datacontentype, string data)
        {
            this.Id=id;
            this.SpecVersion=specVersion;
            this.Source=source;
            this.Type=type;
            this.Subject=subject;
            this.Time=time;
            this.CorrelationID=correlationid;
            this.DataContentType=datacontentype;
            this.Data=data;
        }

        public override string ToString()
        {
            return "Id: " + Id + "Data" + Data;
        }

    }
}