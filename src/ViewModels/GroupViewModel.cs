using System.Collections.Generic;

namespace Aisoftware.Tracker.Admin.Models
{
    public class GroupViewModel
    {
        public Group Group { get; set; }

        public IEnumerable<Group> Groups { get; set; }

    }
}