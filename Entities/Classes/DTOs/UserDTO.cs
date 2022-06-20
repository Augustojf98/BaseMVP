using System;

namespace BaseMVP.Entities.Classes
{
    public class UserDTO : User
    {
        public UserDTO()
        {
        }
        public UserDTO(string name, DateTime bDate) : base(name, bDate)
        {
        }
        public UserDTO(UserSchema user)
        {
            Id = user.Id;
            Name = user.Name;  
            Birth_Date = user.Birth_Date;
            Active = user.Active;
        }

        public static explicit operator UserDTO(UserSchema user)
        {
            UserDTO userDTO = new UserDTO();
            userDTO.Id = user.Id;
            userDTO.Name = user.Name;
            userDTO.Birth_Date = user.Birth_Date;
            userDTO.Active = userDTO.Active;
            return userDTO;
        }
    }
}
