using System.Linq;
using System;
using System.Collections.Generic;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.ObjetosDeValor;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.ServicosDominio;

namespace TestesContratos
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Mutuario> mutuarios = new List<Mutuario>();
            mutuarios.Add(new Mutuario("Jose da Silva","123.123.123.12","11999999999","meuemail@gmail.com",new DateTime(1986,11,01),9000,"Analista de Sistemas"));
            Imovel imovel = new Imovel(new Endereco("Av dos Testes","3","Apto 65","Jd dos Testes","São José dos Testes","SP","01112-111"),TipoImovel.Apartamento,EstadoImovel.Usado,300000M);
            ContratoFinanciamento contrato = new ContratoFinanciamento("0123.456.789",mutuarios,imovel,230000,7.9M,360,new SistemaAmortizacaoPrice(),24,new DateTime(2020,12,24));
            
            Console.Write(".No".PadLeft(10,' ')+"|");
            Console.Write("Data Vencto.".PadLeft(15, ' ') + '|');
            Console.Write("Saldo Dev.".PadLeft(15,' ')+'|');
            Console.Write("Juros".PadLeft(15,' ')+"|");
            Console.Write("Amor. Prest".PadLeft(15,' ')+"|");
            Console.WriteLine("Val. Prest".PadLeft(15,' ')+"|");
            foreach(var prestacao in contrato.Prestacoes)
            {
                Console.Write(prestacao.NumeroPrestacao.ToString().PadLeft(10,' ')+"|");
                Console.Write(prestacao.DataVencimento.ToString("dd/MM/yyyy").PadLeft(15, ' ') + "|");
                Console.Write(Math.Round(prestacao.SaldoDevedor,2).ToString("C2").PadLeft(15,' ')+"|");
                Console.Write(Math.Round(prestacao.ValorJuros,2).ToString("C2").PadLeft(15,' ')+"|");
                Console.Write(Math.Round(prestacao.ValorAmortizacao,2).ToString("C2").PadLeft(15,' ')+"|");
                Console.Write(Math.Round(prestacao.ValorTotalPrestacao,2).ToString("C2").PadLeft(15,' ')+"|");
                Console.WriteLine();
            }
            Console.WriteLine("-------------------------------------------------------");
            Console.WriteLine("Valor total Pago em Juros       :"+Math.Round(contrato.Prestacoes.Sum(c=>c.ValorJuros),2).ToString("C2"));
            Console.WriteLine("Valor total Pago em Amortizações:"+Math.Round(contrato.Prestacoes.Sum(c=>c.ValorAmortizacao),2).ToString("C2"));
            Console.WriteLine("Valor total Pago no Geral       :"+Math.Round(contrato.Prestacoes.Sum(c=>c.ValorTotalPrestacao),2).ToString("C2"));
        }
    }
}
