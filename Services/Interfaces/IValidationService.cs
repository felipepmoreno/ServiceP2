using ServiceP2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServiceP2.Services.Interfaces
{
    public interface IValidationService
    {
        public bool ValidateInformations(P2BodyModel model);
    }
}
