﻿using AutoMapper;
using Backend.Entities;
using Dtos.AdmissionDocumentDtos;
using Dtos.ContractorsDtos;
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

        CreateMap<DocumentPosition, DocumentPositionDto>();
        CreateMap<CreateDocumentPositionDto, DocumentPosition>();
        CreateMap<UpdateDocumentPositionDto, DocumentPosition>();

        CreateMap<Contractor, ContractorDto>();
        CreateMap<UpdateContractorDto, Contractor>();
        CreateMap<CreateContractorDto, Contractor>();
    }
}
