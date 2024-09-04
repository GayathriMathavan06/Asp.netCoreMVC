var builder = WebApplication.CreateBuilder(args);  //creates empty web application

// Add services to the container.
builder.Services.AddControllersWithViews();  //identifies controller and ots relevant views then into a container

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();  //for the security in browser
}

app.UseHttpsRedirection();  //automatically connect with https
app.UseStaticFiles();

app.UseRouting();  //to create routing table

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
