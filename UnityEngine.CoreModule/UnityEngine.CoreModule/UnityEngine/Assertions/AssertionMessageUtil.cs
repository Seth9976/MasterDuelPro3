using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000318 RID: 792
	internal class AssertionMessageUtil
	{
		// Token: 0x06001611 RID: 5649 RVA: 0x0002E504 File Offset: 0x0002C704
		public static string GetMessage(string failureMessage)
		{
			return UnityString.Format("{0} {1}", new object[] { "Assertion failure.", failureMessage });
		}

		// Token: 0x06001612 RID: 5650 RVA: 0x0002E534 File Offset: 0x0002C734
		public static string GetMessage(string failureMessage, string expected)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("{0}{1}{2} {3}", new object[]
			{
				failureMessage,
				Environment.NewLine,
				"Expected:",
				expected
			}));
		}

		// Token: 0x06001613 RID: 5651 RVA: 0x0002E574 File Offset: 0x0002C774
		public static string GetEqualityMessage(object actual, object expected, bool expectEqual)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Values are {0}equal.", new object[] { expectEqual ? "not " : "" }), UnityString.Format("{0} {2} {1}", new object[]
			{
				actual,
				expected,
				expectEqual ? "==" : "!="
			}));
		}

		// Token: 0x06001614 RID: 5652 RVA: 0x0002E5D8 File Offset: 0x0002C7D8
		public static string NullFailureMessage(object value, bool expectNull)
		{
			return AssertionMessageUtil.GetMessage(UnityString.Format("Value was {0}Null", new object[] { expectNull ? "not " : "" }), UnityString.Format("Value was {0}Null", new object[] { expectNull ? "" : "not " }));
		}

		// Token: 0x06001615 RID: 5653 RVA: 0x0002E634 File Offset: 0x0002C834
		public static string BooleanFailureMessage(bool expected)
		{
			return AssertionMessageUtil.GetMessage("Value was " + (!expected).ToString(), expected.ToString());
		}
	}
}
