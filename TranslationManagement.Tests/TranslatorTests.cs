using External.ThirdParty.Services;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TranslationManagement.Application;
using TranslationManagement.Application.Common.Exceptions;
using TranslationManagement.Application.Entities.Translator;
using TranslationManagement.Application.Translator.Commands.AddTranslator;
using TranslationManagement.Application.Translator.Commands.DeleteTranslator;
using TranslationManagement.Application.Translator.Commands.UpdateTranslator;
using TranslationManagement.Application.Translator.Queries.GetTranslator;
using TranslationManagement.Infrastructure;

namespace TranslationManagement.Tests;

public class TranslatorTests
{
    private ISender _sender;
    private AppDbContext _dbContext;
    
    public TranslatorTests()
    {
        var services = new ServiceCollection();
        services.AddApplication();
        services.AddInfrastructure();
        services.AddTransient<INotificationService, UnreliableNotificationService>();
        var serviceProvider = services.BuildServiceProvider();
        _sender = serviceProvider.GetRequiredService<ISender>();
        _dbContext = serviceProvider.GetRequiredService<AppDbContext>();

    }
    
    [Fact]
    public async Task ShouldGetTranslator()
    {
        var translator = _dbContext.Translators.FirstOrDefault(t => !t.IsDeleted);
        
        var query = new GetTranslatorQuery() { Id = translator.Id };

        var result = await _sender.Send(query);

        result.Id.Should().BeEquivalentTo(translator.Id);
        result.Name.Should().BeEquivalentTo(translator.Name);
        result.Type.Should().Be(translator.Type);
        result.HourlyRate.Should().Be(translator.HourlyRate);
        result.CreditCardNumber.Should().BeEquivalentTo(translator.CreditCardNumber);
    }
    
    [Fact]
    public async Task ShouldThrowNotFoundExceptionWhenNonExistingId_OnGet()
    {
        var command = new DeleteTranslatorCommand()
        {
            Id = "random id"
        };

        await Assert.ThrowsAsync<NotFoundException>(async () => await _sender.Send(command));
    }

    [Fact]
    public async Task ShouldAddTranslator()
    {
        var command = new AddTranslatorCommand()
        {
            Name = "test",
            Type = TranslatorType.Applicant,
            HourlyRate = 500,
            CreditCardNumber = "4590181640630931"
        };

        var result = await _sender.Send(command);

        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
    }

    [Fact]
    public async Task ShouldThrowValidationExceptionWhenIncompleteInput_OnAdd()
    {
        var command = new AddTranslatorCommand()
        {
            Name = "",
            Type = TranslatorType.Applicant,
            HourlyRate = 0,
            CreditCardNumber = "4"
        };
        
        await Assert.ThrowsAsync<ValidationException>(async () => await _sender.Send(command));
    }

    [Fact]
    public async Task ShouldUpdateTranslator()
    {
        var translator = _dbContext.Translators.AsQueryable().AsNoTracking().FirstOrDefault();

        var command = new UpdateTranslatorCommand()
        {
            Id = translator.Id,
            Name = "test name",
            Type = TranslatorType.Certified,
            HourlyRate = 600,
            CreditCardNumber = "4590181640630931"
        };

        await _sender.Send(command);

        var updatedTranslator = _dbContext.Translators.AsQueryable().AsNoTracking()
            .FirstOrDefault(t => t.Id == translator.Id);
        
        updatedTranslator.Name.Should().BeEquivalentTo(command.Name);
        updatedTranslator.CreditCardNumber.Should().BeEquivalentTo(command.CreditCardNumber);
        updatedTranslator.HourlyRate.Should().Be(command.HourlyRate);
        updatedTranslator.Type.Should().Be(command.Type);
    }
    
    [Fact]
    public async Task ShouldThrowNotFoundExceptionWhenNotExistingId_OnUpdate()
    {
        var command = new UpdateTranslatorCommand()
        {
            Id = "random Id",
            Name = "test",
            Type = TranslatorType.Applicant,
            HourlyRate = 5,
            CreditCardNumber = "4590181640630931"
        };
        
        await Assert.ThrowsAsync<NotFoundException>(async () => await _sender.Send(command));
    }
    
    [Fact]
    public async Task ShouldThrowValidationExceptionWhenIncompleteInput_OnUpdate()
    {
        var command = new UpdateTranslatorCommand()
        {
            Id = "",
            Name = "",
            Type = TranslatorType.Applicant,
            HourlyRate = 0,
            CreditCardNumber = "4"
        };
        
        await Assert.ThrowsAsync<ValidationException>(async () => await _sender.Send(command));
    }

    [Fact]
    public async Task ShouldDeleteTranslator()
    {
        var translator = _dbContext.Translators.AsQueryable().AsNoTracking().FirstOrDefault(
            t => !t.Jobs.Any() && !t.IsDeleted);

        var command = new DeleteTranslatorCommand()
        {
            Id = translator.Id
        };

        await _sender.Send(command);

        var deletedTranslator = _dbContext.Translators.FirstOrDefault(t => t.Id == translator.Id);

        deletedTranslator.IsDeleted.Should().BeTrue();
    }

    [Fact]
    public async Task ShouldThrowNotFoundExceptionWhenNonExistingId_OnDelete()
    {
        var command = new DeleteTranslatorCommand()
        {
            Id = "random id"
        };

        await Assert.ThrowsAsync<NotFoundException>(async () => await _sender.Send(command));
    }
    
    [Fact]
    public async Task ShouldThrowValidationExceptionWhenEmptyId_OnDelete()
    {
        var command = new DeleteTranslatorCommand()
        {
            Id = ""
        };

        await Assert.ThrowsAsync<ValidationException>(async () => await _sender.Send(command));
    }

    [Fact]
    public async Task ShouldThrowExceptionWhenAssignedJobs_OnDelete()
    {
        var translator = _dbContext.Translators.AsQueryable().AsNoTracking()
            .FirstOrDefault(t => !t.IsDeleted && t.Jobs.Any());

        var command = new DeleteTranslatorCommand()
        {
            Id = translator.Id
        };

        await Assert.ThrowsAsync<ApplicationLayerException>(async () => await _sender.Send(command));
    }
}

