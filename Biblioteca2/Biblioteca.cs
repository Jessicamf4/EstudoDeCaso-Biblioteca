using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca2
{
    internal class Biblioteca
    {
        private No primeiroTitulo;
        private No primeiroAutor;

        public Biblioteca()
        {
            primeiroTitulo = null;
            primeiroAutor = null;
        }

        public void AdicionarLivro(string titulo, string autor)
        {
            Livro novoLivro = new Livro(titulo, autor);
            No novoNo = new No(novoLivro);

            // Adicionar na lista por título
            if (primeiroTitulo == null || String.Compare(titulo, primeiroTitulo.Livro.Titulo, StringComparison.OrdinalIgnoreCase) < 0)
            {
                novoNo.Proximo = primeiroTitulo;
                primeiroTitulo = novoNo;
            }
            else
            {
                No atual = primeiroTitulo;
                while (atual.Proximo != null && String.Compare(titulo, atual.Proximo.Livro.Titulo, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    atual = atual.Proximo;
                }
                novoNo.Proximo = atual.Proximo;
                atual.Proximo = novoNo;
            }

            // Adicionar na lista por autor
           // if (primeiroAutor == null || String.Compare(autor, primeiroAutor.Livro.Autor, StringComparison.OrdinalIgnoreCase) < 0)
           /* {
                novoNo.Proximo = primeiroAutor;
                primeiroAutor = novoNo;
            }
            else
            {
                No atual = primeiroAutor;
                while (atual.Proximo != null && String.Compare(autor, atual.Proximo.Livro.Autor, StringComparison.OrdinalIgnoreCase) > 0)
                {
                    atual = atual.Proximo;
                }
                novoNo.Proximo = atual.Proximo;
                atual.Proximo = novoNo;
            } */
        }

        public void RemoverLivro(string titulo)
        {
            No anteriorTitulo = null;
            No atualTitulo = primeiroTitulo;
            No anteriorAutor = null;
            No atualAutor = primeiroAutor;

            while (atualTitulo != null && String.Compare(atualTitulo.Livro.Titulo, titulo, StringComparison.OrdinalIgnoreCase) != 0)
            {
                anteriorTitulo = atualTitulo;
                atualTitulo = atualTitulo.Proximo;
            }

            while (atualAutor != null && String.Compare(atualAutor.Livro.Titulo, titulo, StringComparison.OrdinalIgnoreCase) != 0)
            {
                anteriorAutor = atualAutor;
                atualAutor = atualAutor.Proximo;
            }

            if (atualTitulo != null)
            {
                if (anteriorTitulo == null)
                {
                    primeiroTitulo = atualTitulo.Proximo;
                }
                else
                {
                    anteriorTitulo.Proximo = atualTitulo.Proximo;
                }
            }

            if (atualAutor != null)
            {
                if (anteriorAutor == null)
                {
                    primeiroAutor = atualAutor.Proximo;
                }
                else
                {
                    anteriorAutor.Proximo = atualAutor.Proximo;
                }
            }
        }

        public void EmprestarLivro(string titulo)
        {
            No atual = primeiroTitulo;
            while (atual != null)
            {
                if (String.Compare(atual.Livro.Titulo, titulo, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    atual.Livro.Disponivel = false;
                    return;
                }
                atual = atual.Proximo;
            }
        }

        public void DevolverLivro(string titulo)
        {
            No atual = primeiroTitulo;
            while (atual != null)
            {
                if (String.Compare(atual.Livro.Titulo, titulo, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    atual.Livro.Disponivel = true;
                    return;
                }
                atual = atual.Proximo;
            }
        }

        public bool BuscarPorTitulo(string titulo)
        {
            No atual = primeiroTitulo;
            while (atual != null)
            {
                if (String.Compare(atual.Livro.Titulo, titulo, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.WriteLine("Livro encontrado: " + atual.Livro.Titulo + ", Autor:  " + atual.Livro.Autor);
                    return true;
                    
                }
                atual = atual.Proximo;
            }
            return false;
        }

        public void BuscarPorAutor(string autor)
        {
            No atual = primeiroAutor;
            while (atual != null)
            {
                if (String.Compare(atual.Livro.Autor, autor, StringComparison.OrdinalIgnoreCase) == 0)
                {
                    Console.WriteLine("Livro encontrado: " + atual.Livro.Titulo + ", Autor:  " + atual.Livro.Autor);
                    
                }
                atual = atual.Proximo;
            }
        }

        public void ImprimirTodosLivros()
        {
            No atual = primeiroTitulo;

            while (atual.Proximo != null)
            {
                Console.WriteLine("Titulo: " + atual.Livro.Titulo + " Autor " + atual.Livro.Autor + " Disponibilidade: " + atual.Livro.Disponivel);
                atual = atual.Proximo;
            }
        }

        public void OrdenarPorTituloInsercao()
        {
            primeiroTitulo = OrdenarPorInsercao(primeiroTitulo, CompararPorTitulo);
        }

        public void OrdenarPorAutorInsercao()
        {
            primeiroAutor = OrdenarPorInsercao(primeiroAutor, CompararPorAutor);
        }

        private No OrdenarPorInsercao(No lista, Comparison<Livro> comparar)
        {
            if (lista == null || lista.Proximo == null)
                return lista;

            No ordenada = null;
            No atual = lista;

            while (atual != null)
            {
                No proximo = atual.Proximo;
                ordenada = InserirNaListaOrdenada(ordenada, atual, comparar);
                atual = proximo;
            }

            return ordenada;
        }

        private No InserirNaListaOrdenada(No listaOrdenada, No novoNo, Comparison<Livro> comparar)
        {
            if (listaOrdenada == null || comparar(listaOrdenada.Livro, novoNo.Livro) >= 0)
            {
                novoNo.Proximo = listaOrdenada;
                return novoNo;
            }

            No atual = listaOrdenada;
            while (atual.Proximo != null && comparar(atual.Proximo.Livro, novoNo.Livro) < 0)
            {
                atual = atual.Proximo;
            }

            novoNo.Proximo = atual.Proximo;
            atual.Proximo = novoNo;

            return listaOrdenada;
        }

        private int CompararPorTitulo(Livro livro1, Livro livro2)
        {
            return string.Compare(livro1.Titulo, livro2.Titulo, StringComparison.OrdinalIgnoreCase);
        }

        private int CompararPorAutor(Livro livro1, Livro livro2)
        {
            return string.Compare(livro1.Autor, livro2.Autor, StringComparison.OrdinalIgnoreCase);
        }

        
        
    }
}
