using System;
using System.Collections.Generic;
using System.Text;

namespace EverBank.FinanciamentoImobiliario.Propostas.Aplicacao.Interfaces
{
    public interface IRepositorioArquivos
    {
        string Salvar(string nomeArquivo, string extensaoArquivo, byte[] arquivo);
    }
}
