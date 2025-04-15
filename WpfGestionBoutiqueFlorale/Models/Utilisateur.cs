using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfGestionBoutiqueFlorale.Models
{
     public class Utilisateur
    {
        [Key]
        public int IdUtilisateur { get; set; }
        public string username { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string role { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string password { get; set; }

    }
}
