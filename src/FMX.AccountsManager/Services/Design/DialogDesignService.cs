using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMX.AccountsManager
{
    public class DialogDesignService : IDialogService
    {
        public IDialog<VM> Get<VM>() where VM : DialogViewModelBase, IRequestClose
        {
            return null;
        }
    }
}
