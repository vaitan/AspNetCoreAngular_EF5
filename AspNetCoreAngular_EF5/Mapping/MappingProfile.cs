using AspNetCoreAngular_EF5.Controllers.Resources;
using AspNetCoreAngular_EF5.Models;
using AutoMapper;
using System.Linq;

namespace AspNetCoreAngular_EF5.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain class to API Resource
            CreateMap<Make, MakeResource>();
            CreateMap<Model, ModelResource>();
            CreateMap<Feature, FeatureResource>();
            CreateMap<Vehicle, SaveVehicleResource>()
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(x => x.FeatureId)));
            CreateMap<Vehicle, VehicleResource>()
                .ForMember(vr => vr.Make, opt => opt.MapFrom(v => v.Model.Make))
                .ForMember(vr => vr.Contact, opt => opt.MapFrom(v => new ContactResource { Name = v.ContactName, Phone = v.ContactPhone, Email = v.ContactEmail }))
                .ForMember(vr => vr.Features, opt => opt.MapFrom(v => v.VehicleFeatures.Select(x => new FeatureResource { Id = x.Feature.Id, Name = x.Feature.Name })));


            //API Resource to domain class
            CreateMap<FilterResource, Filter>();
            CreateMap<SaveVehicleResource, Vehicle>()
                .ForMember(v => v.Id, opt => opt.Ignore())
                .ForMember(v => v.ContactName, opt => opt.MapFrom(vr => vr.Contact.Name))
                .ForMember(v => v.ContactPhone, opt => opt.MapFrom(vr => vr.Contact.Phone))
                .ForMember(v => v.ContactEmail, opt => opt.MapFrom(vr => vr.Contact.Email))
                .ForMember(v => v.VehicleFeatures, opt => opt.Ignore())
                .AfterMap((vr, v) =>
                {
                    //Remove unselected feature
                    var removedFeatures = v.VehicleFeatures.Where(v => !vr.Features.Contains(v.FeatureId)).ToList();
                    foreach (var f in removedFeatures)
                    {
                        v.VehicleFeatures.Remove(f);
                    }

                    //Add feature
                    var addedFeatures = vr.Features.Where(id => !v.VehicleFeatures.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature { FeatureId = id }).ToList();
                    foreach (var f in addedFeatures)
                    {
                        v.VehicleFeatures.Add(f);
                    }
                });
        }
    }
}
