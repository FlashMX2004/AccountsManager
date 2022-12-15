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

        public void SerializeBy(ISerializator serializator, IEnumerable<AccountRecordViewModel> viewModels)
        {
            var ext = serializator is IXMLSerializator ? XMLSerializator.EXTENTION : BinarySerializator.EXTENTION;
            var dialog = new SaveFileDialog
            {
                Title = "Save accounts backup",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultExt = ext
            };

            if (dialog.ShowDialog() ?? false && File.Exists(dialog.FileName))
            {
                serializator.Serialize(viewModels.Select(vm => vm.ToData()).ToList(), dialog.FileName);
            }
        }

        public IEnumerable<AccountRecordViewModel> Deserialize()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Import accounts backup",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false,
                CheckFileExists = true,
                Filter = $"Binary (*{BinarySerializator.EXTENTION})|*{BinarySerializator.EXTENTION}|" +
                         $"XML (*{XMLSerializator.EXTENTION})|*{XMLSerializator.EXTENTION}",
                FilterIndex = 1
            };

            ISerializator serializator = Path.GetExtension(dialog.FileName).ToLower() == BinarySerializator.EXTENTION
                ? _binarySerializator
                : _xmlSerializator;

            return dialog.ShowDialog() ?? false && File.Exists(dialog.FileName)
                        ? serializator.Deserialize(dialog.FileName).Select(_recordVMFactory.Create)
                        : Enumerable.Empty<AccountRecordViewModel>();
        }
    }
}
