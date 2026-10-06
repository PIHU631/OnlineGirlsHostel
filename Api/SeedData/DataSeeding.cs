using DataAccess.Data;
using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.SeedData
{
    public class DataSeeding
    {
        public static class SeedData
        {
            public static async Task InitializeAsync(GirlsHostelDbContext context)
            {
                // Apply migrations
                if ((await context.Database.GetPendingMigrationsAsync()).Any())
                {
                    await context.Database.MigrateAsync();
                }

                // Prevent duplicate seed
                if (context.Hostels.Any())
                    return;

                // =============================
                // HOSTEL (Single)
                // =============================
                var hostel = new Hostel
                {
                    HostelName = "Girls Hostel Mandla",
                    Address = "Mandla Main Area",
                    City = "Mandla",
                    WardenName = "Warden 1",
                    WardenPhone = "9990000001",
                    IsActive = true
                };

                context.Hostels.Add(hostel);
                await context.SaveChangesAsync();

                // =============================
                // ROOMS
                // =============================
                var rooms = new List<Room>();

                for (int i = 1; i <= 5; i++)
                {
                    rooms.Add(new Room
                    {
                        HostelId = hostel.HostelId,   
                        RoomNumber = (100 + i).ToString(),
                        Capacity = 4
                    });
                }

                context.Rooms.AddRange(rooms);
                await context.SaveChangesAsync();

                // =============================
                // STAFF
                // =============================
                var staffList = new List<Staff>();

                for (int i = 1; i <= 5; i++)
                {
                    staffList.Add(new Staff
                    {
                        Name = $"Staff {i}",
                        Phone = "90000000" + i,
                        HostelId = hostel.HostelId   // ✅ FIX
                    });
                }

                context.Staffs.AddRange(staffList);
                await context.SaveChangesAsync();

                // =============================
                // GIRLS
                // =============================
                var girls = new List<Girl>();

                for (int i = 1; i <= 10; i++)
                {
                    girls.Add(new Girl
                    {
                        Name = $"Girl {i}",
                        Phone = "80000000" + i,
                        GuardianName = $"Guardian {i}",
                        GuardianPhone = "90000000" + i,
                        Address = $"Village {i}, Mandla",
                        RoomId = rooms[i % rooms.Count].RoomId,
                        IsActive = true
                    });
                }

                context.Girls.AddRange(girls);
                await context.SaveChangesAsync();

                // =============================
                // ROOM HISTORY
                // =============================
                var histories = girls.Select(g => new RoomHistory
                {
                    GirlId = g.GirlId,
                    RoomId = g.RoomId,
                    StartDate = DateTime.UtcNow.AddDays(-10)
                }).ToList();

                context.RoomHistories.AddRange(histories);
                await context.SaveChangesAsync();

                // =============================
                // FOOD ATTENDANCE
                // =============================
                var foodAttendances = girls.Select(g => new FoodAttendance
                {
                    GirlId = g.GirlId,
                    Date = DateTime.Today,
                    MealType = Common.Enums.MealType.Lunch
                }).ToList();

                context.FoodAttendances.AddRange(foodAttendances);
                await context.SaveChangesAsync();

                // =============================
                // ENTRY LOGS
                // =============================
                var entryLogs = girls.Select(g => new EntryLog
                {
                    GirlId = g.GirlId,
                    OutTime = DateTime.Now.AddHours(-3),
                    InTime = DateTime.Now,
                    Reason = "Test Entry"
                }).ToList();

                context.EntryLogs.AddRange(entryLogs);
                await context.SaveChangesAsync();

                // =============================
                // MARKET VISITS
                // =============================
                var visits = girls.Select(g => new MarketVisit
                {
                    GirlId = g.GirlId,
                    OutTime = DateTime.Now.AddHours(-2),
                    InTime = DateTime.Now,
                    Reason = "Shopping"
                }).ToList();

                context.MarketVisits.AddRange(visits);
                await context.SaveChangesAsync();

                // =============================
                // HOSTEL ATTENDANCE
                // =============================
                var attendances = girls.Select(g => new HostelAttendance
                {
                    GirlId = g.GirlId,
                    Date = DateTime.Today,
                    IsPresent = true
                }).ToList();

                context.HostelAttendances.AddRange(attendances);
                await context.SaveChangesAsync();

                // =============================
                // VISITORS
                // =============================
                var visitors = new List<Visitor>();

                for (int i = 1; i <= 10; i++)
                {
                    visitors.Add(new Visitor
                    {
                        Name = $"Visitor {i}",
                        Phone = "70000000" + i,
                        IdProofNumber = "ID" + (1000 + i),
                        Relation = "Father"
                    });
                }

                context.Visitors.AddRange(visitors);
                await context.SaveChangesAsync();

                // =============================
                // VISITOR ENTRY LOG
                // =============================
                var visitorLogs = new List<VisitorEntryLog>();

                for (int i = 0; i < visitors.Count; i++)
                {
                    visitorLogs.Add(new VisitorEntryLog
                    {
                        VisitorId = visitors[i].VisitorId,
                        GirlId = girls[i % girls.Count].GirlId,
                        InTime = DateTime.Now.AddMinutes(-30),
                        OutTime = DateTime.Now,
                        Purpose = "Meeting"
                    });
                }

                context.VisitorEntryLogs.AddRange(visitorLogs);
                await context.SaveChangesAsync();
            }
        }
    }
}