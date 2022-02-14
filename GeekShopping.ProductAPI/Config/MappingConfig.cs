using AutoMapper;
using GeekShopping.ProductAPI.Data.ValueObjects;
using GeekShopping.ProductAPI.Model;

namespace GeekShopping.ProductAPI.Config;

public class MappingConfig
{
    public static MapperConfiguration RegisterMaps()
    {
        var mappingConfig = new MapperConfiguration(config =>
        {
            //mapeia o ProductVO para o product para
            config.CreateMap<ProductVO, Product>();// entra um ProductVO
            config.CreateMap<Product, ProductVO>();//devolve um ProductVO
        });

        return mappingConfig; 
    }
}

