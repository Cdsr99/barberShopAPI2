using AutoMapper;
using BarberShopAPI2.Controllers.Request.UsersRequests;
using BarberShopAPI2.Models;
using Microsoft.AspNetCore.Identity;

namespace BarberShopAPI2.Services;

public class UserService
{
    private IMapper _mapper;
    private UserManager<User> _userManager;
    private SignInManager<User> _signManager;
    
    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signManager)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        _signManager = signManager ?? throw new ArgumentNullException(nameof(signManager));
    }
    
    public async  Task Create(UserCreateRequest dto)
    {
        User user = _mapper.Map<User>(dto);
        
        
        IdentityResult result = await _userManager.CreateAsync(user, dto.password);

        if (!result.Succeeded)
        {
            throw new ApplicationException($"Falha ao cadastrar o usuário: {result}");
        }
    }
    
}