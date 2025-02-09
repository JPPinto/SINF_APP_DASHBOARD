﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interop.ErpBS800;
using Interop.StdPlatBS800;
using Interop.StdBE800;
using Interop.GcpBE800;
using ADODB;
using Interop.IGcpBS800;
//using Interop.StdBESql800;
//using Interop.StdBSSql800;

namespace FirstREST.Lib_Primavera
{
    public class Comercial
    {
        const String COMPANYNAME = "PEAU";
        //const String COMPANYNAME = "BLFLR";
        const String USERNAME = "";
        const String PASSWORD = "";
        
        # region Cliente

        public static List<Model.Cliente> ListaClientes()
        {
            ErpBS objMotor = new ErpBS();
             
            StdBELista objList;

            Model.Cliente cli = new Model.Cliente();
            List<Model.Cliente> listClientes = new List<Model.Cliente>();


            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                //objList = PriEngine.Engine.Comercial.Clientes.LstClientes();

                objList = PriEngine.Engine.Consulta("SELECT Cliente, Nome, Moeda, NumContrib as NumContribuinte, ModoPag, CondPag FROM  CLIENTES");

                while (!objList.NoFim())
                {
                    cli = new Model.Cliente();
                    cli.CodCliente = objList.Valor("Cliente");
                    cli.NomeCliente = objList.Valor("Nome");
                    cli.Moeda = objList.Valor("Moeda");
                    cli.NumContribuinte = objList.Valor("NumContribuinte");
                    cli.ModoPag = objList.Valor("ModoPag");
                    cli.CondPag = objList.Valor("CondPag");

                    listClientes.Add(cli);
                    objList.Seguinte();

                }

                return listClientes;
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.Cliente GetCliente(string codCliente)
        {
            ErpBS objMotor = new ErpBS();

            GcpBECliente objCli = new GcpBECliente();


            Model.Cliente myCli = new Model.Cliente();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == true)
                {
                    objCli = PriEngine.Engine.Comercial.Clientes.Edita(codCliente);
                    myCli.CodCliente = objCli.get_Cliente();
                    myCli.NomeCliente = objCli.get_Nome();
                    myCli.Moeda = objCli.get_Moeda();
                    myCli.NumContribuinte = objCli.get_NumContribuinte();
                    myCli.ModoPag = objCli.get_ModoPag();
                    myCli.CondPag = objCli.get_CondPag();
                    return myCli;
                }
                else
                {
                    return null;
                }
            }
            else
                return null;
        }

        public static Lib_Primavera.Model.RespostaErro UpdCliente(Lib_Primavera.Model.Cliente cliente)
        {



            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            ErpBS objMotor = new ErpBS();

            GcpBECliente objCli = new GcpBECliente();

            try
            {

                if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
                {

                    if (PriEngine.Engine.Comercial.Clientes.Existe(cliente.CodCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        objCli = PriEngine.Engine.Comercial.Clientes.Edita(cliente.CodCliente);
                        objCli.set_EmModoEdicao(true);

                        objCli.set_Nome(cliente.NomeCliente);
                        objCli.set_NumContribuinte(cliente.NumContribuinte);
                        objCli.set_Moeda(cliente.Moeda);

                        PriEngine.Engine.Comercial.Clientes.Actualiza(objCli);

                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;

                }

            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro DelCliente(string codCliente)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBECliente objCli = new GcpBECliente();


            try
            {

                if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
                {
                    if (PriEngine.Engine.Comercial.Clientes.Existe(codCliente) == false)
                    {
                        erro.Erro = 1;
                        erro.Descricao = "O cliente não existe";
                        return erro;
                    }
                    else
                    {

                        PriEngine.Engine.Comercial.Clientes.Remove(codCliente);
                        erro.Erro = 0;
                        erro.Descricao = "Sucesso";
                        return erro;
                    }
                }

                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir a empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }

        }


        public static Lib_Primavera.Model.RespostaErro InsereClienteObj(Model.Cliente cli)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
         
            GcpBECliente myCli = new GcpBECliente();

            try
            {
                if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
                {

                    myCli.set_Cliente(cli.CodCliente);
                    myCli.set_Nome(cli.NomeCliente);
                    myCli.set_NumContribuinte(cli.NumContribuinte);
                    myCli.set_Moeda(cli.Moeda);

                    PriEngine.Engine.Comercial.Clientes.Actualiza(myCli);

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;
                }
            }

            catch (Exception ex)
            {
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }


        }

        /*
        public static void InsereCliente(string codCliente, string nomeCliente, string numContribuinte, string moeda)
        {
            ErpBS objMotor = new ErpBS();
            MotorPrimavera mp = new MotorPrimavera();

            GcpBECliente myCli = new GcpBECliente();

            objMotor = mp.AbreEmpresa("DEMO", "", "", "Default");

            myCli.set_Cliente(codCliente);
            myCli.set_Nome(nomeCliente);
            myCli.set_NumContribuinte(numContribuinte);
            myCli.set_Moeda(moeda);

            objMotor.Comercial.Clientes.Actualiza(myCli);

        }


        */


        #endregion Cliente;   // -----------------------------  END   CLIENTE    -----------------------


        public static Lib_Primavera.Model.Artigo GetArtigo(string codArtigo)
        {
            

            GcpBEArtigo objArtigo = new GcpBEArtigo();
            Model.Artigo myArt = new Model.Artigo();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                if (PriEngine.Engine.Comercial.Artigos.Existe(codArtigo) == false)
                {
                    return null;
                }
                else
                {
                    objArtigo = PriEngine.Engine.Comercial.Artigos.Edita(codArtigo);
                    myArt.CodArtigo = objArtigo.get_Artigo();
                    myArt.DescArtigo = objArtigo.get_Descricao();

                    return myArt;
                }
                
            }
            else
            {
                return null;
            }

        }

        public static List<Model.Artigo> ListaArtigos()
        {
            ErpBS objMotor = new ErpBS();
           
            StdBELista objList;

            Model.Artigo art = new Model.Artigo();
            List<Model.Artigo> listArts = new List<Model.Artigo>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Comercial.Artigos.LstArtigos();

                while (!objList.NoFim())
                {
                    art = new Model.Artigo();
                    art.CodArtigo = objList.Valor("artigo");
                    art.DescArtigo = objList.Valor("descricao");

                    listArts.Add(art);
                    objList.Seguinte();
                }

                return listArts;

            }
            else
            {
                return null;

            }

        }



        //------------------------------------ ENCOMENDA ---------------------
        /*
        public static Model.RespostaErro TransformaDoc(Model.DocCompra dc)
        {

            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoCompra objEnc = new GcpBEDocumentoCompra();
            GcpBEDocumentoCompra objGR = new GcpBEDocumentoCompra();
            GcpBELinhasDocumentoCompra objLinEnc = new GcpBELinhasDocumentoCompra();
            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();

            List<Model.LinhaDocCompra> lstlindc = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany("BLFLR", "sa", "123456") == true)
                {
                

                    objEnc = PriEngine.Engine.Comercial.Compras.Edita("000", "ECF", "2013", 3);

                    // --- Criar os cabeçalhos da GR
                    objGR.set_Entidade(objEnc.get_Entidade());
                    objEnc.set_Serie("2013");
                    objEnc.set_Tipodoc("ECF");
                    objEnc.set_TipoEntidade("F");

                    objGR = PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(objGR,rl);
 

                    // façam p.f. o ciclo para percorrer as linhas da encomenda que pretendem copiar
                     
                        double QdeaCopiar;
                        PriEngine.Engine.Comercial.Internos.CopiaLinha("C", objEnc, "C", objGR, lin.NumLinha, QdeaCopiar);
                       
                        // precisamos aqui de um metodo que permita actualizar a Qde Satisfeita da linha de encomenda.  Existe em VB mas ainda não sei qual é em c#
                       
                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(objEnc, "");
                    PriEngine.Engine.Comercial.Compras.Actualiza(objGR, "");

                    PriEngine.Engine.TerminaTransaccao();

                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        
        
        }

        */




        // ------------------------ Documentos de Compra --------------------------//

        public static List<Model.DocCompra> VGR_List()
        {
            ErpBS objMotor = new ErpBS();
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocCompra dc = new Model.DocCompra();
            List<Model.DocCompra> listdc = new List<Model.DocCompra>();
            Model.LinhaDocCompra lindc = new Model.LinhaDocCompra();
            List<Model.LinhaDocCompra> listlindc = new List<Model.LinhaDocCompra>(); 

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, NumDocExterno, Entidade, DataDoc, NumDoc, TotalMerc, Serie, ModoPag, CondPag From CabecCompras where TipoDoc='VGR'");
                while (!objListCab.NoFim())
                {
                    dc = new Model.DocCompra();
                    dc.id = objListCab.Valor("id");
                    dc.NumDocExterno = objListCab.Valor("NumDocExterno");
                    dc.Entidade = objListCab.Valor("Entidade");
                    dc.NumDoc = objListCab.Valor("NumDoc");
                    dc.Data = objListCab.Valor("DataDoc");
                    dc.TotalMerc = objListCab.Valor("TotalMerc");
                    dc.Serie = objListCab.Valor("Serie");
                    dc.ModoPag = objListCab.Valor("ModoPag");
                    dc.CondPag = objListCab.Valor("CondPag");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecCompras, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido, Armazem, Lote from LinhasCompras where IdCabecCompras='" + dc.id + "' order By NumLinha");
                    listlindc = new List<Model.LinhaDocCompra>();

                    while (!objListLin.NoFim())
                    {
                        lindc = new Model.LinhaDocCompra();
                        lindc.IdCabecDoc = objListLin.Valor("idCabecCompras");
                        lindc.CodArtigo = objListLin.Valor("Artigo");
                        lindc.DescArtigo = objListLin.Valor("Descricao");
                        lindc.Quantidade = objListLin.Valor("Quantidade");
                        lindc.Unidade = objListLin.Valor("Unidade");
                        lindc.Desconto = objListLin.Valor("Desconto1");
                        lindc.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindc.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindc.TotalLiquido = objListLin.Valor("PrecoLiquido");
                        lindc.Armazem = objListLin.Valor("Armazem");
                        lindc.Lote = objListLin.Valor("Lote");

                        listlindc.Add(lindc);
                        objListLin.Seguinte();
                    }

                    dc.LinhasDoc = listlindc;
                    
                    listdc.Add(dc);
                    objListCab.Seguinte();
                }
            }
            return listdc;
        }



        public static Model.RespostaErro VGR_New(Model.DocCompra dc)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            

            GcpBEDocumentoCompra myGR = new GcpBEDocumentoCompra();
            GcpBELinhaDocumentoCompra myLin = new GcpBELinhaDocumentoCompra();
            GcpBELinhasDocumentoCompra myLinhas = new GcpBELinhasDocumentoCompra();

            PreencheRelacaoCompras rl = new PreencheRelacaoCompras();
            List<Model.LinhaDocCompra> lstlindv = new List<Model.LinhaDocCompra>();

            try
            {
                if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myGR.set_Entidade(dc.Entidade);
                    myGR.set_NumDocExterno(dc.NumDocExterno);
                    myGR.set_Serie(dc.Serie);
                    myGR.set_Tipodoc("VGR");
                    myGR.set_TipoEntidade("F");
                    myGR.set_ModoPag(dc.ModoPag);
                    myGR.set_CondPag(dc.CondPag);
                    // Linhas do documento para a lista de linhas
                    lstlindv = dc.LinhasDoc;
                    PriEngine.Engine.Comercial.Compras.PreencheDadosRelacionados(myGR, rl);
                    foreach (Model.LinhaDocCompra lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Compras.AdicionaLinha(myGR, lin.CodArtigo, lin.Quantidade, lin.Armazem, "", lin.PrecoUnitario, lin.Desconto);
                    }


                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Compras.Actualiza(myGR, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }
        


        // ------ Documentos de venda ----------------------



        public static Model.RespostaErro Encomendas_New(Model.DocVenda dv)
        {
            Lib_Primavera.Model.RespostaErro erro = new Model.RespostaErro();
            GcpBEDocumentoVenda myEnc = new GcpBEDocumentoVenda();
             
            GcpBELinhaDocumentoVenda myLin = new GcpBELinhaDocumentoVenda();

            GcpBELinhasDocumentoVenda myLinhas = new GcpBELinhasDocumentoVenda();
             
            PreencheRelacaoVendas rl = new PreencheRelacaoVendas();
            List<Model.LinhaDocVenda> lstlindv = new List<Model.LinhaDocVenda>();
            
            try
            {
                if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
                {
                    // Atribui valores ao cabecalho do doc
                    //myEnc.set_DataDoc(dv.Data);
                    myEnc.set_Entidade(dv.Entidade);
                    myEnc.set_Serie(dv.Serie);
                    myEnc.set_Tipodoc("ECL");
                    myEnc.set_TipoEntidade("C");
                    myEnc.set_CondPag(dv.CondPag); //pronto pagamento
                    myEnc.set_ModoPag(dv.ModoPag);
                    // Linhas do documento para a lista de linhas
                    lstlindv = dv.LinhasDoc;
                    PriEngine.Engine.Comercial.Vendas.PreencheDadosRelacionados(myEnc, rl);
                    foreach (Model.LinhaDocVenda lin in lstlindv)
                    {
                        PriEngine.Engine.Comercial.Vendas.AdicionaLinha(myEnc, lin.CodArtigo, lin.Quantidade, "", "", lin.PrecoUnitario, lin.Desconto);
                    }


                   // PriEngine.Engine.Comercial.Compras.TransformaDocumento(

                    PriEngine.Engine.IniciaTransaccao();
                    PriEngine.Engine.Comercial.Vendas.Actualiza(myEnc, "Teste");
                    PriEngine.Engine.TerminaTransaccao();
                    erro.Erro = 0;
                    erro.Descricao = "Sucesso";
                    return erro;
                }
                else
                {
                    erro.Erro = 1;
                    erro.Descricao = "Erro ao abrir empresa";
                    return erro;

                }

            }
            catch (Exception ex)
            {
                PriEngine.Engine.DesfazTransaccao();
                erro.Erro = 1;
                erro.Descricao = ex.Message;
                return erro;
            }
        }


        public static List<Model.DocVenda> Encomendas_List(string typeDoc, string dateBegin, string dateEnd)
        {
            ErpBS objMotor = new ErpBS();
            
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            List<Model.DocVenda> listdv = new List<Model.DocVenda>();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new
            List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {
                objListCab = PriEngine.Engine.Consulta("SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie, ModoPag, CondPag From CabecDoc where TipoDoc LIKE '"+typeDoc+"' and Data>='"+dateBegin+"' and Data<='"+dateEnd+"'");
                while (!objListCab.NoFim())
                {
                    dv = new Model.DocVenda();
                    dv.id = objListCab.Valor("id");
                    dv.Entidade = objListCab.Valor("Entidade");
                    dv.NumDoc = objListCab.Valor("NumDoc");
                    dv.Data = objListCab.Valor("Data");
                    dv.TotalMerc = objListCab.Valor("TotalMerc");
                    dv.Serie = objListCab.Valor("Serie");
                    dv.ModoPag = objListCab.Valor("ModoPag");
                    dv.CondPag = objListCab.Valor("CondPag");
                    objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                    listlindv = new List<Model.LinhaDocVenda>();

                    while (!objListLin.NoFim())
                    {
                        lindv = new Model.LinhaDocVenda();
                        lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                        lindv.CodArtigo = objListLin.Valor("Artigo");
                        lindv.DescArtigo = objListLin.Valor("Descricao");
                        lindv.Quantidade = objListLin.Valor("Quantidade");
                        lindv.Unidade = objListLin.Valor("Unidade");
                        lindv.Desconto = objListLin.Valor("Desconto1");
                        lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                        lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                        lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");

                        listlindv.Add(lindv);
                        objListLin.Seguinte();
                    }

                    dv.LinhasDoc = listlindv;
                    listdv.Add(dv);
                    objListCab.Seguinte();
                }
            }
            return listdv;
        }


        public static Model.DocVenda Encomenda_Get(string numdoc)
        {
            ErpBS objMotor = new ErpBS();
             
            StdBELista objListCab;
            StdBELista objListLin;
            Model.DocVenda dv = new Model.DocVenda();
            Model.LinhaDocVenda lindv = new Model.LinhaDocVenda();
            List<Model.LinhaDocVenda> listlindv = new List<Model.LinhaDocVenda>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {
                 
                string st = "SELECT id, Entidade, Data, NumDoc, TotalMerc, Serie, ModoPag, CondPag From CabecDoc where TipoDoc='ECL' and NumDoc='" + numdoc + "'";
                objListCab = PriEngine.Engine.Consulta(st);
                dv = new Model.DocVenda();
                dv.id = objListCab.Valor("id");
                dv.Entidade = objListCab.Valor("Entidade");
                dv.NumDoc = objListCab.Valor("NumDoc");
                dv.Data = objListCab.Valor("Data");
                dv.TotalMerc = objListCab.Valor("TotalMerc");
                dv.Serie = objListCab.Valor("Serie");
                dv.ModoPag = objListCab.Valor("ModoPag");
                dv.CondPag = objListCab.Valor("CondPag");
                objListLin = PriEngine.Engine.Consulta("SELECT idCabecDoc, Artigo, Descricao, Quantidade, Unidade, PrecUnit, Desconto1, TotalILiquido, PrecoLiquido from LinhasDoc where IdCabecDoc='" + dv.id + "' order By NumLinha");
                listlindv = new List<Model.LinhaDocVenda>();

                while (!objListLin.NoFim())
                {
                    lindv = new Model.LinhaDocVenda();
                    lindv.IdCabecDoc = objListLin.Valor("idCabecDoc");
                    lindv.CodArtigo = objListLin.Valor("Artigo");
                    lindv.DescArtigo = objListLin.Valor("Descricao");
                    lindv.Quantidade = objListLin.Valor("Quantidade");
                    lindv.Unidade = objListLin.Valor("Unidade");
                    lindv.Desconto = objListLin.Valor("Desconto1");
                    lindv.PrecoUnitario = objListLin.Valor("PrecUnit");
                    lindv.TotalILiquido = objListLin.Valor("TotalILiquido");
                    lindv.TotalLiquido = objListLin.Valor("PrecoLiquido");
                    listlindv.Add(lindv);
                    objListLin.Seguinte();
                }

                dv.LinhasDoc = listlindv;
                return dv;
            }
            return null;
        }


        internal static IEnumerable<Model.CondPag> ListaCondsPag()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.CondPag cond = new Model.CondPag();
            List<Model.CondPag> listConds = new List<Model.CondPag>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Comercial.CondsPagamento.LstCondsPagamento();

                while (!objList.NoFim())
                {
                    cond = new Model.CondPag();
                    cond.Codigo = objList.Valor("CondPag");
                    cond.Descricao = objList.Valor("Descricao");

                    listConds.Add(cond);
                    objList.Seguinte();
                }

                return listConds;

            }
            else
            {
                return null;

            }
        }

        internal static IEnumerable<Model.ModoPag> ListaModosPag()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.ModoPag modo = new Model.ModoPag();
            List<Model.ModoPag> listModos = new List<Model.ModoPag>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Movim, Descricao FROM DocumentosBancos WHERE MovInterno = 0");

                while (!objList.NoFim())
                {
                    modo = new Model.ModoPag();
                    modo.Codigo = objList.Valor("Movim");
                    modo.Descricao = objList.Valor("Descricao");

                    listModos.Add(modo);
                    objList.Seguinte();
                }

                return listModos;

            }
            else
            {
                return null;

            }
        }

        internal static IEnumerable<Model.Fornecedor> ListaFornecedores()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.Fornecedor fornec = new Model.Fornecedor();
            List<Model.Fornecedor> listFornecedores = new List<Model.Fornecedor>();


            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT Fornecedor, Nome, Moeda, NumContrib as NumContribuinte, ModoPag, CondPag FROM  FORNECEDORES");

                while (!objList.NoFim())
                {
                    fornec = new Model.Fornecedor();
                    fornec.CodFornecedor = objList.Valor("Fornecedor");
                    fornec.NomeFornecedor = objList.Valor("Nome");
                    fornec.Moeda = objList.Valor("Moeda");
                    fornec.NumContribuinte = objList.Valor("NumContribuinte");
                    fornec.ModoPag = objList.Valor("ModoPag");
                    fornec.CondPag = objList.Valor("CondPag");

                    listFornecedores.Add(fornec);
                    objList.Seguinte();

                }

                return listFornecedores;
            }
            else
                return null;
        }

        internal static IEnumerable<Model.TopVenda> ListaTopVenda(long numLinhas)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.TopVenda modo = new Model.TopVenda();
            List<Model.TopVenda> listTopVenda = new List<Model.TopVenda>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP " + numLinhas + " LinhasDoc.Artigo, Artigo.descricao as NomeArtigo, sum(quantidade) AS Quantidade FROM LinhasDoc LEFT JOIN CabecDoc on LinhasDoc.IdCabecDoc = CabecDoc.Id left join Artigo on LinhasDoc.Artigo = Artigo.Artigo WHERE CabecDoc.TipoDoc = 'FA' AND LinhasDoc.Artigo <> 'NULL' Group by LinhasDoc.Artigo, Artigo.Descricao ORDER BY Quantidade DESC");

                while (!objList.NoFim())
                {
                    modo = new Model.TopVenda();
                    modo.CodArtigo = objList.Valor("Artigo");
                    modo.NomeArtigo = objList.Valor("NomeArtigo");
                    modo.Quantidade = objList.Valor("Quantidade");

                    listTopVenda.Add(modo);
                    objList.Seguinte();
                }

                return listTopVenda;

            }
            else
            {
                return null;

            }
        }

        internal static IEnumerable<Model.TopCompra> ListaTopCompra(long numLinhas)
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.TopCompra modo = new Model.TopCompra();
            List<Model.TopCompra> listTopCompra = new List<Model.TopCompra>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT TOP " + numLinhas + " LinhasCompras.Artigo, Artigo.Descricao as NomeArtigo, sum(quantidade) as quantidade FROM LinhasCompras LEFT JOIN CabecCompras on LinhasCompras.IdCabecCompras = CabecCompras.Id left join Artigo on LinhasCompras.artigo = Artigo.artigo WHERE CabecCompras.TipoDoc = 'VFA' AND LinhasCompras.Artigo <> 'NULL' Group by LinhasCompras.Artigo, Artigo.descricao ORDER BY Quantidade ASC");

                while (!objList.NoFim())
                {
                    modo = new Model.TopCompra(); 
                    modo.CodArtigo = objList.Valor("Artigo");
                    modo.NomeArtigo = objList.Valor("NomeArtigo");
                    modo.Quantidade = Math.Abs(objList.Valor("Quantidade")); //primavera grava quantidades de compras com valor negativo

                    listTopCompra.Add(modo);
                    objList.Seguinte();
                }

                return listTopCompra;

            }
            else
            {
                return null;

            }
        }

        internal static IEnumerable<Model.Faturacao> ListaFaturacao(string dateBegin, string dateEnd, string datePart)
        {
            ErpBS objMotor = new ErpBS();
            
            StdBELista objListFa;
            Model.Faturacao fa = new Model.Faturacao();
            List<Model.Faturacao> listfac = new List<Model.Faturacao>();

            bool year = datePart.ToLower().Equals("year");
            string query;
            if (year)
                query = "SELECT DATEPART(year,LinhasDoc.Data) as ano, round(SUM(PrecoLiquido),2) AS total FROM LinhasDoc LEFT JOIN CabecDoc on LinhasDoc.IdCabecDoc = CabecDoc.ID WHERE CabecDoc.TipoDoc = 'FA' and LinhasDoc.Data>='" + dateBegin + "' and LinhasDoc.Data<'" + dateEnd + "' GROUP BY DATEPART(year,LinhasDoc.Data) ORDER BY DATEPART(year,LinhasDoc.Data)";
            else
                query = "SELECT DATEPART(year,LinhasDoc.Data) as ano, DATEPART(" + datePart + ",LinhasDoc.Data) as parte, round(SUM(PrecoLiquido),2) AS total FROM LinhasDoc LEFT JOIN CabecDoc on LinhasDoc.IdCabecDoc = CabecDoc.ID WHERE CabecDoc.TipoDoc = 'FA' and LinhasDoc.Data>='" + dateBegin + "' and LinhasDoc.Data<'" + dateEnd + "' GROUP BY DATEPART(year,LinhasDoc.Data), DATEPART(" + datePart + ",LinhasDoc.Data) ORDER BY DATEPART(year,LinhasDoc.Data), DATEPART(" + datePart + ",LinhasDoc.Data)";

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {
                objListFa = PriEngine.Engine.Consulta(query);
                while (!objListFa.NoFim())
                {
                    fa = new Model.Faturacao();
                    fa.ano = objListFa.Valor("ano");
                    if(year)
                        fa.parte = objListFa.Valor("ano");
                    else
                        fa.parte = objListFa.Valor("parte");
                     fa.total = objListFa.Valor("total");
                    listfac.Add(fa);
                    objListFa.Seguinte();
                }
                return listfac;
            }
            else
            {
                return null;
            }
        }

        internal static IEnumerable<Model.DividaCliente> ListaDividaCliente()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.DividaCliente modo = new Model.DividaCliente();
            List<Model.DividaCliente> listDividaCliente = new List<Model.DividaCliente>();

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT pendentes.entidade as codCliente, Clientes.Nome as nomeCliente, round(pendentes.pendente,2) as pendente, coalesce(round(dividas.divida,2), 0) as divida FROM (select entidade, sum(valorPendente) as divida from Pendentes where dateadd(day,datediff(day,0,getdate()),0)>DataVenc group by entidade) as Dividas RIGHT JOIN (select entidade, sum(valorpendente) as pendente from Pendentes where tipoentidade = 'C' group by entidade) as pendentes ON dividas.entidade = pendentes.Entidade left join Clientes on pendentes.Entidade = Clientes.Cliente order by divida DESC,pendente DESC");

                while (!objList.NoFim())
                {
                    modo = new Model.DividaCliente();
                    modo.codcliente = objList.Valor("codCliente");
                    modo.nomecliente = objList.Valor("nomeCliente");
                    modo.pendente = objList.Valor("pendente");
                    modo.divida = objList.Valor("divida");

                    listDividaCliente.Add(modo);
                    objList.Seguinte();
                }

                return listDividaCliente;

            }
            else
            {
                return null;

            }
        }

        internal static IEnumerable<Model.FaturacaoFamilia> ListaFaturacaoFamilia()
        {
            ErpBS objMotor = new ErpBS();

            StdBELista objList;

            Model.FaturacaoFamilia modo = new Model.FaturacaoFamilia();
            List<Model.FaturacaoFamilia> listFaturacaoFamilia = new List<Model.FaturacaoFamilia>();
            double total = 0;

            if (PriEngine.InitializeCompany(COMPANYNAME, USERNAME, PASSWORD) == true)
            {

                objList = PriEngine.Engine.Consulta("SELECT artigo.familia, familias.descricao, SUM(PrecoLiquido) AS totalFamilia FROM LinhasDoc LEFT JOIN CabecDoc on LinhasDoc.IdCabecDoc = CabecDoc.ID LEFT JOIN Artigo ON LinhasDoc.Artigo = Artigo.Artigo LEFT JOIN Familias on Artigo.Familia = Familias.Familia WHERE CabecDoc.TipoDoc = 'FA' AND Artigo.Artigo <> 'NULL' group by artigo.familia, familias.descricao order by totalFamilia desc");

                while (!objList.NoFim())
                {
                    modo = new Model.FaturacaoFamilia();
                    modo.codFamilia = objList.Valor("familia");
                    modo.descricao = objList.Valor("descricao");
                    modo.percentagem = objList.Valor("totalFamilia");
                    total += modo.percentagem;

                    listFaturacaoFamilia.Add(modo);
                    objList.Seguinte();
                }

                foreach(Model.FaturacaoFamilia ff in listFaturacaoFamilia) {
                    ff.percentagem /= total;
                }

                return listFaturacaoFamilia;

            }
            else
            {
                return null;

            }
        }
    }
}