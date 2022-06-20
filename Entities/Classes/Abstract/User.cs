using System;

namespace BaseMVP.Entities.Classes
{
    public abstract class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime Birth_Date { get; set; }

        public bool Active { get; set; }

        public User(string name, DateTime bDate)
        {
            Name = name;
            Birth_Date = bDate;
            Active = true;
        }

        public User() { }
    }
}
