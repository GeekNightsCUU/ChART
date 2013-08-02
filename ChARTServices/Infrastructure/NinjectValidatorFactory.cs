using ChART.Domain.Entities;
using ChARTServices.Models;
using FluentValidation;
using Ninject;
using System;
using System.Collections.Generic;

namespace ChARTServices.Infrastructure
{

    public class NinjectValidatorFactory : ValidatorFactoryBase
    {
        private IKernel ninjectKernel;
        public NinjectValidatorFactory(IKernel kernel)
        {
            ninjectKernel = kernel;
            AddBindings();
        }

        public override IValidator CreateInstance(Type validatorType)
        {
            return ninjectKernel.Get(validatorType) as IValidator;
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IValidator<Station>>().To<StationValidator>();
        }
    }
}