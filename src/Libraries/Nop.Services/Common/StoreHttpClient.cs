using System;
using System.Net.Http;
using System.Threading.Tasks;
using Nop.Core;

namespace Nop.Services.Common
{
    /// <summary>
    /// Represents the HTTP client to request current store
    /// </summary>
    public partial class StoreHttpClient
    {
        #region Fields

        private readonly HttpClient _httpClient;
        private readonly IWebHelper _webHelper;

        #endregion

        #region Ctor

        public StoreHttpClient(HttpClient client,
            IWebHelper webHelper)
        {
            _httpClient = client;
            _webHelper = webHelper;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Keep the current store site alive
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation
        /// The task result contains the asynchronous task whose result determines that request completed
        /// </returns>
        public virtual async Task KeepAliveAsync()
        {
            //configure client
            _httpClient.BaseAddress = new Uri(await _webHelper.GetStoreLocationAsync());
            await _httpClient.GetStringAsync(NopCommonDefaults.KeepAlivePath);
        }

        #endregion
    }
}