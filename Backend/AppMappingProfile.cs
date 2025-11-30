using AutoMapper;
using Backend.Entities;
using Dtos.AdmissionDocumentDtos;
using Dtos.ContractorsDtos;
using Dtos.CreateDocumentTypeDtos;
using Dtos.DocumentPositionDtos;

namespace Backend;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<AdmissionDocument, AdmissionDocumentDto>()
            .ForMember(dest => dest.ContractorName, opt => opt.MapFrom(src => src.Contractor.Name))
            .ForMember(dest => dest.ContractorSymbol, opt => opt.MapFrom(src => src.Contractor.Symbol));
        CreateMap<CreateAdmissionDocumentDto, AdmissionDocument>();
        CreateMap<UpdateAdmissionDocumentDto, AdmissionDocument>();

        CreateMap<DocumentPosition, DocumentPositionDto>()
            .ForMember(dest => dest.DocumentPositionTypeName, opt => opt.MapFrom(src => src.DocumentPositionType.Name));
        CreateMap<CreateDocumentPositionDto, DocumentPosition>();
        CreateMap<UpdateDocumentPositionDto, DocumentPosition>();

        CreateMap<DocumentPositionTypeDto, DocumentPositionType>();
        CreateMap<UpdateDocumentPositionTypeDto, DocumentPositionType>();
        CreateMap<CreateDocumentPositionTypeDto, DocumentPositionType>();
        CreateMap<DocumentPositionType, DocumentPositionTypeDto>();

        CreateMap<Contractor, ContractorDto>();
        CreateMap<UpdateContractorDto, Contractor>();
        CreateMap<CreateContractorDto, Contractor>();
    }
}
