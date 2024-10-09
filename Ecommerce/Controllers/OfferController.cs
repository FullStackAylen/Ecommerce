using Controller;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Ecommerce.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class OfferController : ControllerBase
    {
        [HttpGet]
        //[Authorize(Roles = "administrator,clients")]
        public async Task<ActionResult> GetOffers(int page = 1, int limit = 10)
        {
            var count = await ControladoraOfertas.Instance.ListadoOfertas(); // Obtén todos los usuarios
            var offers = count.Skip((page - 1) * limit).Take(limit).ToList();
            return Ok(new
            {
                offers = offers,
                totalPages = (int)Math.Ceiling(count.Count / (double)limit)
            });
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Oferta>> GetOfferID(int id)
        {

            var offer = ControladoraOfertas.Instance.OfertaID(id).Result;
            if (offer == null) // Manejo de offeructo no encontrado
                return NotFound();

            return Ok(offer);
        }
        [HttpPost]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> AddOffer(Oferta offer) => await ControladoraOfertas.Instance.AgregarOferta(offer);
        [HttpPut]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> EditOffer(Oferta offer) => await ControladoraOfertas.Instance.EditarOferta(offer);
        [HttpDelete]
        //[Authorize(Roles = "administrator")]
        public async Task<ActionResult<string>> DeleteOffer(Oferta offer) => await ControladoraOfertas.Instance.EliminarOferta(offer);
    }
}
