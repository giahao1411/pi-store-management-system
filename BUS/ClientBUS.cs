using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS
{
    public class ClientBUS
    {
        private ClientDAL clientDAL = new ClientDAL();

        public int getNumOfClient()
        {
            return clientDAL.countClient();
        }
    }
}
