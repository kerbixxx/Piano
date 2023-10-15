using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace piano
{
    public class Song : Player
    {
        [Key]
        public int? Id { get; set; }
        public string SongName { get; set; }
        public Song(string text, int bpm) : base(text, bpm)
        {

        }
        public Song(string songName, string text, int tact) : base (text,tact)
        {
            SongName = songName;
        }
        public Song(int id, string songName, string text, int tact) : base (text,tact)
        {
            Id= id;
            SongName = songName;
        }

    }
    public class SongDb : DbContext
    {
        public DbSet<Song> Songs { get; set; }
        public SongDb()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename = database.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            var songs = new Song[]
            {
                new Song ("Blank","",100) {Id = 0},
        };
            modelBuilder.Entity<Song>().HasData(songs);
        }
    }
}
