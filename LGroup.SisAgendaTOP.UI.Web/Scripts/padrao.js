/*
*
* Author: Nathan Oliveira
* E-Mail: nathan_justino@hotmail.com
* Version: 2.0
*
*/

//IREMOS CRIAR UMA VARIAVEL PARA
//ARMAZENAR AS FUNÇÕES 
var section = {
    init: function () {
        //INICIALIZAMOS A FUNCIONALIDADE 
        //DE MASCARAS
        section.mascara();

        //INICIALIAZAMOS O FUNÇÃO DE VALIDAR EMAIL
        $("#consultar").click(section.validarEmail);

        //INICIALIZAMOS A FUNÇÃO DE VALIDAR TELEFONE
        //BLUR -> FUNCIONALIDADE DE PERDA DE FOCO
        $("input").blur(section.validarTelefone)

        //INICIALIZAMOS A FUNCIONALIDADE DE CARREGAR ENDEREÇO
        $("#CEP").bind("focusout", section.loadCep);
    },
    mascara: function () {

        //INSERIMOS AS MASCARAS
        //UTILIZANDO O JQUERY (OSTENTAÇÃO $)
        //FORMAS DE MANIPULAÇÕES
        //CLASSE = .
        //ID = #
        //ELEMENTOS = LABEL, INPUT, IMG
        $("#Telefone").mask("(99) 9999-9999");
        $("#DataNascimento").mask("99/99/9999");
        $("#CEP").mask("99999-999");

        //DEIXAMOS OS TOOLTP BUNITÃO
        //$("input, select").tooltp();

        //DEIXAMOS O CALANDARIO BUNITÃO 
        //INSERIR UM CALENDAR (JQUERY UI)
        $("#DataNascimento").datepicker({ dateFormat: "dd/mm/yy", showAnim: "bounce" });

    },
    validarEmail: function () {

        //1° PASSO - ARMAZENOS O OBEJTO DO CAMPO EMAIL
        var email = $("#Email").val();

        //2° PASSA - INICIAMOS O AJAX
        $.ajax({
            type: "GET",
            url: "http://localhost:1732/api/Contato/EmailJaExiste",
            data: { texto_: email },
            success: function (retorno) {
                //VERIFICAMOS SE A EMAIL JÁ EXISTE
                if (retorno) {
                    //DISPARAMOS A MENSAGEM INFORMANDO 
                    //QUE O EMAIL JÁ EXISTE
                    bootbox.alert("E-Mail já existe MALANDRÃO!")

                    //DESABILITAMOS O BOTÃO CADASTRAR
                    $("#cadastrar").attr("disabled", "disabled");
                }
                else {
                    //HABILITAMOS O BOTÃO CADASTRAR
                    $("#cadastrar").removeAttr("disabled");
                }

            }
        });

    },
    validarTelefone: function () {

        //1° PASSAO, ARMAZENAMOS O OBJETO DO CAMPO TELEFONE
        var telefone = $(this).val();

        //2° PASSO INICIALIZAMOS O AJAX
        $.ajax({
            type: "GET",
            url: "http://localhost:1732/api/Contato/TelefoneJaExiste",
            data: { numero_: telefone },
            success: function (retorno) {
                if (retorno) {
                    bootbox.alert("Telefone já existe");
                    $("#cadastrar").attr("disabled", "disabled");
                }
                else {
                    $("#cadastrar").removeAttr("disabled");
                }
            }
        });


    },
    loadCep: function (e) {
        var _this = $(e.currentTarget);
        if ($.trim(_this.val()) != "") {
            $.get("http://apps.widenet.com.br/busca-cep/api/cep.json", { code: _this.val() },
                function (result) {
                    if (result.status != 1) {
                        bootbox.alert('Endereço não encontrado');
                        return;
                    }
                    $('section #Bairro').val(result.district);
                    $('section #Logradouro').val(result.address);
                    $('section #Numero').focus();
                });
        }
    }
};
$(document).ready(section.init)