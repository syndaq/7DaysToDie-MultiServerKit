using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;
using static ModEvents;

namespace SdtdMultiServerKit.Functions
{
    public class MuteCommands : FunctionBase<MuteCommandSettings>
    {
        protected override void OnSettingsChanged()
        {
            if (Settings.IsEnabled)
            {
                ModEvents.ChatMessage.RegisterHandler(OnChatMessage);
            }
            else
            {
                ModEvents.ChatMessage.UnregisterHandler(OnChatMessage);
            }
        }

        private EModEventResult OnChatMessage(ref SChatMessageData sChatMessageData)
        {
            if (!Settings.IsEnabled || Settings.MutedCommands == null || Settings.MutedCommands.Length == 0)
            {
                return EModEventResult.Continue;
            }

            string message = sChatMessageData.Message?.Trim() ?? string.Empty;
            if (string.IsNullOrEmpty(message))
            {
                return EModEventResult.Continue;
            }

            string normalized = message;
            string prefix = ConfigManager.GlobalSettings.ChatCommandPrefix;
            if (string.IsNullOrEmpty(prefix) == false
                && normalized.StartsWith(prefix, StringComparison.OrdinalIgnoreCase))
            {
                normalized = normalized.Substring(prefix.Length).TrimStart();
            }

            foreach (var mutedCommand in Settings.MutedCommands)
            {
                if (string.IsNullOrWhiteSpace(mutedCommand))
                {
                    continue;
                }

                if (string.Equals(normalized, mutedCommand, StringComparison.OrdinalIgnoreCase)
                    || normalized.StartsWith(mutedCommand + ConfigManager.GlobalSettings.ChatCommandSeparator, StringComparison.OrdinalIgnoreCase))
                {
                    if (sChatMessageData.SenderEntityId > 0
                        && LivePlayerManager.TryGetByEntityId(sChatMessageData.SenderEntityId, out var managedPlayer)
                        && managedPlayer != null
                        && string.IsNullOrEmpty(Settings.MutedTip) == false)
                    {
                        SendMessageToPlayer(managedPlayer.PlayerId, Settings.MutedTip);
                    }

                    return EModEventResult.StopHandlersAndVanilla;
                }
            }

            return EModEventResult.Continue;
        }
    }
}
