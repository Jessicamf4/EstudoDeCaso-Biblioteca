using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca2
{
    internal class No
    {
        public Livro Livro { get; set; }
        public No Proximo { get; set; }

        public No(Livro livro)
        {
            Livro = livro;
            Proximo = null;
        }

    }
}
