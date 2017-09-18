using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTryout.Model.Services
{
    public interface IEnableLocationService
    {
        bool IsLocationServiceEnabled();
        void EnableLocationService();
    }
}
