using System;
using Unity.Profiling;

namespace UnityEngine.InputSystem.Utilities
{
	// Token: 0x02000239 RID: 569
	internal static class DelegateHelpers
	{
		// Token: 0x060014C8 RID: 5320 RVA: 0x0005EA30 File Offset: 0x0005CC30
		public static void InvokeCallbacksSafe(ref CallbackArray<Action> callbacks, ProfilerMarker marker, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i]();
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", exception.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(exception.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x060014C9 RID: 5321 RVA: 0x0005EACC File Offset: 0x0005CCCC
		public static void InvokeCallbacksSafe<TValue>(ref CallbackArray<Action<TValue>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", exception.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(exception.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x060014CA RID: 5322 RVA: 0x0005EB6C File Offset: 0x0005CD6C
		public static void InvokeCallbacksSafe<TValue1, TValue2>(ref CallbackArray<Action<TValue1, TValue2>> callbacks, TValue1 argument1, TValue2 argument2, ProfilerMarker marker, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					callbacks[i](argument1, argument2);
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", exception.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(exception.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x060014CB RID: 5323 RVA: 0x0005EC10 File Offset: 0x0005CE10
		public static bool InvokeCallbacksSafe_AnyCallbackReturnsTrue<TValue1, TValue2>(ref CallbackArray<Func<TValue1, TValue2, bool>> callbacks, TValue1 argument1, TValue2 argument2, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return true;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument1, argument2))
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", exception.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(exception.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}

		// Token: 0x060014CC RID: 5324 RVA: 0x0005ECC0 File Offset: 0x0005CEC0
		public static void InvokeCallbacksSafe_AndInvokeReturnedActions<TValue>(ref CallbackArray<Func<TValue, Action>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					Action action = callbacks[i](argument);
					if (action != null)
					{
						action();
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", exception.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(exception.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
		}

		// Token: 0x060014CD RID: 5325 RVA: 0x0005ED68 File Offset: 0x0005CF68
		public static bool InvokeCallbacksSafe_AnyCallbackReturnsObject<TValue, TReturn>(ref CallbackArray<Func<TValue, TReturn>> callbacks, TValue argument, string callbackName, object context = null)
		{
			if (callbacks.length == 0)
			{
				return false;
			}
			callbacks.LockForChanges();
			for (int i = 0; i < callbacks.length; i++)
			{
				try
				{
					if (callbacks[i](argument) != null)
					{
						callbacks.UnlockForChanges();
						return true;
					}
				}
				catch (Exception exception)
				{
					Debug.LogException(exception);
					if (context != null)
					{
						Debug.LogError(string.Format("{0} while executing '{1}' callbacks of '{2}'", exception.GetType().Name, callbackName, context));
					}
					else
					{
						Debug.LogError(exception.GetType().Name + " while executing '" + callbackName + "' callbacks");
					}
				}
			}
			callbacks.UnlockForChanges();
			return false;
		}
	}
}
