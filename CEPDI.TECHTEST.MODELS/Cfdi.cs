using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.TECHTEST.MODELS
{
    public class Cfdi
    {
        public Comprobante Comprobante { get; set; }
    }
    public class Comprobante
    {
        public string LugarExpedicion { get; set; }
        public string MetodoPago { get; set; }
        public string TipoDeComprobante { get; set; }
        public decimal Total { get; set; }
        public string Moneda { get; set; }
        public string Certificado { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Descuento { get; set; }
        public string NoCertificado { get; set; }
        public string FormaPago { get; set; }
        public string Sello { get; set; }
        public string Folio { get; set; }
        public string CondicionesDePago { get; set; }
        public decimal TipoCambio { get; set; }
        public string Confirmacion { get; set; }
        public string TipoRelacion { get; set; }
        public DateTime Fecha { get; set; }
        public string Version { get; set; }
        public string Serie { get; set; }
        public Emisor Emisor { get; set; }
        public Receptor Receptor { get; set; }
        public Conceptos Conceptos { get; set; }
        public Impuestos Impuestos { get; set; }
        public Complemento Complemento { get; set; }
    }
    public class CartaPorte
    {
        public string Version { get; set; }
        public string TranspInternac { get; set; }
        public string TotalDistRec { get; set; }
    }

    public class Emisor
    {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string RegimenFiscal { get; set; }
    }

    public class Receptor
    {
        public string Rfc { get; set; }
        public string Nombre { get; set; }
        public string UsoCFDI { get; set; }
        public string ResidenciaFiscal { get; set; }

    }
    public class Conceptos
    {
        public List<Concepto> Concepto { get; set; }
    }

    public class Concepto
    {
        public string ClaveProdServ { get; set; }
        public string Cantidad { get; set; }
        public string ClaveUnidad { get; set; }
        public string Descripcion { get; set; }
        public string ValorUnitario { get; set; }
        public decimal Importe { get; set; }
    }


    public class Retencion
    {
        public string Impuesto { get; set; }
        public decimal Importe { get; set; }
    }

    public class Retenciones
    {
        public List<Retencion> Retencion { get; set; }
    }

    public class Traslado
    {
        public string Impuesto { get; set; }
        public string TipoFactor { get; set; }
        public string TasaOCuota { get; set; }
        public decimal Importe { get; set; }
    }

    public class Traslados
    {
        public List<Traslado> Traslado { get; set; }
    }

    public class Impuestos
    {
        public decimal TotalImpuestosRetenidos { get; set; }
        public decimal TotalImpuestosTrasladados { get; set; }
        public Retenciones Retenciones { get; set; }
        public Traslados Traslados { get; set; }
    }

    public class TimbreFiscalDigital
    {
        public string Version { get; set; }
        public string UUID { get; set; }
        public DateTime FechaTimbrado { get; set; }
        public string RfcProvCertif { get; set; }
        public string SelloCFD { get; set; }
        public string NoCertificadoSAT { get; set; }
        public string SelloSAT { get; set; }
    }

    public class Complemento
    {
        public Pagos Pagos { get; set; }
        public TimbreFiscalDigital TimbreFiscalDigital { get; set; }
        public CartaPorte CartaPorte { get; set; }

    }
    public class Pagos
    {
        public string Version { get; set; }
        public Pago Pago { get; set; }
    }
    public class Pago
    {
        public DateTime FechaPago { get; set; }
        public string FormaDePagoP { get; set; }
        public string MonedaP { get; set; }
        public decimal Monto { get; set; }
        public DoctoRelacionado DoctoRelacionado { get; set; }
    }
    public class DoctoRelacionado
    {
        public string IdDocumento { get; set; }
        public string Serie { get; set; }
        public string Folio { get; set; }
        public string MonedaDR { get; set; }
        public string MetodoDePagoDR { get; set; }
        public string NumParcialidad { get; set; }
        public string ImpSaldoAnt { get; set; }
        public string ImpPagado { get; set; }
        public string ImpSaldoInsoluto { get; set; }
    }

    public class resultadoPDF
    {
        public bool  Exitoso { get; set; }
        public byte[] PDF { get; set; }
        public string MensajeError { get; set; }
        public int CodigoError { get; set; }

    }
}
