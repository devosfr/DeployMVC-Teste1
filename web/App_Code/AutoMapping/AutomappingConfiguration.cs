using FluentNHibernate.Automapping;
using Modelos;
using System;

namespace Repository
{
    public class AutomappingConfiguration : DefaultAutomappingConfiguration
    {
        public override bool ShouldMap(Type type)
        {
            //return type.Namespace == "Modelo_3.Modelses";
            return type.IsSubclassOf(typeof(ModeloBase)) || type.IsSubclassOf(typeof(ModeloImagem));
        }
    }
}