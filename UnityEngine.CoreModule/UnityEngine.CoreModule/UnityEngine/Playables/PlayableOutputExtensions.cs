using System;

namespace UnityEngine.Playables
{
	// Token: 0x02000311 RID: 785
	public static class PlayableOutputExtensions
	{
		// Token: 0x060015CA RID: 5578 RVA: 0x0002DAD4 File Offset: 0x0002BCD4
		public static void SetReferenceObject<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetReferenceObject(value);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x0002DAFC File Offset: 0x0002BCFC
		public static void SetUserData<U>(this U output, Object value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetUserData(value);
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x0002DB24 File Offset: 0x0002BD24
		public static Playable GetSourcePlayable<U>(this U output) where U : struct, IPlayableOutput
		{
			return new Playable(output.GetHandle().GetSourcePlayable());
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x0002DB50 File Offset: 0x0002BD50
		public static void SetSourcePlayable<U, V>(this U output, V value, int port) where U : struct, IPlayableOutput where V : struct, IPlayable
		{
			output.GetHandle().SetSourcePlayable(value.GetHandle(), port);
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x0002DB84 File Offset: 0x0002BD84
		public static int GetSourceOutputPort<U>(this U output) where U : struct, IPlayableOutput
		{
			return output.GetHandle().GetSourceOutputPort();
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x0002DBAC File Offset: 0x0002BDAC
		public static void SetWeight<U>(this U output, float value) where U : struct, IPlayableOutput
		{
			output.GetHandle().SetWeight(value);
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x0002DBD4 File Offset: 0x0002BDD4
		public static void PushNotification<U>(this U output, Playable origin, INotification notification, object context = null) where U : struct, IPlayableOutput
		{
			output.GetHandle().PushNotification(origin.GetHandle(), notification, context);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x0002DC04 File Offset: 0x0002BE04
		public static void AddNotificationReceiver<U>(this U output, INotificationReceiver receiver) where U : struct, IPlayableOutput
		{
			output.GetHandle().AddNotificationReceiver(receiver);
		}
	}
}
