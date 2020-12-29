using Business;

namespace API.ViewModels
{
    public class ExampleBusinessFunctionParameters
    {
        public int AgeToAdd { get; set; }

        internal ExampleParam ToRequest(int id)
        {
            return new ExampleParam(id, AgeToAdd);
        }
    }
}