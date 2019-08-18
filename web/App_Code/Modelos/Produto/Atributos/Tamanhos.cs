using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Modelos
{
    /// <summary>
    /// Summary description for Tamanho
    /// </summary>
    
    
    [Serializable]
    public class Tamanhos : ModeloBase
    {
        
        
        public virtual string Nome { get; set; }
        public virtual string Chave { get; set; }
        public virtual Boolean Visivel { get; set; }
        //public virtual ISet<Peso> Pesos { get; set; }
        public Tamanhos()
        {
            //
            // TODO: Add constructor logic here
            //
            //Pesos = new HashSet<Peso>();
        }
    }
}