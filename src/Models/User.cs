using System;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Admin.Models
{
    ///<summary>
    ///Usuários
    ///</summary>
    public class User
    {
        private int _id;
        private string _name;
        private string _email;
        private string _phone;
        private bool _readonly;
        private bool _administrator;
        private string _map;
        private decimal _latitude;
        private decimal _longitude;
        private decimal _zoom;
        private string _password;
        private bool _twelveHourFormat;
        private string _coordinateFormat;
        private bool _disabled;
        private DateTime? _expirationTime;
        private int _deviceLimit;
        private int _userLimit;
        private bool _deviceReadonly;
        private bool _limitCommands;
        private string _poiLayer;
        private string _token;
        private string _photo;
        private string _whatsapp;
        private string _telegram;
        private int _sms;
        private object _attributes;

        [JsonProperty("id")]
        public int Id { get => _id; set => _id = value; }

        [JsonProperty("name")]
        public string Name { get => _name; set => _name = value; }

        [JsonProperty("email")]
        public string Email { get => _email; set => _email = value; }

        [JsonProperty("phone")]
        public string Phone { get => _phone; set => _phone = value; }

        [JsonProperty("readonly")]
        public bool Readonly { get => _readonly; set => _readonly = value; }

        [JsonProperty("administrator")]
        public bool Administrator { get => _administrator; set => _administrator = value; }

        [JsonProperty("map")]
        public string Map { get => _map; set => _map = value; }

        [JsonProperty("latitude")]
        public decimal Latitude { get => _latitude; set => _latitude = value; }

        [JsonProperty("longitude")]
        public decimal Longitude { get => _longitude; set => _longitude = value; }

        [JsonProperty("zoom")]
        public decimal Zoom { get => _zoom; set => _zoom = value; }

        [JsonProperty("password")]
        public string Password { get => _password; set => _password = value; }

        [JsonProperty("twelveHourFormat")]
        public bool TwelveHourFormat { get => _twelveHourFormat; set => _twelveHourFormat = value; }

        [JsonProperty("coordinateFormat")]
        public string CoordinateFormat { get => _coordinateFormat; set => _coordinateFormat = value; }

        [JsonProperty("disabled")]
        public bool Disabled { get => _disabled; set => _disabled = value; }

        [JsonProperty("expirationTime")]
        public DateTime? ExpirationTime { get => _expirationTime; set => _expirationTime = value; }

        [JsonProperty("deviceLimit")]
        public int DeviceLimit { get => _deviceLimit; set => _deviceLimit = value; }

        [JsonProperty("userLimit")]
        public int UserLimit { get => _userLimit; set => _userLimit = value; }

        [JsonProperty("deviceReadonly")]
        public bool DeviceReadonly { get => _deviceReadonly; set => _deviceReadonly = value; }

        [JsonProperty("limitCommands")]
        public bool LimitCommands { get => _limitCommands; set => _limitCommands = value; }

        [JsonProperty("poiLayer")]
        public string PoiLayer { get => _poiLayer; set => _poiLayer = value; }

        [JsonProperty("token")]
        public string Token { get => _token; set => _token = value; }

        [JsonProperty("photo")]
        public string Photo { get => _photo; set => _photo = value; }

        [JsonProperty("whatsapp")]
        public string Whatsapp { get => _whatsapp; set => _whatsapp = value; }

        [JsonProperty("telegram")]
        public string Telegram { get => _telegram; set => _telegram = value; }

        [JsonProperty("sms")]
        public int Sms { get => _sms; set => _sms = value; }

        [JsonProperty("attributes")]
        public object Attributes { get => _attributes; set => _attributes = value; }
    }
}
