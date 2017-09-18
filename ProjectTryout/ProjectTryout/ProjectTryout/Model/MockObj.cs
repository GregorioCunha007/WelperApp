using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTryout.Model
{
    interface IMock
    {
        void DoWork();
    }

    public class MockObj : IMock
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public void DoWork()
        {
            throw new NotImplementedException();
        }
    }
}
