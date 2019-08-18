/*
function showHideGrid(obj)
{
	var o = obj.parentNode.parentNode.parentNode.parentNode.parentNode.lastChild;
	if(o.style.display == 'none')
		o.style.display = 'block';
	else
		o.style.display = 'none';
}*/

function MascararEntrada(objForm, strField, sMask, evtKeyPress)
{
	/*
	* Descri&aelig;&acirc;o.: formata um campo do formulário de 
	* acordo com a máscara informada... 
	* Parâmetros: - objForm (o Objeto Form) 
	* - strField (string contendo o nome 
	* do textbox) 
	* - sMask (mascara que define o 
	* formato que o dado será apresentado, 
	* usando o algarismo "9" para 
	* definir números e o s&igrave;mbolo "!" para 
	* qualquer caracter... 
	* - evtKeyPress (evento) 
	* 
	* Uso.......: <input type="textbox" 
	* name="xxx"..... 
	* onkeypress="return txtBoxFormat(document.rcfDownload, 'str_cep', '99999-999', event);"> 
	* Observa&aelig;&acirc;o: As máscaras podem ser representadas como os exemplos abaixo: 
	* CEP -> 99.999-999 
	* CPF -> 999.999.999-99 
	* CNPJ -> 99.999.999/9999-99 
	* Data -> 99/99/9999 
	* Tel Resid -> (99) 999-9999 
	* Tel Cel -> (99) 9999-9999 
	* Processo -> 99.999999999/999-99 
	* C/C -> 999999-! 
	* E por a&igrave; vai... 
	*/ 
		
	var i, nCount, sValue, fldLen, mskLen,bolMask, sCod, nTecla; 

	if(document.all)
	{
		// Internet Explorer		
		nTecla = evtKeyPress.keyCode;
	}
	else if(document.layers)
	{ // Nestcape		
		nTecla = evtKeyPress.which;
	}
	else
	{ 		
		nTecla = evtKeyPress.which;
	}
	
	sValue = objForm[strField].value;

	// Limpa todos os caracteres de formata&aelig;&acirc;o que
	// já estiverem no campo. 
	sValue = sValue.toString().replace( "-", "" );
	sValue = sValue.toString().replace( "-", "" );
	sValue = sValue.toString().replace( ".", "" );
	sValue = sValue.toString().replace( ".", "" );
	sValue = sValue.toString().replace( "/", "" );
	sValue = sValue.toString().replace( "/", "" );
	sValue = sValue.toString().replace( "(", "" );
	sValue = sValue.toString().replace( "(", "" ); 
	sValue = sValue.toString().replace( ")", "" ); 
	sValue = sValue.toString().replace( ")", "" ); 
	sValue = sValue.toString().replace( " ", "" ); 
	sValue = sValue.toString().replace( " ", "" ); 
	fldLen = sValue.length;
	mskLen = sMask.length;
	
	i = 0;
	nCount = 0;
	sCod = "";
	mskLen = fldLen;

	while (i <= mskLen)
	{
		bolMask = ((sMask.charAt(i) == "-") || (sMask.charAt(i) == ".") || (sMask.charAt(i) == "/"))
		bolMask = bolMask || ((sMask.charAt(i) == "(") || (sMask.charAt(i) == ")") || (sMask.charAt(i) == " "))
		if (bolMask)
		{
			sCod += sMask.charAt(i);
			mskLen++;
		}
		else
		{
			sCod += sValue.charAt(nCount);
			nCount++;
		}
		i++;
	}

	objForm[strField].value = sCod;
	if (nTecla != 8)
	{
		// backspace
		if (sMask.charAt(i-1) == "9")
		{
			// apenas números...
			// números de 0 a 9
			return ((nTecla > 47) && (nTecla < 58));
		}		
		else
		{
			// qualquer caracter...
			return true;
		}
	}
	else
	{
		return true;
	}
}

function AbrirDetalhes(botaoAbrir, nomeIFrame, src)
{
	var iFrame = document.getElementById(nomeIFrame);
	
	iFrame.style.top = botaoAbrir.offsetTop + 10 + "px";
	iFrame.style.left = botaoAbrir.offsetLeft + "px";
	
	toggleClassName('details-openup', iFrame);
	
	iFrame.src = src;
}

function hasClassName(_class, scope)
{
	var rexp = new RegExp(_class+"\\b","gi");
	if(scope && scope.className.search(rexp)>-1)
	{
		return true;
	}
	else
	{
		return false;
	}
}

function toggleClassName(_class, scope, index)
{
	if(hasClassName(_class, scope))
	{
		removeClassName(_class, scope, index)
		return true;
	}
	else
	{
		appendClassName(_class, scope, index)
		return true;
	}
	return false;
}

function appendClassName(_class, scope, index)
{
	if(hasClassName(_class,scope) == false)
	{
		if(!isNaN(index))
		{
			var a = scope.className.split(" ");
			a[index] = _class;
			scope.className = a.join(" ")
		}
		else
		{
			scope.className += " " + _class;
		}
		return true;
	}
	return false;
}

function removeClassName(_class, scope, index)
{
	if(hasClassName( _class, scope) == true)
	{
		if(!isNaN(index))
		{ 
			var a = scope.className.split(" ");
			delete a[index];
			scope.className = a.join(" ");
		}
		else
		{
			var rexp = new RegExp(_class+"\\b","gi");
			scope.className = scope.className.replace(rexp,"");
		}
		return true;
	}
	return false;
}

function MostrarRelacionar(tipo)
{
	var arquivo;
		
	switch(tipo)
	{
		case 'Galeria':
			arquivo = '../GaleriaRelacionar.aspx';
			break;		
	}
	
	var janela
	janela  = window.open(arquivo,'_blank','menubar=no,resizable=no,scrollbars=yes,height=550,width=633,status=yes,toolbar=no,location=no,top=100,left=100');
}

function PostarJanelaPai()
{
	//window.opener.location.reload();
	
	var formularioPai = window.opener.document.forms[0]
	formularioPai.action = window.opener.location;
	//formularioPai.method = "post";
	formularioPai.submit();
	self.close();
}

function ExibirOcultar(icoMais, icoMenos, objeto) {
	try 
	{
		if(icoMais.style.display == 'none')
		{
			icoMais.style.display = '';
			icoMenos.style.display = 'none';
		}
		else
		{
			icoMais.style.display = 'none';
			icoMenos.style.display = '';
		}
		
		if (objeto.style.display == '' || objeto.style.display == 'block')
		{
			objeto.style.display = 'none';
		}
		else 
		{
			objeto.style.display = 'block';
		}
	} 
	catch (ex) 
	{ 
		alert(ex);
	}
}