using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.ServicosAplicacao
{
    public class AnexarDocumentacao : ServicoAplicacao<AnexarDocumentacaoRequest, AnexarDocumentacaoResponse>
    {
        public IPropostaRepositorio PropostaRepositorio { get; set; }

        public IRepositorioArquivos RepositorioArquivos { get; set; }

        public AnexarDocumentacao(IPropostaRepositorio propostaRepositorio, IRepositorioArquivos repositorioArquivos)
        {
            PropostaRepositorio = propostaRepositorio;
            RepositorioArquivos = repositorioArquivos;
        }

        public AnexarDocumentacaoResponse Executar(AnexarDocumentacaoRequest request)
        {
            PropostaDeNegocio proposta = PropostaRepositorio.CarregarPorNumeroDaProposta(request.NumeroDaProposta);

            if (proposta == null)
                throw new PropostaDeNegocioNaoEncontradaException();

            String caminhoArquivo = RepositorioArquivos.Salvar(request.NomeArquivo, request.ExtensaoArquivo, request.Arquivo);

            proposta.AnexarDocumento(request.NomeDocumento, caminhoArquivo);

            
            return new AnexarDocumentacaoResponse()
            {
                Status = true
            };

        }
    }

    public class AnexarDocumentacaoRequest
    {
        public String NumeroDaProposta { get; set; }

        public String NomeDocumento { get; set; }

        public byte[] Arquivo { get; set; }

        public String ExtensaoArquivo { get; set; }

        public String NomeArquivo{ get; set; }
    }

    public class AnexarDocumentacaoResponse
    {
        public bool Status { get; set; }

        public String MensagemErro { get; set; }
    }
}
