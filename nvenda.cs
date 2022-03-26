using System;
using System.Collections.Generic;
using System.Linq;

class NVenda {
  private NVenda() { }
  static NVenda obj = new NVenda();
  public static NVenda Singleton { get => obj; }
  
  private List<Venda> vendas = new List<Venda>();

  public void Abrir() {
    Arquivo<List<Venda>> f = new Arquivo<List<Venda>>();
    vendas = f.Abrir("./vendas.xml");
    AtualizarCliente();
    AtualizarLivro();
  }

  private void AtualizarCliente() {
    foreach(Venda v in vendas) {
      Cliente c = NCliente.Singleton.Listar(v.ClienteId);
      if (c != null) v.SetCliente(c);
    }
  }

  private void AtualizarLivro() {
    foreach(Venda v in vendas) {
      foreach(VendaItem vi in v.ItemListar()) {
        Livro l  = NLivro.Singleton.Listar(vi.LivroId);
        if (l != null) vi.SetLivro(l);
      }
    }
  }

  public void Salvar() {
    Arquivo<List<Venda>> f = new Arquivo<List<Venda>>();
    f.Salvar("./vendas.xml", Listar());
  }

  public List<Venda> Listar() {
    return vendas;
  }

  public List<Venda> Listar(Cliente c) {
    /*
    List<Venda> vs = new List<Venda>();
    foreach(Venda v in vendas)
      if (v.GetCliente() == c) vs.Add(v);
    return vs;
    */
    return vendas.Where(v => v.GetCliente() == c).ToList();
  }

  public Venda ListarCarrinho(Cliente c) {
    foreach(Venda v in vendas)
      if (v.GetCliente() == c && v.GetCarrinho()) return v;
    return null;
  }

  public void Inserir(Venda v, bool carrinho) {
    int max = 0;
    foreach (Venda obj in vendas)
      if (obj.GetId() > max) max = obj.GetId();

    max = vendas.Max(obj => obj.GetId()); 
    
    
    
    v.SetId(max + 1);
    vendas.Add(v);
    v.SetCarrinho(carrinho);
  }


  
  public List<VendaItem> ItemListar(Venda v) {
    return v.ItemListar();
  }

  public void ItemInserir(Venda v, int quantidade, Livro l) {
    v.ItemInserir(quantidade, l);
  }  

  public void ItemExcluir(Venda v) {
    v.ItemExcluir();
  }
}