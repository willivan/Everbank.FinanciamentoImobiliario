using System;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao
{
    public interface ServicoAplicacao<Request,Response>
    {
        Response Executar(Request request);
    }
}
