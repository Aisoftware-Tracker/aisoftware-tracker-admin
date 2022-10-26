using Newtonsoft.Json;

namespace Aisoftware.Tracker.Borders.Models;
public class Geoference
{
    private int _id;
    private string _name;

    [JsonProperty("id")]
    public int Id { get => _id; set => _id = value; }

    [JsonProperty("name")]
    public string Name { get => _name; set => _name = value; }

}

