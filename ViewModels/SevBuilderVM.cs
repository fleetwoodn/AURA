using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class SevBuilderVM
    {
        public string SevZero { get; set; }
        public int SevDigit { get; set; }
        public string SevDesc { get; set; }
        public decimal SevAmou { get; set; }
        public string SevAc2 { get; set; }
        public string SevAcf { get; set; }
        public string SevHidd { get; set; }
        public string SevNote { get; set; }

        public IEnumerable<AURA.Models.ProductList> ProductLists { get; set; }
    }
}
