using DotNetNuke.Common.Utilities;
using DotNetNuke.Entities.Modules;
using DotNetNuke.Entities.Portals;
using DotNetNuke.Entities.Users;
using DotNetNuke.Security;
using System;
using System.Collections;
using System.Web;
using System.Web.SessionState;

namespace GravityWorks.BecomeUser
{
    public partial class Default : PortalModuleBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SetUserName(UserInfo);
        }

        private int GetUserId(string userName)
        {
            int tmpRtn = 0;
            int totalRecords = 0;

            ArrayList users = UserController.GetUsersByUserName(PortalId, userName, 0, Int16.MaxValue, ref totalRecords);
            users.Sort();

            if (users.Count > 0)
            {
                UserInfo user = (UserInfo)users[0];
                string blockedRole = string.Empty;
                if (Settings[ModuleSettingsNames.BlockedRole] != null)
                    blockedRole = Settings[ModuleSettingsNames.BlockedRole].ToString();

                if (blockedRole.Contains(","))
                {
                    var multipleRoles = blockedRole.Split(',');
                    foreach (var role in multipleRoles)
                    {
                        if (user.IsInRole(role))
                        {
                            return 0;
                        }
                    }
                    tmpRtn = user.UserID;
                }
                else
                {
                    if (!string.IsNullOrEmpty(blockedRole))
                    {
                        if (user.IsInRole(blockedRole) == false)
                            tmpRtn = user.UserID;
                    }
                    else
                    {
                        tmpRtn = user.UserID;
                    }

                }
            }

            // return
            return tmpRtn;
        }

        protected void btnBecomeUser_Click(object sender, EventArgs e)
        {
            int userId = GetUserId(txtUserName.Text.Trim());
            string url = BecomeUser(userId, this.UserId, Context, this.PortalSettings, Session);
            Response.Redirect(url);
        }

        public string BecomeUser(int userToBecomeId, int currentlyLoggedInUser, HttpContext context, PortalSettings portalSettings, HttpSessionState sessionState)
        {
            string url = string.Empty;
            string sessionStateName = string.Empty;
            if (Settings[ModuleSettingsNames.SessionObject] != null)
                sessionStateName = Settings[ModuleSettingsNames.SessionObject].ToString();
            if (userToBecomeId > 0)
            {
                DataCache.ClearUserCache(portalSettings.PortalId, context.User.Identity.Name);
                PortalSecurity portalSecurity = new PortalSecurity();
                portalSecurity.SignOut();

                UserInfo newUserInfo = UserController.GetUserById(portalSettings.PortalId, userToBecomeId);

                if (newUserInfo != null)
                {
                    sessionState.Contents[sessionStateName] = null;
                    UserController.UserLogin(portalSettings.PortalId, newUserInfo, portalSettings.PortalName, HttpContext.Current.Request.UserHostAddress, false);

                    if (currentlyLoggedInUser != 0)
                    {
                        sessionState[sessionStateName] = currentlyLoggedInUser;
                    }
                    else
                    {
                        sessionState[sessionStateName] = null;
                    }
                    url = (context.Request.UrlReferrer.AbsoluteUri);
                }
            }

            return url;
        }

        private void SetUserName(UserInfo userInfo)
        {
            lblCurrentUserName.Text = userInfo.Username;
        }
    }
}