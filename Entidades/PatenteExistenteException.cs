using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades
{
    public class PatenteExistenteException : Exception
    {
        public PatenteExistenteException()
        {
        }

        public PatenteExistenteException(string message) : base(message)
        {
        }
    }
}
