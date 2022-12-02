using System.Collections.Generic;

namespace FMX.PasswordManager
{
    public interface IRecordService
    {
        /// <summary>
        /// Update account records
        /// </summary>
        /// <param name="records">Account records collection</param>
        void UpdateRecords(IEnumerable<AccoundRecordViewModel> records);
    }
}
