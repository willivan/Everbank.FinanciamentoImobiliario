using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces
{
    public interface IServicoEnvioEmail
    {
        void Enviar(IEnumerable<string> enumerable, string v1, string v2);
    }
}
