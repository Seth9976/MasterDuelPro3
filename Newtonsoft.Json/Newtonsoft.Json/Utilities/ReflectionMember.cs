using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x020000E2 RID: 226
	[NullableContext(2)]
	[Nullable(0)]
	internal class ReflectionMember
	{
		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00022416 File Offset: 0x00020616
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0002241E File Offset: 0x0002061E
		public Type MemberType { get; set; }

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00022427 File Offset: 0x00020627
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x0002242F File Offset: 0x0002062F
		[Nullable(new byte[] { 2, 1, 2 })]
		public Func<object, object> Getter
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00022438 File Offset: 0x00020638
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00022440 File Offset: 0x00020640
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<object, object> Setter
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}
	}
}
