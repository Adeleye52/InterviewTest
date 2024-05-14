using DynamicForms.Dtos;
using DynamicForms.Helpers;
using DynamicForms.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DynamicForms.Controllers
{
    [ApiController]
    [Route("api/v1/program-details")]
    public class ProgramDetailController : ControllerBase
    {


        private readonly IProgramDetailsService _programDetailsService;

        public ProgramDetailController(IProgramDetailsService programDetailsService)
        {
            _programDetailsService = programDetailsService;
        }

        /// <summary>
        /// Endpoint to add a new Program detail form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<ProgramDetailsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddProgramDetails([FromBody] AddProgramDetails model)
        {
            var result = await _programDetailsService.AddProgramDetail(model);
            return Ok(result);
        }
        /// <summary>
        /// Endpoint to update Program detail form
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType(typeof(SuccessResponse<ProgramDetailsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdaterogramDetails(Guid id, [FromBody] UpdateProgramDetails model)
        {
            var result = await _programDetailsService.UpdateProgramDetail(id, model);
            return Ok(result);
        }

        /// <summary>
        /// Endpoint to get program detail by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<ProgramDetailsDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetProgramDetailById(Guid id)
        {
            var result = await _programDetailsService.GetProgramDetailById(id);
            return Ok(result);
        }

        /// <summary>
        /// Endpoint to retrieve Program details
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetProgramDetails))]
        [ProducesResponseType(typeof(PagedResponse<IEnumerable<ProgramDetailsDto>>), 200)]
        public async Task<IActionResult> GetProgramDetails([FromQuery] ResourceParameter parameter)
        {
            var response = await _programDetailsService.GetProgramDetails(parameter, nameof(GetProgramDetails), Url);
            return Ok(response);
        }

        /// <summary>
        /// Endpoint to retrieve questions by type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <returns></returns>

        [HttpGet("question-by-type")]
        [ProducesResponseType(typeof(SuccessResponse<IEnumerable<Entities.CustomQuestion>>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetQuestionsbyType(Guid id, string type)
        {
            var result = await _programDetailsService.GetQuestionByType(id,type);
            return Ok(result);
        }
    }
}