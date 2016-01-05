using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaxExperiment.Util
{
    public interface IDBConnector
    {
        bool Open();

        bool Close();
    }
}
