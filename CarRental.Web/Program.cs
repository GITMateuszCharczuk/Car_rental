using CarRental.Web.Data;
using CarRental.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalBlogDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CarRentalAuthDbConnectionString")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    // Default password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequiredUniqueChars = 1;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Login";
    options.AccessDeniedPath = "/AccessDenied";
});

builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IImageRepository, ImageRepositoryCloudinary>();
builder.Services.AddScoped<IBlogPostLikeRepository, BlogPostLikeRepository>();
builder.Services.AddScoped<IBlogPostCommentRepository, BlogPostCommentRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

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
app.MapControllers();

app.Run();