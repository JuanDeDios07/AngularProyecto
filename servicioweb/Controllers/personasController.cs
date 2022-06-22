using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using servicioweb.Datos;
using servicioweb.Models;

namespace servicioweb.Controllers
{
    public class personasController : ApiController
    {
        private suscribeteEntities db = new suscribeteEntities();
         PersonaAdmin admin = new PersonaAdmin();
        // GET: api/personas
        public async Task<IEnumerable<persona>> Getpersona()
        {
            return await admin.Consultar() ;
        }

        // GET: api/personas/5
        [ResponseType(typeof(persona))]
        public async Task<IHttpActionResult> Getpersona(int id)
        {
            persona persona = await db.persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            return Ok(persona);
        }

        // PUT: api/personas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putpersona(int id, persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != persona.Id)
            {
                return BadRequest();
            }

            db.Entry(persona).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!personaExists(id))
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

        // POST: api/personas
        [ResponseType(typeof(persona))]
        public async Task<IHttpActionResult> Postpersona(persona persona)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.persona.Add(persona);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = persona.Id }, persona);
        }

        // DELETE: api/personas/5
        [ResponseType(typeof(persona))]
        public async Task<IHttpActionResult> Deletepersona(int id)
        {
            persona persona = await db.persona.FindAsync(id);
            if (persona == null)
            {
                return NotFound();
            }

            db.persona.Remove(persona);
            await db.SaveChangesAsync();

            return Ok(persona);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool personaExists(int id)
        {
            return db.persona.Count(e => e.Id == id) > 0;
        }
    }
}