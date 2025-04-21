using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultaEmpresa.Domain.Features.Shared;

public interface IValidadorToken
{
    public int Validar(string token);
}