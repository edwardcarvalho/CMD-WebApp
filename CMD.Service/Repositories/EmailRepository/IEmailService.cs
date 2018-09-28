using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMD.Service.Repositories.EmailService
{
    public interface IEmailService
    {
        bool EnviarEmail(string email, string username, long idMedida, bool condicao);
    }
}
