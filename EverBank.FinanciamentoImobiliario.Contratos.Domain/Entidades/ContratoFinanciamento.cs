using System;
using System.Collections.Generic;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Excecoes;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.ServicosDominio;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.ObjetosDeValor;
using System.Linq;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades
{
    public class ContratoFinanciamento
    {
        public String NumeroDoContrato{get;private set;}

        public String NumeroDaProposta{get;private set;}

        public List<Mutuario> MutuariosDoContrato{get;private set;}

        public Imovel Imovel{get;private set;}

        public decimal ValorFinanciado{get;private set;}

        public decimal TaxaDeJurosAnual{get;private set;}

        public int PrazoFinanciamento{get;set;}

        public decimal SaldoDevedor{get;private set;}

        public int DiaVencimento{get;private set;}

        public SistemaAmortizacao SistemaAmortizacao{get;private set;}

        public List<PrestacaoFinanciamento> Prestacoes{get;private set;}

        public TipoSistemaAmortizacao TipoSistemaAmortizacao{get;private set;}

        public DateTime VencimentoPrimeiraParcela{get;private set;}

        public ContratoFinanciamento(String numeroDaProposta, List<Mutuario> mutuarios, Imovel imoveis, decimal valorFinanciado, decimal taxaDeJurosAnual, int prazoFinanciamento, SistemaAmortizacao tabelaAmortizacao, int diaVencimento, DateTime vencimentoPrimeiraParcela)
        {
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(numeroDaProposta),"Número da Proposta é obrigatória");
            ExcecaoDominio.LancarQuando(()=>mutuarios==null || mutuarios.Count==0,"É necessário ao menos um mutuário no contrato");
            ExcecaoDominio.LancarQuando(()=>valorFinanciado == 0,"Valor financiado é inválido");
            ExcecaoDominio.LancarQuando(()=>taxaDeJurosAnual == 0,"Taxa de juros anual é inválida");
            ExcecaoDominio.LancarQuando(()=>diaVencimento<1 || diaVencimento >25,"Dia do vencimento inválido");
            ExcecaoDominio.LancarQuando(()=>vencimentoPrimeiraParcela<DateTime.Today.AddDays(21),"Prazo para primeira parcela deve ser, pelo menos, 21 dias");
            
            NumeroDoContrato = Guid.NewGuid().ToString();
            NumeroDaProposta = numeroDaProposta;
            MutuariosDoContrato = mutuarios;
            Imovel = imoveis;
            ValorFinanciado=valorFinanciado;
            TaxaDeJurosAnual = taxaDeJurosAnual;
            PrazoFinanciamento = prazoFinanciamento;
            SistemaAmortizacao = tabelaAmortizacao;
            TipoSistemaAmortizacao = tabelaAmortizacao.TipoSistemaAmortizacao;
            VencimentoPrimeiraParcela = vencimentoPrimeiraParcela;
            Prestacoes = tabelaAmortizacao.GerarTabelaFinanciamento(this);
        }
        

        public void QuitarPrestacao(int numeroPrestacao, decimal valorPago, DateTime dataPagamento)
        {
            PrestacaoFinanciamento prestacao = Prestacoes.FirstOrDefault(c => c.NumeroPrestacao == numeroPrestacao);
            ExcecaoDominio.LancarQuando(() => prestacao == null, "Prestação não encontrada");

            prestacao.Quitar(valorPago, dataPagamento);

            SaldoDevedor = SaldoDevedor - prestacao.ValorAmortizacao;
        }

        public void AmortizarFinanciamento(decimal valorAmortizacao, TipoAbatimentoAmortizacao tipoAbatimentoAmortizacao)
        {
            this.SaldoDevedor = this.SaldoDevedor - valorAmortizacao;

            if(tipoAbatimentoAmortizacao == TipoAbatimentoAmortizacao.Parcela)
            {
                SistemaAmortizacao.AmortizarValorPrestacao(this, valorAmortizacao);
            }
            else
            {
                SistemaAmortizacao.AmortizarPrazo(this, valorAmortizacao);
            }
        }


    }
}
