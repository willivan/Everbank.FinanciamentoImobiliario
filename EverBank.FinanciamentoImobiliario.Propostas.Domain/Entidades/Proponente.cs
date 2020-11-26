using System;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.ValueObjects
{
    public class Proponente
    {
        public String ID{get;private set;}

        public String NomeCompleto{get;private set;}

        public String CPF{get;private set;}

        public String Telefone { get; private set; }

        public String Email { get; private set; }

        public DateTime DataNascimento{get;private set;}

        public decimal RendaBruta{get;private set;}

        public String Profissao{get;private set;}

        public decimal Score{get;private set;}

        public Proponente(String nome, String cpf, String telefone, String email, DateTime dataNascimento, decimal rendaBruta, String profissao)
        {
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(nome),"Nome do proponente é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(cpf),"Cpf do proponente é obrigatório");
            ExcecaoDominio.LancarQuando(() => String.IsNullOrEmpty(telefone), "Telefone do proponente é obrigatório");
            ExcecaoDominio.LancarQuando(() => String.IsNullOrEmpty(email), "E-mail do proponente é obrigatório");
            ExcecaoDominio.LancarQuando(()=>DataNascimento == DateTime.MinValue,"Data de Nascimento é obrigatória");
            ExcecaoDominio.LancarQuando(()=>rendaBruta==0,"Renda do proponente é obrigatória");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(profissao),"Profissão do proponente é obrigatória");

            NomeCompleto = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
            DataNascimento = dataNascimento;
            RendaBruta = rendaBruta;
            Profissao = profissao;
        }

        public void DefinirScore(decimal score)
        {
            Score = score;
        }
    }
}
