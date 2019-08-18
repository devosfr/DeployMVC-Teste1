using System;
using System.Collections.Generic;

namespace Modelos
{
    [Serializable]
    public class UsuarioVO: ModeloBase,IEquatable<UsuarioVO>
    {
        //[DataMember]
        public virtual string nome { get; set; }
        //[DataMember]
        public virtual string login { get; set; }
        
        public virtual string senha { get; set; }
        //[DataMember]
        public virtual string tipo { get; set; }
        //[DataMember]
        public virtual string status { get; set; }
        public virtual ISet<PermissaoGrupoDePaginasVO> permissoesGrupos { get; set; }
        public virtual ISet<PermissaoVO> permissoesPaginas { get; set; }


        public UsuarioVO() 
        {
            permissoesGrupos = new HashSet<PermissaoGrupoDePaginasVO>();
            permissoesPaginas = new HashSet<PermissaoVO>();
        }



        public virtual bool Equals(UsuarioVO other)
        {
            if (Id == other.Id && Id > 0)
                return true;
            if (login.Equals(other.login))
                return true;
            return false;
        }
    }
}


