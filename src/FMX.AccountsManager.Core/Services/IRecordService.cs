using System.Collections.Generic;

namespace FMX.AccountsManager.Core
{
    /// <summary>
    /// Service for managing account records
    /// </summary>
    public interface IRecordService
    {
        #region All Records Managing

        /// <summary>
        /// Update account records
        /// </summary>
        /// <param name="records">Account records collection</param>
        void UpdateAllRecords(IEnumerable<AccountRecordViewModel> records);

        /// <summary>
        /// Get all account records
        /// </summary>
        /// <returns>Accounts collection</returns>
        IEnumerable<AccountRecordViewModel> GetAllRecords();

        /// <summary>
        /// Filters records by <paramref name="filter"/>
        /// </summary>
        /// <param name="filter">Filter string</param>
        /// <returns>Accounts collection</returns>
        IEnumerable<AccountRecordViewModel> FilterRecords(string filter);

        /// <summary>
        /// Clears all records data from device
        /// </summary>
        void ClearAllData();

        #endregion

        #region Records Managing

        /// <summary>
        /// Adds new account record
        /// </summary>
        /// <param name="newLabel">Label of new record</param>
        void AddRecord(string newLabel);

        /// <summary>
        /// Adds new account record
        /// </summary>
        /// <param name="record">View model of new record</param>
        void AddRecord(AccountRecordViewModel record);

        /// <summary>
        /// Removes record with current label
        /// </summary>
        /// <param name="recordLabel"></param>
        void DeleteRecord(string recordLabel);

        /// <summary>
        /// Updates label of record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of record</param>
        /// <param name="newLabel">New label for record</param>
        void UpdateRecordLabel(string recordLabel, string newLabel);

        #endregion

        #region Record Fields Managing

        /// <summary>
        /// Adds new field for account record
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="newRecordField">New label for new field</param>
        void AddRecordField(string recordLabel, string newRecordField);

        /// <summary>
        /// Removes field with <paramref name="fieldLabel"/> from record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="fieldLabel">Label of field of account record</param>
        void DeleteRecordField(string recordLabel, string fieldLabel);

        /// <summary>
        /// Updates label of record field with <paramref name="fieldLabel"/> label of record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="fieldLabel">Label of field of account record</param>
        /// <param name="newFieldLabel">New label for field of account record</param>
        void UpdateRecordFieldLabel(string recordLabel, string fieldLabel, string newFieldLabel);

        /// <summary>
        /// Updates value of record field with <paramref name="fieldLabel"/> label of record with <paramref name="recordLabel"/> label
        /// </summary>
        /// <param name="recordLabel">Label of account record</param>
        /// <param name="fieldLabel">Label of field of account record</param>
        /// <param name="newFieldValue">>New value for field of account record</param>
        void UpdateRecordFieldValue(string recordLabel, string fieldLabel, string newFieldValue);

        #endregion
    }
}
