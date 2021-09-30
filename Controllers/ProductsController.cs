using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NorthwAPI2021syksy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NorthwAPI2021syksy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private static readonly NorthwindContext db = new NorthwindContext();

        // Käytetään esim. näin:
        // https://localhost:5001/api/products/min-price/100/max-price/200

        [HttpGet]
        [Route("min-price/{min}/max-price/{max}")]
        public List<Product> GetByPrice(int min, int max)
        {

            var tuotteet = from p in db.Products
                           where p.UnitPrice > min && p.UnitPrice < max
                           select p;
            return tuotteet.ToList();

        }

        // Etsi kallein tuote
        [HttpGet]
        [Route("highest-price/{type}")]
        public ActionResult GetHighestPrice(string type)
        {

            var tuotteet = from p in db.Products orderby (p.UnitPrice) select p;
            var kallein = tuotteet.Last();

            if (type == "pelkistetty")
            {
                return Ok("Tuote: " + kallein.ProductName + ", Hinta: " + kallein.UnitPrice.ToString());
            }
            else if (type == "kaikki")
            {
                return Ok(kallein);
            }
            else
            {
                return BadRequest("Väärä tai puuttuva tyyppi parametri");
            }
        }
    }
}
