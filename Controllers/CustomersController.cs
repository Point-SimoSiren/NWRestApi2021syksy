using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NorthwAPI2021syksy.Models;

namespace NorthwAPI2021syksy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private static readonly NorthwindContext db = new NorthwindContext();

        [HttpGet]
        [Route("R")]
        public ActionResult GetSomeCustomers(int offset, int limit, string country)
        {

            try
            {
                if (country != null) // Jos country parametri todellakin annetaan
                {
                    List<Customer> asiakkaat = db.Customers.Where(cust => cust.Country == country).Take(limit).ToList();
                    return Ok(asiakkaat);
                }
                else // Ilman country tietoa
                {
                    List<Customer> asiakkaat = db.Customers.Skip(offset).Take(limit).ToList();
                    return Ok(asiakkaat);
                }
            }
            catch
            {
                return BadRequest("Something went wrong. Try get all and see if the country exists in the listing");
            }
        }



        
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var customers = db.Customers.ToList();

                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest("Virhe tapahtui: " + e);
            }
        }
        

        // Get 1 customer by id
        // polku: https://localhost:5001/api/customers/alfki
        [HttpGet]
        [Route("{id}")]
        public ActionResult GetOneCustomer(string id)
        {
            Customer asiakas = db.Customers.Find(id);

            return Ok(asiakas);
        }


        // Get customers by Country
        // polku: https://localhost:5001/api/customers/country/finland
        [HttpGet]
        [Route("country/{key}")]
        public ActionResult GetCustomerByCountry(string key)
        {
            var asiakkaat = (from c in db.Customers where c.Country == key select c).ToList();

            return Ok(asiakkaat);
        }



        // Get customers by part of Company Name
        // polku: https://localhost:5001/api/customers/search/hakutermi
        [HttpGet]
        [Route("search/{key}")]
        public ActionResult GetCustomerByCompanyName(string key)
        {
            var asiakkaat = (from c in db.Customers where c.CompanyName.ToLower().Contains(key.ToLower()) select c).ToList();

            return Ok(asiakkaat);
        }




        //Add new customer
        [HttpPost]
        public ActionResult AddCustomer([FromBody] Customer cust)
        {
            try
            {
                db.Customers.Add(cust);
                db.SaveChanges();
                return Ok("Lisättiin asiakas id:llä: " + cust.CustomerId);
            }
            catch (Exception e)
            { 
              return BadRequest("Virhe. Lue lisää tästä: " + e);
            }
        }


        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteCustomer(string id)
        {
                var asiakas = db.Customers.Find(id);
                if (asiakas != null)
                  {
                      try
                       {
                         db.Customers.Remove(asiakas);
                         db.SaveChanges();

                         return Ok("Asiakas " + id + " poistettiin");
                      }
                        catch (Exception e)
                      {
                        return BadRequest("Asiakkaalla on tilauksia? Poistaminen estetty? " + e);
                     }
                  }
                else
                  {
                    return NotFound("Asiakasta " + id + " ei löydy");
                  }
        }


        [HttpPut]
        [Route("{key}")]
        public ActionResult PutEdit(string key, [FromBody] Customer asiakas)
        {
            try
            {
                Customer customer = db.Customers.Find(key);
                if (customer != null)
                {
                    customer.CompanyName = asiakas.CompanyName;
                    customer.ContactName = asiakas.ContactName;
                    customer.ContactTitle = asiakas.ContactTitle;
                    customer.Country = asiakas.Country;
                    customer.Address = asiakas.Address;
                    customer.City = asiakas.City;
                    customer.PostalCode = asiakas.PostalCode;
                    customer.Phone = asiakas.Phone;
                    customer.Fax = asiakas.Fax;

                    db.SaveChanges();
                    return Ok("Muokattiin asiakasta " + customer.CompanyName);
                }
                else
                {
                    return NotFound("Päivitettävää asiakasta ei löytynyt!");
                }
            }
            catch (Exception e)
            {
                return BadRequest("Jokin meni pieleen asiakasta päivitettäessä. Tässä lisätietoa: " + e);
            }
        }
    }
}
