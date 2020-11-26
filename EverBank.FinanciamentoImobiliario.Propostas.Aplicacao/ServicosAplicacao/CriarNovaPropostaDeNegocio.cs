using EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Enums;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.CasosDeUso
{

    public class CriarNovaPropostaDeNegocio : ServicoAplicacao<CriarNovaPropostaRequest, CriarNovaPropostaResponse>
    {

        public IPropostaRepositorio PropostaRepositorio { get; set; }

        public IServicoEnvioEmail ServicoEnvioEmail { get; set; }

        public CriarNovaPropostaDeNegocio(IPropostaRepositorio propostaRepositorio, IServicoEnvioEmail servicoEnvioEmail)
        {
            PropostaRepositorio = propostaRepositorio;
            ServicoEnvioEmail = servicoEnvioEmail;
        }

        public CriarNovaPropostaResponse Executar(CriarNovaPropostaRequest request)
        {
            try
            {
                PropostaDeNegocio proposta = new PropostaDeNegocio(request.Proponentes, request.Imovel, request.ValorEntrada, request.ValorFinanciamento, request.PrazoFinanciamento, request.JurosFinanciamento, request.SistemaAmortizacao);
                PropostaRepositorio.Salvar(proposta);

                ServicoEnvioEmail.Enviar(request.Proponentes.Select(c => c.Email), "Proposta de Financiamento", "Prezados Senhores, acabamos de receber sua proposta de financiamento imobiliário, em breve entraremos em contato");

                return new CriarNovaPropostaResponse() { Status = true };
            }
            catch (ExcecaoDominio excecao)
            {
                return new CriarNovaPropostaResponse() { Status = false, MensagemErro = excecao.Message };
            }

        }
    }

    public class CriarNovaPropostaRequest
    {
        public List<Proponente> Proponentes { get; set; }

        public Imovel Imovel { get; set; }

        public decimal ValorEntrada { get; set; }

        public decimal ValorFinanciamento { get; set; }

        public int PrazoFinanciamento { get; set; }

        public decimal JurosFinanciamento { get; set; }

        public SistemaAmortizacao SistemaAmortizacao { get; set; }
    }

    public class CriarNovaPropostaResponse
    {
        public bool Status { get; set; }

        public String MensagemErro { get; set; }
    }

    
}
