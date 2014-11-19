using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FirstREST.Lib_Primavera.Model;

namespace FirstREST.Controllers
{
    public class FornecedoresController : ApiController
    {
        //
        // GET: /Fornecedores/

        public IEnumerable<Lib_Primavera.Model.Fornecedor> Get()
        {
            return Lib_Primavera.Comercial.ListaFornecedores();
        }

    }
}
