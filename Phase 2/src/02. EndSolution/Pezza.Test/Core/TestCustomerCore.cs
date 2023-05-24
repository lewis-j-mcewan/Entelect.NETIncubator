namespace Pezza.Test.Core;

using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Pezza.Common.DTO;
using Pezza.Core.Customer.Commands;
using Pezza.Core.Customer.Queries;
using Pezza.DataAccess;
using Pezza.Test.Setup;
using Pezza.Test.Setup.TestData.Customer;

[TestFixture]
public class TestCustomerCore : QueryTestBase
{
    private CustomerDTO dto;
    private DatabaseContext testDBContext;

    [SetUp]
    public async Task Init()
    {
        this.testDBContext = this.Context;
        this.dto = CustomerTestData.CustomerDTO;
        var sutCreate = new CreateCustomerCommandHandler(this.testDBContext, Mapper());
        var resultCreate = await sutCreate.Handle(
            new CreateCustomerCommand
            {
                Data = this.dto
            }, CancellationToken.None);

        if (!resultCreate.Succeeded)
        {
            Assert.IsTrue(false);
        }

        this.dto = resultCreate.Data;
    }

    [Test]
    public async Task GetAsync()
    {
        var sutGet = new GetCustomerQueryHandler(this.testDBContext, Mapper());
        var resultGet = await sutGet.Handle(
            new GetCustomerQuery
            {
                Id = this.dto.Id
            }, CancellationToken.None);

        Assert.IsTrue(resultGet?.Data != null);
    }

    [Test]
    public async Task GetAllAsync()
    {
        var sutGetAll = new GetCustomersQueryHandler(this.testDBContext, Mapper());
        var resultGetAll = await sutGetAll.Handle(new GetCustomersQuery(), CancellationToken.None);

        Assert.IsTrue(resultGetAll?.Data.Count == 1);
    }

    [Test]
    public void SaveAsync() => Assert.IsTrue(this.dto != null);

    [Test]
    public async Task UpdateAsync()
    {
        var sutUpdate = new UpdateCustomerCommandHandler(this.testDBContext, Mapper());
        var resultUpdate = await sutUpdate.Handle(
            new UpdateCustomerCommand
            {
                Data = new CustomerDTO
                {
                    Id = this.dto.Id,
                    Phone = "0721230000"
                }
            }, CancellationToken.None);

        Assert.IsTrue(resultUpdate.Succeeded);
    }

    [Test]
    public async Task DeleteAsync()
    {
        var sutDelete = new DeleteCustomerCommandHandler(this.testDBContext);
        var outcomeDelete = await sutDelete.Handle(
            new DeleteCustomerCommand
            {
                Id = this.dto.Id
            }, CancellationToken.None);

        Assert.IsTrue(outcomeDelete.Succeeded);
    }
}
