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
        
    }
}
