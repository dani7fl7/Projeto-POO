using System;

public class VendaItem {
  private int quantidade;
  private double preco;
  private Livro livro;
  private int livroId;

  public int Quantidade { get => quantidade; set => quantidade = value; }
  public double Preco { get => preco; set => preco = value; }
  public int LivroId { get => livroId; set => livroId = value; }
  public VendaItem() { }

  public VendaItem(int quantidade, Livro livro) {
    this.quantidade = quantidade;
    this.preco = livro.GetPreco();
    this.livro = livro;
    this.livroId = livro.GetId();
  }
  public void SetQuantidade(int quantidade) {
    this.quantidade = quantidade;
  }
  public void SetPreco(double preco) {
    this.preco = preco;
  }
  public void SetLivro(Livro livro) {
    this.livro = livro;
    this.livroId = livro.GetId();
  }
  public int GetQuantidade() {
    return quantidade;
  }
  public double GetPreco() {
    return preco;
  }
  public Livro GetLivro() {
    return livro;
  }
  public override string ToString() {
    return livro.GetDescricao() + " - Quantidade:" + quantidade + " - Preço: " + preco.ToString("0.00");
  }
}