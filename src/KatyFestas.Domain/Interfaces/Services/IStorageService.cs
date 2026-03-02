namespace KatyFestas.Domain.Interfaces.Services;

public interface IStorageService
{
    /// <summary>
    /// Faz upload de um arquivo e retorna a URL pública.
    /// </summary>
    Task<string> UploadAsync(Stream fileStream, string fileName, string contentType);

    /// <summary>
    /// Remove um arquivo pelo nome.
    /// </summary>
    Task DeleteAsync(string fileName);

    /// <summary>
    /// Extrai o nome do arquivo a partir da URL completa.
    /// </summary>
    string ExtractFileNameFromUrl(string url);
}