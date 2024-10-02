using EmailSenderApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Adiciona o servi�o de EmailService como um servi�o do projeto
builder.Services.AddScoped<IEmailService, EmailService>();

// Adiciona suporte aos controladores (Controllers)
builder.Services.AddControllers();

// Adiciona suporte ao Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Adiciona suporte � configura��o do appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var app = builder.Build();

// Configura o pipeline de requisi��es HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();

    // Ativa o Swagger
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Email API V1");
    });
}

app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
