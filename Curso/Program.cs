// See https://aka.ms/new-console-template for more information
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


Console.WriteLine("Hello, World!");
