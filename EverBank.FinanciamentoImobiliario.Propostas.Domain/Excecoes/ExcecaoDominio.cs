using System;

namespace EverBank.FinanciamentoImobiliario.Propostas.Domain.Excecoes
{
    public class ExcecaoDominio:Exception
    {
        public ExcecaoDominio(String msg):base(msg){}

        public static void LancarQuando(Func<Boolean> condicao, String mensagem)
        {
            if(condicao.Invoke())
            {
                throw new ExcecaoDominio(mensagem);
            }
        }
    }
}
