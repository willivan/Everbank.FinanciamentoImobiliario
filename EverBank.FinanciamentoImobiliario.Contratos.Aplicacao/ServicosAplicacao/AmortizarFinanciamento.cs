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
    /// <summary>
    /// Serviço responsável pela amortização de financiamentos imobiliários.
    /// 
    /// Esta classe implementa a regra de negócio para processar pagamentos adicionais (amortizações)
    /// em contratos de financiamento imobiliário. A amortização pode ser aplicada de diferentes formas,
    /// conforme especificado pelo TipoAbatimentoAmortizacao (redução do valor da parcela ou do prazo).
    /// 
    /// O processo de amortização envolve:
    /// 1. Carregar o contrato de financiamento pelo número
    /// 2. Aplicar o valor da amortização conforme o tipo de abatimento escolhido
    /// 3. Persistir as alterações no contrato
    /// 4. Notificar todos os mutuários por SMS sobre a amortização realizada
    /// 
    /// Dependências:
    /// - IContratoDeFinanciamentoRepositorio: Para acesso e persistência de dados do contrato
    /// - IServicoEnvioSMS: Para notificação dos mutuários via SMS
    /// </summary>
    public class AmortizarFinanciamento : ServicoAplicacao<AmortizarFinanciamentoRequest, AmortizarFinanciamentoResponse>
    {
        /// <summary>
        /// Repositório para acesso e persistência de contratos de financiamento
        /// </summary>
        public IContratoDeFinanciamentoRepositorio ContratoDeFinanciamentoRepositorio { get; set; }

        /// <summary>
        /// Serviço para envio de notificações SMS aos mutuários
        /// </summary>
        public IServicoEnvioSMS ServicoEnvioSMS { get; set; }

        /// <summary>
        /// Construtor da classe AmortizarFinanciamento
        /// </summary>
        /// <param name="contratoDeFinanciamentoRepositorio">Repositório de contratos de financiamento</param>
        /// <param name="servicoEnvioSMS">Serviço de envio de SMS</param>
        public AmortizarFinanciamento(IContratoDeFinanciamentoRepositorio contratoDeFinanciamentoRepositorio, IServicoEnvioSMS servicoEnvioSMS)
        {
            ContratoDeFinanciamentoRepositorio = contratoDeFinanciamentoRepositorio;
            ServicoEnvioSMS = servicoEnvioSMS;
        }

        /// <summary>
        /// Executa o processo de amortização do financiamento imobiliário
        /// </summary>
        /// <param name="request">Objeto contendo os dados necessários para a amortização:
        /// - Número do contrato
        /// - Valor da amortização
        /// - Tipo de abatimento (redução de parcela ou de prazo)</param>
        /// <returns>Resposta indicando o status da operação</returns>
        /// <exception cref="ContratoNaoEncontradoException">Lançada quando o contrato não é encontrado</exception>
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

    /// <summary>
    /// Classe de requisição para amortização de financiamento imobiliário
    /// Contém os dados necessários para processar uma amortização
    /// </summary>
    public class AmortizarFinanciamentoRequest
    {
        /// <summary>
        /// Identificador único do contrato de financiamento
        /// </summary>
        public String NumeroDoContrato { get; set; }

        /// <summary>
        /// Valor a ser amortizado no financiamento
        /// </summary>
        public decimal ValorAmortizacao { get; set; }

        /// <summary>
        /// Define como o valor amortizado será aplicado no contrato:
        /// - Redução do valor das parcelas
        /// - Redução do prazo do financiamento
        /// </summary>
        public TipoAbatimentoAmortizacao TipoAbatimentoAmortizacao { get; set; }
    }

    /// <summary>
    /// Classe de resposta para o processo de amortização
    /// Contém o resultado da operação de amortização
    /// </summary>
    public class AmortizarFinanciamentoResponse
    {
        /// <summary>
        /// Indica se a operação de amortização foi realizada com sucesso
        /// </summary>
        public bool Status { get; set; }
    }
}
