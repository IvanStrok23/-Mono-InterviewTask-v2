using MonoTask.UI.WebApi.Auth.UserPrincipal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerDocumentation();
builder.Services.AddAutoMapperServices();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddDatabaseContext(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IUserPrincipal, UserPrincipal>();
builder.Services.AddAuthenticationServices();
builder.Services.AddAuthorizationServices();
builder.Services.AddApplicationServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();