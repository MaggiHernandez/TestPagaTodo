using MinimalApiExa.Data;
using MinimalApiExa.Repository;

var builder = WebApplication.CreateBuilder(args);

//Registro del servicio en el contenedor de dependencias.
builder.Services.AddSingleton<IcalculosRepository, CalculosRepository>();

var app = builder.Build();

//Aquí se inyecta el servicio IcalculosRepository directamente en el endpoint a través del parámetro CalculosRepository.
app.MapGet("/multiplicar", (int a, int b, IcalculosRepository CalculosRepository) =>
{
    return Results.Ok(CalculosRepository.multiplicar(a, b));
});

app.Run();
