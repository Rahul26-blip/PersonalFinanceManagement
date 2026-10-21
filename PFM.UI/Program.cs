using PFM.UI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//httpClient
builder.Services.AddHttpClient();
//seccesion
builder.Services.AddSession();
//httpcontext
builder.Services.AddHttpContextAccessor();
//UI services
builder.Services.AddHttpClient<ApiService>();
builder.Services.AddHttpClient<AccountServices>();
builder.Services.AddHttpClient<CategoryServices>();
builder.Services.AddHttpClient<TransactionServices>();
builder.Services.AddHttpClient<BudgetServices>();
builder.Services.AddHttpClient<SavingGoleServices>();
builder.Services.AddHttpClient<DebtServices>();
builder.Services.AddHttpClient<InvestmentServices>();
builder.Services.AddHttpClient<UserServices>();
builder.Services.AddHttpClient<RoleServices>();
builder.Services.AddHttpClient<PermissionServices>();
builder.Services.AddHttpClient<RolePermissionServices>();
builder.Services.AddHttpClient<UserRoleServices>();
//builder.Services.AddHttpClient<>();
//builder.Services.AddHttpClient<>();
builder.Services.AddHttpClient<DashboardServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Login}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
