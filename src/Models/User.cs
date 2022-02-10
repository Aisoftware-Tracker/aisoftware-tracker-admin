using System;

namespace Aisoftware.Tracker.Admin.Models
{
    ///<summary>
    ///Usuários
    ///</summary>
    public class User
    {
        private int _id;
        private string _name = null;
        private string _email = null;
        private string _phone = null;
        private bool _readonly = false;
        private bool _administrator = false;
        private string _map = null;
        private decimal _latitude = 0;
        private decimal _longitude = 0;
        private decimal _zoom = 0;
        private string _password = null;
        private bool _twelveHourFormat = false;
        private string _coordinateFormat = null;
        private bool _disabled = false;
        private DateTime? _expirationTime;
        private int _deviceLimit = 0;
        private int _userLimit = 0;
        private bool _deviceReadonly = false;
        private bool _limitCommands = false;
        private string _poiLayer = null;
        private string _token = null;
        private string _photo = null;
        private string _whatsapp = null;
        private string _telegram = null;
        private int _sms = 0;
        private object _attributes = false;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Phone { get => _phone; set => _phone = value; }
        public bool Readonly { get => _readonly; set => _readonly = value; }
        public bool Administrator { get => _administrator; set => _administrator = value; }
        public string Map { get => _map; set => _map = value; }
        public decimal Latitude { get => _latitude; set => _latitude = value; }
        public decimal Longitude { get => _longitude; set => _longitude = value; }
        public decimal Zoom { get => _zoom; set => _zoom = value; }
        public string Password { get => _password; set => _password = value; }
        public bool TwelveHourFormat { get => _twelveHourFormat; set => _twelveHourFormat = value; }
        public string CoordinateFormat { get => _coordinateFormat; set => _coordinateFormat = value; }
        public bool Disabled { get => _disabled; set => _disabled = value; }
        public DateTime? ExpirationTime { get => _expirationTime; set => _expirationTime = value; }
        public int DeviceLimit { get => _deviceLimit; set => _deviceLimit = value; }
        public int UserLimit { get => _userLimit; set => _userLimit = value; }
        public bool DeviceReadonly { get => _deviceReadonly; set => _deviceReadonly = value; }
        public bool LimitCommands { get => _limitCommands; set => _limitCommands = value; }
        public string PoiLayer { get => _poiLayer; set => _poiLayer = value; }
        public string Token { get => _token; set => _token = value; }
        public string Photo { get => _photo; set => _photo = value; }
        public string Whatsapp { get => _whatsapp; set => _whatsapp = value; }
        public string Telegram { get => _telegram; set => _telegram = value; }
        public int Sms { get => _sms; set => _sms = value; }
        public object Attributes { get => _attributes; set => _attributes = value; }
    }
}
