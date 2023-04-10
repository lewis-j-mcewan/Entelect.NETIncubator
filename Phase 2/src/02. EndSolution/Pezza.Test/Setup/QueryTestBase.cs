namespace Test.Setup;

using System;
using AutoMapper;
using Common.Profiles;
using DataAccess;
using static DatabaseContextFactory;

public class QueryTestBase : IDisposable
{
    public DatabaseContext Context => Create();

    public static IMapper Mapper()
    {
        var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
        return mappingConfig.CreateMapper();
    }

    public void Dispose() => Destroy(this.Context);
}