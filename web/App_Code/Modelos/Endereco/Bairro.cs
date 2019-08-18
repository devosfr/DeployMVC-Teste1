using System;

namespace Modelos
{

    //    create table Categorias
    //(
    //id_categoria int,
    //ds_categoria varchar(100),
    //idSegmentoFilho int
    //);



    public class Bairro : ModeloBase, IEquatable<Bairro>
    {
        // Atributos
        public virtual string Nome { get; set; }
        public virtual Cidade Cidade { get; set; }
        //public virtual int idRegiao { get; set; }

        // Propriedades
        public Bairro()
        { }



        public virtual bool Equals(Bairro other)
        {
            if (other.Id == Id && Id > 0)
                return true;
            return false;
        }
    }
}


