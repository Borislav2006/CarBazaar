using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CarBazaar.Models;

public partial class AppDbContext : IdentityDbContext<User>
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Listing> Listings { get; set; }
    public DbSet<ListingImage> ListingImages { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<ListingImage>()
            .HasOne(li => li.Listing) // Each ListingImage has one Listing
            .WithMany(l => l.ListingImages) // Each Listing can have many ListingImages
            .HasForeignKey(li => li.ListingId); // Foreign key in ListingImage pointing to Listing
    }
}
