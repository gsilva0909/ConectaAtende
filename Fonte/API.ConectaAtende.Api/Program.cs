using API.ConectaAtende.Api.Configuracoes;

var builder = WebApplication.CreateBuilder(args);

ConfiguracaoInjecaoDependencias.InjecaoDependencias(builder.Services);

builder.Services.AdicionaConfiguracaoSwagger();

builder.Services.AddControllers();

// outras configurações de serviço que você precise:
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddCors(...);

var app = builder.Build();

app.UtilizaConfiguracaoSwagger(app.Environment);

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

// eventuais middlewares extras:
// app.UseMiddleware<MeuMiddleware>();
// app.UseCors(...);

app.Run();