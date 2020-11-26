using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.ServicosAplicacao
{
    public class ReprovarDocumentacao : ServicoAplicacao<ReprovarDocumentacaoRequest, ReprovarDocumentacaoResponse>
    {

        public IPropostaRepositorio PropostaRepositorio { get; set; }

        public IServicoEnvioEmail ServicoEnvioEmail { get; set; }

        public ReprovarDocumentacao(IPropostaRepositorio propostaRepositorio)
        {
            PropostaRepositorio = propostaRepositorio;
        }

        public ReprovarDocumentacaoResponse Executar(ReprovarDocumentacaoRequest request)
        {
            PropostaDeNegocio proposta = PropostaRepositorio.CarregarPorNumeroDaProposta(request.NumeroDaProposta);
            if (proposta == null)
                throw new PropostaDeNegocioNaoEncontradaException();

            proposta.ReprovarDocumentacao(request.Documento,request.Justificativa);

            ServicoEnvioEmail.Enviar(proposta.Proponentes.Select(c => c.Email), $"Proposta {proposta.ID}: Documento recusado", "Olá! O documento " + request.Documento + " foi recusado pelo motivo de " + request.Justificativa);

            return new ReprovarDocumentacaoResponse() { Status = true };
        }
    }

    public class ReprovarDocumentacaoRequest
    {
        public String NumeroDaProposta { get; set; }

        public String Documento { get; set; }

        public String Justificativa { get; set; }
    }

    public class ReprovarDocumentacaoResponse
    {
        public bool Status { get; set; }
    }
}
