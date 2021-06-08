using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarOptionValue
    {
        public int IdCarOptionValue { get; set; }
        public int IdCarOption { get; set; }
        public int IdCarEquipment { get; set; }
        public short IsBase { get; set; }
        public long DateCreate { get; set; }
        public long DateUpdate { get; set; }
        public int IdCarType { get; set; }
        public string Trial122 { get; set; }

        public virtual CarEquipment IdCarEquipmentNavigation { get; set; }
        public virtual CarOption IdCarOptionNavigation { get; set; }
        public virtual CarType IdCarTypeNavigation { get; set; }
    }
}
