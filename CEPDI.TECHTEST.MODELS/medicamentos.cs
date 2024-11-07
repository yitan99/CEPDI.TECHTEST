using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.TECHTEST.MODELS
{
    public class medicamentos
    {
        [Key]
        public int idmedicamento { get; set; }

        [MaxLength(100)]
        public string nombre { get; set; }
        [MaxLength(100)]
        public string concentracion { get; set; }

        
        public decimal precio { get; set; }

        public int? stock { get; set; }
        [MaxLength(100)]
        public string presentacion { get; set; }

        public int? bhabilitado { get; set; }
        
        //se vincula Foreign key
        public int idformafarmaceutica { get; set; }
        [ForeignKey("idformafarmaceutica")]
        public virtual formasfarmaceuticas formasfarmaceuticas { get; set; } 
    }
}
