// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ApplicationStartModuleBase.cs" company="Devville">
//   Copyright © 2015 All Right Reserved
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Devville.Helpers.Web
{
    using System.Web;

    /// <summary>
    /// The application start module base.
    /// </summary>
    public abstract class ApplicationStartModuleBase : IHttpModule
    {
        #region Static Fields

        /// <summary>
        /// The application start lock.
        /// </summary>
        private static readonly object ApplicationStartLock = new object();

        /// <summary>
        /// The application started.
        /// </summary>
        private static volatile bool applicationStarted;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        ///     Disposes of the resources (other than memory) used by the module that implements
        ///     <see cref="T:System.Web.IHttpModule" />.
        /// </summary>
        public void Dispose()
        {
            // dispose any resources if needed
        }

        /// <summary>
        /// Initializes the specified module.
        /// </summary>
        /// <param name="context">
        /// The application context that instantiated and will be running this module.
        /// </param>
        public void Init(HttpApplication context)
        {
            if (!applicationStarted)
            {
                lock (ApplicationStartLock)
                {
                    if (!applicationStarted)
                    {
                        // this will run only once per application start
                        this.OnStart(context);
                        applicationStarted = true;
                    }
                }
            }

            // this will run on every HttpApplication initialization in the application pool
            this.OnInit(context);
        }

        /// <summary>
        /// Initializes any data/resources on HTTP module start.
        /// </summary>
        /// <param name="context">
        /// The application context that instantiated and will be running this module.
        /// </param>
        public virtual void OnInit(HttpApplication context)
        {
            // put your module initialization code here
        }

        /// <summary>
        /// Initializes any data/resources on application start.
        /// </summary>
        /// <param name="context">
        /// The application context that instantiated and will be running this module.
        /// </param>
        public abstract void OnStart(HttpApplication context);

        #endregion
    }
}