using System.Collections.Generic;
using Aisoftware.Tracker.Borders.Models;

namespace Aisoftware.Tracker.Borders.ViewModels
{
    public class GroupViewModel
    {
        public Group Group { get; set; }

        public IEnumerable<Group> Groups { get; set; }

    }
}