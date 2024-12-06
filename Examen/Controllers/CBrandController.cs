using Examen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    [Route("brands/")]
    [ApiController]
    public class CBrandController : ControllerBase
    {
        private readonly PruebasDbContext _context;
        public CBrandController(PruebasDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// obtiene todos los registros de las marcas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<CatBrand> Get()
        {
            return _context.CatBrands.ToList();
        }


        
        /// <summary>
        /// se agrega una nueva marca
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>

        [HttpPost]
        
        public IActionResult PostBrand([FromBody] CatBrand objeto)
        {

            var lsexistentes = _context.CatBrands.Where(x => x.Namee == objeto.Namee.Trim()).ToList();

            if (lsexistentes.Count == 0)
            {
                objeto.IdBrand = 0;
                _context.CatBrands.Add(objeto);
                _context.SaveChanges();
                return CreatedAtAction(nameof(PostBrand), new { id = objeto.IdBrand }, objeto);
            }
            else
            {
                // Retornar un error de conflicto (409 Conflict)
                return Conflict(new { message = $"The brand with name '{objeto.Namee}' already exists." });
            }
        }




        /// <summary>
        /// se agregan un nuevo modelo
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("id/models")]
        public IActionResult PostModel([FromBody] CatModel objeto)
        {

            var lsexistentes = _context.CatModels.Where(x => x.Namee == objeto.Namee.Trim() && x.IdBrand == objeto.IdBrand).ToList();

            if (lsexistentes.Count == 0)
            {
                objeto.IdModel = 0;
                _context.CatModels.Add(objeto);
                _context.SaveChanges();
                return CreatedAtAction(nameof(PostBrand), new { id = objeto.IdBrand }, objeto);
            }
            else
            {
                // Retornar un error de conflicto (409 Conflict)
                return Conflict(new { message = $"The model with name '{objeto.Namee}' already exists." });
            }
        }


    }
}
