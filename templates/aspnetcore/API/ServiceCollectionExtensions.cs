using API.Validators;
using API.ViewModels;
using Business;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace API
{
    public static class ServiceCollectionExtensions
    {
        public static void AddWebServices(this IServiceCollection services)
        {
            services.AddTransient<IValidator<ExampleViewModel>, ExampleValidator>();
            
        }
    }
}
