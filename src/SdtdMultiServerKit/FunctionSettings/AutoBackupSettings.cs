namespace SdtdMultiServerKit.FunctionSettings
{
    /// <summary>
    /// BackupSet
    /// </summary>
    public class AutoBackupSettings : SettingsBase
    {
        /// <summary>
        /// Backupinterval
        /// </summary>
        public int Interval { get; set; }

        /// <summary>
        /// Maximum number of files to retain
        /// </summary>
        public int RetainedFileCountLimit { get; set; }

        /// <summary>
        /// Whether to reset timer after manual backup
        /// </summary>
        public bool ResetIntervalAfterManualBackup { get; set; }

        /// <summary>
        /// Whether to skip backup when no players are online
        /// </summary>
        public bool SkipIfThereAreNoPlayers { get; set; }

        /// <summary>
        /// Whether to auto-backup on server startup
        /// </summary>
        public bool AutoBackupOnServerStartup { get; set; }

        /// <summary>
        /// Backupfile
        /// </summary>
        public required string ArchiveFolder { get; set; }
    }
}