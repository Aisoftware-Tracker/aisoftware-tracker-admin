namespace Aisoftware.Tracker.Borders.Models.Reports
{
    public static class EventType
    {
        private const string FA_CAR = "fas fa-car";
        private const string FA_KEY = "fa fa-key";
        private const string FA_BELL = "fa fa-bell";

        public static IDictionary<string, string> Get()
        {
            return new Dictionary<string, string>
            {
                { "deviceOffline", "Dispositivo Desligado" },
                { "deviceOnline", "Dispositivo Ligado"  },
                { "deviceMoving", "Dispositivo em Movimento" },
                { "deviceStopped", "Dispositivo Parado" },
                { "ignitionOn", "Ignição Ligada" },
                { "ignitionOff", "Ignição Desligada" },
                { "alarm", "Alarme" },
            };
        }

        public static IDictionary<string, string> GetIcon()
        {
            return new Dictionary<string, string>
            {
                { "deviceOffline", FA_CAR },
                { "deviceOnline", FA_CAR  },
                { "deviceMoving", FA_CAR },
                { "deviceStopped", FA_CAR },
                { "ignitionOn", FA_KEY },
                { "ignitionOff", FA_KEY },
                { "alarm", FA_BELL },
            };
        }
    }
}
