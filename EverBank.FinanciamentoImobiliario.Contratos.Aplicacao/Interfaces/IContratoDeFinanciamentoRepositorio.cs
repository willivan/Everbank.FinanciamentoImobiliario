using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces
{
    public interface IContratoDeFinanciamentoRepositorio
    {
        void Salvar(ContratoFinanciamento contrato);
        ContratoFinanciamento Carregar(string numeroContrato);
    }
}
