using System;
using EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.ValueObjects
{
    public class Imovel
    {
        public String ID{get;private set;}

        public String NumeroCadastroPrefeitura{get;private set;}
        
        public Endereco EnderecoImovel{get;private set;}

        public TipoImovel TipoImovel{get;private set;}

        public EstadoImovel EstadoImovel{get;private set;}

        public decimal ValorImovel{get;private set;}

        public Imovel(String numeroCadastroPrefeitura, Endereco endereco, TipoImovel tipoImovel, EstadoImovel estadoImovel, decimal valorImovel)
        {
            ExcecaoDominio.LancarQuando(()=>String.IsNullOrEmpty(numeroCadastroPrefeitura),"Número de Cadastro da prefeitura é obrigatório");
            ExcecaoDominio.LancarQuando(()=>endereco==null,"Endereço é obrigatório");
            ExcecaoDominio.LancarQuando(()=>valorImovel<=0,"Valor do imóvel é obrigatório");

            ID = Guid.NewGuid().ToString();
            NumeroCadastroPrefeitura = numeroCadastroPrefeitura;
            EnderecoImovel = endereco;
            TipoImovel = tipoImovel;
            EstadoImovel = estadoImovel;
            ValorImovel = valorImovel;
        }
    }
}
