using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Service for storing account records in registry of windows
    /// </summary>
    public class RegistryRecordService : IRecordService
    {
        #region Privates

        private readonly IAccountRecordFieldViewModelFactory _fieldVMFacory;
        private readonly IAccountRecordViewModelFactory _recordVMFactory;

        #endregion

        #region Publix

        /// <summary>
        /// Path to data in regitry
        /// </summary>
        public const string REGISTRY_PATH = @"SOFTWARE\FMX\AccountsManager";

        /// <summary>
        /// Default value for account record field
        /// </summary>
        public const string DEFAULT_VALUE = "";

        /// <summary>
        /// HKEY for registry path
        /// </summary>
        public static RegistryKey RegistryScope => Registry.CurrentUser;

        /// <summary>
        /// Get path to account record in registry by label
        /// </summary>
        /// <param name="label">Account label</param>
        /// <returns>Path to account record in registry</returns>
        public static string GetRecordPath(string label) => REGISTRY_PATH + '\\' + label;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public RegistryRecordService(IAccountRecordFieldViewModelFactory fieldVMFacory,
                                     IAccountRecordViewModelFactory recordVMFactory)
        {
            _fieldVMFacory = fieldVMFacory;
            _recordVMFactory = recordVMFactory;

            // Create main registry key if not exists
            using var mainKey = RegistryScope.OpenSubKey(REGISTRY_PATH);
            if (mainKey is null)
                RegistryScope.CreateSubKey(REGISTRY_PATH);
        }

        #endregion

        #region All Records Managing

        /// <summary>
        /// Update records in registry
        /// </summary>
        /// <param name="records">Account records</param>
        public void UpdateAllRecords(IEnumerable<AccountRecordViewModel> records)
        {
            // Find registry key
            using var mainKey = RegistryScope.OpenSubKey(REGISTRY_PATH);

            // Delete if exists
            if (mainKey is not null)
                RegistryScope.DeleteSubKeyTree(REGISTRY_PATH);

            // Create account record key
            using var createdKey = RegistryScope.CreateSubKey(REGISTRY_PATH);

            // Create subkeys for records (fields)
            foreach (var record in records)
            {
                using var recordKey = createdKey.CreateSubKey(record.Label);

                // Set values for subkey
                foreach (var recordField in record.Fields)
                    recordKey.SetValue(recordField.Label, recordField.Value, RegistryValueKind.String);
            }
        }

        /// <summary>
        /// Get all account records
        /// </summary>
        /// <returns>Accounts collection</returns>
        public IEnumerable<AccountRecordViewModel> GetAllRecords()
        {
            var result = new List<AccountRecordViewModel>();
            using var mainKey = RegistryScope.OpenSubKey(REGISTRY_PATH);
            if (mainKey is null) return result;

            // Fill result by names in main key (acoounts)
            var names = mainKey.GetSubKeyNames();
            for (int i = 0; i < mainKey.SubKeyCount; i++)
            {
                // Get registry key
                using RegistryKey recordKey = mainKey.OpenSubKey(names[i])!;

                // Create account record by key name
                result.Add(
                    _recordVMFactory.Create(label:  names[i], 
                                            fields: recordKey.GetValueNames()!
                                                             .Select(n => _fieldVMFacory.Create(label: n, 
                                                                                                value: recordKey.GetValue(n)!.ToString()!)))
                );
            }

            // Return result
            return result;
        }

        /// <summary>
        /// Filters records by <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">Filter string</param>
        /// <returns>Accounts collection</returns>
        public IEnumerable<AccountRecordViewModel> FilterRecords(string filter)
        {
            return GetAllRecords().Where(r => r.Label.Replace(" ", "")
                                                     .ToLower()
                                                     .Contains(filter.Replace(" ", "")
                                                                     .ToLower()));
        }

        /// <summary>
        /// Clears all records data from device
        /// </summary>
        public void ClearAllData()
        {
            // Find registry key
            using var mainKey = RegistryScope.OpenSubKey(REGISTRY_PATH);

            // Delete if exists
            if (mainKey is not null)
                RegistryScope.DeleteSubKeyTree(REGISTRY_PATH);
        }

        #endregion

        #region Records Managing

        /// <summary>
        /// Adds new account record
        /// </summary>
        /// <param name="newLabel">Label of new record</param>
        public void AddRecord(string newLabel)
        {
            // Find registry recordKey
            using var key = RegistryScope.OpenSubKey(REGISTRY_PATH, true);

            // Add subkey for record
            key?.CreateSubKey(newLabel);
        }

        /// <summary>
        /// Removes record with current label
        /// </summary>
        /// <param name="recordLabel"></param>
        public void DeleteRecord(string recordLabel)
        {
            // Find registry recordKey
            using var key = RegistryScope.OpenSubKey(REGISTRY_PATH, true);

            // Remove subkey bound with record
            key?.DeleteSubKey(recordLabel);
        }

        /// <summary>
        /// Updates label of record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of record</param>
        /// <param name="newLabel">New label for record</param>
        public void UpdateRecordLabel(string recordLabel, string newLabel)
        {
            // Find record mainKey (bound registry recordKey)
            var recordKey = RegistryScope.OpenSubKey(GetRecordPath(recordLabel), true);

            if (recordKey is not null)
            {
                // Create new recordKey
                var createdKey = RegistryScope.CreateSubKey(GetRecordPath(newLabel));

                // Copy record fields
                var names = recordKey.GetValueNames();
                for (int i = 0; i < names.Length; i++)
                    createdKey.SetValue(names[i], recordKey.GetValue(names[i]) ?? "");

                // IMPORTANT: It's neccessary to close, because we don't use using here
                recordKey.Close();

                // Delete this recordKey
                using var mainKey = RegistryScope.OpenSubKey(REGISTRY_PATH, true);
                mainKey!.DeleteSubKey(recordLabel);
            }
        }

        #endregion

        #region Record Fields Managing

        /// <summary>
        /// Adds new field for account record
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="newFieldLabel">New label for new field</param>
        public void AddRecordField(string recordLabel, string newFieldLabel)
        {
            using var recordKey = RegistryScope.OpenSubKey(GetRecordPath(recordLabel), true);
            recordKey?.SetValue(newFieldLabel, DEFAULT_VALUE, RegistryValueKind.String);
        }

        /// <summary>
        /// Removes field with <paramref name="fieldLabel"/> from record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="fieldLabel">Label of field of account record</param>
        public void DeleteRecordField(string recordLabel, string fieldLabel)
        {
            using var recordKey = RegistryScope.OpenSubKey(GetRecordPath(recordLabel), true);
            recordKey?.DeleteValue(fieldLabel);
        }

        /// <summary>
        /// Updates value of record field with <paramref name="fieldLabel"/> label of record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="fieldLabel">Label of field of account record</param>
        /// <param name="newFieldValue">>New value for field of account record</param>
        public void UpdateRecordFieldValue(string recordLabel, string fieldLabel, string newFieldValue)
        {
            using var recordKey = RegistryScope.OpenSubKey(GetRecordPath(recordLabel), true);
            recordKey?.SetValue(fieldLabel, newFieldValue, RegistryValueKind.String);
        }

        /// <summary>
        /// Updates label of record field with <paramref name="fieldLabel"/> label of record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="fieldLabel">Label of field of account record</param>
        /// <param name="newFieldLabel">New label for field of account record</param>
        public void UpdateRecordFieldLabel(string recordLabel, string fieldLabel, string newFieldLabel)
        {
            using var recordKey = RegistryScope.OpenSubKey(GetRecordPath(recordLabel), true);
            if (recordKey is not null)
            {
                recordKey.SetValue(newFieldLabel, recordKey.GetValue(fieldLabel)!, RegistryValueKind.String);
                recordKey.DeleteValue(fieldLabel);
            }
        }

        #endregion
    }
}
