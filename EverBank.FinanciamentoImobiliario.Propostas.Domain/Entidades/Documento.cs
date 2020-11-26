using System;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.ValueObjects;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades
{
    public class Documento
    {
        public String ID { get; set; }

        public String Nome{get;private set;}

        public String Descricao{get;private set;}

        public StatusDocumento StatusDocumento{get;private set;}

        public String CaminhoArquivo{get;private set;}

        public String JustificativaReprovacaoDocumento{get;set;}

        public Documento(String nome)
        {
            ID = Guid.NewGuid().ToString();
            Nome = nome;
            StatusDocumento = StatusDocumento.Pendente;
            CaminhoArquivo = String.Empty;
            JustificativaReprovacaoDocumento = String.Empty;
        }

        public void AprovarDocumento()
        {
            StatusDocumento = StatusDocumento.Aprovado;
            JustificativaReprovacaoDocumento = String.Empty;
        }

        public void ReprovarDocumento(String justificativa)
        {
            JustificativaReprovacaoDocumento = justificativa;
            StatusDocumento = StatusDocumento.Reprovado;
        }

        public void AnexarArquivo(String caminhoArquivo)
        {
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(caminhoArquivo),"Caminho do arquivo é obrigatório");
            CaminhoArquivo = caminhoArquivo;
            StatusDocumento = StatusDocumento.Anexo;
            JustificativaReprovacaoDocumento = String.Empty;
        }

        
    }
}
