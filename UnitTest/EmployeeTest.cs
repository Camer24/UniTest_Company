using Application.DTO.Request;
using Application.DTO.Response;
using Application.Exceptions;
using Application.Interfaces.IRepositories;
using Application.Interfaces.IServices;
using Application.UseCases;
using Azure;
using Azure.Core;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Infrastructure.Repositories.Querys;
using Moq;
using System.Collections;

namespace UnitTest
{
    public class EmployeeTest
    {
        [Fact]
        public async Task TestCreateemployeeHappyWay()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<EmployeeRequest>(), default))
                .ReturnsAsync(new ValidationResult());
            mockCommand.Setup(command => command.InsertEmployee(It.IsAny<Employee>())).ReturnsAsync(1);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            var request = new EmployeeRequest
            {
                FirsName = "Jhon",
                LastName = "Kenedy",
                DepartmentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };

            //ACT
            var amountOfModifiedRegisters = await service.CreateEmployee(request);

            //ASSERT
            Assert.Equal(1, amountOfModifiedRegisters);
        }

        [Fact]
        public async Task TestCreateemployeeCommandError()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<EmployeeRequest>(), default))
                .ReturnsAsync(new ValidationResult());
            mockCommand.Setup(command => command.InsertEmployee(It.IsAny<Employee>())).ReturnsAsync(0);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            var request = new EmployeeRequest
            {
                FirsName = "Jhon",
                LastName = "Kenedy",
                DepartmentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };

            //ACT
            var amountOfModifiedRegisters = await service.CreateEmployee(request);

            //ASSERT
            Assert.Equal(0, amountOfModifiedRegisters);
        }

        [Fact]
        public async Task TestCreatEmployeeBadRequest()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<EmployeeRequest>(), default))
                .ReturnsAsync(new ValidationResult(new List<ValidationFailure>
                {
                    new ValidationFailure("FirstName", "El Nombre no puede ser vacio")
                }));
            mockCommand.Setup(command => command.InsertEmployee(It.IsAny<Employee>())).ReturnsAsync(1);

            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            var request = new EmployeeRequest
            {
                Id = 1,
                FirsName = "",
                LastName = "Kenedy",
                DepartmentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.CreateEmployee(request));
        }

        [Fact]
        public async Task TestGetEmployeeDepartmentHappyWay()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var response = new Department
            {
                Name = "DepartmentTest",
                IdCompany = 1
            };
            mockQuery.Setup(q => q.GetEmployeeDepartment(It.IsAny<int>()))
                .ReturnsAsync(response);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT
            var result = await service.GetEmployeeDepartment(1);

            //ASSERT
            Assert.Equal(result.Name, response.Name);
        }


        [Fact]
        public async Task TestGetDepartmentsByCompanyBadRequest()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var response = new Department
            {
                Name = "DepartmentTest",
                IdCompany = 1
            };
            mockQuery.Setup(q => q.GetEmployeeDepartment(It.IsAny<int>()))
                .ReturnsAsync(response);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT & ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.GetEmployeeDepartment(-1));
        }

        [Fact]
        public async Task TestGetDepartmentsByCompanyNotFound()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            Department? department = null!;
            mockQuery.Setup(q => q.GetEmployeeDepartment(It.IsAny<int>()))
                .ReturnsAsync(department!);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT & ASSERT
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await service.GetEmployeeDepartment(1));
        }

        [Fact]
        public async Task TestGetEmployeeHappyWay()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var response = new Employee
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Kenedy",
                DepartamentId = 2,
                Departament = new Department()
                {
                    IdCompany = 1
                },
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };
            mockQuery.Setup(v => v.GetEmployee(It.IsAny<int>()))
                .ReturnsAsync(response);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT
            var result = await service.GetEmployee(1);

            //ASSERT
            Assert.Equal(result.Id, response.Id);
            Assert.Equal(result.FirsName, response.FirstName);
            Assert.Equal(result.DepartmentId, response.DepartamentId);
            Assert.Equal(result.LastName, response.LastName);
            Assert.Equal(result.PositionId, response.PositionId);
            Assert.Equal(result.SuperiorId, response.SuperiorId);
            Assert.Equal(result.IsApprover, response.IsApprover);
        }

        [Fact]
        public async Task TestGetEmployeeNotFound()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            Employee? response = null;
            mockQuery.Setup(v => v.GetEmployee(It.IsAny<int>()))
                .ReturnsAsync(response!);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await service.GetEmployee(1));
        }

        [Fact]
        public async Task TestGetEmployeeBadRequest()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            Employee? response = null;
            mockQuery.Setup(v => v.GetEmployee(It.IsAny<int>()))
                .ReturnsAsync(response!);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.GetEmployee(-1));
        }

        [Fact]
        public async Task TestDeleteHappyWay()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var response = new Employee
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Kenedy",
                DepartamentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };
            mockQuery.Setup(v => v.GetEmployee(It.IsAny<int>()))
                .ReturnsAsync(response);
            mockCommand.Setup(v => v.DeleteEmployee(It.IsAny<Employee>()))
                .ReturnsAsync(1);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            var request = new DepartmentRequest
            {
                Name = "DepartmentTest",
                IdCompany = 1
            };

            //
            var amountOfModifiedRegisters = await service.DeleteEmployee(1);

            //ASSERT
            Assert.Equal(1, amountOfModifiedRegisters);
        }

        [Fact]
        public async Task TestDeleteBadRequest()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.DeleteEmployee(-1));
        }

        [Fact]
        public async Task TestDeleteNotFound()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            Employee? response = null;
            mockQuery.Setup(v => v.GetEmployee(It.IsAny<int>()))
                .ReturnsAsync(response!);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await service.DeleteEmployee(1));
        }

        [Fact]
        public async Task TestGetSuperiorsHappyWay()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var list = new List<Employee>();
            var response = new Employee
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Kenedy",
                DepartamentId = 2,
                Departament = new Department()
                {
                    IdCompany = 1
                },
                SuperiorId = 2,
                PositionId = 1,
                Position = new Position()
                {
                    Hierarchy = 2
                },
                IsApprover = true
            };
            list.Add(response);
            mockQuery.Setup(q => q.GetEmployeesByDepartment(It.IsAny<int>()))
                .ReturnsAsync(list);
            var position = new Position()
            {
                Id = 1,
                Name = "Leader",
                Hierarchy = 1,
                MaxAmount = 100000,
                IdCompany = 1
            };
            mockPositionService.Setup(q => q.GetPositionEntity(It.IsAny<int>()))
                .ReturnsAsync(position);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT
            var resultList = await service.GetSuperiors(1, 1);

            //ASSERT
            Assert.Equal(resultList.Count, 1);
            var result = resultList[0];
            Assert.Equal(result.Id, response.Id);
            Assert.Equal(result.FirsName, response.FirstName);
            Assert.Equal(result.DepartmentId, response.DepartamentId);
            Assert.Equal(result.LastName, response.LastName);
            Assert.Equal(result.PositionId, response.PositionId);
            Assert.Equal(result.SuperiorId, response.SuperiorId);
            Assert.Equal(result.IsApprover, response.IsApprover);
        }

        [Fact]
        public async Task TestGetSuperiorsPositionBadResquest()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var list = new List<Employee>();
            var response = new Employee
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Kenedy",
                DepartamentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };
            list.Add(response);
            mockQuery.Setup(q => q.GetEmployeesByDepartment(It.IsAny<int>()))
                .ReturnsAsync(list);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.GetSuperiors(1, -1));
        }

        [Fact]
        public async Task TestGetSuperiorsDepartmentBadRequest()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var position = new Position()
            {
                Id = 1,
                Name = "Leader",
                Hierarchy = 1,
                MaxAmount = 100000,
                IdCompany = 1
            };
            mockPositionService.Setup(q => q.GetPositionEntity(It.IsAny<int>()))
                .ReturnsAsync(position);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<BadRequestException>(async () =>
                await service.GetSuperiors(-1, 1));
        }

        [Fact]
        public async Task TestGetSuperiorsPositionNotFound()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var list = new List<Employee>();
            var response = new Employee
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Kenedy",
                DepartamentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                IsApprover = false
            };
            list.Add(response);
            mockQuery.Setup(q => q.GetEmployeesByDepartment(It.IsAny<int>()))
                .ReturnsAsync(list);
            Position? position = null;
            mockPositionService.Setup(q => q.GetPositionEntity(It.IsAny<int>()))
                .ReturnsAsync(position);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT && ASSERT
            await Assert.ThrowsAsync<NotFoundException>(async () =>
                await service.GetSuperiors(1, 1));
        }

        [Fact]
        public async Task TestGetSuperiorsEmployeeEmpltyList()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var list = new List<Employee>();
            mockQuery.Setup(q => q.GetEmployeesByDepartment(It.IsAny<int>()))
                .ReturnsAsync(list);
            var position = new Position()
            {
                Id = 1,
                Name = "Leader",
                Hierarchy = 1,
                MaxAmount = 100000,
                IdCompany = 1
            };
            mockPositionService.Setup(q => q.GetPositionEntity(It.IsAny<int>()))
                .ReturnsAsync(position);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT
            var resultList = await service.GetSuperiors(1, 1);

            //ASSERT
            Assert.Equal(resultList.Count, 0);
        }

        [Fact]
        public async Task TestGetNextApproverNoMoreApporvers()
        {
            //ARRANGE
            var mockQuery = new Mock<IEmployeeQuery>();
            var mockCommand = new Mock<IEmployeeCommand>();
            var mockPositionService = new Mock<IPositionService>();
            var validatorMock = new Mock<IValidator<EmployeeRequest>>();
            var position = new Position()
            {
                Id = 1,
                Name = "Leader",
                Hierarchy = 1,
                MaxAmount = 100,
                IdCompany = 1
            };
            var response = new Employee
            {
                Id = 1,
                FirstName = "Jhon",
                LastName = "Kenedy",
                DepartamentId = 2,
                SuperiorId = 2,
                PositionId = 1,
                Position = position,
                IsApprover = false
            };
            mockQuery.Setup(q => q.GetEmployee(It.IsAny<int>()))
                .ReturnsAsync(response);
            var service = new EmployeeService(mockCommand.Object, mockQuery.Object, mockPositionService.Object, validatorMock.Object);

            //ACT
            var result = await service.GetNextApprover(1, 10);

            //ASSERT
            Assert.Equal(result, 0);
        }
    }
}