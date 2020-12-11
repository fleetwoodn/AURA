using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AURA.ViewModels
{
    public class ThreeDayAvailabilityVM
    {
        public string ThrZero { get; set; }
        public int ThrDigit { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime ThrDate { get; set; }
        public string ThrText { get; set; }

        public IEnumerable<AURA.Models.PostThr> Thr0X { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr0Y { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr0Z { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr00 { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr01 { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr02 { get; set; }
        public IEnumerable<AURA.Models.PostThr> Thr03 { get; set; }
    }
}
