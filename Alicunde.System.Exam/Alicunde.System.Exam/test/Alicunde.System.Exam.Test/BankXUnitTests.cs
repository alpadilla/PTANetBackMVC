using Alicunde.System.Exam.Contracts.Bank;
using Alicunde.System.Exam.Domain;
using Alicunde.System.Exam.EntityFrameworkCore.DbContext;
using Alicunde.System.Exam.EntityFrameworkCore.Repositories;
using Alicunde.System.Exam.Services.Bank.Commands;
using Alicunde.System.Exam.Services.Bank.Queries;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shouldly;

namespace Alicunde.System.Exam.Test;

public class BankXUnitTests
{
    private readonly ServiceProvider _serviceProvider;
    
    public BankXUnitTests()
    {
        var configurationBuilder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

        var configuration = configurationBuilder.Build();
        
        var services = new ServiceCollection();
        services.AddDbContext<AlicundeSystemExamDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
        
        _serviceProvider = services.BuildServiceProvider();
    }

    [Fact]
    public async Task CreateBank()
    {
        // Arrange
        var bankCreateDto = new BankCreateDto
        {
            Bic = "12345678",
            Country = "ES",
            Name = "John Doe"
        };
        
        var bankRepository = _serviceProvider.GetService<IRepository<Bank>>();
        bankRepository.ShouldNotBeNull();
        var createBankCommand = new CreateBankCommand(bankCreateDto);
        var createBankCommandHandler = new CreateBankCommandHandler(bankRepository);
        
        // Act
        var bankDtoResult = await createBankCommandHandler.Handle(createBankCommand, CancellationToken.None);
        
        var getByIdQuery = new GetBackByIdQuery(bankDtoResult.Id);
        var getByIdQueryHandler = new GetBackByIdQueryHandler(bankRepository);
        var bankDto = await getByIdQueryHandler.Handle(getByIdQuery, CancellationToken.None);
        
        // Assert
        bankDto.ShouldNotBeNull();
        bankDto.Bic.ShouldBe("12345678");
        bankDto.Country.ShouldBe("ES");
        bankDto.Name.ShouldBe("John Doe");
    }
}