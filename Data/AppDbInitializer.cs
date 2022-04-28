﻿using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using My_Books.Data.Models;

namespace My_Books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using(var serviceScope= applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();
                //if we don't have any books into our db
                if(!context.Books.Any())
                {
                    context.Books.AddRange(new Book()
                    {
                        Title = "1st Book Title",
                        Description = "1st Book Description",
                        IsRead = true,
                        DateRead = DateTime.Now.AddDays(-10),
                        Rate = 4,
                        Genre = "Biography",
                        //Author = "First Author",
                        CoverUrl = "https://",
                        DateAdded = DateTime.Now
                    },
                    new Book()
                    {
                        Title = "2nd Book Title",
                        Description = "2nd Book Description",
                        IsRead = false,
                        Rate = 3,
                        Genre = "Biography",
                        //Author = "Second Author",
                        CoverUrl = "https://",
                        DateAdded = DateTime.Now
                    }) ;
                    context.SaveChanges();
                }
            }
        }
    }
}