using DotNetNuke.Entities.Modules;
using DotNetNuke.Services.Exceptions;
using System;

namespace GravityWorks.BecomeUser
{
    public partial class Settings : ModuleSettingsBase
    {
        public override void LoadSettings()
        {
            txtRoles.Text = Settings[ModuleSettingsNames.BlockedRole].ToString();
            txtSessionStateToClear.Text = Settings[ModuleSettingsNames.SessionObject].ToString();
        }
        public override void UpdateSettings()
        {
            try
            {
                var settings = new ModuleController();
                settings.UpdateModuleSetting(this.ModuleId, ModuleSettingsNames.BlockedRole, txtRoles.Text);
                settings.UpdateModuleSetting(this.ModuleId, ModuleSettingsNames.SessionObject, txtSessionStateToClear.Text);
                ModuleController.SynchronizeModule(this.ModuleId);
            }
            catch (Exception ex)
            {
                Exceptions.ProcessModuleLoadException(this, ex);
            }

        }
    }
}