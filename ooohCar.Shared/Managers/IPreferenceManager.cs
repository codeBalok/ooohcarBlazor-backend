using ooohCar.Shared.Settings;
using System.Threading.Tasks;

namespace ooohCar.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();
    }
}
