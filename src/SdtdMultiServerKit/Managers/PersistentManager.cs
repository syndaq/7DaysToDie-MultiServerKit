using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Managers
{
    internal static class PersistentManager
    {
        public static async Task SaveChatMessage(ChatMessage chatMessage)
        {
            try
            {
                var chatRecordRepository = ModApi.ServiceContainer.Resolve<IChatRecordRepository>();
                await chatRecordRepository.InsertAsync(new T_ChatRecord()
                {
                    CreatedAt = chatMessage.CreatedAt,
                    ChatType = chatMessage.ChatType,
                    PlayerId = chatMessage.PlayerId,
                    EntityId = chatMessage.EntityId,
                    Message = chatMessage.Message,
                    SenderName = chatMessage.SenderName,
                });
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex, "Error in PersistentManager.OnChatMessage");
            }
        }
    }
}
