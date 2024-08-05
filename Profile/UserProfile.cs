using BarberShopAPI2.Controllers.Request.UsersRequests;
using BarberShopAPI2.Models;

namespace BarberShopAPI2.Profile;

public class UserProfile : AutoMapper.Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateRequest, User>();
        CreateMap<UserLoginRequest, User>();
        CreateMap<UserUpdateRequest, User>();
    }
}