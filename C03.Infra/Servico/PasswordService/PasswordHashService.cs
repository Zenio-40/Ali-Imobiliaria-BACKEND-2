using System.Security.Cryptography;

namespace Corretora.C03.Infra.Servico.PasswordService;

public class PasswordHashService : Corretora.C02.Aplication.Servico.IPasswordService.IPasswordHash
{
public Task<(string hash, string salt)> HashAsync(string senha)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(senha, salt, 100000, HashAlgorithmName.SHA512, 64);

return Task.FromResult((Convert.ToBase64String(hash), Convert.ToBase64String(salt)));
    }
}