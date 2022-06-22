using servicioweb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace servicioweb.Datos
{
    public class PersonaAdmin
    {
        public async Task<IEnumerable<persona>> Consultar()
        {
            using (suscribeteEntities contexto = new suscribeteEntities())
            {
                return await contexto.persona.AsNoTracking().ToListAsync();
            }
        }
    }
}