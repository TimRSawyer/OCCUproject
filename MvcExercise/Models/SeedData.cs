using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcExercise.Data;
using System;
using System.Linq;

namespace MvcExercise.Models
{
    public static class SeedData
    {
        // static Random for StatusValue population
        static Random rand = new Random();

        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MvcExerciseContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<MvcExerciseContext>>()))
            {
                // Look for any data
                if (context.SimpleDataSet != null && context.SimpleDataSet.Any())
                {
                    return;   // DB has been seeded
                }

                // populate StatusValues
                List<String> s = new List<string> { "Fail", "Warn", "Pass" };
                DateTime now = DateTime.Now;
                for (int i = 0; i < 37; i++)
                {
                    context.Add(
                        new StatusValues
                        {
                            ID = Guid.NewGuid(),
                            StatusValue = s[rand.Next(s.Count)]
                        }
                    );
                }

                context.SimpleDataSet.AddRange(
                    new SimpleDataSet
                    {
                        Name = "Hikaru Sulu",
                        Rank = "Lieutenant",
                        Position = "Helmsman",
                        Posting = "USS Enterprise"                        
                    },

                    new SimpleDataSet
                    {
                        Name = "James Tiberius Kirk",
                        Rank = "Captain",
                        Position = "Commanding Officer",
                        Posting  = "USS Enterprise"
                    },

                    new SimpleDataSet
                    {
                        Name = "Leonard 'Bones' McCoy",
                        Rank = "Lieutenant Commander",
                        Position = "Chief Medical Officer",
                        Posting = "USS Enterprise"
                    },

                    new SimpleDataSet
                    {
                        Name = "Montgomery 'Scotty' Scott",
                        Rank = "Lieutenant Commander",
                        Position = "Chief Engineer",
                        Posting = "USS Enterprise"
                    },

                    new SimpleDataSet
                    {
                        Name = "Nyota Uhura",
                        Rank = "Lieutenant",
                        Position = "Communications Officer",
                        Posting = "USS Enterprise"
                    },

                    new SimpleDataSet
                    {
                        Name = "Pavel Chekov",
                        Rank = "Ensign",
                        Position = "Navigator",
                        Posting = "USS Enterprise"
                    },

                    new SimpleDataSet
                    {
                        Name = "Spock",
                        Rank = "Commander",
                        Position = "First Officer",
                        Posting = "USS Enterprise"
                    }
                );
                context.SaveChanges();
            }
        }
    }
}
