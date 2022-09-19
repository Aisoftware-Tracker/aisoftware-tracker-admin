namespace Aisoftware.Tracker.Borders.Models
{
    ///<summary>
    ///Gerenciamento de sessão de usuário
    ///</summary>
    public class Session
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
        private object _attributes;

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
        public object Attributes { get => _attributes; set => _attributes = value; }
        public string Role { get => _deviceReadonly ? "readonly" : "admin"; }

    }
}
