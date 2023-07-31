using AutoMapper;
using ToDo.Business.Entities.Models;
using ToDo.Client.Entities.Requests.Category;
using ToDo.Client.Entities.Requests.Item;
using ToDo.Client.Entities.Responses.Category;
using ToDo.Client.Entities.Responses.Item;

namespace ToDo.Business.AutoMapper
{
    public class AutoMapperLoader
    {
        public readonly IMapper Mapper;

        public AutoMapperLoader()
        {
            var config = ConfigureAutoMappers();
            Mapper = config.CreateMapper();
        }

        private MapperConfiguration ConfigureAutoMappers()
        {

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Category, GetCategoryResponse>().ReverseMap();
                cfg.CreateMap<Category, AddCategoryRequest>().ReverseMap();
                cfg.CreateMap<Category, UpdateCategoryRequest>().ReverseMap();

                cfg.CreateMap<Item, GetItemResponse>().ReverseMap();
                cfg.CreateMap<Item, AddItemRequest>().ReverseMap();
                cfg.CreateMap<Item, UpdateItemRequest>().ReverseMap();
            });

            return config;
        }
    }
}
