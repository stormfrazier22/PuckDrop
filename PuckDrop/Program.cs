using PuckDrop.Startup;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.RegisterServices();
var app = builder.Build();


app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  
});

app.Run();
