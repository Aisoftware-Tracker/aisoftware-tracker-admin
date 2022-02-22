using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Domain.Common.Constants
{
    public static class States
    {
        public static IList<string> Get()
        {
            return new List<string>
            {
                "AC", "AL", "AP", "AM", "BA", "CE", "DF", "ES", "GO",
                "MA", "MT", "MS", "MG", "PA", "PB", "PR", "PE", "PI",
                "RJ", "RN", "RS", "RO", "RR", "SC", "SP", "SE", "TO"
            };
        }
    }
}
