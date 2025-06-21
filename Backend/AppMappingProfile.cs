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
        CreateMap<CreateAdmissionDocumentDto, AdmissionDocument>();
        CreateMap<UpdateAdmissionDocumentDto, AdmissionDocument>();

        CreateMap<DocumentPosition, DocumentPositionDto>();
        CreateMap<CreateDocumentPositionDto, DocumentPosition>();
        CreateMap<UpdateDocumentPositionDto, DocumentPosition>();

        CreateMap<Contractor, ContractorDto>();
        CreateMap<UpdateContractorDto, Contractor>();
        CreateMap<CreateContractorDto, Contractor>();
    }
}