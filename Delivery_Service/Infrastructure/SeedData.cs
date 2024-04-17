using Delivery_Service.Domain;
using DeliveryService.Infrastructure;
using Microsoft.EntityFrameworkCore;

public class SeedData
{
    public static async void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MasterContext(
            serviceProvider.GetRequiredService<DbContextOptions<MasterContext>>()))
        {
            context.Database.Migrate();
            if (!context.Driver.Any())
            {
                var drivers = new List<Driver>
                {
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق سعيد",
                        Email = "driver1@example.com",
                        Password = "password1",
                        PhoneNumber = "+967 771234567",
                        AvailabilityStatus = "online",
                        IsBusy = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق محمد",
                        Email = "driver2@example.com",
                        Password = "password2",
                        PhoneNumber = "+967 772345678",
                        AvailabilityStatus = "offline",
                        IsBusy = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق علي",
                        Email = "driver3@example.com",
                        Password = "password3",
                        PhoneNumber = "+967 773456789",
                        AvailabilityStatus = "online",
                        IsBusy = true,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق أحمد",
                        Email = "driver4@example.com",
                        Password = "password4",
                        PhoneNumber = "+967 774567890",
                        AvailabilityStatus = "online",
                        IsBusy = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق خالد",
                        Email = "driver5@example.com",
                        Password = "password5",
                        PhoneNumber = "+967 775678901",
                        AvailabilityStatus = "online",
                        IsBusy = true,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق عبدالله",
                        Email = "driver6@example.com",
                        Password = "password6",
                        PhoneNumber = "+967 776789012",
                        AvailabilityStatus = "online",
                        IsBusy = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق حسين",
                        Email = "driver7@example.com",
                        Password = "password7",
                        PhoneNumber = "+967 777890123",
                        AvailabilityStatus = "online",
                        IsBusy = true,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق عمر",
                        Email = "driver8@example.com",
                        Password = "password8",
                        PhoneNumber = "+967 778901234",
                        AvailabilityStatus = "online",
                        IsBusy = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق فارس",
                        Email = "driver9@example.com",
                        Password = "password9",
                        PhoneNumber = "+967 779012345",
                        AvailabilityStatus = "offline",
                        IsBusy = false,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    },
                    new Driver {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        Name = "سائق رامي",
                        Email = "driver10@example.com",
                        Password = "password10",
                        PhoneNumber = "+967 771012345",
                        AvailabilityStatus = "online",
                        IsBusy = true,
                        CreateTime = DateTime.Now,
                        UpdateTime = DateTime.Now
                    }
                };



                await context.Driver.AddRangeAsync(drivers);
                context.SaveChanges();

                var halfOfhour = DateTime.Now.AddMinutes(30);
                var hour = DateTime.Now.AddHours(1);
                var deliveryRequests = new List<DeliveryRequest>
                {
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "1",
                        OrderCode = "ORD001",
                        CompoundName = "Restaurant A",
                        Status = "pickedup",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = hour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "2",
                        OrderCode = "ORD002",
                        CompoundName = "Restaurant B",
                        Status = "onway",
                        CreateTime = DateTime.Now,
                        OnwayTime = hour,
                        DeliveredTime = hour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "3",
                        OrderCode = "ORD003",
                        CompoundName = "Restaurant C",
                        Status = "delivered",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = hour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "4",
                        OrderCode = "ORD004",
                        CompoundName = "Restaurant D",
                        Status = "pickedup",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = halfOfhour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "5",
                        OrderCode = "ORD005",
                        CompoundName = "Restaurant E",
                        Status = "pickedup",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = halfOfhour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "6",
                        OrderCode = "ORD006",
                        CompoundName = "Restaurant F",
                        Status = "onway",
                        CreateTime = DateTime.Now,
                        OnwayTime = hour,
                        DeliveredTime = hour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "7",
                        OrderCode = "ORD007",
                        CompoundName = "Restaurant G",
                        Status = "pickedup",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = halfOfhour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "8",
                        OrderCode = "ORD008",
                        CompoundName = "Restaurant H",
                        Status = "delivered",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = hour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "9",
                        OrderCode = "ORD009",
                        CompoundName = "Restaurant I",
                        Status = "pickedup",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = halfOfhour
                    },
                    new DeliveryRequest {
                        Id = Guid.NewGuid().ToString(), // Generate a unique Id for each object
                        OrderId = "10",
                        OrderCode = "ORD010",
                        CompoundName = "Restaurant J",
                        Status = "onway",
                        CreateTime = DateTime.Now,
                        OnwayTime = halfOfhour,
                        DeliveredTime = hour
                    }
                };
                await context.RequestForDelivery.AddRangeAsync(deliveryRequests);
                context.SaveChanges();
            }
        }
    }

}