﻿using AutoMapper;
using KEPAVerwaltungWPF.DTOs;

namespace KEPAVerwaltungWPF.Services;

public class AutoMapperConfig  : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<Mitgliederliste, Mitgliederliste>();
        CreateMap<Meisterschaftsdaten, Meisterschaftsdaten>();
        // CreateMap<Models.Local.TblMeisterschaften, Models.Web.TblMeisterschaften>();
    }
}