using Application.DTO.Request;
using Application.DTO.Response;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.UseCases;
using Application.Validators;
using Azure;
using Azure.Core;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using System.Collections;

namespace UnitTest
{
    public class CompanyTest
    {
        [Fact]
        public async Task CreateCompany_Ok()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validatorMock = new Mock<IValidator<CompanyRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CompanyRequest>(), default))
                .ReturnsAsync(new ValidationResult());
            mockCommand.Setup(command => command.InsertCompany(It.IsAny<Company>())).ReturnsAsync(1);
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validatorMock.Object);

            var request = new CompanyRequest
            {
                Cuit = "1122334455",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "5544332211"
            };

            //ACT
            var amountOfModifiedRegisters = await service.CreateCompany(request);

            //ASSERT
            Assert.Equal(1, amountOfModifiedRegisters);
        }

        [Fact]
        public async Task CreateCompanyCommandError()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validatorMock = new Mock<IValidator<CompanyRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<CompanyRequest>(), default))
                .ReturnsAsync(new ValidationResult());
            mockCommand.Setup(command => command.InsertCompany(It.IsAny<Company>())).ReturnsAsync(0);
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validatorMock.Object);

            var request = new CompanyRequest
            {
                Cuit = "1122334455",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "5544332211"
            };

            //ACT
            var amountOfModifiedRegisters = await service.CreateCompany(request);

            //ASSERT
            Assert.Equal(0, amountOfModifiedRegisters);
        }

        [Fact]
        public async Task CreateCompany_EmptyCuit()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "5544332211"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_EmptyName()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "20354987562",
                Name = "",
                Adress = "Test address",
                Phone = "5544332211"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_EmptyAdress()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "20354987562",
                Name = "CompanyTest",
                Adress = "",
                Phone = "5544332211"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_EmptyPhone()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "20354987562",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = ""
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_LargeCuit()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "1234567891234567891",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "5544332211"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_LargeName()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "20354987562",
                Name = "123456789123456789123456789123456789123456789123456789123456789",
                Adress = "Test address",
                Phone = "5544332211"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_LargeAdress()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "20354987562",
                Name = "CompanyTest",
                Adress = "123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789123456789",
                Phone = "5544332211"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task CreateCompany_LargePhone()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validator = new CreateCompanyValidator();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validator);

            var request = new CompanyRequest
            {
                Cuit = "20354987562",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "1234567891234567891"
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateCompany(request));

            //ASSERT
        }

        [Fact]
        public async Task TestGetCompanysMethod()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var list = new List<Company>();
            var response = new Company
            {
                Cuit = "27458965875",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "5544332211"
            };
            list.Add(response);
            response = new Company
            {
                Cuit = "24658986584",
                Name = "CompanyTest2",
                Adress = "Test address2",
                Phone = "1122334455"
            };
            list.Add(response);
            mockQuery.Setup(v => v.GetCompanys())
                .ReturnsAsync(list);
            var validatorMock = new Mock<IValidator<CompanyRequest>>();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validatorMock.Object);

            //ACT
            var result = await service.GetCompanys();

            //ASSERT
            Assert.Equal(result.Count, list.Count);
        }

        [Fact]
        public async Task TestGetCompanyHappyWay()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var response = new Company
            {
                Cuit = "27458965875",
                Name = "CompanyTest",
                Adress = "Test address",
                Phone = "5544332211"
            };
            mockQuery.Setup(v => v.GetCompany(It.IsAny<int>()))
                .ReturnsAsync(response);
            var validatorMock = new Mock<IValidator<CompanyRequest>>();
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validatorMock.Object);

            //ACT
            var result = await service.GetCompany(1);

            //ASSERT
            Assert.Equal(result.Cuit, response.Cuit);
            Assert.Equal(result.Name, response.Name);
            Assert.Equal(result.Adress, response.Adress);
            Assert.Equal(result.Phone, response.Phone);
        }

        [Fact]
        public async Task TestGetCompanyNotFound()
        {
            //ARRANGE
            var mockQuery = new Mock<ICompanyQuery>();
            var mockCommand = new Mock<ICompanyCommand>();
            var validatorMock = new Mock<IValidator<CompanyRequest>>();
            Company? response = null;
            mockQuery.Setup(v => v.GetCompany(It.IsAny<int>()))
                .ReturnsAsync(response!);
            var service = new CompanyService(mockQuery.Object, mockCommand.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await service.GetCompany(1));
        }
    }
}