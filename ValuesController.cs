using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ServiceP2.Models;
using ServiceP2.Services;
using ServiceP2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

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
        public async Task<string> P2ValidationAsync([FromBody] P2BodyModel model)
        {
           try
           {
                bool validated = _validationService.ValidateInformations(model);

                if (validated)
                {
                    var jsonModel = JsonConvert.SerializeObject(model);

                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:5002/api/p3/generate-token");
                    request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    request.Content = new StringContent(jsonModel, Encoding.UTF8);
                    request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    var response = await client.SendAsync(request);
                    var content = await response.Content.ReadAsStringAsync();

                    return content.ToString();
                }
                else
                    return "Parâmetros inválidos";
                
           }
           catch(Exception ex)
           {
                return ex.Message;     
           }
           
        }

    }
}

