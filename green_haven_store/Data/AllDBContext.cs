using green_haven_store.Models;
using Microsoft.EntityFrameworkCore;

namespace green_haven_store.Data
{
    public class AllDBContext : DbContext
    {
        public AllDBContext()
        {
        }
        public AllDBContext(DbContextOptions<AllDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Product
            modelBuilder.Entity<Product>()
            .HasOne(t => t.Category)
            .WithMany(t => t.Products)
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

            // Category
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Cat_Id = 1,
                    Cat_Name = "Plant",
                    Cat_Date = DateTime.Now,
                    Cat_IsActive = true
                });

            // User
            modelBuilder.Entity<User>().HasData(
               new User
               {
                   U_Id = 1,
                   U_Email = "test@gmail.com",
                   U_Address = "65 Foresthead Street, Kitchener",
                   U_Password = "test",
                   U_Name = "Test Customer",
                   U_Pincode = "N2L 3T6",
                   U_Type = "user",
                   U_IsActive = true
               }, new User
               {
                   U_Id = 2,
                   U_Email = "admin@gmail.com",
                   U_Address = "Canada",
                   U_Password = "admin",
                   U_Name = "Admin",
                   U_Pincode = "N2L 3T6",
                   U_Type = "admin",
                   U_IsActive = true
               });

            // Product
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    P_Id = 1,
                    CategoryId = 1,
                    P_Name = "Bromeliad Pineapple",
                    P_Price = "79.99",
                    P_Description = "Have you ever wanted to grow your own adorable pineapple indoors? With the Bromeliad Pineapple, you can! This bromeliad sprouts a fun ornamental pineapple from a whorl of long green leaves. Sure to spark a conversation with your next dinner guests, you’ll know your pineapple fruit has reached maturity when it turns fragrant and a brighter shade of yellow. Although this variety is not grown for edible consumption, it’s still a unique gift or addition to any personal collection.",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_bromeliad-pineapple_charcoal-e1625250094764.jpg?ver=279190",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 2,
                    CategoryId = 1,
                    P_Name = "Parlor Palm",
                    P_Price = "69.99",
                    P_Description = "Easy and graceful, with lush dark green fronds",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_parlor-palmloomscape_slate-e1625249746437.jpg?ver=279264",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 3,
                    CategoryId = 1,
                    P_Name = "Monstera Deliciosa",
                    P_Price = "84.99",
                    P_Description = "The Monstera deliciosa, also known as the Swiss Cheese Plant, is a striking tropical plant originating from the rainforests of southern Mexico. Lively and wild with large, tropical leaves make it a popular choice for indoor gardens.",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_monstera_alt_clay.jpg?ver=279414",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 4,
                    CategoryId = 1,
                    P_Name = "Bird of Paradise",
                    P_Price = "139.99",
                    P_Description = "Impressive and tropical with large, glossy leaves that naturally split over time.",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_bird-of-paradise_indigo.jpg?ver=279491",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 5,
                    CategoryId = 1,
                    P_Name = "Bamboo Palm",
                    P_Price = "160.99",
                    P_Description = "With dense foliage and lush fronds, the Bamboo Palm makes a statement. An air-purifying plant adaptable to low light, this palm can reach heights of up to 8 feet tall in the right conditions.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_bamboo-palm_stone.jpg?ver=279484",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 6,
                    CategoryId = 1,
                    P_Name = "Money Tree",
                    P_Price = "135.99",
                    P_Description = "Popular for its use in Feng Shui, the Money Tree is a pet-friendly and air-purifying plant with large star-shaped leaves and a braided trunk to give your home a tropical feel.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_money-tree_slate-e1643402075928.jpg?ver=279409",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 7,
                    CategoryId = 1,
                    P_Name = "Sansevieria",
                    P_Price = "119.99",
                    P_Description = "Architectural and sturdy, this plant is easy to care for and highly adaptable. Also known as a Snake Plant and Mother-in-Law’s Tongue.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_sansevieria_charcoal-e1633460982733.jpg?ver=279439",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 8,
                    CategoryId = 1,
                    P_Name = "Parlor Palm",
                    P_Price = "69.99",
                    P_Description = "Easy and graceful, with lush dark green fronds",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_parlor-palmloomscape_slate-e1625249746437.jpg?ver=279264",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 9,
                    CategoryId = 1,
                    P_Name = "Burgundy Rubber Tree",
                    P_Price = "84.99",
                    P_Description = "Robust and dramatic, the Burgundy Rubber Tree sports large, glossy leaves on durable, upright stems. This striking plant is ready to make a statement in your home with its dark, moody color palette ranging from deep forest green to rich burgundy red. This low-maintenance plant will be happiest in a spot with bright indirect light.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2022/08/bloomscape_burgundy-rubber-tree_lg_charcoal.jpg?ver=927090",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 10,
                    CategoryId = 1,
                    P_Name = "Parlor Palm",
                    P_Price = "69.99",
                    P_Description = "Easy and graceful, with lush dark green fronds",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_parlor-palmloomscape_slate-e1625249746437.jpg?ver=279264",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 11,
                    CategoryId = 1,
                    P_Name = "ZZ Plant",
                    P_Price = "119.99",
                    P_Description = "With graceful layered leaves, the hardy ZZ Plant is a statement plant that can reach up to 2.5 feet tall. This drought-tolerant plant is tough, beautiful, and nearly indestructible.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/08/bloomscape_zz-plant_charcoal-e1625251444532.jpg?ver=279472",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 12,
                    CategoryId = 1,
                    P_Name = "Bromeliad Summer",
                    P_Price = "49.99",
                    P_Description = "A colorful and low-maintenance air plant, the Bromeliad Summer is a striking houseplant that adds warmth to your home. With a bright and cheery magenta flower, this bromeliad is pet-friendly and makes for a great gift for any plant lover or beginner plant parent.",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2023/04/BloomScape_SM_Bromeliad-Summer_white-scaled.jpg?ver=1022056",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 13,
                    CategoryId = 1,
                    P_Name = "Monstera Adansonii",
                    P_Price = "79.99",
                    P_Description = "Easy-going Monstera with striking, fenestrated leaves. Also known as the Swiss Cheese Vine.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2019/11/bloomscape_monstera-adansonii_md_clay-scaled-e1721151440768.jpg?ver=440338",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 14,
                    CategoryId = 1,
                    P_Name = "Ficus Tineke",
                    P_Price = "79.99",
                    P_Description = "The Ficus Tineke (Ficus elastica), the vibrant younger sibling of our popular Burgundy Rubber Tree. Boasting variegated leaves in hues of creamy pink, yellow, and green, this rubber tree is a captivating presence in any interior setting, whether it graces your space solo or thrives amidst your collection.\r\n\r\n",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2023/08/230805_BB_fiscus_elastica_tineke_slate-scaled-e1692911022828.jpg?ver=1057588",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                },
                new Product
                {
                    P_Id = 15,
                    CategoryId = 1,
                    P_Name = "Neon Prayer Plant",
                    P_Price = "69.99",
                    P_Description = "Vibrant and bright with patterned, neon green leaves.",
                    P_PictureUri = "https://bloomscape.com/wp-content/uploads/2020/09/bloomscape_neon-prayer-plant_charcoal-e1625250223838.jpg?ver=292318",
                    P_IsActive = true,
                    P_Date = DateTime.Now,
                }
                );
        }
    }
}
