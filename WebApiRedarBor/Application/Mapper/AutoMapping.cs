namespace Application.Mapper
{
    using Application.Dto;
    using AutoMapper;
    using Domain.Entity;

    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            /*CreateMap<EmployeEntity, EmployeeDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email.Value))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username.Value))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name != null ? src.Name.Value : null))
            .ForMember(dest => dest.Fax, opt => opt.MapFrom(src => src.Fax != null ? src.Fax.Value : null))
            .ForMember(dest => dest.Telephone, opt => opt.MapFrom(src => src.Telephone != null ? src.Telephone.Value : null));*/

            CreateMap<EmployeEntity, EmployeeDto>()
            .ForCtorParam("Email", opt => opt.MapFrom(src => src.Email.Value))
            .ForCtorParam("Username", opt => opt.MapFrom(src => src.Username.Value))
            .ForCtorParam("Name", opt => opt.MapFrom(src => src.Name != null ? src.Name.Value : null))
            .ForCtorParam("Fax", opt => opt.MapFrom(src => src.Fax != null ? src.Fax.Value : null))
            .ForCtorParam("Telephone", opt => opt.MapFrom(src => src.Telephone != null ? src.Telephone.Value : null));
        }
    }
}