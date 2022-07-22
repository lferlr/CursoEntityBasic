// See https://aka.ms/new-console-template for more information
using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using Microsoft.EntityFrameworkCore;

using var db = new CursoEFCore.Data.ApplicationContext();

// Para toda vez que o projeto rodar acontecer a migração. Não é recomendado
//db.Database.Migrate();

// Como validar se existe migrações pendentes - Inicio
var existe = db.Database.GetPendingMigrations().Any();

if (existe)
{
    // Tomada de decisão
}
// Como validar se existe migrações pendentes - Fim

//InserindoDados();
//InserirDadosEmMassa();
//ConsultarDados();
//CadastrarPedido();
//ConsultarPedidos();
//AtualizarDados();
RemoverRegistro();

static void InserirDadosEmMassa()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste 2",
        CodigoBarras = "12345678912313",
        Valor = 11m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    var cliente = new Cliente
    {
        Nome = "Rafael Almeida",
        Telefone = "99000001111",
        CEP = "99999910",
        Estado = "SE",
        Cidade = "Itabaiana",
    };

    var listaClientes = new[]
    {
        new Cliente
        {
            Nome = "Guilherme Costa",
            Telefone = "99000001111",
            CEP = "99999910",
            Estado = "DF",
            Cidade = "Brasília",
        },

        new Cliente
        {
            Nome = "Henrique Almeida",
            Telefone = "99000001111",
            CEP = "99999910",
            Estado = "ES",
            Cidade = "Vitória",
        },
    };

    using var db = new CursoEFCore.Data.ApplicationContext();
    //db.AddRange(produto, cliente);
    db.Set<Cliente>().AddRange(listaClientes);
    //db.Clientes.AddRange(listaClientes);

    var registros = db.SaveChanges();
    Console.WriteLine($"Total registro(s): {registros}");
}

static void InserindoDados()
{
    var produto = new Produto
    {
        Descricao = "Produto Teste",
        CodigoBarras = "1234567891231",
        Valor = 10m,
        TipoProduto = TipoProduto.MercadoriaParaRevenda,
        Ativo = true
    };

    using var db = new CursoEFCore.Data.ApplicationContext();
    //db.Produtos.Add(produto);
    //db.Set<Produto>().Add(produto);
    //db.Entry(produto).State = EntityState.Added;
    db.Add(produto);

    var registros = db.SaveChanges();
    Console.WriteLine($"Total registro(s): {registros}");
}

static void ConsultarDados()
{
    using var db = new CursoEFCore.Data.ApplicationContext();

    //var consultaPorSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
    var consultaPorMetodo = db.Clientes
                                .Where(p => p.Id > 0)
                                .OrderBy(p => p.Id)
                                .ToList();

    foreach (var cliente in consultaPorMetodo)
    {
        Console.WriteLine($"Consultando cliente: {cliente.Id}");
        //db.Clientes.Find(cliente.Id);
        db.Clientes.FirstOrDefault(p => p.Id == cliente.Id);
    }
}

static void CadastrarPedido()
{
    using var db = new CursoEFCore.Data.ApplicationContext();

    var cliente = db.Clientes.FirstOrDefault();
    var produto = db.Produtos.FirstOrDefault();

    var pedido = new Pedido
    {
        ClienteId = cliente.Id,
        IniciadoEm = DateTime.Now,
        FinalizadoEm = DateTime.Now,
        Observacao = "Pedido Teste",
        Status = StatusPedido.Analise,
        TipoFrete = TipoFrete.SemFrete,
        Itens = new List<PedidoItem>
        {
            new PedidoItem
            {
                ProdutoId = produto.Id,
                Desconto = 0,
                Quantidade = 1,
                Valor = 10
            }
        }
    };

    db.Pedidos.Add(pedido);
    db.SaveChanges();
}

static void ConsultarPedidos()
{
    using var db = new CursoEFCore.Data.ApplicationContext();
    var pedidos = db.Pedidos
                    .Include(p => p.Itens)
                        .ThenInclude(p => p.Produto)
                    .ToList();

    Console.WriteLine(pedidos.Count);
}

static void AtualizarDados()
{
    // Método Generico para atualizar um dado - Inicio

    //using var db = new CursoEFCore.Data.ApplicationContext();
    //var cliente = db.Clientes.FirstOrDefault(p => p.Id == 4);
    //db.SaveChanges();

    // Método Generico para atualizar um dado - Fim

    // Método desconectado - Inicio

    using var db = new CursoEFCore.Data.ApplicationContext();
    //var cliente = db.Clientes.Find(4);

    var cliente = new Cliente
    {
        Id = 4
    };
    var clienteDesconhecido = new
    {
        Nome = "Cliente Desconhecido passo 3",
        Telefone = "7966669999"
    };

    db.Attach(cliente);
    db.Entry(cliente).CurrentValues.SetValues(clienteDesconhecido);
    db.SaveChanges();

    // Método desconectado - Fim
}

static void RemoverRegistro()
{
    using var db = new CursoEFCore.Data.ApplicationContext();
    
    // Localizando o registro para depois excluir
    //var cliente = db.Clientes.Find(4);

    var cliente = new Cliente { Id = 6 };

    //db.Clientes.Remove(cliente);
    //db.Remove(cliente);
    db.Entry(cliente).State = EntityState.Deleted;
    db.SaveChanges();
}