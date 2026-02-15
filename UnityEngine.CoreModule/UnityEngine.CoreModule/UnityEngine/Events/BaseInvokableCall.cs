using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000223 RID: 547
	internal abstract class BaseInvokableCall
	{
		// Token: 0x06001421 RID: 5153 RVA: 0x000205EB File Offset: 0x0001E7EB
		protected BaseInvokableCall()
		{
		}

		// Token: 0x06001422 RID: 5154 RVA: 0x0002A188 File Offset: 0x00028388
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			bool flag = function == null;
			if (flag)
			{
				throw new ArgumentNullException("function");
			}
			bool isStatic = function.IsStatic;
			if (isStatic)
			{
				bool flag2 = target != null;
				if (flag2)
				{
					throw new ArgumentException("target must be null");
				}
			}
			else
			{
				bool flag3 = target == null;
				if (flag3)
				{
					throw new ArgumentNullException("target");
				}
			}
		}

		// Token: 0x06001423 RID: 5155
		public abstract void Invoke(object[] args);

		// Token: 0x06001424 RID: 5156 RVA: 0x0002A1EC File Offset: 0x000283EC
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			bool flag = arg != null && !(arg is T);
			if (flag)
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0002A23C File Offset: 0x0002843C
		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			bool flag = target == null;
			bool flag2;
			if (flag)
			{
				flag2 = true;
			}
			else
			{
				Object unityObj = target as Object;
				bool flag3 = unityObj != null;
				flag2 = !flag3 || unityObj != null;
			}
			return flag2;
		}

		// Token: 0x06001426 RID: 5158
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
