using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using QuanLyNhaSach.DataAccess;
using QuanLyNhaSach.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie(options => {
    options.LoginPath = "/User/Login";
    options.LogoutPath = "/User/Logout";
    options.AccessDeniedPath = "/AccessDenied";
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("isAdmin", policy =>
	{
		policy.RequireRole("Admin");
	});
});

builder.Services.AddRazorPages();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapRazorPages();

app.Run();
