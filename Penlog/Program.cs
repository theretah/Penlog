using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Penlog.Data.Context;
using Penlog.Data.Repository;
using Penlog.Data.Repository.IRepository;
using Penlog.Entities;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("PenlogConnection");

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddDbContext<PenlogDbContext>
    (options => options.UseSqlServer(connectionString));

builder.Services.AddIdentity<AppUser, IdentityRole>(
    options =>
    {
        options.SignIn.RequireConfirmedAccount = true;
        options.User.AllowedUserNameCharacters =
        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ_ ";
    }
).AddEntityFrameworkStores<PenlogDbContext>()
.AddDefaultUI()
.AddTokenProvider<DataProtectorTokenProvider<AppUser>>(TokenOptions.DefaultProvider);

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
