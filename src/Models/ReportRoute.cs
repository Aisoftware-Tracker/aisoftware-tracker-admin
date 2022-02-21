using System;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Models
{
    public class ReportRoute
    {
        private int _id;
        private int _deviceId;
        private string _protocol;
        private DateTime? _deviceTime;
        private DateTime? _fixTime;
        private DateTime? _serverTime;
        private bool _outdated;
        private bool _valid;
        private int _latitude;
        private int _longitude;
        private int _altitude;
        private int _speed;
        private int _course;
        private string _address;
        private int _accuracy;
        private object _network;
        private ReportRouteAttributes _attributes;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }

        [JsonProperty("deviceId")]
        public int DeviceId { get => _deviceId; set => _deviceId = value; }

        [JsonProperty("protocol")]
        public string Protocol { get => _protocol; set => _protocol = value; }

        [JsonProperty("deviceTime")]
        public DateTime? DeviceTime { get => _deviceTime; set => _deviceTime = value; }

        [JsonProperty("fixTime")]
        public DateTime? FixTime { get => _fixTime; set => _fixTime = value; }

        [JsonProperty("serverTime")]
        public DateTime? ServerTime { get => _serverTime; set => _serverTime = value; }

        [JsonProperty("outdated")]
        public bool Outdated { get => _outdated; set => _outdated = value; }

        [JsonProperty("valid")]
        public bool Valid { get => _valid; set => _valid = value; }

        [JsonProperty("latitude")]
        public int Latitude { get => _latitude; set => _latitude = value; }

        [JsonProperty("longitude")]
        public int Longitude { get => _longitude; set => _longitude = value; }

        [JsonProperty("altitude")]
        public int Altitude { get => _altitude; set => _altitude = value; }

        [JsonProperty("speed")]
        public int Speed { get => _speed; set => _speed = value; }

        [JsonProperty("course")]
        public int Course { get => _course; set => _course = value; }

        [JsonProperty("address")]
        public string Address { get => _address; set => _address = value; }

        [JsonProperty("accuracy")]
        public int Accuracy { get => _accuracy; set => _accuracy = value; }

        [JsonProperty("network")]
        public object Network { get => _network; set => _network = value; }

        [JsonProperty("attributes")]
        public ReportRouteAttributes Attributes { get => _attributes; set => _attributes = value; }
    }

}