using FMX.AccountsManager.Core.Data;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace FMX.AccountsManager
{

    public class BinarySerializator : IBinarySerializator
    {
        #region Constants

        /// <summary>
        /// Extention of files with serialized backup of accounts manager
        /// </summary>
        public const string EXTENTION = ".amg";

        #endregion

        private readonly IFormatter _formatter = new BinaryFormatter();

        public void Serialize(List<AccountRecord> records, string path)
        {
            using FileStream fs = new(path, FileMode.OpenOrCreate);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
            _formatter.Serialize(fs, records);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
        }

        public List<AccountRecord> Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using FileStream fs = new(path, FileMode.Open);
#pragma warning disable SYSLIB0011 // Type or member is obsolete
                return (List<AccountRecord>)_formatter.Deserialize(fs);
#pragma warning restore SYSLIB0011 // Type or member is obsolete
            }
            else throw new System.Exception("File doesn't exist");
        }
    }

}
