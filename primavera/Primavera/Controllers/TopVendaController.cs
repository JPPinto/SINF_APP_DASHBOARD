using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;


namespace FirstREST.Controllers
{
    public class TopVendaController : ApiController
    {
        //
        // GET: /TopVenda/

        public IEnumerable<Lib_Primavera.Model.TopVenda> Get(long numLinhas)
        {
            return Lib_Primavera.Comercial.ListaTopVenda(numLinhas);
        }

    }
}

