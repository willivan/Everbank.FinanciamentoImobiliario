using EverBank.FinanciamentoImobiliario.Contratos.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Contratos.Aplicacao.Interfaces
{
    public interface IServicoEnvioSMS
    {
        void EnviarMensagem(List<String> telefones, String mensagem);
    }
}
