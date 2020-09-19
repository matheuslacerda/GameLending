using GameLending.Localization;
using Volo.Abp.Application.Services;

namespace GameLending
{
    /* Inherit your application services from this class.
     */
    public abstract class GameLendingAppService : ApplicationService
    {
        protected GameLendingAppService()
        {
            LocalizationResource = typeof(GameLendingResource);
        }
    }
}
