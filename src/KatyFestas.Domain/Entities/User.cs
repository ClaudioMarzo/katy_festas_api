using KatyFestas.Domain.Exceptions;
using KatyFestas.Domain.Interfaces.Services;

namespace KatyFestas.Domain.Entities;

public class User : BaseEntity
{
    // Propriedades
    public string Name { get; private set; } = string.Empty;
    public string Email { get; private set; } = string.Empty;
    public string PasswordHash { get; private set; } = string.Empty;
    public bool IsActive { get; private set; }

    // Propriedades - Relacionamento
    public Guid StoreId { get; private set; }
    // Navegação
    public Store Store { get; private set; } = null!;

    protected User() { }

    public static User Create(Guid storeId, string name, string email, string passwordHash, IEncryptionService encryptionService)
    {
        return new User
        {
            StoreId = storeId,
            Name = name,
            Email = email.ToLowerInvariant().Trim(),
            PasswordHash = encryptionService.Encrypt(passwordHash),
            IsActive = true
        };
    }

    public bool VerifyPassword(string password, IEncryptionService encryptionService)
    {
        try
        {
            var decryptedPassword = encryptionService.Decrypt(PasswordHash);
            return decryptedPassword == password;
        }
        catch
        {
            return false;
        }
    }

    public void UpdatePassword(string newPasswordHash, IEncryptionService encryptionService)
    {
        PasswordHash = encryptionService.Encrypt(newPasswordHash);
        SetUpdatedAt();
    }

    public void Activate()
    {
        if (DeletedAt.HasValue)
            throw new DomainException("Usuário deletado não pode ser ativado.");
        
        IsActive = true;
        SetUpdatedAt();
    }
    public void Deactivate()
    {
        if (DeletedAt.HasValue)
            throw new DomainException("Usuário deletado não pode ser desativado.");
        
        IsActive = false;
        SetUpdatedAt();
    }

    public void Delete()
    {
        IsActive = false;
        SetDeletedAt();
    }
}