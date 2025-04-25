using Quartz;
using SWSA.MvcPortal.Commons.Enums;
using SWSA.MvcPortal.Commons.Exceptions;
using SWSA.MvcPortal.Models.ScheduledJobs;

namespace SWSA.MvcPortal.Commons.Helpers;
/// <summary>
/// A utility class for building Quartz-compatible Cron expressions.
/// Use the provided methods for common patterns, or use Build() for custom expressions.
/// </summary>
public static class CronExpressionBuilder
{
    /// <summary>
    /// 尝试从 "HH:mm" 解析 hour 和 minute，默认返回 0, 0
    /// </summary>
    private static (int hour, int minute) ParseTime(string? time)
    {
        if (string.IsNullOrWhiteSpace(time)) return (0, 0);

        var parts = time.Split(":");
        if (parts.Length != 2) return (0, 0);

        bool parsedHour = int.TryParse(parts[0], out int hour);
        bool parsedMinute = int.TryParse(parts[1], out int minute);

        return (parsedHour ? hour : 0, parsedMinute ? minute : 0);
    }

    /// <summary>
    /// 验证是否为合法的 Quartz Cron 表达式（支持 6 或 7 段格式）
    /// </summary>
    public static bool IsValid(string? cron)
    {
        if (string.IsNullOrWhiteSpace(cron))
            return false;

        return CronExpression.IsValidExpression(cron);
    }

    /// <summary>
    /// 每天指定时间执行 24hr
    /// </summary>
    public static string DailyAt(string? time)
    {
        var (hour, minute) = ParseTime(time);
        return $"0 {minute} {hour} ? * *";
    }

    /// <summary>
    /// 每周指定星期几与时间执行（DayOfWeek: 0 = Sunday, 1 = Monday, ..., 6 = Saturday）
    /// </summary>
    public static string WeeklyOn(DayOfWeek dayOfWeek, string? time)
    {
        var (hour, minute) = ParseTime(time);
        int quartzDay = (int)dayOfWeek;
        if (quartzDay == 0) quartzDay = 7; // Sunday in Quartz = 7
        return $"0 {minute} {hour} ? * {quartzDay}";
    }

    /// <summary>
    /// 每月指定日期与时间执行（day: "1" ~ "31"）
    /// </summary>
    public static string MonthlyAt(string? dayOfMonth, string? time)
    {
        var (hour, minute) = ParseTime(time);
        var day = string.IsNullOrWhiteSpace(dayOfMonth) ? "1" : dayOfMonth;
        return $"0 {minute} {hour} {day} * ?";
    }

    public static string BuildCronExpression(CronFields cronFields, string cronExpression, ScheduleType type)
    {
        return type switch
        {
            ScheduleType.Once => string.Empty,
            ScheduleType.Cron => CronExpressionBuilder.IsValid(cronExpression)
                ? cronExpression
                : throw new BusinessLogicException("Invalid Cron Expression"),
            ScheduleType.Daily => CronExpressionBuilder.DailyAt(cronFields.Time),
            ScheduleType.Weekly => cronFields.DayOfWeek is not null
                ? CronExpressionBuilder.WeeklyOn(cronFields.DayOfWeek.Value, cronFields.Time)
                : throw new BusinessLogicException("DayOfWeek is required for weekly schedule."),
            ScheduleType.Monthly => CronExpressionBuilder.MonthlyAt(cronFields.DayOfMonth, cronFields.Time),
            _ => throw new NotSupportedException("Unsupported ScheduleType.")
        };
    }
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
// - weekday: 1~7，7 表示周日，使用 "?" 表示不指定（不能与 day 同时指定）

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