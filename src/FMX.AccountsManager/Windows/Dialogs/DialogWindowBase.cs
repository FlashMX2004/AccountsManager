using FMX.AccountsManager.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMX.AccountsManager
{
    public class DialogWindowBase<VM> : WindowBase<VM>, IDialog<VM>
        where VM : DialogViewModelBase, IRequestClose
    {
        protected override void OnCloseRequested(object? sender, RequestCloseEventArgs e)
        {
            if (e.Result is not null) 
                DialogResult = e.Result;
            else 
                base.OnCloseRequested(sender, e);
        }
    }
}
