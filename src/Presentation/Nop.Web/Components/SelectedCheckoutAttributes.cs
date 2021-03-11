﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Factories;
using Nop.Web.Framework.Components;

namespace Nop.Web.Components
{
    public class SelectedCheckoutAttributesViewComponent : NopViewComponent
    {
        private readonly IShoppingCartModelFactory _shoppingCartModelFactory;

        public SelectedCheckoutAttributesViewComponent(IShoppingCartModelFactory shoppingCartModelFactory)
        {
            _shoppingCartModelFactory = shoppingCartModelFactory;
        }

        /// <returns>A task that represents the asynchronous operation</returns>
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var attributes = await _shoppingCartModelFactory.FormatSelectedCheckoutAttributesAsync();
            return await PublishAndViewAsync(null, attributes);
        }
    }
}
