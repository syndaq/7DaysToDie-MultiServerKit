using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// Interface for the repository that handles task schedule commands.
    /// </summary>
    public interface ITaskScheduleCommandRepository : IRepository<T_TaskScheduleCommand>
    {
        /// <summary>
        /// Deletes all task schedule commands by task schedule ID.
        /// </summary>
        /// <param name="taskScheduleId"></param>
        /// <returns></returns>
        Task<int> DeleteByTaskScheduleIdAsync(int taskScheduleId);
    }
}