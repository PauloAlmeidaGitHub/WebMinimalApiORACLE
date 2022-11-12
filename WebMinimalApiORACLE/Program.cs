using Microsoft.EntityFrameworkCore;
using WebMinimalApiORACLE.Config;
using WebMinimalApiORACLE.Models;

// Coxexão com o ORACLE
var strConnection = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)" +
                    "(HOST=SCADUFAX)(PORT=1521))" +
                    "(CONNECT_DATA=(SERVICE_NAME=XEPDB1)));" +
                    "User Id=Y2F0;Password=y2f0;";

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
app.MapPost("PRODUTO/AdicionarProduto", async (Produto produto, Contexto contexto) =>
{
    contexto.PRODUTO.Add(produto);
    await contexto.SaveChangesAsync();
});

// Update
//===========================================================================
app.MapPut("PRODUTO/AlterarProduto", async (Produto produto, Contexto contexto) =>
{
    if (produto != null)
    {
        contexto.PRODUTO.Update(produto);
        await contexto.SaveChangesAsync();
    }
});

// Delete
//===========================================================================
app.MapDelete("PRODUTO/ExcluirProduto/{id}", async (int id, Contexto contexto) =>
{
    var produto = await contexto.PRODUTO.FirstOrDefaultAsync(p=> p.ID == id);
    if(produto != null)
    {
        contexto.PRODUTO.Remove(produto);
        await contexto.SaveChangesAsync();
    }
});

// PRODUTO/GetAll
//===========================================================================
app.MapGet("PRODUTO/ListarProdutos", async (Contexto contexto) =>
{
    return await contexto.PRODUTO.ToListAsync();
});

// GetById
//===========================================================================
app.MapGet("PRODUTO/ObterProduto/{id}", async (int id, Contexto contexto) =>
{
    return await contexto.PRODUTO.FirstOrDefaultAsync(p => p.ID == id);
});



// ORGAO/GetAll
//===========================================================================
app.MapGet("ORGAO/ListarOrgaos", async (Contexto contexto) =>
{
    return await contexto.ORGA_FOTR_SUB.ToListAsync();
});
// GetSubstitutoByOrgao
//===========================================================================
app.MapGet("ORGAO/GetSubstitutoByOrgao/{orgaoId}", async (int orgaoId, Contexto contexto) =>
{
    //return await contexto.ORGA_FOTR_SUB.ToListAsync();

    List<OrgaFotrSub> L = new List<OrgaFotrSub>();
    L = await contexto.ORGA_FOTR_SUB.ToListAsync();
    for (int i = L.Count -1 ; i >= 0; i--)
    {
        if (L[i].ORGA_CD_ORGAO != orgaoId)
        {
            L.RemoveAt(i);
        }
    }
    return L;
});
app.Run();
