using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TEST_DEV_MAGB_190621_BACK.Models;
using TEST_DEV_MAGB_190621_BACK.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;

namespace TEST_DEV_MAGB_190621_BACK.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonaFisicaController : ControllerBase
    {
        private readonly PRUEBASContext _context;
        public PersonaFisicaController(PRUEBASContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IEnumerable<TbPersonasFisica> GetPersonasFisicas()
        {
            return _context.Set<TbPersonasFisica>().Where(x=> x.Activo == true).ToArray();
        }
        [HttpGet("{id}")]
        public TbPersonasFisica GetPersonaFisica(int id)
        {
            return _context.Find<TbPersonasFisica>(id);
        }

        [HttpPost]
        public IActionResult Create(TbPersonasFisica personaFisica)
        {
            var parameters = new object[]
            {
                personaFisica.Nombre,
                personaFisica.ApellidoPaterno,
                personaFisica.ApellidoMaterno,
                personaFisica.Rfc,
                personaFisica.FechaNacimiento,
                personaFisica.UsuarioAgrega
            };
            string sql = "EXEC dbo.sp_AgregarPersonaFisica @Nombre = {0}, @ApellidoPaterno = {1}, @ApellidoMaterno = {2}, @RFC = {3}, @FechaNacimiento = {4}, @UsuarioAgrega = {5}";
            if (ExecuteStoredProcedure(sql, parameters))
                return Ok();
            else
                return BadRequest();
        }

        [HttpPut]
        public IActionResult Put(TbPersonasFisica personaFisica)
        {
            var parameters = new object[]
            {
                personaFisica.IdPersonaFisica,
                personaFisica.Nombre,
                personaFisica.ApellidoPaterno,
                personaFisica.ApellidoMaterno,
                personaFisica.Rfc,
                personaFisica.FechaNacimiento,
                personaFisica.UsuarioAgrega
            };
            string sql = "EXEC dbo.sp_ActualizarPersonaFisica @IdPersonaFisica = {0}, @Nombre = {1}, @ApellidoPaterno = {2}, @ApellidoMaterno = {3}, @RFC = {4}, @FechaNacimiento = {5}, @UsuarioAgrega = {6}";
            if (ExecuteStoredProcedure(sql, parameters))
                return Ok();
            else
                return BadRequest();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            string sql = "EXEC sp_EliminarPersonaFisica @IdPersonaFisica = {0}";
            if (ExecuteStoredProcedure(sql, id))
                return Ok();
            else
                return BadRequest();
        }
        internal bool ExecuteStoredProcedure(string sql, params object[] parameters)
        {
            bool success = false;
            using IDbContextTransaction transaction = _context.Database.BeginTransaction();
            try
            {
                _context.Database.ExecuteSqlRaw(sql, parameters);
                transaction.Commit();
                success = true;
            }
            catch(SqlException ex)
            {
                if (ex.Number == 5000) success = false;
                transaction.Rollback();
            }
            return success;
        }
    }
}
