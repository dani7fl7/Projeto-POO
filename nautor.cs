using System;
using System.Xml.Serialization;
using System.Text;
using System.IO;
using System.Linq;

class NAutor {
  private NAutor() { }
  static NAutor obj = new NAutor();
  public static NAutor Singleton { get => obj; }

  private Autor[] autores = new Autor[10];
  private int nt;

  public void Abrir() {
    Arquivo<Autor[]> f = new Arquivo<Autor[]>();
    autores = f.Abrir("./autores.xml");
    nt = autores.Length;
    //XmlSerializer xml = new XmlSerializer(typeof(Autor[]));
    //StreamReader f = new StreamReader("./autores.xml", //Encoding.Default);
    //autores = (Autor[]) xml.Deserialize(f);
    //f.Close();
    //nt = autores.Length;
  }

  public void Salvar() {
    Arquivo<Autor[]> f = new Arquivo<Autor[]>();
    f.Salvar("./autores.xml", Listar());
    //XmlSerializer xml = new XmlSerializer(typeof(Autor[]));
    //StreamWriter f = new StreamWriter("./autores.xml", false, //Encoding.Default);
    //xml.Serialize(f, Listar());
    //f.Close();
  }

  public Autor[] Listar() {
    /*
    Autor[] t = new Autor[nt];
    Array.Copy(autores, t, nt);
    t.OrderBy(obj => obj.GetDescricao());
    return t;
    */
    return 
      autores.Take(nt).OrderBy(obj => obj.GetDescricao()).ToArray();
  }

  public Autor Listar(int id) { 
    /*
    for (int i = 0; i < nt; i++)
      if (autores[i].GetId() == id) return autores[i];
    return null;  
    
    var r = autores.Where(obj => obj.GetId() == id);
    if (r.Count() == 0) return null;
    return r.First();
    */
  return autores.FirstOrDefault(obj => obj.GetId() == id);
  }

  public void Inserir(Autor t) {
    if (nt == autores.Length) {
      Array.Resize(ref autores, 2 * autores.Length);
    }
    autores[nt] = t;
    nt++;
  } 

  public void Atualizar(Autor t) {
    Autor t_atual = Listar(t.GetId());
    if (t_atual == null) return;
    t_atual.SetDescricao(t.GetDescricao());
  } 

  private int Indice(Autor t) {
    for (int i = 0; i < nt; i++)
      if (autores[i] == t) return i;
    return -1;      
  }

  public void Excluir(Autor t) {
    int n = Indice(t);
    if (n == -1) return;
    for (int i = n; i < nt - 1; i++)
      autores[i] = autores[i + 1];
    nt--;
    Livro[] ls = t.LivroListar();
    foreach(Livro l in ls) l.SetAutor(null); 
  } 
}