using EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces;
using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Infraestrutura.Servicos
{
    public class ServicoEnvioBoleto : IServicoEnvioBoleto
    {
        public void Enviar(IEnumerable<string> enumerable, ContratoFinanciamento contrato)
        {
            throw new NotImplementedException();
        }
    }
}
