using System;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Device
    {
        private int _id;
        private string _name;
        private string _uniqueId;
        private string _status;
        private bool _disabled;
        private DateTime? _lastUpdate;
        private int _positionId;
        private int _groupId;
        private string _phone;
        private string _model;
        private string _contact;
        private string _category;
        private List<int> _geofenceIds;
        private object _attributes;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }

        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }

        [JsonProperty("uniqueId")]
        public string UniqueId { get => _uniqueId; set => _uniqueId = value; }

        [JsonProperty("status")]
        public string Status { get => _status; set => _status = value; }

        [JsonProperty("disabled")]
        public bool Disabled { get => _disabled; set => _disabled = value; }

        [JsonProperty("lastUpdate")]
        public DateTime? LastUpdate { get => _lastUpdate; set => _lastUpdate = value; }

        [JsonProperty("positionId")]
        public int PositionId { get => _positionId; set => _positionId = value; }

        [JsonProperty("groupId")]
        public int GroupId { get => _groupId; set => _groupId = value; }

        [JsonProperty("phone")]
        public string Phone { get => _phone; set => _phone = value; }

        [JsonProperty("model")]
        public string Model { get => _model; set => _model = value; }

        [JsonProperty("contact")]
        public string Contact { get => _contact; set => _contact = value; }

        [JsonProperty("category")]
        public string Category { get => _category; set => _category = value; }

        [JsonProperty("geofenceIds")]
        public List<int> GeofenceIds { get => _geofenceIds; set => _geofenceIds = value; }

        [JsonProperty("attributes")]
        public object Attributes { get => _attributes; set => _attributes = value; }
    }
}