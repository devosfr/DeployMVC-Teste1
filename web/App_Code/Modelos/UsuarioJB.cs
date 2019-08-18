using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Modelos;

namespace Modelos
{

    public class UsuarioJB : ModeloBase
    {
        public static List<UsuarioJB> listadeUsuarios = new List<UsuarioJB>();

        public virtual string nome { get; set; }
        public virtual string email { get; set; }
        public virtual string telefone { get; set; }
        public virtual string cpf { get; set; }
        public virtual string tipo { get; set; }
        public virtual string sexo { get; set; }
        public virtual string status { get; set; }
        //public virtual Arquivo arquivo { get; set; }
        public virtual DateTime dataInicio { get; set; }
        public virtual string senha { get; set; }
        //public virtual ContatoVO contato { get; set; }
        public virtual Endereco endereco { get; set; }


        public UsuarioJB() { }

        public virtual bool Equals(UsuarioJB other)
        {
            if (other.Id == Id && Id > 0)
                return true;
            if (other.nome.Equals(nome))
                return true;

            return false;
        }

    }
}


