using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI2_Demo_20170603.Models;

namespace WebAPI2_Demo_20170603.Controllers
{
    [RoutePrefix("Clients")]
    public class ClientsController : ApiController
    {
        private FabricsEntities db = new FabricsEntities();

        // GET: api/Clients
        [Route("")]
        public IQueryable<Client> GetClient()
        {
            return db.Client;
        }

        // GET: api/Clients/5
        [ResponseType(typeof(Client))]
        [Route("{id}")]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        [Route("{id}/Orders")]
        public IHttpActionResult GetClientOrders(int id)
        {
            var Order = db.Order.Where(o => o.ClientId == id);
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        [Route("{id}/Orders/{orderId:int}")]
        public IHttpActionResult GetClientOrder(int id,int orderId)
        {
            var Order = db.Order.Where(o => o.ClientId == id && o.OrderId == orderId);
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        [HttpGet]
        [Route("~/Api/Clients/{id}/Orders/pending")]
        public IHttpActionResult FindClientOrdersPending(int id)
        {
            var Order = db.Order.Where(o => o.ClientId == id && o.OrderStatus == "P");
            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        [Route("{id}/Orders/{*date}")]
        public IHttpActionResult GetClientOrder(int id, DateTime date)
        {
            var Order = db.Order.Where(o => o.ClientId == id 
            && o.OrderDate.Value.Year == date.Year 
            && o.OrderDate.Value.Month == date.Month 
            && o.OrderDate.Value.Day == date.Day);

            if (Order == null)
            {
                return NotFound();
            }

            return Ok(Order);
        }

        // PUT: api/Clients/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ClientId)
            {
                return BadRequest();
            }

            db.Entry(client).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Clients
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Client.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ClientId }, client);
        }

        // DELETE: api/Clients/5
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Client.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Client.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Client.Count(e => e.ClientId == id) > 0;
        }
    }
}