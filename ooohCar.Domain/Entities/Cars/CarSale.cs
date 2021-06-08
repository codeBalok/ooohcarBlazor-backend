using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarSale
    {
        public long Id { get; set; }
        public int? IdCarMake { get; set; }
        public int? IdCarModel { get; set; }
        public int? IdCarTrim { get; set; }
        public int? IdCarType { get; set; }
        public int? IdCarColor { get; set; }
        public decimal? CarPrice { get; set; }
        public int? CarYear { get; set; }
        public int? CarKmDriven { get; set; }
        public string CarOwnership { get; set; }
        public string CarLocation { get; set; }
        public long? CarSaleImageId { get; set; }
        public string CarSallerName { get; set; }
        public string CarSallerContactType { get; set; }
        public string CarSallerContact { get; set; }
    }
}
