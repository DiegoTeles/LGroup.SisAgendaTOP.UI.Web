//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LGroup.SisAgendaTOP.DataAccess
{
    using System;
    using System.Collections.Generic;
    
    public partial class TB_CONTATO
    {
        public int ID_CONTATO { get; set; }
        public int ID_SEXO { get; set; }
        public string NM_CONTATO { get; set; }
        public string DS_EMAIL { get; set; }
        public string NR_TELEFONE { get; set; }
        public Nullable<System.DateTime> DT_NASCIMENTO { get; set; }
        public string NR_CEP { get; set; }
        public string NM_LOGRADOURO { get; set; }
        public string NM_BAIRRO { get; set; }
        public string NR_NUMERO { get; set; }
        public Nullable<System.DateTime> DT_CADASTRO { get; set; }
        public Nullable<System.DateTime> DT_ALTERACAO { get; set; }
        public Nullable<bool> CD_ATIVO { get; set; }
    
        public virtual TB_SEXO TB_SEXO { get; set; }
    }
}
