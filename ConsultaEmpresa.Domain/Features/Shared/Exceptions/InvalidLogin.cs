using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Domain.Features.Shared.Exceptions;

public class InvalidLogin : Exception
{
    public InvalidLogin(string message) : base(message)
    {
    }
}