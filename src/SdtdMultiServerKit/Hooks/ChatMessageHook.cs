using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.Functions;
using SdtdMultiServerKit.Managers;
using System.Collections.Immutable;

namespace SdtdMultiServerKit.Hooks
{
    internal delegate Task<bool> ChatHook(string cmd, ManagedPlayer managedPlayer);
    /// <summary>
    /// Static class representing chat message hooks。
    /// </summary>
    internal static class ChatMessageHook
    {
        private static ImmutableList<ChatHook> _chatHooks = ImmutableList<ChatHook>.Empty;
        private static readonly Cache _cache;

        static ChatMessageHook()
        {
            _cache = new Cache();
        }

        /// <summary>
        /// Add chat hook。
        /// </summary>
        /// <param name="chatHook">Add Chat hook。</param>
        public static void AddHook(ChatHook chatHook)
        {
            _chatHooks = _chatHooks.Add(chatHook);
        }

        /// <summary>
        /// Remove chat hook。
        /// </summary>
        /// <param name="chatHook">Chat hook to remove。</param>
        public static void RemoveHook(ChatHook chatHook)
        {
            _chatHooks = _chatHooks.Remove(chatHook);
        }

        /// <summary>
        /// Handle chat command。
        /// </summary>
        /// <param name="chatHook">Chat hook to process。</param>
        /// <param name="cmd">Chat command。</param>
        /// <param name="managedPlayer">Online player。</param>
        /// <returns>Task indicating whether the chat command was handled。</returns>
        private static async Task<bool> HandleChatCmd(ChatHook chatHook, string cmd, ManagedPlayer managedPlayer)
        {
            try
            {
                return await chatHook.Invoke(cmd, managedPlayer);
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex, "Error in ChatMessageHook.HandleChatMessage");

                Utilities.Utils.SendPrivateMessage(new PrivateMessage()
                {
                    Message = ConfigManager.GlobalSettings.HandleChatMessageError,
                    //SenderName = ConfigManager.GlobalSettings.ServerName,
                    TargetPlayerIdOrName = managedPlayer.PlayerId,
                });

                return false;
            }
        }

        /// <summary>
        /// Handle chat message。
        /// </summary>
        /// <param name="chatMessage">Chat message。</param>
        internal static async Task OnChatMessage(ChatMessage chatMessage)
        {
            try
            {
                string? playerId = chatMessage.PlayerId;
                if (playerId != null
                    && chatMessage.ChatType == ChatType.Global
                    && LivePlayerManager.TryGetByPlayerId(playerId, out var player) 
                    && player != null)
                {
                    string cmd = chatMessage.Message;
                    string chatPrefix = ConfigManager.GlobalSettings.ChatCommandPrefix;

                    if (string.IsNullOrEmpty(chatPrefix) == false)
                    {
                        if (cmd.StartsWith(chatPrefix) == false)
                        {
                            return;
                        }
                        else
                        {
                            cmd = cmd.Substring(chatPrefix.Length);
                        }
                    }

                    var chatHook = _cache.Get(cmd);
                    if (chatHook != null && chatHook.Target is IFunction function && function.IsRunning)
                    {
                        bool isHandled = await HandleChatCmd(chatHook, cmd, player);

                        if (isHandled)
                        {
                            return;
                        }
                        else
                        {
                            _cache.Remove(cmd);
                        }
                    }

                    foreach (var hook in _chatHooks)
                    {
                        if (hook == chatHook)
                        {
                            continue;
                        }

                        bool isHandled = await HandleChatCmd(hook, cmd, player);
                        // If the command was handled by another feature
                        if (isHandled && cmd.Length <= 8)
                        {
                            _cache.Set(cmd, hook);
                            return;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CustomLogger.Error(ex, "Error in ChatMessageHook.OnChatMessage");

                Utilities.Utils.SendPrivateMessage(new PrivateMessage()
                {
                    Message = ConfigManager.GlobalSettings.HandleChatMessageError,
                    TargetPlayerIdOrName = chatMessage.PlayerId!,
                });
            }
        }

        /// <summary>
        /// Cache for chat hooks。
        /// </summary>
        class Cache
        {
            private const int CacheSize = 64;
            private readonly (string? Message, ChatHook? ChatHook)[] _cache;
            public Cache()
            {
                _cache = new (string? Message, ChatHook? ChatHook)[CacheSize];
            }

            /// <summary>
            /// Get chat hook for the specified message。
            /// </summary>
            /// <param name="message">Message。</param>
            /// <returns>Chat hook for the specified message，Ifnotinreturn null。</returns>
            public ChatHook? Get(string message)
            {
                for (int i = 0; i < CacheSize; i++)
                {
                    if (_cache[i].Message == message)
                    {
                        return _cache[i].ChatHook;
                    }
                }

                return null;
            }

            /// <summary>
            /// Remove chat hook for the specified message。
            /// </summary>
            /// <param name="message">Message。</param>
            public void Remove(string message)
            {
                for (int i = 0; i < CacheSize; i++)
                {
                    if (_cache[i].Message == message)
                    {
                        _cache[i] = default;
                        return;
                    }
                }
            }

            /// <summary>
            /// Set chat hook for the specified message。
            /// </summary>
            /// <param name="message">Message。</param>
            /// <param name="chatHook">Chat hook。</param>
            public void Set(string message, ChatHook chatHook)
            {
                for (int i = 0; i < CacheSize; i++)
                {
                    if (_cache[i].Message == null)
                    {
                        _cache[i] = (message, chatHook);
                        return;
                    }
                }
            }
        }
    }
}