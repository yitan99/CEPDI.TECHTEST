using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CEPDI.TECHTEST.MODELS
{
    public  class formasfarmaceuticas
    {
        [Key]
        public int idformafarmaceutica { get; set; }
        [MaxLength(100)]
        public string nombre { get; set; }
        public int? habilitado { get; set; }
    }
}
