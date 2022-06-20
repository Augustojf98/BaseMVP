using System;

namespace BaseMVP.Entities.Classes
{
    public class UserSchema: User
    {
        public UserSchema()
        {
        }
        public UserSchema(string name, DateTime bDate) : base(name, bDate)
        {
        }
        public UserSchema(UserDTO userDTO)
        {
            Id = userDTO.Id;
            Name = userDTO.Name;
            Birth_Date = userDTO.Birth_Date;
            Active = userDTO.Active;
        }

        public static explicit operator UserSchema(UserDTO userDTO)
        {
            UserSchema user = new UserSchema();
            user.Id = userDTO.Id;
            user.Name = userDTO.Name;
            user.Birth_Date = userDTO.Birth_Date;
            user.Active = userDTO.Active;
            return user;
        }
    }
}
