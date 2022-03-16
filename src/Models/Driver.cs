using System;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Driver
    {
        private int _id;
        private object _attributes;
        private string _name;
        private string _uniqueId;
        private string _photo;
        private string _document;
        private string _documentValidAt;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }
        
        [JsonProperty("attributes")]
        public object Attributes { get => _attributes; set => _attributes = value; }
        
        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }
        
        [JsonProperty("uniqueId")]
        public string UniqueId { get => _uniqueId; set => _uniqueId = value; }
        
        [JsonProperty("photo")]
        public string Photo { get => _photo; set => _photo = value; }
        
        [JsonProperty("document")]
        public string Document { get => _document; set => _document = value; }
        
        [JsonProperty("documentValidAt")]
        public string DocumentValidAt { get => _documentValidAt; set => _documentValidAt = value; }
    }
}