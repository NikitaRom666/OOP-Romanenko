using System;

#region BAD VERSION (порушення SRP)

// ❌ Поганий клас: робить ВСЕ
public class BadAuthService
{
    public void Login(string username, string password)
    {
        Console.WriteLine("Перевірка облікових даних...");
        if (string.IsNullOrWhiteSpace(username) || password.Length < 6)
        {
            Console.WriteLine("Невірні облікові дані");
            return;
        }

        Console.WriteLine("Генерація токена...");
        string token = Guid.NewGuid().ToString();

        Console.WriteLine("Логування...");
        Console.WriteLine($"Користувач {username} увійшов у систему");

        Console.WriteLine($"Успішний вхід. Token: {token}");
    }
}

#endregion

#region INTERFACES (SRP + DIP)

public interface ICredentialValidator
{
    bool Validate(string username, string password);
}

public interface ITokenGenerator
{
    string GenerateToken(string username);
}

public interface ILogger
{
    void Log(string message);
}

#endregion

#region IMPLEMENTATIONS (заглушки)

public class SimpleCredentialValidator : ICredentialValidator
{
    public bool Validate(string username, string password)
    {
        return !string.IsNullOrWhiteSpace(username) && password.Length >= 6;
    }
}

public class SimpleTokenGenerator : ITokenGenerator
{
    public string GenerateToken(string username)
    {
        return Convert.ToBase64String(Guid.NewGuid().ToByteArray());
    }
}

public class ConsoleLogger : ILogger
{
    public void Log(string message)
    {
        Console.WriteLine("[LOG]: " + message);
    }
}

#endregion

#region GOOD VERSION (SRP)

public class AuthService
{
    private readonly ICredentialValidator _validator;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly ILogger _logger;

    public AuthService(
        ICredentialValidator validator,
        ITokenGenerator tokenGenerator,
        ILogger logger)
    {
        _validator = validator;
        _tokenGenerator = tokenGenerator;
        _logger = logger;
    }

    public void Login(string username, string password)
    {
        _logger.Log("Початок входу користувача");

        if (!_validator.Validate(username, password))
        {
            _logger.Log("Помилка валідації");
            Console.WriteLine("Вхід заборонено");
            return;
        }

        string token = _tokenGenerator.GenerateToken(username);
        _logger.Log($"Користувач {username} успішно увійшов");

        Console.WriteLine($"Успішний вхід. Token: {token}");
    }
}

#endregion

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== BAD AuthService ===");
        BadAuthService badAuth = new BadAuthService();
        badAuth.Login("admin", "123456");

        Console.WriteLine("\n=== GOOD AuthService (SRP + DIP) ===");

        ICredentialValidator validator = new SimpleCredentialValidator();
        ITokenGenerator tokenGenerator = new SimpleTokenGenerator();
        ILogger logger = new ConsoleLogger();

        AuthService authService = new AuthService(
            validator,
            tokenGenerator,
            logger);

        authService.Login("admin", "password123");

        Console.ReadLine();
    }
}
