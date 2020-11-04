using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFJ.Models
{
    public class ManagePlacementsModel
    {
        public int? totalPlacementsCount { get; set; }
        public List<PlacementsModel> placements { get; set; }
    }
}
