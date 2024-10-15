using DAL;
using DTO;
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

        public List<ClientDTO> getList()
        {
            return clientDAL.getClientList();
        }

        public bool addClient(ClientDTO clientDTO)
        {
            return clientDAL.insert(clientDTO);
        }

        public bool updateClient(ClientDTO clientDTO)
        {
            return clientDAL.update(clientDTO);
        }

        public bool deleteClient(ClientDTO clientDTO)
        {
            return clientDAL.delete(clientDTO);
        }
    }
}
