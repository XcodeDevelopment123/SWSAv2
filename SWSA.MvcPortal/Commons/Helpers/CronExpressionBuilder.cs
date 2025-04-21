using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Entities;

namespace SWSA.MvcPortal.Commons.Helpers;
/// <summary>
/// A utility class for building Quartz-compatible Cron expressions.
/// Use the provided methods for common patterns, or use Build() for custom expressions.
/// </summary>
public static class CronExpressionBuilder
{
    /// <summary>
    /// Build a full 5-field cron expression.
    /// Format: minute hour day month weekday
    /// </summary>
    /// <param name="minute">0-59</param>
    /// <param name="hour">0-23</param>
    /// <param name="day">1-31</param>
    /// <param name="month">1-12</param>
    /// <param name="weekday">0-6 (0=Sunday), use "?" when day is specified</param>
    /// <returns>Formatted cron string</returns>
    public static string Build(
        string minute = "*",
        string hour = "*",
        string day = "*",
        string month = "*",
        string weekday = "*")
    {
        return $"{minute} {hour} {day} {month} {weekday}";
    }

    /// <summary>
    /// 每天指定时间执行
    /// </summary>
    public static string DailyAt(int hour = 9, int minute = 0)
        => $"0 {minute} {hour} ? * *"; // 秒 分 时 日 月 星期

    /// <summary>
    /// 每周指定星期几与时间执行（1=Sunday, 2=Monday, ..., 7=Saturday）
    /// </summary>
    public static string WeeklyOn(int weekday = 2, int hour = 9, int minute = 0)
        => $"0 {minute} {hour} ? * {weekday}";

    /// <summary>
    /// 每月指定日期与时间执行（day: 1~31）
    /// </summary>
    public static string MonthlyAt(int day = 1, int hour = 9, int minute = 0)
        => $"0 {minute} {hour} {day} * ?";

    /// <summary>
    /// 每 X 分钟执行一次
    /// </summary>
    public static string EveryXMinutes(int interval)
        => $"0 0/{interval} * * * ?";

    /// <summary>
    /// 每 X 小时执行一次
    /// </summary>
    public static string EveryXHours(int interval)
        => $"0 0 0/{interval} * * ?";

    /// <summary>
    /// 每小时的固定分钟执行（例如：每小时第15分钟执行）
    /// </summary>
    public static string HourlyAt(int minute = 0)
        => $"0 {minute} * * * ?";

    /// <summary>
    /// 每分钟执行一次
    /// </summary>
    public static string EveryMinute()
        => "0 * * * * ?";

    /// <summary>
    /// Usage Examples:
    /// - CronExpressionBuilder.DailyAt(9, 0) → "0 9 * * ?" (every day 9AM)
    /// - CronExpressionBuilder.WeeklyOn(1, 8) → "0 8 ? * 1" (every Monday 8AM)
    /// - CronExpressionBuilder.Build("0", "12", "1", "*", "?") → every month 1st, 12PM
    /// </summary>
    public static void Examples() { }
}

//┌────────── 秒
// ┌───────── 分钟 (0-59)
// │  ┌────── 小时 (0-23)
// │  │ ┌──── 日期 (1-31)
// │  │ │ ┌── 月份 (1-12)
// │  │ │ │ ┌ 星期 (0=周日 ~ 6=周六)
// │  │ │ │ │
// *  *  *  *  *

// 字段说明：
// - minute: 0~59，例如 "0" 整点，"*/15" 每 15 分钟
// - hour: 0~23，例如 "9" 上午九点，"*/2" 每 2 小时
// - day: 1~31，月中的日期，例如 "1" 每月 1 日
// - month: 1~12，例如 "1,6" 表示一月与六月
// - weekday: 0~6，0 表示周日，使用 "?" 表示不指定（不能与 day 同时指定）

// 特殊符号：
// * 任意值, / 步长, , 多值, - 范围, ? 不指定

// 示例：
// - "0 9 * * ?" 每天 9 点
// - "0 0 1 * ?" 每月 1 日 0 点
// - "0 0 ? * 1" 每周一 0 点
// - "0 0 1 1 ?" 每年元旦
// - "*/5 * * * ?" 每 5 分钟一次

// ✅ 指引：使用 Build(minute, hour, day, month, weekday) 方法自定义组合
// 例：每月 10 号 14:30 → Build("30", "14", "10", "*", "?")
//     每周五上午 8 点 → Build("0", "8", "?", "*", "5")