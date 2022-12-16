using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMX.AccountsManager.Core.Design
{
    public class SerializationServiceDesign : ISerializationService
    {
        public IEnumerable<AccountRecordViewModel> Deserialize(string path)
        {
            throw new NotImplementedException();
        }

        public void SerializeBy(ISerializator serializator, IEnumerable<AccountRecordViewModel> viewModels, string path)
        {
            throw new NotImplementedException();
        }
    }
}
