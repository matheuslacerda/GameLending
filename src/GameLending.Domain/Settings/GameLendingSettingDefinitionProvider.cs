using Volo.Abp.Settings;

namespace GameLending.Settings
{
    public class GameLendingSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(GameLendingSettings.MySetting1));
        }
    }
}
