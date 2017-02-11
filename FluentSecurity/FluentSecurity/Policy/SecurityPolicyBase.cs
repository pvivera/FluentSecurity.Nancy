using System;
using System.Collections.Generic;
using System.Linq;
using FluentSecurity.Internals;
using FluentSecurity.ServiceLocation;

namespace FluentSecurity.Policy
{
	public abstract class SecurityPolicyBase<TSecurityContext> : ISecurityPolicy where TSecurityContext : class, ISecurityContext
	{
		public abstract PolicyResult Enforce(TSecurityContext securityContext);

		public PolicyResult Enforce(ISecurityContext securityContext)
		{
			var customContext =
				TryGetFromContainer() ??
				TryCreateSingleArgumentContext(securityContext) ??
				TryCreateEmptyConstructorContext();

			if (customContext == null)
				throw new ArgumentException(String.Format("The generic argument {0} could not be created or resolved from the container.", typeof(TSecurityContext).Name));

			return Enforce(customContext);
		}

		private static TSecurityContext TryGetFromContainer()
		{
			return ServiceLocator.Current.ResolveAll<TSecurityContext>().SingleOrDefault();
		}

		private static TSecurityContext TryCreateEmptyConstructorContext()
		{
			TSecurityContext customContext = null;

			var contextType = typeof(TSecurityContext);
			if (contextType.HasEmptyConstructor())
				customContext = Activator.CreateInstance<TSecurityContext>();
			
			return customContext;
		}

		private static TSecurityContext TryCreateSingleArgumentContext(ISecurityContext securityContext)
		{
			TSecurityContext customContext = null;

			var contextType = typeof (TSecurityContext);
			var constructors = contextType.GetConstructors();
			var constructor = constructors.SingleOrDefault(x => x.GetParameters().Count() == 1 && x.GetParameters().Single().ParameterType == typeof (ISecurityContext));
			if (constructor != null)
				customContext = (TSecurityContext) constructor.Invoke(new List<object> { securityContext }.ToArray());

			return customContext;
		}
	}
}