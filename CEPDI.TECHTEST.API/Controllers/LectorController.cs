using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using CEPDI.TECHTEST.MODELS;
using System.Xml;
using Newtonsoft.Json;
using System.Net;
using System;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CEPDI.TECHTEST.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LectorController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        public LectorController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // POST api/<LectorController>
        [HttpPost]
        public async Task<ActionResult<resultadoPDF>>  Post(IFormFile xmlFile)
        {

            //Obtención de UUID de XML
            //UUID del Nodo tfd:TimbreFiscalDigital
            Cfdi cfdiFile = MapeoXML.XmlToCfdi(xmlFile);
            string sUUID = cfdiFile.Comprobante.Complemento.TimbreFiscalDigital.UUID;

            //Consulta de parametros para consulta de WS
            string sUrl = _configuration["urlWS"];
            string susuarioWS = _configuration["usuarioWS"];
            string spasswordWS = _configuration["passwordWS"];

            resultadoPDF sPDFFile = MapeoXML.WSObtenerPDFAsync(susuarioWS, spasswordWS, sUUID);

            return sPDFFile;
        }

        
    }
}

