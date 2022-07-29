using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Application.Translator.Commands.AddTranslator;
using TranslationManagement.Application.Translator.Commands.DeleteTranslator;
using TranslationManagement.Application.Translator.Commands.UpdateTranslator;
using TranslationManagement.Application.Translator.Queries;
using TranslationManagement.Application.Translator.Queries.GetTranslator;
using TranslationManagement.Application.Translator.Queries.GetTranslators;

namespace TranslationManagement.Api.Controllers
{
    [Route("api/translators")]
    public class TranslatorsController : ApiController
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslatorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetTranslators()
        {
            return Ok(await Sender.Send(new GetTranslatorsQuery()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslatorDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTranslatorById(string id)
        {
            return Ok(await Sender.Send(new GetTranslatorQuery() { Id = id }));
        }

        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AddTranslator(AddTranslatorCommand command)
        {
            return Ok(await Sender.Send(command));
        }
        
        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTranslator(UpdateTranslatorCommand command)
        {
            return Ok(await Sender.Send(command));
        }
        
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTranslator(DeleteTranslatorCommand command)
        {
            return Ok(await Sender.Send(command));
        }
    }
}