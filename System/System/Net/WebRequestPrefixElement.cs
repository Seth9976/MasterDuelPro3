using System;
using System.Globalization;
using System.Reflection;

namespace System.Net
{
	// Token: 0x020003B4 RID: 948
	internal class WebRequestPrefixElement
	{
		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06001791 RID: 6033 RVA: 0x0006493C File Offset: 0x00062B3C
		// (set) Token: 0x06001792 RID: 6034 RVA: 0x000649BC File Offset: 0x00062BBC
		public IWebRequestCreate Creator
		{
			get
			{
				if (this.creator == null && this.creatorType != null)
				{
					lock (this)
					{
						if (this.creator == null)
						{
							this.creator = (IWebRequestCreate)Activator.CreateInstance(this.creatorType, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, new object[0], CultureInfo.InvariantCulture);
						}
					}
				}
				return this.creator;
			}
			set
			{
				this.creator = value;
			}
		}

		// Token: 0x06001793 RID: 6035 RVA: 0x000649C8 File Offset: 0x00062BC8
		public WebRequestPrefixElement(string P, Type creatorType)
		{
			if (!typeof(IWebRequestCreate).IsAssignableFrom(creatorType))
			{
				throw new InvalidCastException(SR.GetString("Invalid cast from {0} to {1}.", new object[] { creatorType.AssemblyQualifiedName, "IWebRequestCreate" }));
			}
			this.Prefix = P;
			this.creatorType = creatorType;
		}

		// Token: 0x06001794 RID: 6036 RVA: 0x00064A22 File Offset: 0x00062C22
		public WebRequestPrefixElement(string P, IWebRequestCreate C)
		{
			this.Prefix = P;
			this.Creator = C;
		}

		// Token: 0x04000ED2 RID: 3794
		public string Prefix;

		// Token: 0x04000ED3 RID: 3795
		internal IWebRequestCreate creator;

		// Token: 0x04000ED4 RID: 3796
		internal Type creatorType;
	}
}
