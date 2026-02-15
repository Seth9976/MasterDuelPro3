using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace UnityEngine.Assertions
{
	// Token: 0x02000316 RID: 790
	[DebuggerStepThrough]
	public static class Assert
	{
		// Token: 0x060015FE RID: 5630 RVA: 0x0002E1CC File Offset: 0x0002C3CC
		private static void Fail(string message, string userMessage)
		{
			bool flag = !Assert.raiseExceptions;
			if (flag)
			{
				bool flag2 = message == null;
				if (flag2)
				{
					message = "Assertion has failed\n";
				}
				bool flag3 = userMessage != null;
				if (flag3)
				{
					message = userMessage + "\n" + message;
				}
				Debug.LogAssertion(message);
				return;
			}
			throw new AssertionException(message, userMessage);
		}

		// Token: 0x060015FF RID: 5631 RVA: 0x0002E220 File Offset: 0x0002C420
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.IsTrue(condition, null);
			}
		}

		// Token: 0x06001600 RID: 5632 RVA: 0x0002E240 File Offset: 0x0002C440
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsTrue(bool condition, string message)
		{
			bool flag = !condition;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(true), message);
			}
		}

		// Token: 0x06001601 RID: 5633 RVA: 0x0002E264 File Offset: 0x0002C464
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition)
		{
			if (condition)
			{
				Assert.IsFalse(condition, null);
			}
		}

		// Token: 0x06001602 RID: 5634 RVA: 0x0002E280 File Offset: 0x0002C480
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsFalse(bool condition, string message)
		{
			if (condition)
			{
				Assert.Fail(AssertionMessageUtil.BooleanFailureMessage(false), message);
			}
		}

		// Token: 0x06001603 RID: 5635 RVA: 0x0002E2A0 File Offset: 0x0002C4A0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual)
		{
			Assert.AreEqual<T>(expected, actual, null);
		}

		// Token: 0x06001604 RID: 5636 RVA: 0x0002E2AC File Offset: 0x0002C4AC
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message)
		{
			Assert.AreEqual<T>(expected, actual, message, EqualityComparer<T>.Default);
		}

		// Token: 0x06001605 RID: 5637 RVA: 0x0002E2C0 File Offset: 0x0002C4C0
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual<T>(T expected, T actual, string message, IEqualityComparer<T> comparer)
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.AreEqual(expected as Object, actual as Object, message);
			}
			else
			{
				bool flag2 = !comparer.Equals(actual, expected);
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
				}
			}
		}

		// Token: 0x06001606 RID: 5638 RVA: 0x0002E334 File Offset: 0x0002C534
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(Object expected, Object actual, string message)
		{
			bool flag = actual != expected;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.GetEqualityMessage(actual, expected, true), message);
			}
		}

		// Token: 0x06001607 RID: 5639 RVA: 0x0002E35C File Offset: 0x0002C55C
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value) where T : class
		{
			Assert.IsNull<T>(value, null);
		}

		// Token: 0x06001608 RID: 5640 RVA: 0x0002E368 File Offset: 0x0002C568
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNull(value as Object, message);
			}
			else
			{
				bool flag2 = value != null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
				}
			}
		}

		// Token: 0x06001609 RID: 5641 RVA: 0x0002E3CC File Offset: 0x0002C5CC
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNull(Object value, string message)
		{
			bool flag = value != null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, true), message);
			}
		}

		// Token: 0x0600160A RID: 5642 RVA: 0x0002E3F3 File Offset: 0x0002C5F3
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value) where T : class
		{
			Assert.IsNotNull<T>(value, null);
		}

		// Token: 0x0600160B RID: 5643 RVA: 0x0002E400 File Offset: 0x0002C600
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull<T>(T value, string message) where T : class
		{
			bool flag = typeof(Object).IsAssignableFrom(typeof(T));
			if (flag)
			{
				Assert.IsNotNull(value as Object, message);
			}
			else
			{
				bool flag2 = value == null;
				if (flag2)
				{
					Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
				}
			}
		}

		// Token: 0x0600160C RID: 5644 RVA: 0x0002E464 File Offset: 0x0002C664
		[Conditional("UNITY_ASSERTIONS")]
		public static void IsNotNull(Object value, string message)
		{
			bool flag = value == null;
			if (flag)
			{
				Assert.Fail(AssertionMessageUtil.NullFailureMessage(value, false), message);
			}
		}

		// Token: 0x0600160D RID: 5645 RVA: 0x0002E48C File Offset: 0x0002C68C
		[Conditional("UNITY_ASSERTIONS")]
		public static void AreEqual(int expected, int actual)
		{
			bool flag = expected != actual;
			if (flag)
			{
				Assert.AreEqual<int>(expected, actual, null);
			}
		}

		// Token: 0x04000832 RID: 2098
		[Obsolete("Future versions of Unity are expected to always throw exceptions and not have this field.")]
		public static bool raiseExceptions = true;
	}
}
