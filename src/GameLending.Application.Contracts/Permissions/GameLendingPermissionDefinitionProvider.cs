using GameLending.Localization;
using Volo.Abp;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace GameLending.Permissions
{
    public class GameLendingPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Design", "CA1062:Validate arguments of public methods", Justification = "Check.NotNull")]
        public override void Define(IPermissionDefinitionContext context)
        {
            Check.NotNull(context, nameof(context));

            var myGroup = context.AddGroup(GameLendingPermissions.GroupName);

            //Define your own permissions here. Example:
            myGroup.AddPermission(GameLendingPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<GameLendingResource>(name);
        }
    }
}
