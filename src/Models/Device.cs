using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Device
    {
        private int _id;
        private object _attributes;
        private int _groupId;
        private string _name;
        private string _uniqueId;
        private string _status;
        private DateTime? _lastUpdate;
        private int _positionId;
        private List<int> _geofenceIds;
        private string _phone;
        private string _model;
        private string _contact;
        private string _category;
        private bool _disabled;
        private string _photo;
        private string _icc;
        private string _vendor;
        private string _operator;
        private string _hardware;
        private string _infoinstalacao;
        private string _tech;
        private string _localInstall;
        private string _dateInstall;

        
        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }
        
        [JsonProperty("attributes")]
        public object Attributes { get => _attributes; set => _attributes = value; }
        
        [JsonProperty("groupId")]
        public int GroupId { get => _groupId; set => _groupId = value; }
        
        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }
        
        [JsonProperty("uniqueId")]
        public string UniqueId { get => _uniqueId; set => _uniqueId = value; }
        
        [JsonProperty("status")]
        public string Status { get => _status; set => _status = value; }
        
        [JsonProperty("lastUpdate")]
        public DateTime? LastUpdate { get => _lastUpdate; set => _lastUpdate = value; }
        
        [JsonProperty("positionId")]
        public int PositionId { get => _positionId; set => _positionId = value; }
        
        [JsonProperty("geofenceIds")]
        public List<int> GeofenceIds { get => _geofenceIds; set => _geofenceIds = value; }
        
        [JsonProperty("phone")]
        public string Phone { get => _phone; set => _phone = value; }
        
        [JsonProperty("model")]
        public string Model { get => _model; set => _model = value; }
        
        [JsonProperty("contact")]
        public string Contact { get => _contact; set => _contact = value; }
        
        [JsonProperty("category")]
        public string Category { get => _category; set => _category = value; }
        
        [JsonProperty("disabled")]
        public bool Disabled { get => _disabled; set => _disabled = value; }
        
        [JsonProperty("photo")]
        public string Photo { get => _photo; set => _photo = value; }
        
        [JsonProperty("icc")]
        public string Icc { get => _icc; set => _icc = value; }
        
        [JsonProperty("vendor")]
        public string Vendor { get => _vendor; set => _vendor = value; }
        
        [JsonProperty("operator")]
        public string Operator { get => _operator; set => _operator = value; }
        
        [JsonProperty("hardware")]
        public string Hardware { get => _hardware; set => _hardware = value; }
        
        [JsonProperty("infoinstalacao")]
        public string InfoInstalacao { get => _infoinstalacao; set => _infoinstalacao = value; }
        
        [JsonProperty("tech")]
        public string Tech { get => _tech; set => _tech = value; }
        
        [JsonProperty("localInstall")]
        public string LocalInstall { get => _localInstall; set => _localInstall = value; }
        
        [JsonProperty("dateInstall")]
        public string DateInstall { get => _dateInstall; set => _dateInstall = value; }

        public string LastUpdateStr
        {
            get => _lastUpdate?.ToString("dd/MM/yyyy HH:mm:ss") ?? string.Empty;
        }
    }
}