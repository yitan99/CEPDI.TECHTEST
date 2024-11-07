using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CEPDI.TECHTEST.MODELS
{
    public class usuarios
    {
        [Key]
        public int idUsuario { get; set; }
        [MaxLength(200)]
        public string nombre { get; set; }

        public DateTime? fechacreacion { get; set; }
        [MaxLength(100)]
        public string usuario { get; set; }
        [MaxLength(100)]
        public string password { get; set; }

        public int? estatus { get; set; }
    }
}
