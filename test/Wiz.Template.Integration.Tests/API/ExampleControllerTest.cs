using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using FluentAssertions;
using Wiz.Template.API.ViewModels.ExampleViewModels;
using Wiz.Template.Shared.Fixture;
using Wiz.Template.Shared.Mocks;
using Xunit;

namespace Wiz.Template.Integration.Tests.API
{
    public class ExampleControllerTest : ControllerBaseTest
    {
        private readonly string _baseUrl = "/Example";

        [Fact]
        public async void GetExample_Get_Returns_List_ResponseExampleViewModel()
        {
            // Arrange
            await using var application = new WebApplicationFixture();
            var client = application.CreateClient();

            // Act
            var examples = await client
                .GetFromJsonAsync<List<ResponseExampleViewModel>>(_baseUrl);

            // Assert
            examples.Should().NotBeEmpty();
            foreach (var example in examples!)
            {
                example.TemperatureC.Should().BeInRange(-20, 55);
            }
        }

        [Fact]
        public async void GetMediatR_Get_Returns_ResponseExampleViewModel()
        {
            // Arrange
            await using var application = new WebApplicationFixture();
            var client = application.CreateClient();

            // Act
            var response = await client
                .PostAsJsonAsync<RequestExampleViewModel>(
                    $"{_baseUrl}/mediatr",
                    new RequestExampleViewModel {
                        Date = new DateTime(2022, 2, 2)
                    }
                );

            var example = await DeserializeObject<ResponseExampleViewModel>(
                response
            );

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();

            example!.Date.Should().Be(new DateTime(2022, 2, 2));
            example!.TemperatureC.Should().Be(25);
            example!.Summary.Should().Be("Mild");
        }

        [Fact]
        public async void GetMediatR_Get_Returns_NotFound()
        {
            // Arrange
            await using var application = new WebApplicationFixture();
            var client = application.CreateClient();

            // Act
            var response = await client
                .PostAsJsonAsync<RequestExampleViewModel>(
                    $"{_baseUrl}/mediatr",
                    new RequestExampleViewModel {
                        Date = new DateTime(2022, 2, 3)
                    }
                );

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        }

        [Fact]
        public async void CreateExample_Post_Returns_Created_Example()
        {
            // Arrange
            await using var application = new WebApplicationFixture();
            var client = application.CreateClient();

            var request = ExampleMock.RequestCreateExampleViewModelFaker
                .Generate();

            // Act
            var response = await client
                .PostAsJsonAsync<RequestCreateExampleViewModel>(
                    _baseUrl,
                    request
                );

            var example = await DeserializeObject<ResponseExampleViewModel>(
                response
            );

            // Assert
            response.IsSuccessStatusCode.Should().BeTrue();
            example!.TemperatureC.Should().Be(request.TemperatureC);
            example!.Summary.Should().Be(request.Summary);
            example!.Date.Date.Should().Be(DateTime.Now.Date);
        }

        [Fact]
        public async void CreateExample_Post_Returns_BadRequest_Se_Example_Invalido()
        {
            // Arrange
            await using var application = new WebApplicationFixture();
            var client = application.CreateClient();

            var request = ExampleMock.RequestCreateExampleViewModelFaker
                .RuleFor(x => x.TemperatureC, 101)
                .Generate();

            // Act
            var response = await client
                .PostAsJsonAsync<RequestCreateExampleViewModel>(
                    _baseUrl,
                    request
                );

            // Assert
            response.IsSuccessStatusCode.Should().BeFalse();
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }
    }
}
