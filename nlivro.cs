using System;
using System.Xml.Serialization;
using System.Text;
using System.IO;

class NLivro {
  private NLivro() { }
  static NLivro obj = new NLivro();
  public static NLivro Singleton { get => obj; }

  private Livro[] livros = new Livro[10];
  private int nl;

  public void Abrir() {
    Arquivo<Livro[]> f = new Arquivo<Livro[]>();
    livros = f.Abrir("./livros.xml");
    nl = livros.Length;
    AtualizarGenero();
    AtualizarAutor();

    //XmlSerializer xml = new XmlSerializer(typeof(Livro[]));
    //StreamReader f = new StreamReader("./livros.xml", Encoding.Default);
    //livros = (Livro[]) xml.Deserialize(f);
    //f.Close();
    //nl = livros.Length;
    //AtualizarGenero();
    //AtualizarAutor;
  }

  
  public void Salvar() {
    Arquivo<Livro[]> f = new Arquivo<Livro[]>();
    f.Salvar("./livros.xml", Listar());

    //XmlSerializer xml = new XmlSerializer(typeof(Livro[]));
    //StreamWriter f = new StreamWriter("./livros.xml", false, Encoding.Default);
    //xml.Serialize(f, Listar());
    //f.Close();
  }




  

  private void AtualizarGenero() {
    for(int i = 0; i < nl; i++) {
      Livro l = livros[i];
      Genero g = NGenero.Singleton.Listar(l.GeneroId);
      if (g != null) {
        l.SetGenero(g);
        g.LivroInserir(l);
      }
    }
  }

    private void AtualizarAutor() {
    for(int i = 0; i < nl; i++) {
      Livro l = livros[i];
      Autor t = NAutor.Singleton.Listar(l.AutorId);
      if (t != null) {
        l.SetAutor(t);
        t.LivroInserir(l);
      }
    }
  }


  


  public Livro[] Listar() {
    Livro[] l = new Livro[nl];
    Array.Copy(livros, l, nl);
    return l;
  }

  public Livro Listar(int id) { 
    for (int i = 0; i < nl; i++)
      if (livros[i].GetId() == id) return livros[i];
    return null;  
  }

  public void Inserir(Livro l) {
    if (nl == livros.Length) {
      Array.Resize(ref livros, 2 * livros.Length);
    }
    livros[nl] = l;
    nl++;
    
    Genero g = l.GetGenero();
    if (g!= null) g.LivroInserir(l);
    Autor t = l.GetAutor();
    if (t!= null) t.LivroInserir(l);
  } 

  public void Atualizar(Livro l) {
    Livro l_atual = Listar(l.GetId());
    if (l_atual == null) return;
    l_atual.SetDescricao(l.GetDescricao());
    //l_atual.SetQuantidade(l_atual.GetQuantidade() - emprestada);
    l_atual.SetQuantidade(l.GetQuantidade());
    l_atual.SetPreco(l.GetPreco());
    
    if (l_atual.GetGenero() != null) 
      l_atual.GetGenero().LivroExcluir(l_atual);
    l_atual.SetGenero(l.GetGenero());
    if (l_atual.GetGenero() != null)
      l_atual.GetGenero().LivroInserir(l_atual);

    if (l_atual.GetAutor() != null) 
      l_atual.GetAutor().LivroExcluir(l_atual);
    l_atual.SetAutor(l.GetAutor());
    if (l_atual.GetAutor() != null)
      l_atual.GetAutor().LivroInserir(l_atual);
    
  }

  private int Indice(Livro l) {
    for(int i = 0; i < nl; i++)
      if (livros[i] == l) return i;
    return -1;  
  }

  public void Excluir(Livro l) {
    int n = Indice(l);
    if (n == -1) return;
    for (int i = n; i < nl - 1; i++)
      livros[i] = livros[i + 1];
    nl--;
    
    Genero g = l.GetGenero();
    if (g != null) g.LivroExcluir(l);  
    Autor t = l.GetAutor();
    if (t != null) t.LivroExcluir(l);  
  }
}