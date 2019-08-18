using System.IO;
using System.Text.RegularExpressions;
public static class StringHelpers
{
    public static string ToSeoUrl(this string url)
    {
        // make the url lowercase
        string encodedUrl = (url ?? "").ToLower();

        // replace & with and
        encodedUrl = Regex.Replace(encodedUrl, @"\&+", "and");

        // remove characters
        encodedUrl = encodedUrl.Replace("'", "");

        encodedUrl = RemoverAcentos(encodedUrl);

        // remove invalid characters
        encodedUrl = Regex.Replace(encodedUrl, @"[^a-z0-9]", "-");

        // remove duplicates
        encodedUrl = Regex.Replace(encodedUrl, @"-+", "-");

        // trim leading & trailing characters
        encodedUrl = encodedUrl.Trim('-');

        return encodedUrl;
    }

    public static string ToSeoImage(this string url)
    {
        FileInfo info = new FileInfo(url);
        string extension = info.Extension;
        string nome = info.Name;
        return nome.ToSeoUrl() + extension;
    }

    public static string CortarTexto(this string text, int quantidade)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        int lenght = text.Length;
        if (lenght < quantidade)
            return text;
        return text.Substring(0, quantidade);
    }

    public static string CortarTextoLimpo(this string text, int quantidade)
    {
        if (string.IsNullOrEmpty(text))
            return text;
        text = text.Strip();
        int lenght = text.Length;
        if (lenght < quantidade)
            return text;
        return text.Substring(0, quantidade);
    }

    public static string Strip(this string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;
            
        return Regex.Replace(text, @"<(.|\n)*?>", string.Empty);
    }

    public static string RemoverAcentos(string input)
    {
        if (string.IsNullOrEmpty(input))
            return "";
        else
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(input);
            return System.Text.Encoding.UTF8.GetString(bytes);
        }
    }
}