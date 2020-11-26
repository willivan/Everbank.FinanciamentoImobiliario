using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces
{
    public interface IServicoEnvioBoleto
    {
        void Enviar(IEnumerable<string> enumerable, ContratoFinanciamento contrato);
    }
}
