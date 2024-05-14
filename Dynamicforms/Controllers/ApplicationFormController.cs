using DynamicForms.Dtos;
using DynamicForms.Helpers;
using DynamicForms.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace DynamicForms.Controllers
{
    [ApiController]
    [Route("api/v1/application-form")]
    public class ApplicationFormController : ControllerBase
    {


        private readonly IApplicationFormService _applicationService;

        public ApplicationFormController(IApplicationFormService applicationService)
        {
            _applicationService = applicationService;
        }

        /// <summary>
        /// Endpoint to submit applicant form
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(SuccessResponse<ApplicationFormDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CreateApplicationForm([FromBody] CreateApplicationForm model)
        {
            var result = await _applicationService.SubmitApplicationForm(model);
            return Ok(result);
        }

        /// <summary>
        /// Endpoint to get applicant form by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(SuccessResponse<ApplicationFormDto>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetApplicationFormbyId(Guid id)
        {
            var result = await _applicationService.GetApplicationFormById(id);
            return Ok(result);
        }

    }
}