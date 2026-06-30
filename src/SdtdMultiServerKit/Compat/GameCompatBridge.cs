using System.Reflection;
using UnityEngine;

namespace SdtdMultiServerKit.Compat
{
    /// <summary>
    /// Reflection-based access to game types that changed between v2.6 (lowercase fields)
    /// and v3.0+ (publicized PascalCase properties).
    /// </summary>
    internal static class GameCompatBridge
    {
        private static readonly Func<BlockChangeInfo, Vector3i> GetBlockChangePos = CreateInstanceGetter<BlockChangeInfo, Vector3i>("pos", "Pos");
        private static readonly Func<BlockChangeInfo, int> GetBlockChangeClrIdx = CreateInstanceGetter<BlockChangeInfo, int>("clrIdx", "ClrIdx");
        private static readonly Func<BlockChangeInfo, BlockValue> GetBlockChangeBlockValue = CreateInstanceGetter<BlockChangeInfo, BlockValue>("blockValue", "BlockValue");
        private static readonly Action<BlockChangeInfo, BlockValue> SetBlockChangeBlockValueImpl = CreateInstanceSetter<BlockChangeInfo, BlockValue>("blockValue", "BlockValue");

        private static readonly ConstructorInfo? BlockChangeCtor4 = typeof(BlockChangeInfo).GetConstructor(
            new[] { typeof(Vector3i), typeof(BlockValue), typeof(bool), typeof(bool) });

        private static readonly ConstructorInfo? BlockChangeCtor3 = typeof(BlockChangeInfo).GetConstructor(
            new[] { typeof(int), typeof(Vector3i), typeof(BlockValue) });

        private static readonly Func<Explosion, int> GetExplosionClrIdx = CreateInstanceGetter<Explosion, int>("clrIdx", "ClrIdx");

        private static readonly Func<Dictionary<string, string[]>> GetLocalizationDictionary = CreateStaticGetter<Dictionary<string, string[]>>(
            typeof(Localization),
            "dictionary",
            "Dictionary",
            "mDictionary");

        private static readonly Func<Dictionary<string, string[]>> GetLocalizationDictionaryCaseInsensitive = CreateStaticGetter<Dictionary<string, string[]>>(
            typeof(Localization),
            "mDictionaryCaseInsensitive",
            "MDictionaryCaseInsensitive");

        private static readonly Func<EntityCreationData, Vector3> GetEntityCreationPos = CreateInstanceGetter<EntityCreationData, Vector3>("pos", "Pos");
        private static readonly Action<EntityCreationData, Vector3> SetEntityCreationPosImpl = CreateInstanceSetter<EntityCreationData, Vector3>("pos", "Pos");
        private static readonly Func<EntityCreationData, Vector3> GetEntityCreationRot = CreateInstanceGetter<EntityCreationData, Vector3>("rot", "Rot");
        private static readonly Func<EntityCreationData, EntityStats?> GetEntityCreationStats = CreateInstanceGetter<EntityCreationData, EntityStats?>("stats", "Stats");

        public static Vector3i BlockChangePos(BlockChangeInfo info) => GetBlockChangePos(info);

        public static int BlockChangeClrIdx(BlockChangeInfo info) => GetBlockChangeClrIdx(info);

        public static BlockValue BlockChangeBlockValue(BlockChangeInfo info) => GetBlockChangeBlockValue(info);

        public static void SetBlockChangeBlockValue(BlockChangeInfo info, BlockValue value) => SetBlockChangeBlockValueImpl(info, value);

        public static BlockChangeInfo CreateBlockChange(Vector3i pos, BlockValue blockValue, bool updateLight = true, bool playSound = false)
        {
            if (BlockChangeCtor4 == null)
            {
                throw new MissingMethodException(typeof(BlockChangeInfo).FullName, "ctor(Vector3i, BlockValue, bool, bool)");
            }

            return (BlockChangeInfo)BlockChangeCtor4.Invoke(new object[] { pos, blockValue, updateLight, playSound });
        }

        /// <summary>
        /// Builds a client sync entry that restores the block currently in the world at the change position.
        /// </summary>
        public static BlockChangeInfo CreateRestoreBlockChange(BlockChangeInfo source, BlockValue currentBlock)
        {
            if (BlockChangeCtor3 != null)
            {
                return (BlockChangeInfo)BlockChangeCtor3.Invoke(new object[]
                {
                    BlockChangeClrIdx(source),
                    BlockChangePos(source),
                    currentBlock,
                });
            }

            return CreateBlockChange(BlockChangePos(source), currentBlock, true, false);
        }

        public static BlockChangeInfo CreateRestoreBlockChange(Vector3i pos, BlockValue currentBlock, Explosion explosion)
        {
            if (BlockChangeCtor3 != null)
            {
                return (BlockChangeInfo)BlockChangeCtor3.Invoke(new object[]
                {
                    GetExplosionClrIdx(explosion),
                    pos,
                    currentBlock,
                });
            }

            return CreateBlockChange(pos, currentBlock, true, false);
        }

        public static Dictionary<string, string[]> LocalizationDictionary => GetLocalizationDictionary();

        public static Dictionary<string, string[]> LocalizationDictionaryCaseInsensitive => GetLocalizationDictionaryCaseInsensitive();

        public static string[] KnownLanguages => GetLocalizationDictionary()["KEY"];

        public static Vector3 EntityCreationPos(EntityCreationData ecd) => GetEntityCreationPos(ecd);

        public static void SetEntityCreationPos(EntityCreationData ecd, Vector3 pos) => SetEntityCreationPosImpl(ecd, pos);

        public static Vector3 EntityCreationRot(EntityCreationData ecd) => GetEntityCreationRot(ecd);

        public static EntityStats? EntityCreationStats(EntityCreationData ecd) => GetEntityCreationStats(ecd);

        private static Func<TInstance, TResult> CreateInstanceGetter<TInstance, TResult>(params string[] memberNames)
        {
            var member = ResolveInstanceMember(typeof(TInstance), memberNames);
            return instance => (TResult)member.GetValue(instance)!;
        }

        private static Action<TInstance, TValue> CreateInstanceSetter<TInstance, TValue>(params string[] memberNames)
        {
            var member = ResolveInstanceMember(typeof(TInstance), memberNames);
            return (instance, value) => member.SetValue(instance, value);
        }

        private static Func<TResult> CreateStaticGetter<TResult>(Type type, params string[] memberNames)
        {
            var member = ResolveStaticMember(type, memberNames);
            return () => (TResult)member.GetValue(null)!;
        }

        private static MemberAccessor ResolveInstanceMember(Type type, params string[] memberNames)
        {
            foreach (var name in memberNames)
            {
                var property = type.GetProperty(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (property != null)
                {
                    return MemberAccessor.FromProperty(property);
                }

                var field = type.GetField(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != null)
                {
                    return MemberAccessor.FromField(field);
                }
            }

            throw new MissingMemberException(type.FullName, string.Join("/", memberNames));
        }

        private static MemberAccessor ResolveStaticMember(Type type, params string[] memberNames)
        {
            foreach (var name in memberNames)
            {
                var property = type.GetProperty(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (property != null)
                {
                    return MemberAccessor.FromProperty(property);
                }

                var field = type.GetField(name, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
                if (field != null)
                {
                    return MemberAccessor.FromField(field);
                }
            }

            throw new MissingMemberException(type.FullName, string.Join("/", memberNames));
        }

        private readonly struct MemberAccessor
        {
            private readonly PropertyInfo? _property;
            private readonly FieldInfo? _field;

            private MemberAccessor(PropertyInfo property)
            {
                _property = property;
                _field = null;
            }

            private MemberAccessor(FieldInfo field)
            {
                _property = null;
                _field = field;
            }

            public static MemberAccessor FromProperty(PropertyInfo property) => new(property);

            public static MemberAccessor FromField(FieldInfo field) => new(field);

            public object? GetValue(object? instance)
            {
                if (_property != null)
                {
                    return _property.GetValue(instance);
                }

                return _field!.GetValue(instance);
            }

            public void SetValue(object? instance, object? value)
            {
                if (_property != null)
                {
                    _property.SetValue(instance, value);
                    return;
                }

                _field!.SetValue(instance, value);
            }
        }
    }
}
