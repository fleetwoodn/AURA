using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.ViewModels
{
    public class PriceToolAvailabilityVM
    {





        //for the dates
        public IEnumerable<AURA.Models.PostThr> ThrZ { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr0 { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr1 { get; set; }
    }
}
