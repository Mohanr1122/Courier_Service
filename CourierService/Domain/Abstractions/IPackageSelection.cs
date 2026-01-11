namespace CourierService.Core.Domain.Abstractions
{
    /// <summary>
    /// Package Selection
    /// </summary>
    public interface IPackageSelection
    {
        /// <summary>
        /// Get Maximum weighted Packages based on the capacity
        /// </summary>
        /// <param name="values"></param>
        /// <param name="capacity"></param>
        /// <returns></returns>
        public IEnumerable<long> GetPackages(List<(long Id, int Weight)> values, int capacity);
    }
}
