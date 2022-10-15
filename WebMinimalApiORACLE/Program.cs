using Microsoft.EntityFrameworkCore;
using WebMinimalApiORACLE.Config;
using WebMinimalApiORACLE.Models;

// Coxexão com o ORACLE
var strConnection = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                    "(HOST=SCADUFAX)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));" +
                    "User Id=Y2F0;Password=Ganzahtn19;";

// Services
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();  // Abre o Navegador
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Contexto>(options => options.UseOracle(strConnection));
var app = builder.Build();

// Ativa o Swagger
app.UseSwagger();
app.UseSwaggerUI();



// Protocolos HTTP
// Insert
//===========================================================================
app.MapPost("AdicionarProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.Produto.Add(produto);
    await contexto.SaveChangesAsync();
});

// Update
//===========================================================================
app.MapPut("AlterarProduto", async (Produto produto, Contexto contexto) =>
{
    if (produto != null)
    {
        contexto.Produto.Update(produto);
        await contexto.SaveChangesAsync();
    }
});

// Delete
//===========================================================================
app.MapDelete("ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produto = await contexto.Produto.FirstOrDefaultAsync(p=> p.Id == id);
    if(produto != null)
    {
        contexto.Produto.Remove(produto);
        await contexto.SaveChangesAsync();
    }
});

// GetAll
//===========================================================================
app.MapGet("ListarProdutos", async (Contexto contexto) =>
{
    return await contexto.Produto.ToListAsync();
});

// GetById
//===========================================================================
app.MapGet("ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.Produto.FirstOrDefaultAsync(p => p.Id == id);
});


app.Run();
