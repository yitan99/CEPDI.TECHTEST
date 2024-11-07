using CEPDI.TECHTEST.MODELS;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using System.Xml;

namespace CEPDI.TECHTEST.Api
{
    public class MapeoXML
    {

        public static resultadoPDF WSObtenerPDFAsync(string susuarioWS, string spasswordWS, string sUUID)
        {
            resultadoPDF resultado = new resultadoPDF(); ;
            byte[] sPDF = null;
            WSDemo.WS wSDemo = new WSDemo.WSClient();
            WSDemo.ObtenerPDFRequest requestPDF = new WSDemo.ObtenerPDFRequest(susuarioWS, spasswordWS, sUUID);
            Task<WSDemo.ObtenerPDFResponse> responsePDF = wSDemo.ObtenerPDFAsync(requestPDF);
            responsePDF.Wait();
            if (responsePDF.Result.@return != null)
            {
                resultado.Exitoso = responsePDF.Result.@return.Exitoso;
                resultado.PDF = responsePDF.Result.@return.PDF;
                resultado.MensajeError = responsePDF.Result.@return.MensajeError;
                resultado.CodigoError = responsePDF.Result.@return.CodigoError;
            }

            return resultado;
        }
    
    /// <summary>
    /// XmlToCfdi: Metodo para Convertir XML a clase Cfdi
    /// </summary>
    /// <param name="xmlFile">Archivo XML</param>
    /// <returns></returns>
    public static Cfdi XmlToCfdi(IFormFile xmlFile)
        {
            Cfdi xml = new Cfdi();
            string xmlString = string.Empty;
            using (StreamReader reader = new StreamReader(xmlFile.OpenReadStream()))
            {
                xmlString = reader.ReadToEnd();
            }

            xmlString = ArrayForceNodes(xmlString);
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlString);

            string jsonSerializable = JsonConvert.SerializeXmlNode(doc);
            jsonSerializable = CleanPropertiesJsonX(jsonSerializable);

            Cfdi cfdiFile = JsonConvert.DeserializeObject<Cfdi>(jsonSerializable);


            return cfdiFile;
        }


        /// <summary>
        /// ArrayForceNodes: Forza los nodos internos
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public static string ArrayForceNodes(string xml)
        {

            //Limpia Caracter Raro.
            xml = Regex.Replace(xml, "\\p{C}+", " ");


            ////Validamos si el nodo de Conceptos es una matriz
            XmlDocument xmlAux = new XmlDocument();
            xmlAux.LoadXml(xml);

            if (xmlAux["cfdi:Comprobante"]["cfdi:Conceptos"].ChildNodes.Count == 1)
            {
                //Solo cuando no es una matriz                
                xml = xml.Replace("<cfdi:Concepto ", "<cfdi:Concepto json:Array='true' ");
            }

            if (xmlAux["cfdi:Comprobante"]["cfdi:Impuestos"] != null)
            {
                if (xmlAux["cfdi:Comprobante"]["cfdi:Impuestos"]["cfdi:Traslados"] != null)
                {
                    if (xmlAux["cfdi:Comprobante"]["cfdi:Impuestos"]["cfdi:Traslados"].ChildNodes.Count == 1)
                    {
                        //Solo cuando no es una matriz                
                        xml = xml.Replace("<cfdi:Traslado ", "<cfdi:Traslado json:Array='true' ");
                    }
                }

                if (xmlAux["cfdi:Comprobante"]["cfdi:Impuestos"]["cfdi:Retenciones"] != null)
                {
                    if (xmlAux["cfdi:Comprobante"]["cfdi:Impuestos"]["cfdi:Retenciones"].ChildNodes.Count == 1)
                    {
                        //Solo cuando no es una matriz                
                        xml = xml.Replace("<cfdi:Retencion ", "<cfdi:Retencion json:Array='true' ");
                    }
                }
            }


            //Agrega namespaces especial al nodo padre del XML.  Para forzar configuraciones al JSON
            xml = xml.Replace("<cfdi:Comprobante", "<cfdi:Comprobante xmlns:json=\"http://james.newtonking.com/projects/json\"");

            return xml;
        }

        /// <summary>
        /// CleanPropertiesJsonX:limpia las propiedades de XML para un mapeo mas certero.
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static string CleanPropertiesJsonX(string json)
        {
            //Ajuste 3.1 para considerar todas las versiones de cartaporte20 o cartaporte30
            json = Regex.Replace(json, "cartaporte[0-9]{2,}:", "cartaporte:");

            json = json.Replace("?xml", "xml")
            .Replace("\"@", "\"")
            .Replace("xmlns:xsi", "xmlnsXsi")
            .Replace("xmlns:cfdi", "xmlnsCfdi")
            .Replace("cfdi:Comprobante", "Comprobante")
            .Replace("xsi:schemaLocation", "xsiSchemaLocation")
            .Replace("cfdi:Comprobante", "Comprobante")
            .Replace("cfdi:Emisor", "Emisor")
            .Replace("cfdi:Receptor", "Receptor")
            .Replace("cfdi:Conceptos", "Conceptos")
            .Replace("cfdi:Concepto", "Concepto")
            .Replace("cfdi:Impuestos", "Impuestos")
            .Replace("cfdi:Retenciones", "Retenciones")
            .Replace("cfdi:Retencion", "Retencion")
            .Replace("tfd:TimbreFiscalDigital", "TimbreFiscalDigital")
            .Replace("xmlns:tfd", "xmlnsTfd")
            .Replace("xsi:schemaLocation", "xsiSchemaLocation")
            .Replace("cfdi:Complemento", "Complemento")
            .Replace("xmlns:pago10", "xmlnspago10")
            .Replace("pago10:Pagos", "Pagos")
            .Replace("pago10:Pago", "Pago")
            .Replace("pago10:DoctoRelacionado", "DoctoRelacionado")
            .Replace("cfdi:Traslados", "Traslados")
                               //.Replace("cartaporte20:CartaPorte", "CartaPorte")                    
                               .Replace("cartaporte:CartaPorte", "CartaPorte")
            .Replace("cfdi:Traslado", "Traslado");



            return json;
        }
    }

}