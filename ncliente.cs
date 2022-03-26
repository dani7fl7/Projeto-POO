using System;
using System.Collections.Generic;

class NCliente {
  private NCliente() { }
  static NCliente obj = new NCliente();
  public static NCliente Singleton { get => obj; }

  private List<Cliente> clientes = new List<Cliente>();

  public void Abrir() {
    Arquivo<List<Cliente>> f = new Arquivo<List<Cliente>>();
    clientes = f.Abrir("./clientes.xml");
  }

  public void Salvar() {
    Arquivo<List<Cliente>> f = new Arquivo<List<Cliente>>();
    f.Salvar("./clientes.xml", Listar());
  }


  public List<Cliente> Listar() {
    clientes.Sort();
    return clientes;
  }

  public Cliente Listar(int id) {
    for (int i = 0; i < clientes.Count; i++)
      if (clientes[i].Id == id) return clientes[i];
    return null;  
  }

  public void Inserir(Cliente c) {
    int max = 0;
    foreach(Cliente obj in clientes)
      if (obj.Id > max) max = obj.Id;
    c.Id = max + 1;      
    clientes.Add(c);
  } 

  public void Atualizar(Cliente c) {
    Cliente c_atual = Listar(c.Id);
    if (c_atual == null) return;
    c_atual.Nome = c.Nome;
    c_atual.Nascimento = c.Nascimento;
  } 

  public void Excluir(Cliente c) {
    if (c != null) clientes.Remove(c);
  } 
}