using System;
using Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Hahn.ApplicationProcess.Application.Hahn.ApplicatonProcess.December2020.Data {
    public class ApplicationDbContext : DbContext {
        public ApplicationDbContext (DbContextOptions<ApplicationDbContext> options) : base (options) {

        }

        public DbSet<Applicant> Applicant { get; set; }
    }
}