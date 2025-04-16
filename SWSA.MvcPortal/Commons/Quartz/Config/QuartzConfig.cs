namespace SWSA.MvcPortal.Commons.Quartz.Config;


public class QuartzConfig
{
    public SchedulerConfig Scheduler { get; set; }
    public ThreadPoolConfig ThreadPool { get; set; }
    public JobStoreConfig JobStore { get; set; }
    public DataSourceConfig DataSource { get; set; }
}

public class SchedulerConfig
{
    public string InstanceName { get; set; }
    public string InstanceId { get; set; }
}

public class ThreadPoolConfig
{
    public string Type { get; set; }
    public int MaxConcurrency { get; set; }
}

public class JobStoreConfig
{
    public string Type { get; set; }
    public string DriverDelegateType { get; set; }
    public string DataSource { get; set; }
    public string TablePrefix { get; set; }
    public int MisfireThreshold { get; set; }
}

public class DataSourceConfig
{
    public DefaultConfig Default { get; set; }
}

public class DefaultConfig
{
    public string ConnectionString { get; set; }
    public string Provider { get; set; }
}
