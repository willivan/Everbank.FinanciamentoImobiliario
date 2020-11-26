using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.ServicosAplicacao
{
    public class AprovarDocumentacao : ServicoAplicacao<AprovarDocumentacaoRequest, AprovarDocumentacaoResponse>
    {

        public IPropostaRepositorio PropostaRepositorio { get; set; }

        public AprovarDocumentacao(IPropostaRepositorio propostaRepositorio)
        {
            PropostaRepositorio = propostaRepositorio;
        }

        public AprovarDocumentacaoResponse Executar(AprovarDocumentacaoRequest request)
        {
            PropostaDeNegocio proposta = PropostaRepositorio.CarregarPorNumeroDaProposta(request.NumeroDaProposta);
            if (proposta == null)
                throw new PropostaDeNegocioNaoEncontradaException();

            proposta.AprovarDocumentacao(request.Documento);

            return new AprovarDocumentacaoResponse() { Status = true };
        }
    }

    public class AprovarDocumentacaoRequest
    {
        public String NumeroDaProposta { get; set; }

        public String Documento { get; set; }
    }

    public class AprovarDocumentacaoResponse
    {
        public bool Status { get; set; }
    }
}
