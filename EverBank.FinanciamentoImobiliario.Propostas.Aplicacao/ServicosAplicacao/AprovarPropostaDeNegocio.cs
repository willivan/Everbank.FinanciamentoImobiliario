using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.ServicosAplicacao
{
    public class AprovarPropostaDeNegocio : ServicoAplicacao<AprovarPropostaDeNegocioRequest, AprovarPropostaDeNegocioResponse>
    {
        public IPropostaRepositorio PropostaRepositorio { get; set; }

        public IServicoEnvioEmail ServicoEnvioEmail { get; set; }

        public IEmissaoContratoServico ServicoEmissaoContrato { get; set; }

        public AprovarPropostaDeNegocioResponse Executar(AprovarPropostaDeNegocioRequest request)
        {
            PropostaDeNegocio proposta = PropostaRepositorio.CarregarPorNumeroDaProposta(request.NumeroDaProposta);
            proposta.AceitarProposta();
            PropostaRepositorio.Salvar(proposta);
            ServicoEmissaoContrato.EmitirContratoParaProposta(proposta);

            ServicoEnvioEmail.Enviar(proposta.Proponentes.Select(c => c.Email), $"Proposta {proposta.ID} aceita!", "Parabéns, sua proposta foi aceita! Aguarde o contato com os nossos analistas para darmos prosseguimento à emissão do contrato");
            return new AprovarPropostaDeNegocioResponse() { Status = true };
        }
    }

    public class AprovarPropostaDeNegocioRequest
    { 
        public String NumeroDaProposta { get; set; }
    }

    public class AprovarPropostaDeNegocioResponse
    {
        public bool Status { get; set; }
    }

}
