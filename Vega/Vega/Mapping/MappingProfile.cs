﻿using AutoMapper;
using System.Linq;
using Vega.Controllers.Resources;
using Vega.Core.Models;
namespace Vega.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domaine to API Resource
            CreateMap<Photo, PhotoResource>();

            CreateMap(typeof(QueryResult<>), typeof(QueryResultResource<>));

            CreateMap<Make, MakeResource>();

            CreateMap<Make, KeyValuePairResource>();

            CreateMap<Model, KeyValuePairResource>();

            CreateMap<Feature, KeyValuePairResource>();

            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));

            CreateMap<Vehicle, GetVehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.Features.Select(vf => new KeyValuePairResource { Id = vf.FeatureId, Name = vf.Feature.Name })));


            //API Resource to Domaine 

            CreateMap<VehicleQueryResource, VehicleQuery>();

            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.Features, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {

                    //Remove unslected features
                    //var removedF = new List<VehicleFeature>();
                    //foreach (var f in v.Features)
                    //    if (vr.Features.Contains(f.FeatureId))
                    //        removedF.Add(f);

                    var removedF = v.Features.Where(f => !vr.Features.Contains(f.FeatureId));

                    foreach (VehicleFeature f in removedF.ToList())
                        v.Features.Remove(f);

                    //Add new F
                    //foreach (var id in vr.Features)
                    //    if (!v.Features.Any(f => f.FeatureId == id))
                    //        v.Features.Add(new VehicleFeature { FeatureId = id });

                    var addedF = vr.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id });
                    foreach (var f in addedF.ToList())
                        v.Features.Add(f);
                });

        }
    }
}
