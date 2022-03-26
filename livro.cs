
using System;

public class Livro {
  private int id;
  private string descricao;
  private int quantidade;
  private double preco;
  

  private int generoId;
  private Genero genero;
  private int autorId;
  private Autor autor;

  public int Id { get => id; set => id = value; }
  public string Descricao { get => descricao; set => descricao = value; }
  public int Quantidade { get => quantidade; set => quantidade = value; }
  public double Preco { get => preco; set => preco = value; }

  public int GeneroId { get => generoId; set => generoId = value; }
  public int AutorId { get => autorId; set => autorId = value; }
  
  public Livro() { }

  public Livro(int id, string descricao, int quantidade, double preco) {
    this.id = id;
    this.descricao = descricao;
    this.quantidade = quantidade > 0 ? quantidade : 0;
    this.preco = preco > 0 ? preco : 0;
  }
  
  public Livro(int id, string descricao, int quantidade, double preco, Genero genero, Autor autor) : this(id, descricao, quantidade, preco) {
    this.genero = genero;
    this.generoId = genero.GetId();
    this.autor = autor;
    this.autorId = autor.GetId();

  }

  
  public void SetId(int id) {
    this.id = id;
  }
  public void SetDescricao(string descricao) {
    this.descricao = descricao;
  }
  public void SetQuantidade(int quantidade) {
    this.quantidade = quantidade > 0 ? quantidade : 0;
  }
  public void SetPreco(double preco) {
    this.preco = preco > 0 ? preco : 0;
  }

  public void SetGenero(Genero genero) {
    this.genero = genero;
    this.generoId = genero.GetId();
  }
  
  public void SetAutor(Autor autor) {
    this.autor = autor;
    this.autorId = autor.GetId();
  }



  
  public int GetId() {
    return id;
  }
  public string GetDescricao() {
    return descricao;
  }
  public int GetQuantidade() {
    return quantidade;
  }
  public double GetPreco() {
    return preco;
  }
  
  public Genero GetGenero() {
    return genero;
  }
  
  public Autor GetAutor() {
    return autor;
  }


  /* ajeitar abaixo */
  
  public override string ToString() {
    if (genero == null)
      return id + " - " + descricao + " - Estoque: " + quantidade + " - Preço: " + preco.ToString("0.00");
    if (autor == null)
      return id + " - " + descricao + " - Estoque: " + quantidade + " - Preço: " + preco.ToString("0.00");
    else  
      return id + " - " + descricao + " - Estoque: " + quantidade + " - Preço: " + preco.ToString("0.00") + " - " + genero.GetDescricao() + " - " + autor.GetDescricao(); 
  }
}