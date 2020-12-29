using API.ViewModels;
using Business;
using MediatR;
using Microsoft.AspNet.OData;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ExampleController : ControllerBase
    {
        private readonly IRepository<ExampleBusinessModel> repository;
        private readonly IMediator mediator;

        public ExampleController(IRepository<ExampleBusinessModel> repository, IMediator mediator)
        {
            this.repository = repository;
            this.mediator = mediator;
        }

        /*
        In this example the odata works via /example?$select=name but not with the the odata prefix eg. /odata/example?$select=name
        due to the odata system not finding the right get when routing
        Ideally for odata routes the odata prefix is included as it adds odata metadata to the response.
        
        This is only an issue if having 2 http gets in the same controller (odata and query based search) and so shouldnt be an issue in any production system 
        as either odata would be used or not. The example just contains both for deomnstration purposes
         */
        [HttpGet]
        [EnableQuery]
        public IEnumerable<ExampleBusinessModel> OdataGet()
        {
            return this.repository.All;
        }

        [HttpGet]
        [Route("rest")]
        public async Task<IActionResult> Get([FromQuery] ExampleQueryModel query)
        {
            return Ok(await repository.GetBySpecAsync(query.ToSpecification()));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await repository.GetByIdAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ExampleViewModel newModel)
        {
            await repository.AddAsync(newModel.ToBusiness());

            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await repository.DeleteByIdAsync(id);

            return Ok();
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> Put([FromBody] ExampleViewModel newModel)
        {
            await repository.UpdateAsync(newModel.ToBusiness());

            return Ok();
        }

        [HttpPatch]
        [Route("{id}")]
        public async Task<IActionResult> Patch(int id, [FromBody] JsonPatchDocument<ExampleViewModel> patchDoc)
        {
            var model = await repository.GetByIdAsync(id);

            if (model is null)
            {
                return NotFound();
            }

            var viewModel = new ExampleViewModel(model);

            patchDoc.ApplyTo(viewModel);

            await repository.UpdateAsync(viewModel.ToBusiness());

            return Ok();
        }

        [HttpPost]
        [Route("{id}/example-business-function")]
        public async Task<IActionResult> ExampleBusinessFunction(int id, [FromBody] ExampleBusinessFunctionParameters parameters)
        {
            var result = await mediator.Send(parameters.ToRequest(id));

            if (!result.Success)
            {
                return NotFound();
            }

            return Ok(new ExampleBusinessFunctionResult(result));
        }
    }
}
