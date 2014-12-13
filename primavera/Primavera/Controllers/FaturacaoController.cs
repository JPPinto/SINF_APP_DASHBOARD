using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;
using System.Web.Http.Cors;


namespace FirstREST.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class FaturacaoController : ApiController
    {
        //
        // GET: /TopVenda/

        public IEnumerable<Lib_Primavera.Model.Faturacao> Get(string dateBegin, string dateEnd, string datePart)
        {
            return Lib_Primavera.Comercial.ListaFaturacao(dateBegin, dateEnd, datePart);
        }

    }
}

