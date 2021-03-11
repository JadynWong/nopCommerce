using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Nop.Core;
using Nop.Core.Events;
using Nop.Core.Infrastructure;
using Nop.Web.Framework.Events;
using Nop.Web.Framework.Models;

namespace Nop.Web.Framework.Components
{
    /// <summary>
    /// Base class for ViewComponent in nopCommerce
    /// </summary>
    public abstract class NopViewComponent : ViewComponent
    {
        private async Task PublishModelPreparedAsync<TModel>(TModel model)
        {
            //Components are not part of the controller life cycle.
            //Hence, we could no longer use Action Filters to intercept the Models being returned
            //as we do in the /Nop.Web.Framework/Mvc/Filters/PublishModelEventsAttribute.cs for controllers

            //model prepared event
            if (model is BaseNopModel)
            {
                var eventPublisher = EngineContext.Current.Resolve<IEventPublisher>();

                //we publish the ModelPrepared event for all models as the BaseNopModel, 
                //so you need to implement IConsumer<ModelPrepared<BaseNopModel>> interface to handle this event
                await eventPublisher.ModelPreparedAsync(model as BaseNopModel);
            }

            if (model is IEnumerable<BaseNopModel> modelCollection)
            {
                var eventPublisher = EngineContext.Current.Resolve<IEventPublisher>();

                //we publish the ModelPrepared event for collection as the IEnumerable<BaseNopModel>, 
                //so you need to implement IConsumer<ModelPrepared<IEnumerable<BaseNopModel>>> interface to handle this event
                await eventPublisher.ModelPreparedAsync(modelCollection);
            }
        }

        /// <summary>
        /// Returns a result which will render the partial view with name <paramref name="viewName"/>.
        /// </summary>
        /// <param name="viewName">The name of the partial view to render.</param>
        /// <param name="model">The model object for the view.</param>
        /// <returns>A <see cref="IViewComponentResult"/>.</returns>
        public async Task<IViewComponentResult> PublishAndViewAsync<TModel>(string viewName, TModel model)
        {
            await PublishModelPreparedAsync(model);

            //invoke the base method
            return base.View<TModel>(viewName, model);
        }

        /// <summary>
        /// Returns a result which will render the partial view
        /// </summary>
        /// <param name="model">The model object for the view.</param>
        /// <returns>A <see cref="IViewComponentResult"/>.</returns>
        public async Task<IViewComponentResult> PublishAndViewAsync<TModel>(TModel model)
        {
            await PublishModelPreparedAsync(model);

            //invoke the base method
            return base.View<TModel>(model);
        }
    }
}