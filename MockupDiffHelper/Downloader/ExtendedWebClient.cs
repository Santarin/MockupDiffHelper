using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MockupDiffHelper.Downloader
{
    /// <summary>
    /// Extends <see cref="WebClient"/> class to provide a property where we can specify/set the timeout value
    /// </summary>
    public class ExtendedWebClient : WebClient
    {
        private int timeout;

        /// <summary>
        /// Gets or sets the timeout.
        /// </summary>
        /// <value>The timeout.</value>
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedWebClient"/> class and set web request time to 60 sec by default.
        /// </summary>
        public ExtendedWebClient()
        {
            this.timeout = 60000; // In Milli seconds
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExtendedWebClient"/> class.
        /// </summary>
        /// <param name="timeout">The timeout </param>
        public ExtendedWebClient(int timeout)
        {
            this.timeout = timeout; // In Milli seconds
        }

        /// <summary>
        /// Returns a <see cref="T:System.Net.WebRequest"/> object for the specified resource.
        /// </summary>
        /// <param name="address">A <see cref="T:System.Uri"/> that identifies the resource to request.</param>
        /// <returns>
        /// A new <see cref="T:System.Net.WebRequest"/> object for the specified resource.
        /// </returns>
        protected override WebRequest GetWebRequest(Uri address)
        {
            var webRequest = base.GetWebRequest(address);
            webRequest.Timeout = this.timeout;
            return webRequest;
        }
    }
}
