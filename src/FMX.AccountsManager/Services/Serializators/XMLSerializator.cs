using FMX.AccountsManager.Core.Data;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace FMX.AccountsManager
{
    public class XMLSerializator : IXMLSerializator
    {
        #region Constants

        /// <summary>
        /// Extention of files with serialized backup of accounts manager
        /// </summary>
        public const string EXTENTION = ".amx";

        #endregion

        private XmlSerializer _serializer = new(typeof(List<AccountRecord>));

        public void Serialize(List<AccountRecord> records, string path)
        {
            using FileStream fs = new(path, FileMode.OpenOrCreate);
            _serializer.Serialize(fs, records);
        }

        public List<AccountRecord> Deserialize(string path)
        {
            if (File.Exists(path))
            {
                using FileStream fs = new(path, FileMode.Open);
                return (List<AccountRecord>)_serializer.Deserialize(fs)!;
            }
            else throw new Exception("File doesn't exist");
        }
    }
}
