using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AURA.ViewModels
{
    public class ManifestVM
    {
        public IEnumerable<AURA.Models.Manifest> ManifestsI  { get; set; }
        public IEnumerable<AURA.Models.Manifest> ManifestsF { get; set; }
        public IEnumerable<AURA.Models.Manifest> ManifestsL { get; set; }
        public IEnumerable<AURA.Models.Manifest> ManifestsS { get; set; }
        public IEnumerable<AURA.Models.Manifest> ManifestsO { get; set; }
    }
}
