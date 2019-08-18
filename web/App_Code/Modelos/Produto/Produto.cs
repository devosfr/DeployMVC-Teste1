
using System.Collections.Generic;
using System.Linq;
using System;



namespace Modelos
{
    /// <summary>
    /// Summary description for Produto
    /// </summary>
    /// 


    public class Produto : ModeloBase
    {
        public virtual string Nome { get; set; }
        public virtual string Chave { get; set; }
        public virtual string Referencia { get; set; }
        [StringLength(10000)]
        public virtual string Descricao { get; set; }
        [StringLength(10000)]
        public virtual string Resumo { get; set; }
        public virtual bool Atacado { get; set; }
        public virtual bool Visivel { get; set; }
        public virtual bool Indisponivel { get; set; }
        public virtual IList<SegmentoProduto> Segmentos { get; set; }
        public virtual IList<SubSegmentoProduto> SubSegmentos { get; set; }
        public virtual IList<CategoriaProduto> Categorias { get; set; }
        public virtual Preco Preco { get; set; }
        public virtual IList<Cor> Cores { get; set; }
        public virtual IList<Destaque> Destaques { get; set; }
        public virtual ImagemProduto Imagem { get; set; }
        public virtual IList<ImagemProduto> listaFotos { get; set; }
        public virtual int NumeroVendas { get; set; }
        public virtual int NumeroVisitas { get; set; }
        //public virtual string Random { get; set; }
        public virtual bool Destaque { get; set; }
        public virtual IList<InformacaoProduto> Informacoes { get; set; }
        public virtual Marca Marca { get; set; }
        //public virtual IList<Album> Albuns { get; set; }
        public virtual Peso Peso { get; set; }
        public virtual string Tamanhos { get; set; }
        public virtual bool Importado { get; set; }
        public virtual bool ImportarPreco { get; set; }
        public virtual bool PromocaoHome { get; set; }
        public virtual bool Infantil { get; set; }
        public virtual bool Goleiro { get; set; }
        //public virtual string Marca { get; set; }
        public virtual bool Novidade { get; set; }
        public virtual bool Personalizavel { get; set; }
        public virtual string Genero { get; set; }
        public virtual string Esporte { get; set; }
        public virtual string Carrossel { get; set; }
        public virtual int Prazo_com_estoque { get; set; }
        public virtual int Prazo_sem_estoque { get; set; }

        public Produto()
        {
            //
            // TODO: Add constructor logic here
            //

            Cores = new List<Cor>();
            //Albuns = new List<Album>();
            Categorias = new List<CategoriaProduto>();
            Segmentos = new List<SegmentoProduto>();
            SubSegmentos = new List<SubSegmentoProduto>();
            Informacoes = new List<InformacaoProduto>();
            Destaques = new List<Destaque>();
        }

        #region customLoja


        //public virtual string GetPreco()
        //{
        //    //string retorno = "";

        //    return "R$ " + this.Preco.Valor.ToString();

        //}

        public virtual string GetPreco()
        {
            string retorno = "";
            //semPromocao = precocheio
            //valor = remarcado
            //a vista = avista
            if (this.Preco != null && !this.Indisponivel)
            {
                retorno += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 valor-produto-itens no-padding'>";
                if (this.Preco.Valor > 0)
                {
                    if (this.Preco.ValorSemPromocao > 0)
                        retorno += "<div class='de-por'>De " + this.Preco.ValorSemPromocao.ToString("C") + "</div>";

                    retorno += this.Preco.Valor.ToString("C") + "</div><br/>";
                    retorno += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 condicao-produto-itens no-padding'>À Vista</div>";
                }
                else
                {
                    retorno += this.Preco.ValorAvista.ToString("C");
                    retorno += "<div class='col-lg-12 col-md-12 col-sm-12 col-xs-12 condicao-produto-itens no-padding'>À Vista</div>";
                    retorno += "</div>";
                }
            }
            else
            {

            }
            return retorno;
        }

        public virtual string GetUrl()
        {
            string retorno = "";

            retorno += MetodosFE.BaseURL + "/detalhe-produto/?q=" + this.Chave;

            return retorno;
        }

        public virtual string GetDescontoDiv()
        {
            string retorno = "";

            if (this.Preco != null && !this.Indisponivel)
            {
                if (this.Preco.Desconto != null && !String.IsNullOrEmpty(this.Preco.Desconto.ToString()))
                {
                    if (this.Preco.Desconto > 0)
                        retorno += "<div class='rgba-banner'>-" + this.Preco.Desconto.ToString().Replace(",00", "") + "%</div>";
                }
            }

            return retorno;
        }

        public virtual List<string> GetEsportes()
        {
            try
            {
                return this.Esporte.Split(',').ToList();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public virtual List<string> GetCarrossel()
        {
            try
            {
                return this.Carrossel.Split(',').ToList();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        public virtual List<string> GetGeneros()
        {
            try
            {
                return this.Genero.Split(',').ToList();
            }
            catch (Exception ex)
            {
                return new List<string>();
            }
        }

        #endregion

        //public virtual string GetPrimeiraImagemLQ()
        //{
        //    if (Albuns.Count > 0)
        //    {
        //        Album album = Albuns.FirstOrDefault();
        //        if (album != null)
        //            if (album.Primeira != null)
        //                return album.Primeira.GetEnderecoImagemLQ();
        //            else if (album.Imagens.Count > 0)
        //            {
        //                return album.Imagens[0].GetEnderecoImagemLQ();
        //            }


        //        foreach (var item in Albuns)
        //            if (item.Primeira != null)
        //                return item.Primeira.GetEnderecoImagemLQ();
        //            else if (item.Imagens.Count > 0)
        //                return item.Imagens[0].GetEnderecoImagemLQ();

        //    }

        //    return uplImage.imgSemImagem;
        //}

        //public virtual string GetPrimeiraImagemHQ()
        //{
        //    if (Albuns.Count > 0)
        //    {
        //        Album album = Albuns.FirstOrDefault();
        //        if (album != null)
        //            if (album.Primeira != null)
        //                return album.Primeira.GetEnderecoImagemHQ();
        //            else if (album.Imagens.Count > 0)
        //            {
        //                return album.Imagens[0].GetEnderecoImagemHQ();
        //            }
        //            else
        //            {
        //                return uplImage.imgSemImagem;
        //            }

        //        foreach (var item in Albuns)
        //            if (item.Primeira != null)
        //                return item.Primeira.GetEnderecoImagemHQ();
        //            else if (item.Imagens.Count > 0)
        //                return item.Imagens[0].GetEnderecoImagemHQ();
        //    }

        //    return uplImage.imgSemImagem;
        //}

        //public virtual string GetSegundaImagemLQ()
        //{
        //    if (Albuns.Count > 0)
        //    {
        //        Album album = Albuns.FirstOrDefault();
        //        if (album != null)
        //            if (album.Primeira != null)
        //                return album.Primeira.GetEnderecoImagemLQ();
        //            else if (album.Imagens.Count > 1)
        //            {
        //                return album.Imagens[1].GetEnderecoImagemLQ();
        //            }


        //        foreach (var item in Albuns)
        //            if (item.Primeira != null)
        //                return item.Primeira.GetEnderecoImagemLQ();
        //            else if (item.Imagens.Count == 1)
        //                return item.Imagens[0].GetEnderecoImagemLQ();
        //            else if (item.Imagens.Count > 1)
        //                return item.Imagens[1].GetEnderecoImagemLQ();

        //    }

        //    return uplImage.imgSemImagem;
        //}


        public virtual IList<ImagemProduto> getImagensOrdenadas()
        {
            return listaFotos.OrderBy(x => x.Ordem).ThenBy(x => x.Nome).ToList();
        }

        public virtual string getPrimeiraImagemProdutoLQ()
        {
            if (listaFotos.Count > 0)
            {
                listaFotos = listaFotos.OrderBy(x => x.Ordem).ToList();
                return MetodosFE.BaseURL + "/ImagensLQ/" + listaFotos[0].Nome;
            }
            else
                return uplImage.imgSemImagem;
        }

        public virtual string getPrimeiraImagemProdutoHQ()
        {
            if (listaFotos.Count > 0)
            {
                listaFotos = listaFotos.OrderBy(x => x.Ordem).ToList();
                return MetodosFE.BaseURL + "/ImagensHQ/" + listaFotos[0].Nome;
            }
            else
                return uplImage.imgSemImagem;
        }
    }
}