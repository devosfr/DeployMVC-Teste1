

var itemLista = $('.itemLista');

if (itemLista.length == 2) {

    $('#listaCabecalho').css('paddin-left', '560px');

    //var listaCabecalho = $('#listaCabecalho');


    //$('li.linkLista').hide();


    $('.itemLista').hide();
    $('.linkLista').show();

    //for (var i = 0; i < itemLista.length; i++) {


    //    $('.itemLista a').mouseover(function () {
    //        //$('.cabecalhoSegmentos').toggleClass('cabecalhoSegmentosB');
    //        $(this).css('text-decoration', 'underline');
    //    });



    //}

} else {

    $('.itemLista').show();
    $('.linkLista').hide();

}

//Pagina Produtos

var listaImagnes = $('.img-produto');

var linkLeiaMais = $('.linkLeiaMais');

var imgModal = $('#imgModal');

var nomeProdutoModal = $('#nomeProdutoModal');

var descricaoProdutoModal = $('#descricaoProdutoModal');

function getProduct(obj) {


    //Populando a Modal
    imgModal[0].src = obj.children[0].children[0].src;

    nomeProdutoModal[0].innerText = obj.children[1].innerText;

    descricaoProdutoModal[0].innerText = obj.children[2].value;

}

function verificaForm() {


    var nomeLista = $('.nome');
    var nome = null;

    var foneLista = $('.fone');
    var fone = null;

    var emailLista = $('.email');
    var email = null;

    var msnLista = $('.msn');
    var msn = null;

    for (var i = 0; i < nomeLista.length; i++) {

        if (nomeLista.eq(i).val() != "") {
            nome = nomeLista.eq(i).val();
        }

        if (foneLista.eq(i).val() != "") {
            fone = foneLista.eq(i).val();
        }


        if (emailLista.eq(i).val() != "") {
            email = emailLista.eq(i).val();
        }

        if (msnLista.eq(i).val() != "") {
            msn = msnLista.eq(i).val();
        }

    }
}



$.ajax({
    url: 'Produtos/btnAdd_ServerClick',
    type: 'POST',
    dataType: 'html',
    contentType: 'application/*; charset=utf-8',
    data: {
        _nome: "",
        _fone: ""
    }
});



