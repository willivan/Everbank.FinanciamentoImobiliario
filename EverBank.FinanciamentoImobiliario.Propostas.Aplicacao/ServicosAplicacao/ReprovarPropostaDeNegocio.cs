using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.ServicosAplicacao
{
    public class ReprovarPropostaDeNegocio : ServicoAplicacao<ReprovarPropostaDeNegocioRequest, ReprovarPropostaDeNegocioResponse>
    {

        public IPropostaRepositorio PropostaRepositorio { get; set; }

        public IServicoEnvioEmail ServicoEmail { get; set; }



        public ReprovarPropostaDeNegocioResponse Executar(ReprovarPropostaDeNegocioRequest request)
        {
            PropostaDeNegocio proposta = PropostaRepositorio.CarregarPorNumeroDaProposta(request.NumeroDaProposta);
            
            if (proposta == null)
                throw new PropostaDeNegocioNaoEncontradaException();
            
            proposta.RecusarProposta(request.Justificativa);
            PropostaRepositorio.Salvar(proposta);

            ServicoEmail.Enviar(proposta.Proponentes.Select(c => c.Email), $"Proposta {proposta.ID} Recusada", "Atenção, sua proposta foi recusada pelo motivo de " + request.Justificativa);

            return new ReprovarPropostaDeNegocioResponse() { Status = true };
        }
    }

    public class ReprovarPropostaDeNegocioRequest
    {
        public String NumeroDaProposta { get; set; }

        public String Justificativa { get; set; }
    }

    public class ReprovarPropostaDeNegocioResponse
    {
        public bool Status { get; set; }

        public String MensagemErro { get; set; }
    }
}
