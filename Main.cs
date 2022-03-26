using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Linq;

class MainClass {
  private static NGenero ngenero = NGenero.Singleton;
  private static NAutor nautor = NAutor.Singleton;
  private static NLivro nlivro = NLivro.Singleton;
  private static NCliente ncliente = NCliente.Singleton;
  private static NVenda nvenda = NVenda.Singleton;

  private static Cliente clienteLogin = null;
  private static Venda clienteVenda = null;

  public static void Main() {
    Thread.CurrentThread.CurrentCulture = new CultureInfo("pt-BR");

    try {
      ngenero.Abrir();
      nautor.Abrir();
      nlivro.Abrir();
      ncliente.Abrir();
      nvenda.Abrir();
    }
    catch(Exception erro) {
      Console.WriteLine(erro.Message);
    }

    int ol = 0;
    int perfil = 0;
    Console.WriteLine ("---- SEBO VIRTUAL DE LIVROS ------");
    do {
      try {
        if (perfil == 0) {
          ol = 0;
          perfil = MenuUsuario();
        }
        
        if (perfil == 1) {
          ol = MenuVendedor();
          switch(ol) {
            case 1  : GeneroListar(); break;
            case 2  : GeneroInserir(); break;
            case 3  : GeneroAtualizar(); break;
            case 4  : GeneroExcluir(); break;
            case 5  : AutorListar(); break;
            case 6  : AutorInserir(); break;
            case 7  : AutorAtualizar(); break;
            case 8  : AutorExcluir(); break;
            case 9  : LivroListar(); break;
            case 10  : LivroInserir(); break;
            case 11  : LivroAtualizar(); break;
            case 12  : LivroExcluir(); break;
            case 13  : ClienteListar(); break;
            case 14 : ClienteInserir(); break;
            case 15 : ClienteAtualizar(); break;
            case 16 : ClienteExcluir(); break;
            case 17 : VendaListar(); break;
            case 99 : perfil = 0; break;
          }
        }
        
        if (perfil == 2 && clienteLogin == null) {
          ol = MenuClienteLogin();
          switch(ol) {
            case 1  : ClienteLogin(); break;
            case 99 : perfil = 0; break;
          }
        }
        if (perfil == 2 && clienteLogin != null) {
          ol = MenuClienteLogout();
          switch(ol) {
            case 1  : ClienteVendaListar(); break;
            case 2  : ClienteLivroListar(); break;
            case 3  : ClienteLivroInserir(); break;
            case 4  : ClienteCarrinhoVisualizar(); break;
            case 5  : ClienteCarrinhoLimpar(); break;
            case 6  : ClienteCarrinhoComprar(); break;
            case 99 : ClienteLogout(); break;
          }
        }
      }
      catch (Exception erro) {
        Console.WriteLine(erro.Message);
        ol = 100;
      }
    } while (ol != 0);
    try {
      ngenero.Salvar();
      nautor.Salvar();
      nlivro.Salvar();
      ncliente.Salvar();
      nvenda.Salvar();
    }
    catch(Exception erro) {
      Console.WriteLine(erro.Message);
    }
    Console.WriteLine ("FIM.....");    
  }
  public static int MenuUsuario() {
    Console.WriteLine();
    Console.WriteLine("----------------------------------");
    Console.WriteLine("");
    Console.WriteLine("1 - VENDEDOR - Entrar");
    Console.WriteLine("2 - CLIENTE - Entrar");
    Console.WriteLine("0 - FIM");
    Console.WriteLine("");
    Console.WriteLine("----------------------------------");
    Console.Write("Escolha uma opção: ");
    int ol = int.Parse(Console.ReadLine());
    Console.WriteLine();
    return ol; 
  }
  public static int MenuVendedor() {
    Console.WriteLine();
    Console.WriteLine("----------------------------------");
    Console.WriteLine("GÊNERO");
    Console.WriteLine("01 - Listar");
    Console.WriteLine("02 - Inserir");
    Console.WriteLine("03 - Atualizar");
    Console.WriteLine("04 - Excluir");
    Console.WriteLine("AUTOR(A)");
    Console.WriteLine("05 - Listar");
    Console.WriteLine("06 - Inserir");
    Console.WriteLine("07 - Atualizar");
    Console.WriteLine("08 - Excluir");
    Console.WriteLine("LIVRO");
    Console.WriteLine("09 - Listar");
    Console.WriteLine("10 - Inserir");
    Console.WriteLine("11 - Atualizar");
    Console.WriteLine("12 - Excluir");
    Console.WriteLine("CLIENTE");
    Console.WriteLine("13 - Listar");
    Console.WriteLine("14 - Inserir");
    Console.WriteLine("15 - Atualizar");
    Console.WriteLine("16 - Excluir");
    Console.WriteLine("VENDA");
    Console.WriteLine("17 - Listar");
    Console.WriteLine("OUTRAS OPÇÕES");
    Console.WriteLine("99 - Voltar");
    Console.WriteLine("00  - FIM");
    Console.WriteLine("----------------------------------");
    Console.Write("Escolha uma opção: ");
    int ol = int.Parse(Console.ReadLine());
    Console.WriteLine();
    return ol; 
  }
  public static int MenuClienteLogin() {
    Console.WriteLine();
    Console.WriteLine("----------------------------------");
    Console.WriteLine("01 - LOGIN");
    Console.WriteLine("99 - VOLTAR");
    Console.WriteLine("0  - FIM");
    Console.WriteLine("----------------------------------");
    Console.Write("Escolha uma opção: ");
    int ol = int.Parse(Console.ReadLine());
    Console.WriteLine();
    return ol; 
  }
  public static int MenuClienteLogout() {
    Console.WriteLine();
    Console.WriteLine("----------------------------------");
    Console.WriteLine("Bem vindo(a), " + clienteLogin.Nome);
    Console.WriteLine("----------------------------------");
    Console.WriteLine("01 - Listar minhas compras");
    Console.WriteLine("02 - Listar livros");
    Console.WriteLine("03 - Inserir um livro no carrinho");
    Console.WriteLine("04 - Visualizar o carrinho");
    Console.WriteLine("05 - Limpar o carrinho");
    Console.WriteLine("06 - Confirmar a compra");
    Console.WriteLine("99 - Logout");
    Console.WriteLine("0  - Fim");
    Console.WriteLine("----------------------------------");
    Console.Write("Escolha uma opção: ");
    int ol = int.Parse(Console.ReadLine());
    Console.WriteLine();
    return ol; 
  }

  
  public static void GeneroListar() {
    Console.WriteLine("-------- Lista de gêneros --------");
    Genero[] gs = ngenero.Listar();
    if (gs.Length == 0) {
      Console.WriteLine("Nenhum gênero cadastrado");
      return;
    }
    foreach(Genero g in gs) Console.WriteLine(g);
    Console.WriteLine();  
  }
  public static void GeneroInserir() {
    Console.WriteLine("------ Inserção de gêneros -------");
    Console.Write("Código para o gênero: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Gênero: ");
    string descricao = Console.ReadLine();
    Genero g = new Genero(id, descricao);
    ngenero.Inserir(g);
  }
  public static void GeneroAtualizar() {
    Console.WriteLine("----- Atualização de Gêneros -----");
    GeneroListar();
    Console.Write("Código do Gênero a ser alterado: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Informe um gênero: ");
    string descricao = Console.ReadLine();
    Genero g = new Genero(id, descricao);
    ngenero.Atualizar(g);
  }
  public static void GeneroExcluir() {
    Console.WriteLine("------ Exclusão de Gêneros -------");
    GeneroListar();
    Console.Write("Código do Gênero a ser excluído: ");
    int id = int.Parse(Console.ReadLine());
    Genero g = ngenero.Listar(id);
    ngenero.Excluir(g);
  }
  
  public static void AutorListar() {
    Console.WriteLine("-------- Lista de Autores --------");
    Autor[] ts = nautor.Listar();
    if (ts.Length == 0) {
      Console.WriteLine("Nenhum(a) Autor(a) cadastrado(a)");
      return;
    }
    foreach(Autor t in ts) Console.WriteLine(t);
    Console.WriteLine();  
  }
  public static void AutorInserir() {
    Console.WriteLine("------ Inserção de Autores -------");
    Console.Write("Código para o(a) Autor(a): ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Autor(a): ");
    string descricao = Console.ReadLine();
    Autor t = new Autor(id, descricao);
    nautor.Inserir(t);
  }
  public static void AutorAtualizar() {
    Console.WriteLine("----- Atualização de Autores -----");
    AutorListar();
    Console.Write("Código do(a) Autor(a) a ser alterado(a): ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Informe um autor: ");
    string descricao = Console.ReadLine();
    Autor t = new Autor(id, descricao);
    nautor.Atualizar(t);
  }
  public static void AutorExcluir() {
    Console.WriteLine("------ Exclusão de Autores -------");
    AutorListar();
    Console.Write("Código do(a) Autor(a) a ser excluído(a): ");
    int id = int.Parse(Console.ReadLine());
    Autor t = nautor.Listar(id);
    nautor.Excluir(t);
  }

  
  public static void LivroListar() {
    Console.WriteLine("-------- Lista de Livros ---------");
    Livro[] ls = nlivro.Listar();
    if (ls.Length == 0) {
      Console.WriteLine("Nenhum Livro cadastrado");
      return;
    }
    foreach(Livro l in ls) Console.WriteLine(l);
    Console.WriteLine();  
  }
  
  public static void LivroInserir() {
    Console.WriteLine("------- Inserção de Livros -------");
    Console.Write("Código para o Livro: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Título do livro: ");
    string descricao = Console.ReadLine();
    Console.Write("Estoque do Livro: ");
    int quantidade = int.Parse(Console.ReadLine());
    Console.Write("Preço do Livro: ");
    double preco = double.Parse(Console.ReadLine());
    
    GeneroListar();
    Console.Write("Código do gênero do livro: ");
    int idgenero = int.Parse(Console.ReadLine());

    AutorListar();
    Console.Write("Código do(a) autor(a) do livro: ");
    int idautor = int.Parse(Console.ReadLine());
    
    Genero g = ngenero.Listar(idgenero);
    
    Autor t = nautor.Listar(idautor);  
    
    Livro l = new Livro(id, descricao, quantidade, preco, g, t);
    
    nlivro.Inserir(l);
  }
  
  public static void LivroAtualizar() {
    Console.WriteLine("----- Atualização de Livros ------");
    LivroListar();
    Console.Write("Código do Livro a ser atualizado: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Informe o livro: ");
    string descricao = Console.ReadLine();
    Console.Write("Informe o estoque do Livro: ");
    int quantidade = int.Parse(Console.ReadLine());
    Console.Write("Informe o preço do Livro: ");
    double preco = double.Parse(Console.ReadLine());
    
    GeneroListar();
    Console.Write("Código do Gênero do livro: ");
    int idgenero = int.Parse(Console.ReadLine());
    Genero g = ngenero.Listar(idgenero);

    AutorListar();
    Console.Write("Código do autor do livro: ");
    int idautor = int.Parse(Console.ReadLine());
    Autor t = nautor.Listar(idautor);
    
    Livro l = new Livro(id, descricao, quantidade, preco, g, t);
    
    nlivro.Atualizar(l);
  }
  
  public static void LivroExcluir() {
    Console.WriteLine("------- Exclusão de Livros -------");
    LivroListar();
    Console.Write("Código do livro a ser excluído: ");
    int id = int.Parse(Console.ReadLine());
    Livro l = nlivro.Listar(id);
    nlivro.Excluir(l);
  }
  

  public static void ClienteListar() {
    Console.WriteLine("-------- Lista de Clientes -------");
    List<Cliente> cs = ncliente.Listar();
    if (cs.Count == 0) {
      Console.WriteLine("Nenhum cliente cadastrado");
      return;
    }
    foreach(Cliente c in cs) Console.WriteLine(c);
    Console.WriteLine();  
  }

  public static void ClienteInserir() {
    Console.WriteLine("------ Inserção de Clientes ------");
    Console.Write("Nome do cliente: ");
    string nome = Console.ReadLine();
    Console.Write("Data de nascimento (dd/mm/aaaa): ");
    DateTime nasc = DateTime.Parse(Console.ReadLine());
    Cliente c = new Cliente { Nome = nome, Nascimento = nasc };
    ncliente.Inserir(c);
  }

  public static void ClienteAtualizar() {
    Console.WriteLine("---- Atualização de Clientes -----");
    ClienteListar();
    Console.Write("Código do cliente a ser atualizado: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Nome do cliente: ");
    string nome = Console.ReadLine();
    Console.Write("Data de nascimento (dd/mm/aaaa): ");
    DateTime nasc = DateTime.Parse(Console.ReadLine());
    Cliente c = new Cliente { Id = id, Nome = nome, Nascimento = nasc };
    ncliente.Atualizar(c);
  }

  public static void ClienteExcluir() {
    Console.WriteLine("------ Exclusão de Clientes ------");
    ClienteListar();
    Console.Write("Código do cliente a ser excluído: ");
    int id = int.Parse(Console.ReadLine());
    Cliente c = ncliente.Listar(id);
    ncliente.Excluir(c);
  }

  
  public static void VendaListar() { 
    Console.WriteLine("-------- Lista de Vendas ---------");
    List<Venda> vs = nvenda.Listar();
    if (vs.Count == 0) {
      Console.WriteLine("Nenhuma venda cadastrada");
      return;
    }
    foreach(Venda v in vs) {
      Console.WriteLine(v);
      foreach (VendaItem item in nvenda.ItemListar(v))
        Console.WriteLine("  " + item);
    }    
    Console.WriteLine();

    var r1 = vs.Select(v => new {
      MesAno = v.Data.Month + "/" + v.Data.Year,
      Total  = v.Itens.Sum(vi => vi.Quantidade * vi.Preco)
    });

    foreach(var item in r1) Console.WriteLine(item);
    Console.WriteLine();

    var r2 = r1.GroupBy(item => item.MesAno,
      (key, items) => new {
        MesAno = key,
        Total = items.Sum(item => item.Total) });

    foreach(var item in r2) Console.WriteLine(item);
    Console.WriteLine();
  }

  
  public static void ClienteLogin() { 
    Console.WriteLine("-------- LOGIN DO CLIENTE --------");
    ClienteListar();
    Console.Write("Código do cliente para logar: ");
    int id = int.Parse(Console.ReadLine());
    clienteLogin = ncliente.Listar(id);
    clienteVenda = nvenda.ListarCarrinho(clienteLogin);
  }
  
  public static void ClienteLogout() { 
    Console.WriteLine("----- Logout do Cliente -----");
    if (clienteVenda != null) nvenda.Inserir(clienteVenda, true);
    clienteLogin = null;
    clienteVenda = null;
  }
  
  public static void ClienteVendaListar() { 
    Console.WriteLine("----- Minhas Compras -----");
    List<Venda> vs = nvenda.Listar(clienteLogin);
    if (vs.Count == 0) {
      Console.WriteLine("Nenhuma compra cadastrada");
      return;
    }
    foreach(Venda v in vs) {
      Console.WriteLine(v);
      foreach (VendaItem item in nvenda.ItemListar(v))
        Console.WriteLine("  " + item);
    }    
    Console.WriteLine();
  }
  
  public static void ClienteLivroListar() { 
    LivroListar();
  }
  public static void ClienteLivroInserir() { 
    LivroListar();
    Console.Write("Código do livro a ser comprado: ");
    int id = int.Parse(Console.ReadLine());
    Console.Write("Quantidade: ");
    int quantidade = int.Parse(Console.ReadLine());
    Livro l = nlivro.Listar(id);
    if (l != null) {
      if (clienteVenda == null)
        clienteVenda = new Venda(DateTime.Now, clienteLogin);
      nvenda.ItemInserir(clienteVenda, quantidade, l);  
    }
  }
  public static void ClienteCarrinhoVisualizar() { 
    if (clienteVenda == null) {
      Console.WriteLine("Nenhum livro no carrinho");
      return;
    }
    List<VendaItem> itens = nvenda.ItemListar(clienteVenda);
    foreach(VendaItem item in itens) Console.WriteLine(item);
    Console.WriteLine();
  }
  public static void ClienteCarrinhoLimpar() { 
    if (clienteVenda != null)
      nvenda.ItemExcluir(clienteVenda);
  }
  
  public static void ClienteCarrinhoComprar() { 
    if (clienteVenda == null) {
      Console.WriteLine("Nenhum livro no carrinho");
      return;
    }   
    nvenda.Inserir(clienteVenda, false);
    clienteVenda = null;
    Console.WriteLine("-- Compra realizada com sucesso --");
  }

}