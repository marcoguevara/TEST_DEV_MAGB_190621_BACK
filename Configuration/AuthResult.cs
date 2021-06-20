using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TEST_DEV_MAGB_190621_BACK.Configuration
{
    public class AuthResult
    {
        public string Token { get; set; }
        public bool Success { get; set; }
        public List<String> Errors { get; set; }
    }
}
