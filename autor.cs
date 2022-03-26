using System;

public class Autor {
  private int id;
  private string descricao;
  private Livro[] livros = new Livro[10];
  private int nl;
  public int Id { get => id; set => id = value; }
  public string Descricao { get => descricao; set => descricao = value; }
  public Autor() { }

  public Autor(int id, string descricao) {
    this.id = id;
    this.descricao = descricao;
  }
  public void SetId(int id) {
    this.id = id;
  }
  public void SetDescricao(string descricao) {
    this.descricao = descricao;
  }
  public int GetId() {
    return id;
  }
  public string GetDescricao() {
    return descricao;
  }
  public Livro[] LivroListar() {
    Livro[] t = new Livro[nl];
    Array.Copy(livros, t, nl);
    return t;
  }
  public void LivroInserir(Livro l) {
    if (nl == livros.Length) {
      Array.Resize(ref livros, 2 * livros.Length);
    }
    livros[nl] = l;
    nl++;
  }
  private int LivroIndice(Livro l) {
    for (int i = 0; i < nl; i++)
      if (livros[i] == l) return i;
    return -1;  
  }
  public void LivroExcluir(Livro l) {
    int n = LivroIndice(l);
    if (n == -1) return;
    for (int i = n; i < nl - 1; i++)
      livros[i] = livros[i + 1];
    nl--;  
  }
  public override string ToString() {
    return id + " - " + descricao + " - Nº de livros: " + nl;
  }
}