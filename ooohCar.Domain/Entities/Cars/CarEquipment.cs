using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarEquipment
    {
        public CarEquipment()
        {
            CarOptionValues = new HashSet<CarOptionValue>();
        }

        public int IdCarEquipment { get; set; }
        public int IdCarTrim { get; set; }
        public string Name { get; set; }
        public int? Year { get; set; }
        public long DateCreate { get; set; }
        public long DateUpdate { get; set; }
        public int IdCarType { get; set; }
        public string Trial109 { get; set; }

        public virtual CarType IdCarTypeNavigation { get; set; }
        public virtual ICollection<CarOptionValue> CarOptionValues { get; set; }
    }
}
