using AutoMapper;
using Client.DAL.Models;
using Client.PL.ViewModels;

namespace Client.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles() 
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            //.ForMember(d => d.Name, o => o.MapFrom(s => s.Name));
        }
    }
}
