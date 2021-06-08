using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarTrim
    {
        public CarTrim()
        {
            CarSpecificationValues = new HashSet<CarSpecificationValue>();
        }

        public int IdCarTrim { get; set; }
        public int IdCarSerie { get; set; }
        public int IdCarModel { get; set; }
        public string Name { get; set; }
        public int? StartProductionYear { get; set; }
        public int? EndProductionYear { get; set; }
        public long DateCreate { get; set; }
        public long DateUpdate { get; set; }
        public int IdCarType { get; set; }
        public string Trial148 { get; set; }

        public virtual CarModel IdCarModelNavigation { get; set; }
        public virtual CarSerie IdCarSerieNavigation { get; set; }
        public virtual CarType IdCarTypeNavigation { get; set; }
        public virtual ICollection<CarSpecificationValue> CarSpecificationValues { get; set; }
    }
}
