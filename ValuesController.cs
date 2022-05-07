using Microsoft.AspNetCore.Mvc;
using ServiceP2.Models;
using ServiceP2.Services;
using ServiceP2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ServiceP2
{
    [Route("api/p2/validate")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        public IValidationService _validationService;
        public HttpClient client = new HttpClient();
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
                if(validated)
                    await client.PostAsync(
                        "https://localhost:5002/api/p3/generate-token", 
                        new StringContent(model.ToString(), Encoding.UTF8, "application/json")
                     );
                else
                {
                    return Ok("Parâmetros inválidos");
                }

                return Ok(validated);
           }
           catch(Exception ex)
           {
                return StatusCode(500, ex.Message);     
           }
           
        }

    }
}

