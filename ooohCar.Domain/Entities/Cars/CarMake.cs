using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarMake
    {
        public CarMake()
        {
            CarModels = new HashSet<CarModel>();
        }

        public int IdCarMake { get; set; }
        public string Name { get; set; }
        public long DateCreate { get; set; }
        public long DateUpdate { get; set; }
        public int IdCarType { get; set; }
        public string Trial112 { get; set; }

        public virtual CarType IdCarTypeNavigation { get; set; }
        public virtual ICollection<CarModel> CarModels { get; set; }
    }
}
