using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ooohCar.Domain.Entities.Cars
{
    public partial class CarType
    {
        public CarType()
        {
            CarEquipments = new HashSet<CarEquipment>();
            CarGenerations = new HashSet<CarGeneration>();
            CarMakes = new HashSet<CarMake>();
            CarModels = new HashSet<CarModel>();
            CarOptionValues = new HashSet<CarOptionValue>();
            CarOptions = new HashSet<CarOption>();
            CarSeries = new HashSet<CarSerie>();
            CarSpecificationValues = new HashSet<CarSpecificationValue>();
            CarSpecifications = new HashSet<CarSpecification>();
            CarTrims = new HashSet<CarTrim>();
        }

        public int IdCarType { get; set; }
        public string Name { get; set; }
        public string Trial152 { get; set; }

        public virtual ICollection<CarEquipment> CarEquipments { get; set; }
        public virtual ICollection<CarGeneration> CarGenerations { get; set; }
        public virtual ICollection<CarMake> CarMakes { get; set; }
        public virtual ICollection<CarModel> CarModels { get; set; }
        public virtual ICollection<CarOptionValue> CarOptionValues { get; set; }
        public virtual ICollection<CarOption> CarOptions { get; set; }
        public virtual ICollection<CarSerie> CarSeries { get; set; }
        public virtual ICollection<CarSpecificationValue> CarSpecificationValues { get; set; }
        public virtual ICollection<CarSpecification> CarSpecifications { get; set; }
        public virtual ICollection<CarTrim> CarTrims { get; set; }
    }
}
