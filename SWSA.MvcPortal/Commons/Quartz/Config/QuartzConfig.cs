namespace SWSA.MvcPortal.Commons.Quartz.Config;

public class QuartzConfig
{
    /// <summary>
    /// Quartz 调度器核心配置（实例名称和 ID）。
    /// </summary>
    public SchedulerConfig Scheduler { get; set; }

    /// <summary>
    /// 线程池配置，用于控制最大并发执行线程数。
    /// </summary>
    public ThreadPoolConfig ThreadPool { get; set; }

    /// <summary>
    /// 作业持久化配置（包括事务型存储、集群设置等）。
    /// </summary>
    public JobStoreConfig JobStore { get; set; }

    /// <summary>
    /// 数据库连接配置。
    /// </summary>
    public DataSourceConfig DataSource { get; set; }

    /// <summary>
    /// 序列化器配置，常用 json。
    /// </summary>
    public SerializerConfig Serializer { get; set; }
}

public class SchedulerConfig
{
    /// <summary>
    /// 实例名称（用于日志和集群标识）。
    /// </summary>
    public string InstanceName { get; set; }

    /// <summary>
    /// 实例 ID，"AUTO" 表示自动生成。
    /// </summary>
    public string InstanceId { get; set; }
    /// <summary>
    /// 等待时间（毫秒），当没有 Trigger 即将触发时的轮询间隔
    /// </summary>
    public int IdleWaitTime { get; set; }

}

public class ThreadPoolConfig
{
    /// <summary>
    /// 使用的线程池类型（推荐 SimpleThreadPool）。
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 最大并发线程数，建议为 CPU 核心数或略高。
    /// </summary>
    public int MaxConcurrency { get; set; }
}

public class JobStoreConfig
{
    /// <summary>
    /// Job 持久化类型（推荐 JobStoreTX）。
    /// </summary>
    public string Type { get; set; }

    /// <summary>
    /// 数据库委托类型（需与数据库类型匹配）。
    /// </summary>
    public string DriverDelegateType { get; set; }

    /// <summary>
    /// 使用的数据源名称。
    /// </summary>
    public string DataSource { get; set; }

    /// <summary>
    /// Quartz 表的前缀（默认 QRTZ_）。
    /// </summary>
    public string TablePrefix { get; set; }

    /// <summary>
    /// Misfire 判定阈值（毫秒），常设为 60000。
    /// </summary>
    public int MisfireThreshold { get; set; }

    /// <summary>
    /// 是否启用集群模式（多节点部署必须为 true）。
    /// </summary>
    public bool Clustered { get; set; }


    /// <summary>
    /// 是否在加锁范围内获取 Trigger（建议开启）。
    /// </summary>
    public bool AcquireTriggersWithinLock { get; set; }
}

public class DataSourceConfig
{
    public DefaultConfig Default { get; set; }
}

public class DefaultConfig
{
    /// <summary>
    /// 数据库连接字符串。
    /// </summary>
    public string ConnectionString { get; set; }

    /// <summary>
    /// 数据库提供者（如 SqlServer、MySql）。
    /// </summary>
    public string Provider { get; set; }
}

public class SerializerConfig
{
    /// <summary>
    /// Quartz 序列化类型，推荐 json。
    /// </summary>
    public string Type { get; set; }
}