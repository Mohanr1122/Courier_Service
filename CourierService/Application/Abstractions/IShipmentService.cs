using CourierService.Core.Domain.Bussiness;

namespace CourierService.Core.Application.Abstractions
{
    public interface IShipmentService
    {
        /// <summary>
        /// Dispatch packages to vechicle
        /// </summary>
        /// <param name="packages"></param>
        /// <param name="vehicles"></param>
        /// <returns></returns>
        public IEnumerable<Vehicle> Dispatch(List<Package> packages, List<Vehicle> vehicles);
    }
}
