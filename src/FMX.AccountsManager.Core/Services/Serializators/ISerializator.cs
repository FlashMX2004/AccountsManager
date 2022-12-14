using FMX.AccountsManager.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMX.AccountsManager.Core
{
    public interface ISerializator
    {
        void Serialize(List<AccountRecord> records, string path);

        List<AccountRecord> Deserialize(string path);

    }
}
