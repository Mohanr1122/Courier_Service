using CourierService.Core.Domain.Abstractions;

namespace CourierService.Core.Domain.Services
{
    public class MaxPackageSelectionService : IPackageSelection
    {
        public IEnumerable<long> GetPackages(List<(long Id, int Weight)> values, int capacity)
        {
            var weightMap = new Dictionary<int, List<long>>
            {
                [0] = []
            };

            foreach (var (Id, Weight) in values)
            {
                var snapshot = weightMap.ToList();
                foreach (var entry in snapshot)
                {
                    int newWeight = entry.Key + Weight;
                    if (newWeight > capacity)
                        continue;

                    if (!weightMap.ContainsKey(newWeight))
                    {
                        weightMap[newWeight] = [.. entry.Value, Id];
                    }
                }
            }
            var maxWeightPackage = weightMap
                .OrderByDescending(x => x.Key)
                .First();
            return maxWeightPackage.Value;
        }
    }
}
