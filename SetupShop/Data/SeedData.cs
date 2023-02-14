using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.EntityFrameworkCore;
using SetupShop.Models;

namespace SetupShop.Data
{
    public class SeedData
    {
        public static void SeedDatabase(SetupContext context)
        {
            context.Database.Migrate();

            if (!context.Setup.Any())
            {
                context.Setup.AddRange(
                    new Setup
                    {
                        Name = "Base Setup",
                        Author = "Samuel Ward",
                        File = File.ReadAllBytes(@"C:\Users\samue\Documents\slw_gt3r_base_23s1.sto"),
                        Car = "Porsche 911 GT3 R",
                        Track = "None",
                        Season = "2023 Season 1",
                        Week = "None",
                        Series = "None",
                        VideoUrl = "None",
                        Price = 0.50M,
                        Image = "gt3r.jpg"
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
