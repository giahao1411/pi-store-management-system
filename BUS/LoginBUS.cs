using DAL;
using DTO;

namespace BUS
{
    public class LoginBUS
    {
        private LoginDAL loginDAL = new LoginDAL();

        public bool validateLogin(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.username) || string.IsNullOrWhiteSpace(loginDTO.password)) 
            {
                return false;
            }

            return loginDAL.verifyLogin(loginDTO);
        }
    }
}
