using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarOption
    {
        public CarOption()
        {
            CarOptionValues = new HashSet<CarOptionValue>();
            InverseIdParentNavigation = new HashSet<CarOption>();
        }

        public int IdCarOption { get; set; }
        public string Name { get; set; }
        public int? IdParent { get; set; }
        public long DateCreate { get; set; }
        public long DateUpdate { get; set; }
        public int IdCarType { get; set; }
        public string Trial119 { get; set; }

        public virtual CarType IdCarTypeNavigation { get; set; }
        public virtual CarOption IdParentNavigation { get; set; }
        public virtual ICollection<CarOptionValue> CarOptionValues { get; set; }
        public virtual ICollection<CarOption> InverseIdParentNavigation { get; set; }
    }
}
