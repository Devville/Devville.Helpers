// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ImpersonationHelper.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace Devville.Helpers.SharePoint
{
    using System;

    using Microsoft.SharePoint;

    /// <summary>
    ///     Impersonation Helper class.
    /// </summary>
    public static class ImpersonationHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Gets the system token.
        ///     Source: http://solutionizing.net/2009/01/06/elegant-spsite-elevation/
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <returns>
        /// The <see cref="SPUserToken"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/10/2013</created>
        public static SPUserToken GetSystemToken(this SPSite site)
        {
            SPUserToken token = null;
            bool catchAccessDeniedException = site.CatchAccessDeniedException;
            try
            {
                site.CatchAccessDeniedException = false;
                token = site.SystemAccount.UserToken;
            }
            catch (UnauthorizedAccessException)
            {
                SPSecurity.RunWithElevatedPrivileges(
                    () =>
                        {
                            using (var elevSite = new SPSite(site.ID))
                            {
                                token = elevSite.SystemAccount.UserToken;
                            }
                        });
            }
            finally
            {
                site.CatchAccessDeniedException = catchAccessDeniedException;
            }

            return token;
        }

        /// <summary>
        /// Runs as system.
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/10/2013</created>
        public static void RunAsSystem(this SPSite site, Action<SPSite> action)
        {
            using (var elevSite = new SPSite(site.ID, site.GetSystemToken()))
            {
                action(elevSite);
            }
        }

        /// <summary>
        /// The run as system.
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="webId">
        /// The web id.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public static void RunAsSystem(this SPSite site, Guid webId, Action<SPWeb> action)
        {
            site.RunAsSystem(s => action(s.OpenWeb(webId)));
        }

        /// <summary>
        /// The run as system.
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public static void RunAsSystem(this SPSite site, string url, Action<SPWeb> action)
        {
            site.RunAsSystem(s => action(s.OpenWeb(url)));
        }

        /// <summary>
        /// The run as system.
        /// </summary>
        /// <param name="web">
        /// The web.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        public static void RunAsSystem(this SPWeb web, Action<SPWeb> action)
        {
            web.Site.RunAsSystem(web.ID, action);
        }

        /// <summary>
        /// Runs the elevated.
        /// </summary>
        /// <param name="siteId">
        /// The site id.
        /// </param>
        /// <param name="webId">
        /// The web id.
        /// </param>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/10/2013</created>
        public static void RunElevated(Guid siteId, Guid webId, Action<SPSite, SPWeb> function)
        {
            SPSecurity.RunWithElevatedPrivileges(
                () =>
                    {
                        using (var site = new SPSite(siteId))
                        using (SPWeb web = site.OpenWeb(webId))
                        {
                            web.AllowUnsafeUpdates = true;
                            function(site, web);
                            web.AllowUnsafeUpdates = false;
                        }
                    });
        }

        /// <summary>
        /// Runs the elevated.
        /// </summary>
        /// <param name="siteId">
        /// The site id.
        /// </param>
        /// <param name="webId">
        /// The web id.
        /// </param>
        /// <param name="listName">
        /// Name of the list.
        /// </param>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/14/2013</created>
        public static void RunElevated(Guid siteId, Guid webId, string listName, Action<SPSite, SPWeb, SPList> function)
        {
            SPSecurity.RunWithElevatedPrivileges(
                () =>
                    {
                        using (var site = new SPSite(siteId))
                        using (SPWeb web = site.OpenWeb(webId))
                        {
                            SPList list = web.Lists.TryGetList(listName);
                            if (list != null)
                            {
                                web.AllowUnsafeUpdates = true;
                                function(site, web, list);
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
        }

        /// <summary>
        /// Runs the elevated.
        /// </summary>
        /// <param name="siteId">
        /// The site id.
        /// </param>
        /// <param name="webUrl">
        /// The web URL.
        /// </param>
        /// <param name="listName">
        /// Name of the list.
        /// </param>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/13/2013</created>
        public static void RunElevated(
            Guid siteId, 
            string webUrl, 
            string listName, 
            Action<SPSite, SPWeb, SPList> function)
        {
            SPSecurity.RunWithElevatedPrivileges(
                () =>
                    {
                        using (var site = new SPSite(siteId))
                        using (SPWeb web = site.OpenWeb(webUrl))
                        {
                            SPList list = web.Lists.TryGetList(listName);
                            if (list != null)
                            {
                                web.AllowUnsafeUpdates = true;
                                function(site, web, list);
                                web.AllowUnsafeUpdates = false;
                            }
                        }
                    });
        }

        /// <summary>
        /// Runs the elevated.
        /// </summary>
        /// <param name="siteId">
        /// The site id.
        /// </param>
        /// <param name="webUrl">
        /// The web URL.
        /// </param>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/10/2013</created>
        public static void RunElevated(Guid siteId, string webUrl, Action<SPSite, SPWeb> function)
        {
            SPSecurity.RunWithElevatedPrivileges(
                () =>
                    {
                        using (var site = new SPSite(siteId))
                        using (SPWeb web = site.OpenWeb(webUrl))
                        {
                            web.AllowUnsafeUpdates = true;
                            function(site, web);
                            web.AllowUnsafeUpdates = false;
                        }
                    });
        }

        /// <summary>
        /// Runs the elevated with RootWeb.
        /// </summary>
        /// <param name="siteId">
        /// The site id.
        /// </param>
        /// <param name="function">
        /// The function.
        /// </param>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/10/2013</created>
        public static void RunElevated(Guid siteId, Action<SPSite, SPWeb> function)
        {
            SPSecurity.RunWithElevatedPrivileges(
                () =>
                    {
                        using (var site = new SPSite(siteId))
                        using (SPWeb rootWeb = site.RootWeb)
                        {
                            rootWeb.AllowUnsafeUpdates = true;
                            function(site, rootWeb);
                            rootWeb.AllowUnsafeUpdates = false;
                        }
                    });
        }

        /// <summary>
        /// Selects as system.
        /// </summary>
        /// <typeparam name="T">
        /// </typeparam>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        /// <author>Ahmed Magdy (ahmed.magdy@devville.net)</author>
        /// <created>1/10/2013</created>
        public static T SelectAsSystem<T>(this SPSite site, Func<SPSite, T> selector)
        {
            using (var elevSite = new SPSite(site.ID, site.GetSystemToken()))
            {
                return selector(elevSite);
            }
        }

        /// <summary>
        /// The select as system.
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="webId">
        /// The web id.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T SelectAsSystem<T>(this SPSite site, Guid webId, Func<SPWeb, T> selector)
        {
            return site.SelectAsSystem(s => selector(s.OpenWeb(webId)));
        }

        /// <summary>
        /// The select as system.
        /// </summary>
        /// <param name="site">
        /// The site.
        /// </param>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T SelectAsSystem<T>(this SPSite site, string url, Func<SPWeb, T> selector)
        {
            return site.SelectAsSystem(s => selector(s.OpenWeb(url)));
        }

        /// <summary>
        /// The select as system.
        /// </summary>
        /// <param name="web">
        /// The web.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public static T SelectAsSystem<T>(this SPWeb web, Func<SPWeb, T> selector)
        {
            return web.Site.SelectAsSystem(web.ID, selector);
        }

        #endregion
    }
}