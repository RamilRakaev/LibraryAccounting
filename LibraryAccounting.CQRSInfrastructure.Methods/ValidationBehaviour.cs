﻿using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LibraryAccounting.CQRSInfrastructure.Methods
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
       where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> _logger;
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehaviour(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                string typeName = request.GetType().Name;
                _logger.LogInformation("----- Validating command {CommandType}", typeName);

                ValidationContext<TRequest> context = new ValidationContext<TRequest>(request);
                ValidationResult[] validationResults =
                    await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                List<ValidationFailure> failures = validationResults.SelectMany(result => result.Errors)
                    .Where(error => error != null).ToList();
                if (failures.Any())
                {
                    _logger.LogWarning(
                        "Validation errors - {CommandType} - Command: {@Command} - Errors: {@ValidationErrors}",
                        typeName, request, failures);

                    throw new CQRSSampleDomainException(
                        $"Command Validation Errors for type {typeof(TRequest).Name}");
                    throw new ValidationException("Validation exception", failures);
                }
            }

            return await next();
        }
    }
}
