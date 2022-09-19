using Aisoftware.Tracker.Borders.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aisoftware.Tracker.Reports.UseCases
{
    public class ExternalMapsTool
    {
        public List<decimal[]> GetRoutes(IEnumerable<ReportRoute> routes)
        {
            List<decimal[]> latLongList = new List<decimal[]>();

            // if (routes == null || routes.Count() == 0)
            // {
            //     return string.Empty;
            // }

            int i = 0;
            foreach (var item in routes)
            {
                if (!(i - 1 == -1))
                {
                    if (
                        item.Latitude != routes.ToList()[i - 1].Latitude &&
                        item.Longitude != routes.ToList()[i - 1].Longitude
                    )
                    {
                        decimal[] latLong = new decimal[2] { item.Latitude, item.Longitude };
                        latLongList.Add(latLong);
                    }
                }
                i++;
            }

            return latLongList.ToList();

        }

        private string BuildRoutes(List<decimal[]> latLongList)
        {
            string latLong = string.Empty;

            int i = 0;
            int size = latLongList.Count;

            foreach (var item in latLongList)
            {
                i++;
                string atSign = i == size ? "@" : string.Empty;
                latLong += $"/{atSign}{item[0]},{item[1]}";
            }

            return $"https://www.google.com/maps/dir{latLong}";
        }
    }
}
