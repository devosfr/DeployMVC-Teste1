using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;
using Modelos;

namespace Repository.Conventions
{
    public class ProdutoOverride : IAutoMappingOverride<Produto>
    {
        public void Override(AutoMapping<Produto> mapping)
        {
            //mapping.References(x => x.Categoria).Fetch.Join();
            //mapping.References(x => x.Segmento).Fetch.Join();
            //mapping.References(x => x.SubSegmento).Fetch.Join();
            mapping.References(x => x.Peso).Fetch.Join();
            mapping.References(x => x.Imagem).Fetch.Join();
            //mapping.References(x => x.Albuns).Fetch.Join();
            //mapping.References(x=> x.Random).Formula("NEWID()");
            //mapping.References(x => x.Albuns).Not.LazyLoad();


            //mapping.HasMany(x => x.Albuns).KeyColumn("id").Not.LazyLoad();
            mapping.HasMany(x => x.Cores).KeyColumn("id").Not.LazyLoad();

            mapping.HasManyToMany(x => x.Segmentos).Table("nn_segmentosprodutos_produtos")
            .ParentKeyColumn("ProdutoID")
            .ChildKeyColumn("SegmentoID")
            .Cascade.All().Not.LazyLoad();

            

            mapping.HasManyToMany(x => x.SubSegmentos).Table("nn_subsegmentosprodutos_produtos")
                    .ParentKeyColumn("ProdutoID")
                    .ChildKeyColumn("SubsegmentoID")
                    .Cascade.All().Not.LazyLoad();

            mapping.HasManyToMany(x => x.Categorias).Table("nn_categoriasprodutos_produtos")
                                    .ParentKeyColumn("ProdutoID")
                                    .ChildKeyColumn("CategoriaID")
                                    .Cascade.All();

            mapping.HasManyToMany(x => x.Destaques).Table("nn_destaques_produtos")
                                    .ParentKeyColumn("ProdutoID")
                                    .ChildKeyColumn("DestaqueID")
                                    .Cascade.All();


            mapping.HasManyToMany(x => x.Cores).Table("nn_cores_produtos")
                                    .ParentKeyColumn("ProdutoID")
                                    .ChildKeyColumn("CorID")
                                    .Cascade.All().Not.LazyLoad();
        }
    }
}

