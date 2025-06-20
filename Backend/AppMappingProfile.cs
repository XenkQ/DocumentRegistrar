using AutoMapper;
using Backend.Entities;
using Backend.Models.AdmissionDocumentDtos;
using Backend.Models.ContractorsDtos;
using Backend.Models.DocumentPositionDtos;

namespace Backend;

public class AppMappingProfile : Profile
{
    public AppMappingProfile()
    {
        CreateMap<AdmissionDocument, AdmissionDocumentDto>()
            .ForMember(m => m.ContractorName, c => c.MapFrom(s => s.Contractor.Name))
            .ForMember(m => m.ContractorSymbol, c => c.MapFrom(s => s.Contractor.Symbol));
        CreateMap<CreateAdmissionDocumentDto, AdmissionDocumentDto>();
        CreateMap<UpdateAdmissionDocumentDto, AdmissionDocumentDto>();

        CreateMap<DocumentPosition, DocumentPositionDto>();
        CreateMap<CreateDocumentPositionDto, DocumentPositionDto>();
        CreateMap<UpdateDocumentPositionDto, DocumentPositionDto>();

        CreateMap<Contractor, ContractorDto>();
        CreateMap<UpdateContractorDto, ContractorDto>();
        CreateMap<CreateContractorDto, ContractorDto>();
    }
}
