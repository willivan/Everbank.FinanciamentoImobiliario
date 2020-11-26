using System;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Enums;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.ObjetosDeValor;

namespace EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades
{
    public class Imovel
    {
        public Endereco EnderecoImovel{get;private set;}

        public TipoImovel TipoImovel{get;private set;}

        public EstadoImovel EstadoImovel{get;private set;}

        public decimal ValorImovel{get;private set;}

        public Imovel(Endereco endereco, TipoImovel tipoImovel, EstadoImovel estadoImovel, decimal valorImovel)
        {
            EnderecoImovel = endereco;
            TipoImovel = tipoImovel;
            EstadoImovel = estadoImovel;
            ValorImovel = valorImovel;
        }
    }
}
