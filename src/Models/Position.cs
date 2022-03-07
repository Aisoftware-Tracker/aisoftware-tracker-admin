using System;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Models
{
    public class Position
    {
        private int _id;
        private PositionAttributes _attributes;
        private int _deviceId;
        private object _type;
        private string _protocol;
        private DateTime? _serverTime;
        private DateTime? _deviceTime;
        private DateTime? _fixTime;
        private bool _outdated;
        private bool _valid;
        private double _latitude;
        private double _longitude;
        private double _altitude;
        private double _speed;
        private double _course;
        private string _address;
        private double _accuracy;
        private object _network;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }

        [JsonProperty("attributes")]
        public PositionAttributes Attributes { get => _attributes; set => _attributes = value; }

        [JsonProperty("deviceId")]
        public int DeviceId { get => _deviceId; set => _deviceId = value; }

        [JsonProperty("type")]
        public object Type { get => _type; set => _type = value; }

        [JsonProperty("protocol")]
        public string Protocol { get => _protocol; set => _protocol = value; }

        [JsonProperty("serverTime")]
        public DateTime? ServerTime { get => _serverTime; set => _serverTime = value; }

        [JsonProperty("deviceTime")]
        public DateTime? DeviceTime { get => _deviceTime; set => _deviceTime = value; }

        [JsonProperty("fixTime")]
        public DateTime? FixTime { get => _fixTime; set => _fixTime = value; }

        [JsonProperty("outdated")]
        public bool OutDated { get => _outdated; set => _outdated = value; }

        [JsonProperty("valid")]
        public bool Valid { get => _valid; set => _valid = value; }

        [JsonProperty("latitude")]
        public double Latitude { get => _latitude; set => _latitude = value; }

        [JsonProperty("longitude")]
        public double Longitude { get => _longitude; set => _longitude = value; }

        [JsonProperty("altitude")]
        public double Altitude { get => _altitude; set => _altitude = value; }

        [JsonProperty("speed")]
        public double Speed { get => _speed; set => _speed = value; }

        [JsonProperty("course")]
        public double Course { get => _course; set => _course = value; }

        [JsonProperty("address")]
        public string Address { get => _address; set => _address = value; }

        [JsonProperty("accuracy")]
        public double Accuracy { get => _accuracy; set => _accuracy = value; }

        [JsonProperty("network")]
        public object Network { get => _network; set => _network = value; }
    }


}
