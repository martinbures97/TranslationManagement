using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TranslationManagement.Api.Common;
using TranslationManagement.Application.TranslationJob.Commands.AddTranslationJob;
using TranslationManagement.Application.TranslationJob.Commands.DeleteTranslationJob;
using TranslationManagement.Application.TranslationJob.Commands.UpdateTranslationJobCommand;
using TranslationManagement.Application.TranslationJob.Queries;
using TranslationManagement.Application.TranslationJob.Queries.GetTranslationJob;
using TranslationManagement.Application.TranslationJob.Queries.GetTranslationJobs;

namespace TranslationManagement.Api.Controllers
{
    [Route("api/translationJobs")]
    public class TranslationJobsController : ApiController
    {
        private readonly IMapper _mapper;

        public TranslationJobsController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslationJobDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetJobs()
        {
            return Ok(await Sender.Send(new GetTranslationJobsQuery()));
        }
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TranslationJobDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetJobById(string id)
        {
            return Ok(await Sender.Send(new GetTranslationJobQuery() { Id = id }));  
        }
        
        [HttpPost]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddJob(AddTranslationJobCommand command)
        {
            return Ok(await Sender.Send(command));
        }
        
        [HttpPost]
        [Route("/translationJobs/addJobWithFile")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> AddJobWithFile(IFormFile file, string? customer)
        {
            return Ok(await Sender.Send(_mapper.Map<AddTranslationJobCommand>(
                    await FileParser.GetDataFromFile(file, customer)
                ))
                
            );
        }

        [HttpPut]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateJob(UpdateTranslationJobCommand command)
        {
            return Ok(await Sender.Send(command));
        }
        
        [HttpDelete]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteJob(DeleteTranslationJobCommand command)
        {
            return Ok(await Sender.Send(command));
        }
    }
}