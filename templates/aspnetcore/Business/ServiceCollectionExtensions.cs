using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Business
{
    public static class ServiceCollectionExtensions
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddMediatR(GetMarkerTypes());
        }

        /// <summary>
        /// Returns a list containing 1 IRequestHandler from each assembly that contains one
        /// </summary>
        /// <returns></returns>
        private static Type[] GetMarkerTypes()
        {
            return new Type[]
                {
                    typeof(ExampleBusinessFunction)
                };
        }
    }
}
