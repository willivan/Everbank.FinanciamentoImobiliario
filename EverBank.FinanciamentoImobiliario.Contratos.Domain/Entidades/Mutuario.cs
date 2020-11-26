using System;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Excecoes;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades
{
    public class Mutuario
    {
        public String NomeCompleto{get;private set;}

        public String CPF{get;private set;}

        public DateTime DataNascimento{get;private set;}

        public decimal RendaBruta{get;private set;}

        public String Profissao{get;private set;}

        public String Telefone { get; private set; }

        public String Email { get; private set; }

        public Mutuario(string nomeCompleto, string cPF, string telefone, String email, DateTime dataNascimento, decimal rendaBruta, string profissao)
        {
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(nomeCompleto),"Nome do mutuário é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(cPF),"CPF do mutuário é obrigatório");
            ExcecaoDominio.LancarQuando(() => String.IsNullOrEmpty(telefone), "Telefone do mutuário é obrigatório");
            ExcecaoDominio.LancarQuando(() => String.IsNullOrEmpty(email), "E-mail do mutuário é obrigatório");

            ExcecaoDominio.LancarQuando(()=>rendaBruta<=0,"Renda do mutuário é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(cPF),"Profissáo do mutuário é obrigatório");

            NomeCompleto = nomeCompleto;
            CPF = cPF;
            DataNascimento = dataNascimento;
            RendaBruta = rendaBruta;
            Profissao = profissao;
        }
    }
}
