using EverBank.FinanciamentoImobiliario.Propostas.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces
{
    public interface IPropostaRepositorio
    {
        void Salvar(PropostaDeNegocio proposta);
        PropostaDeNegocio CarregarPorNumeroDaProposta(string numeroDaProposta);
    }
}
