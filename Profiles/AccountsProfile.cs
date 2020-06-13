using AutoMapper;
using PizzaShop.Entities;
using PizzaShop.Models;

namespace PizzaShop.Profiles
{
    public class AccountsProfile : Profile
    {
        public AccountsProfile ()
        {
            CreateMap<Account, AccountDto> ().ReverseMap ();
            CreateMap<Account, AccountForCreationDto> ().ReverseMap ();
            CreateMap<Account, AccountForUpdateDto> ().ReverseMap ();
        }
    }
}
