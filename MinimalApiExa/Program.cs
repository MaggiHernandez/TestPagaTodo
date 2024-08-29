using MinimalApiExa.Data;
using MinimalApiExa.Repository;

var builder = WebApplication.CreateBuilder(args);

//Registro del servicio en el contenedor de dependencias.
builder.Services.AddSingleton<IcalculosRepository, CalculosRepository>();

var app = builder.Build();

//Aqu� se inyecta el servicio IcalculosRepository directamente en el endpoint a trav�s del par�metro CalculosRepository.
app.MapGet("/multiplicar", (int a, int b, IcalculosRepository CalculosRepository) =>
{
    return Results.Ok(CalculosRepository.multiplicar(a, b));
});

app.Run();
