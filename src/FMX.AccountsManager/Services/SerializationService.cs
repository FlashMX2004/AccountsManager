using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMX.AccountsManager
{
    public class SerializationService : ISerializationService
    {
        private readonly IDialogService _dialogService;

        public SerializationService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public void SerializeBy(ISerializator serializator, IEnumerable<AccountRecordViewModel> viewModels)
        {
            var ext = serializator is IXMLSerializator ? XMLSerializator.EXTENTION : BinarySerializator.EXTENTION;
            var dialog = new SaveFileDialog()
            {
                Title = "Save accounts backup",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultExt = ext
            };

            if (dialog.ShowDialog() ?? false)
            {
                serializator.Serialize(viewModels.Select(vm => vm.ToData()).ToList(), dialog.FileName);
                _dialogService.MessageBox($"{dialog.SafeFileName} backup file saved.").ShowDialog();
            }
        }
    }
}
