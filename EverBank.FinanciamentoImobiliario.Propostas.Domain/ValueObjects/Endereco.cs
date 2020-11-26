using System;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.ValueObjects
{
    public class Endereco
    {
        public String Logradouro{get;private set;}

        public String Numero{get;private set;}

        public String Complemento{get;private set;}

        public String Bairro{get;private set;}

        public String Cidade{get;private set;}

        public String Estado{get;private set;}

        public String Cep{get;private set;}

        public Endereco(string logradouro, string numero, string complemento, string bairro, string cidade, string estado, string cep)
        {
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(logradouro),"Logradouro é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(numero),"Número é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(bairro),"Bairro é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(cidade),"Cidade é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(estado),"Estado é obrigatório");
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(cep),"CEP é obrigatório");

            Logradouro = logradouro;
            Numero = numero;
            Complemento = complemento;
            Bairro = bairro;
            Cidade = cidade;
            Estado = estado;
            Cep = cep;

        }
    }
}
