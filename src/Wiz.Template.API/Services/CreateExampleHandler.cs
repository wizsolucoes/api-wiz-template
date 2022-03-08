using LanguageExt;
using MediatR;
using Wiz.Template.API.ViewModels.ExampleViewModels;
using Wiz.Template.Domain.Entities;
using Wiz.Template.Domain.Interfaces.Repositories;
using Wiz.Template.Domain.Interfaces.UoW;

namespace Wiz.Template.API.Services
{
    public class CreateExampleHandler :
        RequestHandler<RequestCreateExampleViewModel, Option<Example>>
    {
        private readonly IExampleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateExampleHandler(
            IExampleRepository repository,
            IUnitOfWork unitOfWork
        )
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        protected override Option<Example> Handle(
            RequestCreateExampleViewModel request
        )
        {
            var example = Example.From(request.TemperatureC, request.Summary);

            if (!example.TemperatureC.IsValid)
            {
                return Option<Example>.None;
            }

            _repository.Add(example);
            _unitOfWork.Commit();

            return example;
        }
    }
}
