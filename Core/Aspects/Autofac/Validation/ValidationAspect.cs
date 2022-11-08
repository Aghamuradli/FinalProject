﻿using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Interceptors;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Aspects.Autofac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatorType;
        public ValidationAspect(Type validatorType)
        {
            //Defensive coding
            if (!typeof(IValidator).IsAssignableFrom(validatorType))
            {
                throw new System.Exception("Bu bir dorulama sinifi deyil .");
            }

            _validatorType = validatorType;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator = (IValidator)Activator.CreateInstance(_validatorType); //-reflection - calisma aninda new'leyir .
            var entityType = _validatorType.BaseType.GetGenericArguments()[0]; // - prductvalidator'un calisma tipini tap(generic yapisindan ilkini) .
            var entities = invocation.Arguments.Where(t => t.GetType() == entityType); // - onun parametrlerini tap . entityTyp'a esit olani tap .
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator, entity); //validate et ! .
            }
        }
    }
}
