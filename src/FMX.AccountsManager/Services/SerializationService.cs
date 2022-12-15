using FMX.AccountsManager.Core.Data;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Service for serializing / deserializing data by some serializators
    /// </summary>
    public class SerializationService : ISerializationService
    {
        private readonly IAccountRecordViewModelFactory _recordVMFactory;
        private readonly IBinarySerializator _binarySerializator;
        private readonly IXMLSerializator _xmlSerializator;

        public SerializationService(IAccountRecordViewModelFactory recordVMFactory,
                                    IBinarySerializator binarySerializator,
                                    IXMLSerializator xmlSerializator)
        {
            _recordVMFactory = recordVMFactory;
            _binarySerializator = binarySerializator;
            _xmlSerializator = xmlSerializator;
        }

        public void SerializeBy(ISerializator serializator, IEnumerable<AccountRecordViewModel> viewModels, string path)
        {
            if (File.Exists(path))
            {
                serializator.Serialize(viewModels.Select(vm => vm.ToData()).ToList(), path);
            }
        }

        public IEnumerable<AccountRecordViewModel> Deserialize(string path)
        {
            ISerializator serializator = Path.GetExtension(path).ToLower() == BinarySerializator.EXTENTION
                ? _binarySerializator
                : _xmlSerializator;

            return File.Exists(path)
                        ? serializator.Deserialize(path).Select(_recordVMFactory.Create)
                        : Enumerable.Empty<AccountRecordViewModel>();
        }
    }
}
