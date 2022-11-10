using Aisoftware.Tracker.Borders.Constants;
using Newtonsoft.Json;

namespace Aisoftware.Tracker.Borders.Models;
public class ReportRouteDTO
{
    private string _licensePlate;
    private string _routeProtocol;
    private string _routeDeviceTimeStr;
    private string _routeFixTimeStr;
    private string _routeServerTimeStr;
    private string _outdated;
    private string _valid;
    private string _routeLatitude;
    private string _routeLongitude;
    private string _routeAltitude;
    private string _routeSpeed;
    private string _routeAddress;
    private string _routeAccuracy;
    private string _ignition;
    private string _routeAttributesStatus;
    private string _routeAttributesDistance;
    private string _routeAttributesTotalDistance;
    private string _motion;
    private string _routeAttributesHours;

    [JsonProperty("licensePlate")]
    public string LicensePlate { get => _licensePlate; set => _licensePlate = value; }
    [JsonProperty("routeProtocol")]
    public string RouteProtocol { get => _routeProtocol; set => _routeProtocol = value; }
    [JsonProperty("routeDeviceTimeStr")]
    public string RouteDeviceTimeStr { get => _routeDeviceTimeStr; set => _routeDeviceTimeStr = value; }
    [JsonProperty("routeFixTimeStr")]
    public string RouteFixTimeStr { get => _routeFixTimeStr; set => _routeFixTimeStr = value; }
    [JsonProperty("routeServerTimeStr")]
    public string RouteServerTimeStr { get => _routeServerTimeStr; set => _routeServerTimeStr = value; }
    [JsonProperty("outdated")]
    public string Outdated { get => _outdated; set => _outdated = value; }
    [JsonProperty("valid")]
    public string Valid { get => _valid; set => _valid = value; }
    [JsonProperty("routeLatitude")]
    public string RouteLatitude { get => _routeLatitude; set => _routeLatitude = value; }
    [JsonProperty("routeLongitude")]
    public string RouteLongitude { get => _routeLongitude; set => _routeLongitude = value; }
    [JsonProperty("routeAltitude")]
    public string RouteAltitude { get => _routeAltitude; set => _routeAltitude = value; }
    [JsonProperty("routeSpeed")]
    public string RouteSpeed { get => _routeSpeed; set => _routeSpeed = value; }
    [JsonProperty("routeAddress")]
    public string RouteAddress { get => _routeAddress; set => _routeAddress = value; }
    [JsonProperty("routeAccuracy")]
    public string RouteAccuracy { get => _routeAccuracy; set => _routeAccuracy = value; }
    [JsonProperty("ignition")]
    public string Ignition { get => _ignition; set => _ignition = value; }
    [JsonProperty("routeAttributesStatus")]
    public string RouteAttributesStatus { get => _routeAttributesStatus; set => _routeAttributesStatus = value; }
    [JsonProperty("routeAttributesDistance")]
    public string RouteAttributesDistance { get => _routeAttributesDistance; set => _routeAttributesDistance = value; }
    [JsonProperty("routeAttributesTotalDistance")]
    public string RouteAttributesTotalDistance { get => _routeAttributesTotalDistance; set => _routeAttributesTotalDistance = value; }
    [JsonProperty("motion")]
    public string Motion { get => _motion; set => _motion = value; }
    [JsonProperty("routeAttributesHours")]
    public string RouteAttributesHours { get => _routeAttributesHours; set => _routeAttributesHours = value; }

    // [JsonProperty("id")]
    // public int Id { get => _id; set => _id = value; }



}
