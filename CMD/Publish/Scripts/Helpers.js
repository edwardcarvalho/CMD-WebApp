
function setCustomStyleSheet(){
    //adicionar a classe loader na tag de style da pagina
	var css = '.loader {padding: 11px; width: 120px;height: 120px;-webkit-animation: spin 2s linear infinite;animation: spin 2s linear infinite;}@-webkit-keyframes spin {0% {-webkit-transform: rotate(0deg);}100% {-webkit-transform: rotate(360deg);}}@keyframes spin {0% {transform: rotate(0deg);}100% {transform: rotate(360deg);}}',
	head = document.head || document.getElementsByTagName('head')[0],
	style = document.createElement('style');

	style.type = 'text/css';
	
	if (style.styleSheet){
		style.styleSheet.cssText = css;
	} else {
		style.appendChild(document.createTextNode(css));
	}

	head.appendChild(style);
}

function freezeScreen(operation){
	if(operation == "add"){
        var div = '<div class="freeze" style="background: black;width: 100%;height: 100%;z-index: 10000;position: fixed;opacity: 0.5;"><div style="position: absolute;left: 47%;font-size: 30px;top: 40%;"><i class="fa fa-spinner fa-spin" style="font-size: 50px;"></i></div></div>';
		$('body').prepend(div);
	}else if(operation == "remove"){
		$('div.freeze').remove();
	}
}

//converte tabela HTML em JSON
function tableToJson(table) {
    var data = [];

    // first row needs to be headers
    var headers = [];
    for (var i=0; i<table.rows[0].cells.length; i++) {
        headers[i] = table.rows[0].cells[i].innerHTML.toLowerCase().replace(/ /gi,'');
    }
    // go through cells
    for (var i=0; i<table.rows.length; i++) {

        var tableRow = table.rows[i];
        var rowData = {};

        for (var j=0; j<tableRow.cells.length; j++) {
            rowData[ headers[j] ] = tableRow.cells[j].innerHTML;
        }
        data.push(rowData);
    }       

    return data;
};

function getAddressInformationByCep(cep){
	freezeScreen("add");
	if(cep.replace("-","").length == 8){
	var url = "https://viacep.com.br/ws/"+cep+"/json"
     $.get(url,function(data){
		 if(!data.erro){
			freezeScreen("remove");
		 }else{
			freezeScreen("remove");
		 }
	 });
	}else{
		freezeScreen("remove");
	}
}

function formatCurrency (prefix , num) {
    return prefix + " " + num
       .toFixed(2) // always two decimal digits
       .replace(".", ",") // replace decimal point character with ,
       .replace(/(\d)(?=(\d{3})+(?!\d))/g, "$1.") // use . as a separator
}

function CPF() {

     this.validate = function (cpf) {
        var sum = 0;
        var remainder;

        cpf = cpf.replace(/\D/g,""); //remove tudo que não é digito

        var allEqual = true;
        for (var i = 0; i < cpf.length - 1; i++) {
            if (cpf[i] != cpf[i + 1])
                allEqual = false;
        }
        if (allEqual)
            return false;

        for (i = 1; i <= 9; i++)
            sum = sum + parseInt(cpf.substring(i - 1, i)) * (11 - i);
        remainder = (sum * 10) % 11;

        if ((remainder == 10) || (remainder == 11))
            remainder = 0;
        if (remainder != parseInt(cpf.substring(9, 10)))
            return false;

        sum = 0;
        for (i = 1; i <= 10; i++)
            sum = sum + parseInt(cpf.substring(i - 1, i)) * (12 - i); remainder = (sum * 10) % 11;

        if ((remainder == 10) || (remainder == 11))
            remainder = 0;
        if (remainder != parseInt(cpf.substring(10, 11)))
            return false;

        return true;
    };
};


//retorna o numero do telefone com máscara
    ////necessário adicionar pattern ao input -> ('pattern','\([0-9]{2}\)[\s][0-9]{4}-[0-9]{4,5}');
    ////adicionar a mascara -> .mask("(00) 0000-00009");
function setPhoneMask(v){
    v=v.replace(/\D/g,"");             //Remove tudo o que não é dígito
    v=v.replace(/^(\d{2})(\d)/g,"($1) $2"); //Coloca parênteses em volta dos dois primeiros dígitos
    v=v.replace(/(\d)(\d{4})$/,"$1-$2");    //Coloca hífen entre o quarto e o quinto dígitos
    return v;
}


//retorna o numero do CPF formatado com . / , e -
function formatCPF(cpf){

	cpf = cpf.toString();
	var complemento = "";

	if(cpf.length < 11){
		for(var i = 0; i < 11 - cpf.length; i++){
			complemento += "0";
		};
	};

	cpf = complemento + cpf;
	cpf = cpf.replace( /\D/g , ""); //Remove tudo o que não é dígito
    cpf = cpf.replace( /(\d{3})(\d)/ , "$1.$2"); //Coloca um ponto entre o terceiro e o quarto dígitos
	cpf = cpf.replace( /(\d{3})(\d)/ , "$1.$2"); //de novo (para o segundo bloco de números)
	cpf = cpf.replace( /(\d{3})(\d{1,2})$/ , "$1-$2"); //Coloca um hífen entre o terceiro e o quarto dígitos
	
    return cpf;

};