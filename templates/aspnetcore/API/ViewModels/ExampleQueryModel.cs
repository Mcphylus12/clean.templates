using API.ViewModels;
using Ardalis.Specification;
using Business;
using System;

namespace API.ViewModels
{
    public class ExampleQueryModel
    {
        internal ISpecification<ExampleBusinessModel, ExampleViewModel> ToSpecification()
        {
            throw new NotImplementedException();
        }
    }
}