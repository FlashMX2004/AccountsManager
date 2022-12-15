using FMX.AccountsManager.Core;
using Microsoft.Win32;
using System;
using System.Collections.Generic;

namespace FMX.AccountsManager
{
    /// <summary>
    /// Dialog service implementation for WPF
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Occurs when dialogs are starting to register
        /// </summary>
        public static event Action<DialogService>? OnDialogFactoriesRegistring;

        /// <summary>
        /// Mapped factories of dialogs by view model type
        /// </summary>
        private readonly Dictionary<Type, Func<object>> MappedFactories = new();

        /// <summary>
        /// Register factory
        /// </summary>
        /// <typeparam name="TViewModel">View model type</typeparam>
        /// <param name="factory">Factory for dialog mapped to <typeparamref name="TViewModel"/></param>
        public void RegisterDialogFactory<TViewModel>(Func<IDialog<TViewModel>> factory)
            where TViewModel: DialogViewModelBase, IRequestClose
                => MappedFactories[typeof(TViewModel)] = factory;

        /// <summary>
        /// Returns dialog by <typeparamref name="VM"/> view model
        /// </summary>
        /// <typeparam name="VM">View model type</typeparam>
        public IDialog<VM> Get<VM>() where VM : DialogViewModelBase, IRequestClose
            => (IDialog<VM>)MappedFactories[typeof(VM)]();

        /// <summary>
        /// Default constructor
        /// </summary>
        public DialogService() =>
            // Start registring (mapping) factories
            OnDialogFactoriesRegistring?.Invoke(this);

        public string? SaveDialog(ISerializator serializator)
        {
            var dialog = new SaveFileDialog
            {
                Title = "Save accounts backup",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                DefaultExt = serializator is IXMLSerializator ? XMLSerializator.EXTENTION : BinarySerializator.EXTENTION
            };

            return dialog.ShowDialog() ?? false ? dialog.FileName : null;
        }
        
        public string? OpenDialog()
        {
            var dialog = new OpenFileDialog
            {
                Title = "Import accounts backup",
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Multiselect = false,
                CheckFileExists = true,
                Filter = $"Binary (*{BinarySerializator.EXTENTION})|*{BinarySerializator.EXTENTION}|" +
                         $"XML (*{XMLSerializator.EXTENTION})|*{XMLSerializator.EXTENTION}",
                FilterIndex = 2
            };

            return dialog.ShowDialog() ?? false ? dialog.FileName : null;
        }
    }
}
