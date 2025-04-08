namespace SWSA.MvcPortal.Commons.Helpers;

public class PasswordHasher
{
    /// <summary>
    /// 生成加密后的密码（BCrypt）
    /// </summary>
    public static string Hash(string plainPassword)
    {
        return BCrypt.Net.BCrypt.HashPassword(plainPassword);
    }

    /// <summary>
    /// 验证用户输入密码是否匹配数据库中的加密密码
    /// </summary>
    public static bool Verify(string plainPassword, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.Verify(plainPassword, hashedPassword);
    }

    /// <summary>
    /// 判断旧的加密值是否需要重新哈希（用于安全升级）
    /// </summary>
    public static bool NeedsRehash(string hashedPassword, int workFactor = 10)
    {
        return BCrypt.Net.BCrypt.PasswordNeedsRehash(hashedPassword, workFactor);
    }
}
