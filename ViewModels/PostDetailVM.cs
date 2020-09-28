using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class PostDetailVM
    {
        public string OneZero { get; set; }
        public string OneStag { get; set; }
        public string OneAgen { get; set; }
        public string OnePart { get; set; }
        public string OneTitl { get; set; }

        public IEnumerable<AURA.Models.PostThr> PostThrs { get; set; }
        public IEnumerable<AURA.Models.PostFou> PostFous { get; set; } 
        public IEnumerable<AURA.Models.PostFiv> PostFivs { get; set; }
        public IEnumerable<AURA.Models.PostSix> PostSixs { get; set; }
        public IEnumerable<AURA.Models.PostSev> PostSevs { get; set; }
        public IEnumerable<AURA.Models.PostEig> PostEigs { get; set; }
        public IEnumerable<AURA.Models.PostNin> PostNins { get; set; }
    }
}
