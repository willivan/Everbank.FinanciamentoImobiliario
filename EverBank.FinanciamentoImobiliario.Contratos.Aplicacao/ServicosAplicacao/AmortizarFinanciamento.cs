using EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Excecoes;
using EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.ServicosAplicacao
{
    public class AmortizarFinanciamento : ServicoAplicacao<AmortizarFinanciamentoRequest, AmortizarFinanciamentoResponse>
    {
        public IContratoDeFinanciamentoRepositorio ContratoDeFinanciamentoRepositorio { get; set; }

        public IServicoEnvioSMS ServicoEnvioSMS { get; set; }

        public AmortizarFinanciamento(IContratoDeFinanciamentoRepositorio contratoDeFinanciamentoRepositorio, IServicoEnvioSMS servicoEnvioSMS)
        {
            ContratoDeFinanciamentoRepositorio = contratoDeFinanciamentoRepositorio;
            ServicoEnvioSMS = servicoEnvioSMS;
        }

        public AmortizarFinanciamentoResponse Executar(AmortizarFinanciamentoRequest request)
        {
            ContratoFinanciamento contrato = ContratoDeFinanciamentoRepositorio.Carregar(request.NumeroDoContrato);

            if (contrato == null)
                throw new ContratoNaoEncontradoException();

            contrato.AmortizarFinanciamento(request.ValorAmortizacao, request.TipoAbatimentoAmortizacao);

            ContratoDeFinanciamentoRepositorio.Salvar(contrato);

            ServicoEnvioSMS.EnviarMensagem(contrato.MutuariosDoContrato.Select(c => c.Telefone).ToList(), $"Recebemos o valor de {request.ValorAmortizacao} referente a amortização do contrato {request.NumeroDoContrato}.");

            return new AmortizarFinanciamentoResponse() { Status = true };
        }
    }

    public class AmortizarFinanciamentoRequest
    {
        public String NumeroDoContrato { get; set; }

        public decimal ValorAmortizacao { get; set; }

        public TipoAbatimentoAmortizacao TipoAbatimentoAmortizacao { get; set; }

    }


    public class AmortizarFinanciamentoResponse
    {
        public bool Status { get; set; }
    }
}
