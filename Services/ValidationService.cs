using ServiceP2.Models;
using ServiceP2.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ServiceP2.Services
{
    public class ValidationService : IValidationService
    {
        public bool ValidateInformations(P2BodyModel model)
        {
            if (model.n > 5000 && model.n < 15000)
            {
                if (model.code >= 10000000)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
