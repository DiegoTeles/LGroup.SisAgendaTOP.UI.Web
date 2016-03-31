using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//SUBIMOS PARA MEMORIA OS DATA ANNOTATIONS
using System.ComponentModel.DataAnnotations;

namespace LGroup.SisAgendaTOP.Model
{
    public class ContatoMOD
    {
        public int CodigoContato { get; set; }

        [Required(ErrorMessage=" ")]
        public string Nome { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name="E-Mail")]
        public string Email { get; set; }

        [Required(ErrorMessage = " ")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = " ")]
        [Display(Name="Data de Nascimento")]
        public DateTime? DataNascimento { get; set; }

        //IREMOS UTILIZAR O CONCEITO DE P.O.O
        //ORIENTAÇÃO A OBJETOS
        //NOME BUNITÃO => COMPOSIÇÃO
        [Required(ErrorMessage = " ")]
        public SexoMOD Sexo { get; set; }

        [Required(ErrorMessage=" ")]
        public string CEP { get; set; }

        [Required(ErrorMessage = " ")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = " ")]
        public string Bairro { get; set; }

        [Required(ErrorMessage = " ")]
        public string Numero { get; set; }
    }
}
