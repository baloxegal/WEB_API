using WEB_API.DTO;
using WEB_API.Models;

namespace WEB_API.Infrastructure
{
    public class UserRepository : GenericRepository<User, UserDTO>
    {
        public UserRepository(UserContext userContext) : base(userContext)
        {

        }
        public override UserDTO UserToUserDTO(User user) =>
            new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                IsEmployee = user.IsEmployee
            };
        public override User UserDTOToUser(UserDTO userDTO) =>
            new User
            {
                Id = userDTO.Id,
                Name = userDTO.Name,
                IsEmployee = userDTO.IsEmployee
            };

        public override void ModifyUserDTO(UserDTO userBase, UserDTO user)
        {
            userBase.Id = user.Id;
            userBase.Name = user.Name;
            userBase.IsEmployee = user.IsEmployee;
        }
        public override void ModifyUser(User userBase, User user)
        {
            userBase.Id = user.Id;
            userBase.Name = user.Name;
            userBase.IsEmployee = user.IsEmployee;
            userBase.Secret = user.Secret;
        }
    }
}
