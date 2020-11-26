using EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Excecoes;
using EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.ServicosAplicacao
{
    public class QuitarPrestacaoContrato : ServicoAplicacao<QuitarParcelaContratoRequest, QuitarParcelaContratoResponse>
    {

        public IContratoDeFinanciamentoRepositorio ContratoDeFinanciamentoRepositorio { get; set; }

        public IServicoEnvioSMS ServicoEnvioSMS { get; set; }

        public QuitarPrestacaoContrato(IContratoDeFinanciamentoRepositorio contratoDeFinanciamentoRepositorio, IServicoEnvioSMS servicoEnvioSMS)
        {
            ContratoDeFinanciamentoRepositorio = contratoDeFinanciamentoRepositorio;
            ServicoEnvioSMS = servicoEnvioSMS;
        }

        public QuitarParcelaContratoResponse Executar(QuitarParcelaContratoRequest request)
        {

            ContratoFinanciamento contrato = ContratoDeFinanciamentoRepositorio.Carregar(request.NumeroContrato);
            if (contrato == null)
                throw new ContratoNaoEncontradoException();

            contrato.QuitarPrestacao(request.NumeroParcela, request.ValorPago, request.DataPagamento);

            ContratoDeFinanciamentoRepositorio.Salvar(contrato);

            ServicoEnvioSMS.EnviarMensagem(contrato.MutuariosDoContrato.Select(c => c.Telefone).ToList(), $"Everbank informa: recebemos o valor de {request.ValorPago} referente a parcela número {request.NumeroParcela} do contrato {request.NumeroContrato}");

            return new QuitarParcelaContratoResponse() { Status = true };
        }
    }

    public class QuitarParcelaContratoRequest
    {
        public String NumeroContrato { get; set; }

        public int NumeroParcela { get; set; }

        public decimal ValorPago { get; set; }

        public DateTime DataPagamento { get; set; }
    }

    public class QuitarParcelaContratoResponse
    { 
        public bool Status { get; set; }
    }

}
