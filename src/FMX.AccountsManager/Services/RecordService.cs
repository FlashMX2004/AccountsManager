using Microsoft.Win32;
using System.Collections.Generic;

namespace FMX.PasswordManager
{
    public class RecordService : IRecordService
    {
        /// <summary>
        /// Update blocks in registry
        /// </summary>
        /// <param name="records">Blocks</param>
        public void UpdateRecords(IEnumerable<AccoundRecordViewModel> records)
        {
            // Find registry key
            using var key = Registry.CurrentUser.OpenSubKey(App.REGISTRY_PATH);

            // Delete if exists
            if (key is not null)
                Registry.CurrentUser.DeleteSubKeyTree(App.REGISTRY_PATH);

            // Create key
            var createdKey = Registry.CurrentUser.CreateSubKey(App.REGISTRY_PATH);

            foreach (var block in records)
            {
                var blockKey = createdKey.CreateSubKey(App.REGISTRY_PATH + '\\' + block.Label);
                
            }
        }
    }
}
