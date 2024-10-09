using Controller;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserHistoryController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetHistoryUsers(int page = 1, int limit = 10)
        {
            var count = ControladoraHistorialUsuarios.Instance.ListadoHistorial().Result;
            var history = count.Skip((page - 1) * limit).Take(limit).ToList();

            return Ok(new
            {
                historyUsers = history,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit) // Cambié totalPage a totalPages
            });
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddHistoryUser(HistorialUsuario history) => await ControladoraHistorialUsuarios.Instance.AgregarHistorial(history);
        [HttpPut]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> EditHistoryUser(HistorialUsuario history) => await ControladoraHistorialUsuarios.Instance.EditarHistorial(history);
   
    }
}
