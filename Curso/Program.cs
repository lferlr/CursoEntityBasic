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
InserirDadosEmMassa();

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