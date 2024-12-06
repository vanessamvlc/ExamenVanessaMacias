using Examen.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Newtonsoft.Json;

namespace Examen.Controllers
{
    [Route("model/[controller]")]
    [ApiController]
    public class CModelController : ControllerBase
    {
        private readonly PruebasDbContext _context;
        public CModelController(PruebasDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// obtiene todas las modelos de acuerdo al id de las marcas
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id}/models")]
        public List<CatModel> Get(int id)
        {
            return _context.CatModels.Where(x => x.IdBrand == id).ToList();
        }


        /// <summary>
        /// actualiza solo el precio de un objeto
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("id")]
        public CatModel PutModel([FromBody] CatModel objeto)
        {
            var res = _context.CatModels.Where(x => x.IdModel == objeto.IdModel).FirstOrDefault();

            res.AveragePrice = objeto.AveragePrice;
            _context.SaveChanges();

            return objeto;
        }



        /// <summary>
        /// obtiene el listado de los modelos que estan dentro de un rango de precios
        /// </summary>
        /// <param name="preciomayor"></param>
        /// <param name="preciomenor"></param>
        /// <returns></returns>
        [HttpGet]
        //[Route("?mayor={preciomayor}&menor={preciomenor}")]
        public List<CatModel> GetModelsRange( decimal preciomenor, decimal preciomayor) {

            return  _context.CatModels.Where(x => x.AveragePrice >= preciomenor && x.AveragePrice <= preciomayor).ToList();

        }



        [HttpPost]
        [Route("InsertData")]
        public void InsertData([FromBody] List<CatJsonGenerico> lst) 
        { 

            //var lst = JsonConvert.DeserializeObject<List<CatJsonGenerico>>(json);
            var lstBrand = new List<CatBrand>();
            var lstModel = new List<CatModel>();

            var lstb = lst.Select(x => x.brand_name).Distinct().ToList();
            
            
            foreach (var vl in lstb) {
                lstBrand.Add( new CatBrand() { Namee= (vl??"") });
            }
            _context.CatBrands.AddRange(lstBrand);
            _context.SaveChanges();


            foreach (var val in lst) {
                var idbrand = lstBrand.Where(x => x.Namee == val.brand_name).Select(x => x.IdBrand).FirstOrDefault();
                lstModel.Add(new CatModel() { Namee= val.name, IdBrand = idbrand, AveragePrice=val.average_price });
            }
            _context.CatModels.AddRange(lstModel);
            _context.SaveChanges();

            foreach (var brands in lstBrand) {

                var numModel = lstModel.Where(x => x.IdBrand == brands.IdBrand).Count();
                var totalaveraje = lstModel.Where(x => x.IdBrand == brands.IdBrand).Sum(x => x.AveragePrice);

                var a = _context.CatBrands.Where(x => x.IdBrand == brands.IdBrand).FirstOrDefault();
                a.AveragePrice = totalaveraje / numModel;
                _context.SaveChanges();

            }


        }


    }
}
