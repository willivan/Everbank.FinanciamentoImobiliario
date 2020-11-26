using EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.ServicosAplicacao
{
    public class EmitirContratoDeFinanciamento : ServicoAplicacao<EmitirContratoRequest, EmitirContratoResponse>
    {
        public IContratoDeFinanciamentoRepositorio ContratoDeFinanciamentoRepositorio { get; set; }

        public IServicoEnvioBoleto ServicoEnvioBoleto { get; set; }

        public EmitirContratoResponse Executar(EmitirContratoRequest request)
        {

            ContratoDeFinanciamentoRepositorio.Salvar(request.Contrato);

            ServicoEnvioBoleto.Enviar(request.Contrato.MutuariosDoContrato.Select(c => c.Email), request.Contrato);

            return new EmitirContratoResponse() { Status = true };
        }
    }

    public class EmitirContratoRequest
    {
        //Toooodas as propriedades
        public ContratoFinanciamento Contrato { get; set; }
    }

    public class EmitirContratoResponse
    {
        public bool Status { get; set; }
    }
}
