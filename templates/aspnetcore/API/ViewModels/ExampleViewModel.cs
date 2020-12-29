using Business;
using Mapster;
using System;

namespace API.ViewModels
{
    public class ExampleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public ExampleViewModel(ExampleBusinessModel model)
        {
            model.Adapt(this);
            /*
            Mapster is a light mapper that can be used without a bunch of setup.
            Tests can be made not caring how the maps occurs. this could be moved 
            to a manual map with no issue testwise
             */
        }

        internal ExampleBusinessModel ToBusiness()
        {
            return this.Adapt<ExampleBusinessModel>();
        }
    }
}
