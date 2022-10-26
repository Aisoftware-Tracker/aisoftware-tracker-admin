using Aisoftware.Tracker.Borders.Constants;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Borders.Models;
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
    private decimal _latitude;
    private decimal _longitude;
    private double _altitude;
    private double _speed;
    private double _course;
    private string _address;
    private double _accuracy;
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
    public decimal Latitude { get => _latitude; set => _latitude = value; }

    [JsonProperty("longitude")]
    public decimal Longitude { get => _longitude; set => _longitude = value; }

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

    [JsonProperty("attributes")]
    public ReportRouteAttributes Attributes { get => _attributes; set => _attributes = value; }

    public string DeviceTimeStr { get => _deviceTime?.ToString(FormatString.FORMAT_DATE_TIME_BR); }
    public string FixTimeStr { get => _fixTime?.ToString(FormatString.FORMAT_DATE_TIME_BR); }
    public string ServerTimeStr { get => _serverTime?.ToString(FormatString.FORMAT_DATE_TIME_BR); }
    public string LatitudeStr { get => _latitude.ToString(); }
    public string LongitudeStr { get => _longitude.ToString(); }

}
