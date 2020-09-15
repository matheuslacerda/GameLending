using GameLending.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace GameLending.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class GameLendingController : AbpController
    {
        protected GameLendingController()
        {
            LocalizationResource = typeof(GameLendingResource);
        }
    }
}