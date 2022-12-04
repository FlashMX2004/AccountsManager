using System;
using System.Collections.Generic;

namespace FMX.AccountsManager
{
    public class DialogService : IDialogService
    {
        public static event Action<DialogService>? OnDialogsRegister;

        private readonly Dictionary<Type, Func<object>> MappedFactories = new();

        public void RegisterFactory<TViewModel>(Func<IDialog<TViewModel>> factory)
            where TViewModel: ViewModelBase, IRequestClose
                => MappedFactories[typeof(TViewModel)] = factory;
        
        public IDialog<VM> Get<VM>() where VM : ViewModelBase, IRequestClose
            => (IDialog<VM>)MappedFactories[typeof(VM)]();


        public DialogService()
        {
            OnDialogsRegister?.Invoke(this);
        }
    }
}
