using DataAccess.Data;
using DataAccess.Repositories.Implementations;
using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Api.SeedData.DataSeeding;

var builder = WebApplication.CreateBuilder(args);

// ==========================
// Add Services
// ==========================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// DB Context
builder.Services.AddDbContext<GirlsHostelDbContext>(options =>
    options.UseSqlServer(builder.Configuration
        .GetConnectionString("DefaultConnection")));

// ==========================
// Register Repositories
// ==========================
builder.Services.AddScoped<IEntryLogRepository, EntryLogRepository>();
builder.Services.AddScoped<IFoodAttendanceRepository, FoodAttendanceRepository>();
builder.Services.AddScoped<IGirlRepository, GirlRepository>();
builder.Services.AddScoped<IHostelRepository, HostelRepository>();
builder.Services.AddScoped<IHostelAttendanceRepository, HostelAttendanceRepository>();
builder.Services.AddScoped<IMarketVisitRepository, MarketVisitRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IRoomHistoryRepository, RoomHistoryRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();
builder.Services.AddScoped<IVisitorRepository, VisitorRepository>();
builder.Services.AddScoped<IVisitorEntryLogRepository, VisitorEntryLogRepository>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<GirlsHostelDbContext>();

    await SeedData.InitializeAsync(context);
}

app.UseHttpsRedirection();

app.UseDefaultFiles();   
app.UseStaticFiles();    

app.UseAuthorization();

// APIs
app.MapControllers();

app.Run();