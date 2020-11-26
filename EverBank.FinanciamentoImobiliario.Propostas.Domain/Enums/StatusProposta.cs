using System;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.Enums
{
    public enum StatusProposta:int
    {
        Criada=1,
        AguardandoDocumentacao=2,
        EmAnalise=2,
        Aprovada=3,
        Reprovada=4
    }
}
