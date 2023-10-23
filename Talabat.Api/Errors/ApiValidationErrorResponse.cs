using System;
using System.Collections.Generic;

namespace Talabat.Api.Errors
{
    public class ApiValidationErrorResponse : ApiResponse
    {
        public IEnumerable<string> Errors { get; set; }
        public ApiValidationErrorResponse():base(400)
        {
                
        }
    }
}
