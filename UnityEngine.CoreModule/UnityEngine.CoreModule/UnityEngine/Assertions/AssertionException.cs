using System;

namespace UnityEngine.Assertions
{
	// Token: 0x02000317 RID: 791
	public class AssertionException : Exception
	{
		// Token: 0x0600160F RID: 5647 RVA: 0x0002E4B6 File Offset: 0x0002C6B6
		public AssertionException(string message, string userMessage)
			: base(message)
		{
			this.m_UserMessage = userMessage;
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001610 RID: 5648 RVA: 0x0002E4C8 File Offset: 0x0002C6C8
		public override string Message
		{
			get
			{
				string message = base.Message;
				bool flag = this.m_UserMessage != null;
				if (flag)
				{
					message = this.m_UserMessage + "\n" + message;
				}
				return message;
			}
		}

		// Token: 0x04000833 RID: 2099
		private string m_UserMessage;
	}
}
