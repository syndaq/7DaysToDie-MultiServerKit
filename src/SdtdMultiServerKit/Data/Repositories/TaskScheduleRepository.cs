using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// Represents a task schedule repository.
    /// </summary>
    public class TaskScheduleRepository : DefaultRepository<T_TaskSchedule>, ITaskScheduleRepository
    {
    }
}