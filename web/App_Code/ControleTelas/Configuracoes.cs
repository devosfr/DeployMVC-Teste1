using System;
using System.Configuration;

/// <summary>
/// Descrição resumida para configuracoes
/// </summary>
public class Configuracoes
{

    #region RETORNA O VALOR DA CONFIGURAÇÃO REQUERIDA
    /// <summary>
    /// Retorna valor correspondente a chave de parametro
    /// </summary>
    /// <param name="key">Chave de referencia do valor a ser retornado</param>
    /// <returns></returns>
    public static String getSetting(String key)
    {

        try
        {
            string chave = ConfigurationManager.AppSettings[key].ToString();
            return chave;
        }
        catch (Exception ex)
        {
            throw new Exception("Chave não encontrada.");
        }
    }


    #endregion

}