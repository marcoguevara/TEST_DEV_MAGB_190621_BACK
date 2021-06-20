using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TEST_DEV_MAGB_190621_BACK.Models;
using TEST_DEV_MAGB_190621_BACK.Data;

namespace TEST_DEV_MAGB_190621_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaFisicaController : ControllerBase
    {
        private readonly PRUEBASContext _context;
        public PersonaFisicaController(PRUEBASContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<TbPersonasFisica> Get()
        {
            return _context.Set<TbPersonasFisica>().ToArray();
        }
    }
}
