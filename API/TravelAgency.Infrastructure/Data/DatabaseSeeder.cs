using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TravelAgency.Application;
using TravelAgency.Application.Services.Interfaces;
using TravelAgency.Domain.Entities;
using TravelAgency.Domain.Helpers;
using TravelAgency.Shared.Enum;

namespace TravelAgency.Infrastructure.Data
{
    public class DatabaseSeeder
    {
        public static async Task SeedDatabase(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<TravelAgencyDbContext>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var configuration = scope.ServiceProvider.GetRequiredService<Config>();
            var imageService = scope.ServiceProvider.GetRequiredService<IImageService>();
            var adminRoleName = Roles.AdminRoleName;
            var dataExists = await dbContext.Users.AnyAsync();

            var roleExist = await roleManager.RoleExistsAsync(adminRoleName);
            if (!roleExist) await roleManager.CreateAsync(new IdentityRole(adminRoleName));

            if (string.IsNullOrWhiteSpace(configuration.AdminUserEmail) || string.IsNullOrWhiteSpace(configuration.AdminUserPassword))
                throw new Exception(
                    "You need to provide a default user account which will be created with the Admin role, keys: AppSettings:AdminUserEmail and AppSettings:AdminUserPassword");

            var defaultUser = new ApplicationUser
            {
                UserName = configuration.AdminUserEmail,
                Email = configuration.AdminUserEmail
            };

            var user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);

            if (user == null)
            {
                var createPowerUser = await userManager.CreateAsync(defaultUser, configuration.AdminUserPassword);
                if (createPowerUser.Succeeded)
                {
                    await userManager.AddToRoleAsync(defaultUser, Roles.AdminRoleName);
                    user = await userManager.FindByEmailAsync(configuration.AdminUserEmail);
                }
            }
            else
            {
                var isAdmin = await userManager.IsInRoleAsync(user, Roles.AdminRoleName);
                if (!isAdmin) await userManager.AddToRoleAsync(user, Roles.AdminRoleName);
            }

            if (user != null && !dataExists)
            {
                await SeedSampleData(dbContext, imageService, user);
            }
        }

        private static async Task SeedSampleData(TravelAgencyDbContext dbContext, IImageService imageService, ApplicationUser user)
        {
            var esImage = await UploadImageFromSeedingImages("country-es.jpg", user, imageService);
            var frImage = await UploadImageFromSeedingImages("country-fr.jpg", user, imageService);
            var barcelonaBungalowParkImage = await UploadImageFromSeedingImages("bungalow-park-barcelona.jpg", user, imageService);
            var barcelonaBungalowImage = await UploadImageFromSeedingImages("bungalow-barcelona.jpg", user, imageService);
            var hotelMadridImage = await UploadImageFromSeedingImages("hotel-madrid.jpg", user, imageService);
            var hotelMadridDeluxeSuiteImage = await UploadImageFromSeedingImages("hotel-madrid-deluxe-suite.jpg", user, imageService);
            var niceGuesthouse = await UploadImageFromSeedingImages("nice-guesthouse.jpg", user, imageService);
            var niceGuesthouseRoom = await UploadImageFromSeedingImages("nice-guesthouse-room.jpg", user, imageService);
            var villaParis = await UploadImageFromSeedingImages("villa-paris.jpg", user, imageService);
            var villaParisSuite = await UploadImageFromSeedingImages("villa-paris-parisian-suite.jpg", user, imageService);

            var countries = new[] {
              new Country("ES", new GeoCoordinates(40.2085, -3.713), new List<EntityImage>()
              {
                  { new EntityImage(true, esImage!.Id) }
              }),
              new Country("FR", new GeoCoordinates(46.603354, 1.888334), new List<EntityImage>()
              {
                  { new EntityImage(true, frImage!.Id) }
              }),
            };

            var locations = new[] {
              new Location(
                  new [] {
                    new TranslatedText("en", "Hotel Madrid"),
                      new TranslatedText("nl", "Hotel Madrid"),
                      new TranslatedText("es", "Hotel Madrid")
                  },
                  new [] {
                    new TranslatedText("en", "A beautiful hotel in the heart of Madrid."),
                      new TranslatedText("nl", "Een prachtig hotel in het hart van Madrid."),
                      new TranslatedText("es", "Un hermoso hotel en el corazón de Madrid.")
                  },
                  LocationType.Hotel, countries[0], new GeoCoordinates(40.4167, -3.7038), new List<EntityImage>()
                  {
                      { new EntityImage(true, hotelMadridImage!.Id) }
                  }),
                new Location(
                  new [] {
                    new TranslatedText("en", "Barcelona Bungalow Park"),
                      new TranslatedText("nl", "Bungalowpark Barcelona"),
                      new TranslatedText("es", "Parque de Bungalows Barcelona")
                  },
                  new [] {
                    new TranslatedText("en", "A charming bungalow park in Barcelona."),
                      new TranslatedText("nl", "Een charmant bungalowpark in Barcelona."),
                      new TranslatedText("es", "Un encantador parque de bungalows en Barcelona.")
                  },
                  LocationType.BungalowPark, countries[0], new GeoCoordinates(41.3851, 2.1734), new List<EntityImage>()
                  {
                      { new EntityImage(true, barcelonaBungalowParkImage!.Id) }
                  }),
                new Location(
                  new [] {
                    new TranslatedText("en", "Paris Villa"),
                      new TranslatedText("nl", "Parijs Villa"),
                      new TranslatedText("es", "Villa París")
                  },
                  new [] {
                    new TranslatedText("en", "A beautiful villa in Paris, France."),
                      new TranslatedText("nl", "Een prachtige villa in Parijs, Frankrijk."),
                      new TranslatedText("es", "Una hermosa villa en París, Francia.")
                  },
                  LocationType.Villa, countries[1], new GeoCoordinates(48.8566, 2.3522), new List<EntityImage>()
                  {
                      { new EntityImage(true, villaParis!.Id) }
                  }),
                new Location(
                  new [] {
                    new TranslatedText("en", "Nice Guesthouse"),
                      new TranslatedText("nl", "Nice Pension"),
                      new TranslatedText("es", "Casa de Huéspedes Niza")
                  },
                  new [] {
                    new TranslatedText("en", "A cozy guesthouse in Nice, France."),
                      new TranslatedText("nl", "Een gezellig pension in Nice, Frankrijk."),
                      new TranslatedText("es", "Una acogedora casa de huéspedes en Niza, Francia.")
                  },
                  LocationType.GuestHouse, countries[1], new GeoCoordinates(43.7102, 7.2620), new List<EntityImage>()
                  {
                      { new EntityImage(true, niceGuesthouse!.Id) }
                  })
            };

            var residences = new[] {
              new Residence(
                  new [] {
                    new TranslatedText("en", "Deluxe Suite"),
                      new TranslatedText("nl", "Deluxe Suite"),
                      new TranslatedText("es", "Suite Deluxe")
                  },
                  new [] {
                    new TranslatedText("en", "A spacious suite with a king-size bed and stunning city views."),
                      new TranslatedText("nl", "Een ruime suite met een kingsize bed en een prachtig uitzicht op de stad."),
                      new TranslatedText("es", "Una suite espaciosa con una cama king-size y vistas impresionantes de la ciudad.")
                  },
                  locations[0], new GeoCoordinates(40.4182, -3.7008), new List<EntityImage>()
                  {
                      { new EntityImage(true, hotelMadridDeluxeSuiteImage!.Id) }
                  }),
                new Residence(
                  new [] {
                    new TranslatedText("en", "Barcelona Bungalow"),
                      new TranslatedText("nl", "Barcelona Bungalow"),
                      new TranslatedText("es", "Bungalow Barcelona")
                  },
                  new [] {
                    new TranslatedText("en", "A stylish bungalow with a private garden and city view."),
                      new TranslatedText("nl", "Een stijlvolle bungalow met een privétuin en uitzicht op de stad."),
                      new TranslatedText("es", "Un bungalow elegante con jardín privado y vista a la ciudad.")
                  },
                  locations[1], new GeoCoordinates(41.3875, 2.1699), new List<EntityImage>()
                  {
                      { new EntityImage(true, barcelonaBungalowImage!.Id) }
                  }),
                new Residence(
                  new [] {
                    new TranslatedText("en", "Parisian Suite"),
                      new TranslatedText("nl", "Parijse Suite"),
                      new TranslatedText("es", "Suite Parisina")
                  },
                  new [] {
                    new TranslatedText("en", "A luxurious suite with a view of the Eiffel Tower."),
                      new TranslatedText("nl", "Een luxe suite met uitzicht op de Eiffeltoren."),
                      new TranslatedText("es", "Una suite de lujo con vista a la Torre Eiffel.")
                  },
                  locations[2], new GeoCoordinates(48.8578, 2.2950), new List<EntityImage>()
                  {
                      { new EntityImage(true, villaParisSuite!.Id) }
                  }),
                new Residence(
                  new [] {
                    new TranslatedText("en", "Comfort Room"),
                      new TranslatedText("nl", "Comfortkamer"),
                      new TranslatedText("es", "Habitación Confort")
                  },
                  new [] {
                    new TranslatedText("en", "A cozy room with modern amenities and a view of the city."),
                      new TranslatedText("nl", "Een gezellige kamer met moderne voorzieningen en uitzicht op de stad."),
                      new TranslatedText("es", "Una habitación acogedora con comodidades modernas y vista a la ciudad.")
                  },
                  locations[3], new GeoCoordinates(43.7122, 7.2608), new List<EntityImage>()
                  {
                      { new EntityImage(true, niceGuesthouseRoom!.Id) }
                  })
            };

            dbContext.Countries.AddRange(countries);
            dbContext.Locations.AddRange(locations);
            dbContext.Residences.AddRange(residences);

            await dbContext.SaveChangesAsync();
        }

        private static async Task<Image?> UploadImageFromSeedingImages(string fileName, ApplicationUser user, IImageService imageService)
        {
            string imageFolderPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Seeding\\Images");
            string imagePath = Path.Combine(imageFolderPath, fileName);

            if (File.Exists(imagePath))
            {
                byte[] imageData = await File.ReadAllBytesAsync(imagePath);
                var formFile = CreateIFormFile(fileName, imageData);
                if(formFile != null)
                {
                    return await imageService.AddImage(formFile, user);
                }
            }

            return null;
        }

        private static IFormFile? CreateIFormFile(string fileName, byte[] fileBytes)
        {
            if (string.IsNullOrEmpty(fileName) || fileBytes == null || fileBytes.Length == 0)
            {
                return null;
            }

            var stream = new MemoryStream(fileBytes);
            var formFile = new FormFile(stream, 0, fileBytes.Length, "file", fileName);
            return formFile;
        }
    }
}
