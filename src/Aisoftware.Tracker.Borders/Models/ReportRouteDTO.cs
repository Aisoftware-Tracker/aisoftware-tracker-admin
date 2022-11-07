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
    private double _routeAltitude;
    private double _routeSpeed;
    private string _routeAddress;
    private double _routeAccuracy;
    private string _ignition;
    private string _routeAttributesStatus;
    private decimal _routeAttributesDistance;
    private decimal _routeAttributesTotalDistance;
    private string _motion;
    private long _routeAttributesHours;

    public string LicensePlate { get => _licensePlate; set => _licensePlate = value; }
    public string RouteProtocol { get => _routeProtocol; set => _routeProtocol = value; }
    public string RouteDeviceTimeStr { get => _routeDeviceTimeStr; set => _routeDeviceTimeStr = value; }
    public string RouteFixTimeStr { get => _routeFixTimeStr; set => _routeFixTimeStr = value; }
    public string RouteServerTimeStr { get => _routeServerTimeStr; set => _routeServerTimeStr = value; }
    public string Outdated { get => _outdated; set => _outdated = value; }
    public string Valid { get => _valid; set => _valid = value; }
    public string RouteLatitude { get => _routeLatitude; set => _routeLatitude = value; }
    public string RouteLongitude { get => _routeLongitude; set => _routeLongitude = value; }
    public double RouteAltitude { get => _routeAltitude; set => _routeAltitude = value; }
    public double RouteSpeed { get => _routeSpeed; set => _routeSpeed = value; }
    public string RouteAddress { get => _routeAddress; set => _routeAddress = value; }
    public double RouteAccuracy { get => _routeAccuracy; set => _routeAccuracy = value; }
    public string Ignition { get => _ignition; set => _ignition = value; }
    public string RouteAttributesStatus { get => _routeAttributesStatus; set => _routeAttributesStatus = value; }
    public decimal RouteAttributesDistance { get => _routeAttributesDistance; set => _routeAttributesDistance = value; }
    public decimal RouteAttributesTotalDistance { get => _routeAttributesTotalDistance; set => _routeAttributesTotalDistance = value; }
    public string Motion { get => _motion; set => _motion = value; }
    public long RouteAttributesHours { get => _routeAttributesHours; set => _routeAttributesHours = value; }

    // [JsonProperty("id")]
    // public int Id { get => _id; set => _id = value; }



}
