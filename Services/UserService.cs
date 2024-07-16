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
    private TokenService _tokenService;

    public UserService(IMapper mapper, UserManager<User> userManager, SignInManager<User> signManager, TokenService tokenService)
    {
        _mapper = mapper;
        _userManager = userManager;
        _signManager = signManager;
        _tokenService = tokenService;
    }
    


    #region Creating user

    public async  Task Create(UserCreateRequest dto)
    {
        try
        {
            User user = _mapper.Map<User>(dto);
            
            IdentityResult result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Error to create the user: {result}");
            }
        }
        catch ( Exception e)
        {
            Console.WriteLine($"{e.Message}");
            throw;
        }

    }

    #endregion
    
    #region Authenticate user

    public async Task<string> Login(UserLoginRequest dto)
    {
        
        var result = await _signManager.PasswordSignInAsync(dto.UserName, dto.Password, false, false);

        Console.WriteLine($"This is result of result: {result}");
        
        if (!result.Succeeded)
        {
            throw new ApplicationException("The login or password is wrong");
        }

        var userToken = _signManager
            .UserManager
            .Users
            .FirstOrDefault(a => a.NormalizedUserName == dto.UserName);

        var token = _tokenService.GenerateToken(userToken);

        return token;
    }

    #endregion
    
}
