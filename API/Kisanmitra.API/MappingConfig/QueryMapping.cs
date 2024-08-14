using Mapster;
using Models.DTOs;
using Models.Entities;

namespace Kisanmitra.API.MappingConfig
{
    public class QueryMapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<TbQuery, QueryDto>()
                .Map(dest => dest.QueryId, src => src.QueryId)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.FarmerId, src => src.FarmerId)
                .Map(dest => dest.QueryTitle, src => src.QueryTitle)
                .Map(dest => dest.QueryDescription, src => src.QueryDescription)
                .Map(dest => dest.InsertedBy, src => src.InsertedBy)
                .Map(dest => dest.InsertedDate, src => src.InsertedDate)
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy)
                .Map(dest => dest.UpdatedDate, src => src.UpdatedDate)
                .Map(dest => dest.TimeStamp, src => src.TimeStamp);

            config.NewConfig<QueryDto, TbQuery>()
                .Map(dest => dest.QueryId, src => src.QueryId)
                .Map(dest => dest.CategoryId, src => src.CategoryId)
                .Map(dest => dest.FarmerId, src => src.FarmerId)
                .Map(dest => dest.QueryTitle, src => src.QueryTitle)
                .Map(dest => dest.QueryDescription, src => src.QueryDescription)
                .Map(dest => dest.InsertedBy, src => src.InsertedBy)
                .Map(dest => dest.InsertedDate, src => src.InsertedDate)
                .Map(dest => dest.UpdatedBy, src => src.UpdatedBy)
                .Map(dest => dest.UpdatedDate, src => src.UpdatedDate)
                .Map(dest => dest.TimeStamp, src => src.TimeStamp);
        }
    }
}

