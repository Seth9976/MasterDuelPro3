using System;

namespace Mono.Unix.Native
{
	// Token: 0x02000008 RID: 8
	[Map("struct pollfd")]
	public struct Pollfd : IEquatable<Pollfd>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000023CC File Offset: 0x000005CC
		public override int GetHashCode()
		{
			return this.events.GetHashCode() ^ this.revents.GetHashCode();
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000023F4 File Offset: 0x000005F4
		public override bool Equals(object obj)
		{
			if (obj == null || obj.GetType() != base.GetType())
			{
				return false;
			}
			Pollfd pollfd = (Pollfd)obj;
			return pollfd.events == this.events && pollfd.revents == this.revents;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002448 File Offset: 0x00000648
		public bool Equals(Pollfd value)
		{
			return value.events == this.events && value.revents == this.revents;
		}

		// Token: 0x0400002A RID: 42
		public int fd;

		// Token: 0x0400002B RID: 43
		[CLSCompliant(false)]
		public PollEvents events;

		// Token: 0x0400002C RID: 44
		[CLSCompliant(false)]
		public PollEvents revents;
	}
}
