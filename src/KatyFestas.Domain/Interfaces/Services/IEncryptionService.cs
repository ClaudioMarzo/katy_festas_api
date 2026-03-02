namespace KatyFestas.Domain.Interfaces.Services;

public interface IEncryptionService
{
    /// <summary>
    /// Criptografa um texto plano.
    /// </summary>
    string Encrypt(string plainText);

    /// <summary>
    /// Descriptografa um texto cifrado.
    /// </summary>
    string Decrypt(string cipherText);
}
