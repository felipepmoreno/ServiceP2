using Microsoft.AspNetCore.Mvc;
using ServiceP2.Models;
using ServiceP2.Services;
using ServiceP2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceP2
{
    [Route("api/p2/validate")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IValidationService _validationService;
        public ValuesController(IValidationService validationService)
        {
            _validationService = validationService;
        }

        [HttpPost]
        public async Task<IActionResult> P2ValidationAsync([FromBody] P2BodyModel model)
        {
           try
           {
                bool validated = _validationService.ValidateInformations(model);
                return Ok(validated);
           }
           catch(Exception ex)
           {
                return StatusCode(500, ex.Message);     
           }
           
        }

    }
}

