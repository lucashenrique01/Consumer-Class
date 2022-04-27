namespace EventNamespace
{
    public class Event {
        private string Id {get; set;}
        private string SpecVersion {get; set;}
        private string Source {get; set;}
        private string Type {get;set;}
        private string Subject {get;set;}
        private string Time {get;set;}
        private string CorrelationID {get;set;}
        private string DataContentType {get;set;}
        private string Data {get;set;}

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
        
        public string getId(){
            return this.Id;
        }
        public string getSpecVersion(){
            return this.SpecVersion;
        }
        public string getSource(){
            return this.Source;
        }
        public string getType(){
            return this.Type;
        }
        public string getSubject(){
            return this.Subject;
        }
        public string getTime(){
            return this.Time;
        }
        public string getCorrelationID(){
            return this.CorrelationID;
        }
        public string getDataContentType(){
            return this.DataContentType;
        }
        public string getData(){
            return this.Data;
        }
    }
}