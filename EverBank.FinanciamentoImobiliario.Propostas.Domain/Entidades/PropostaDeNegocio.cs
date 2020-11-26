using System;
using System.Collections.Generic;
using System.Linq;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Enums;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.ValueObjects;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades
{
    public class PropostaDeNegocio
    {
        public String ID{get;private set;}

        public DateTime DataProposta{get;private set;}

        public List<Proponente> Proponentes{get;private set;}

        public Imovel Imovel{get;private set;}

        public decimal ValorFinanciamento{get;private set;}

        public decimal ValorEntrada{get;private set;}

        public int PrazoFinanciamento{get;private set;}

        public decimal JurosAnuais{get;private set;}

        public StatusProposta StatusProposta {get;private set;}

        public SistemaAmortizacao SistemaAmortizacao{get;private set;}

        public List<Documento> DocumentosObrigatorios{get;private set;}

        public String JustificativaRecusaProposta{get;private set;}


        public PropostaDeNegocio(List<Proponente> proponentes, Imovel imovel, decimal valorEntrada, decimal valorFinanciamento, int prazoFinanciamento, decimal jurosAnuais, SistemaAmortizacao sistemaAmortizacao)
        {
            ExcecaoDominio.LancarQuando(()=>proponentes == null || proponentes.Count==0,"Proponentes são obrigatórios");
            ExcecaoDominio.LancarQuando(()=>imovel == null,"Imóvel é obrigatório");
            ExcecaoDominio.LancarQuando(()=>valorFinanciamento<30000,"Valor mínimo para financiamento é R$ 30.000,00");
            ExcecaoDominio.LancarQuando(()=>ValidarEntradaMinima(valorEntrada,valorFinanciamento),"Entrada deve ser pelo menos 20% do valor a ser financiado");
            ExcecaoDominio.LancarQuando(()=>prazoFinanciamento>12 && prazoFinanciamento < 360,"Prazo para financiamento deve ser entre 12 e 360 meses");

            Proponentes = proponentes;
            Imovel = imovel;
            ValorEntrada = valorEntrada;
            ValorFinanciamento = valorFinanciamento;
            PrazoFinanciamento = prazoFinanciamento;
            JurosAnuais = jurosAnuais;
            SistemaAmortizacao = sistemaAmortizacao;

            DataProposta = DateTime.Now;
            ID = Guid.NewGuid().ToString();
            StatusProposta = StatusProposta.AguardandoDocumentacao;
            DocumentosObrigatorios = new List<Documento>();
        }

        private bool ValidarEntradaMinima(decimal valorEntrada, decimal valorFinanciamento)
        {
            return (valorEntrada/valorFinanciamento)>0.2M;
        }

        public void AnexarDocumento(String nomeDocumento, String caminhoArquivo)
        {
            Documento doc = DocumentosObrigatorios.FirstOrDefault(c => c.Nome == nomeDocumento);
            ExcecaoDominio.LancarQuando(() => doc == null, "Documento inválido");

            doc.AnexarArquivo(caminhoArquivo);
            if (DocumentosObrigatorios.Any(c => c.StatusDocumento != StatusDocumento.Anexo))
            {
                StatusProposta = StatusProposta.AguardandoDocumentacao;
            }
            else
            {
                StatusProposta = StatusProposta.EmAnalise;
            }
        }

        public void AprovarDocumentacao(string documento)
        {
            Documento doc = DocumentosObrigatorios.FirstOrDefault(c => c.Nome == documento);
            ExcecaoDominio.LancarQuando(() => doc == null, "Documento inválido");
            doc.AprovarDocumento();
        }

        public void ReprovarDocumentacao(string documento, String justificativa)
        {
            Documento doc = DocumentosObrigatorios.FirstOrDefault(c => c.Nome == documento);
            ExcecaoDominio.LancarQuando(() => doc == null, "Documento inválido");
            doc.ReprovarDocumento(justificativa);
        }

        public void AceitarProposta()
        {
            ExcecaoDominio.LancarQuando(() => DocumentosObrigatorios.Any(c => c.StatusDocumento != StatusDocumento.Aprovado), "Proposta não pode ser aceita enquanto houver documentos pendentes de aprovação");
            StatusProposta = StatusProposta.Aprovada;
        }

        public void RecusarProposta(String justificativa)
        {
            StatusProposta = StatusProposta.Reprovada;
            JustificativaRecusaProposta = justificativa;
        }
    }
}
