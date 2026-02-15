using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Mono.Net.Dns
{
	// Token: 0x02000090 RID: 144
	internal class DnsResponse : DnsPacket
	{
		// Token: 0x06000233 RID: 563 RVA: 0x00008ADD File Offset: 0x00006CDD
		public DnsResponse(byte[] buffer, int length)
			: base(buffer, length)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00008AF0 File Offset: 0x00006CF0
		private ReadOnlyCollection<DnsResourceRecord> GetRRs(int count)
		{
			if (count <= 0)
			{
				return DnsResponse.EmptyRR;
			}
			List<DnsResourceRecord> list = new List<DnsResourceRecord>(count);
			for (int i = 0; i < count; i++)
			{
				list.Add(DnsResourceRecord.CreateFromBuffer(this, this.position, ref this.offset));
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00008B38 File Offset: 0x00006D38
		private ReadOnlyCollection<DnsQuestion> GetQuestions(int count)
		{
			if (count <= 0)
			{
				return DnsResponse.EmptyQS;
			}
			List<DnsQuestion> list = new List<DnsQuestion>(count);
			for (int i = 0; i < count; i++)
			{
				DnsQuestion dnsQuestion = new DnsQuestion();
				this.offset = dnsQuestion.Init(this, this.offset);
				list.Add(dnsQuestion);
			}
			return list.AsReadOnly();
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00008B88 File Offset: 0x00006D88
		public ReadOnlyCollection<DnsQuestion> GetQuestions()
		{
			if (this.question == null)
			{
				this.question = this.GetQuestions((int)base.Header.QuestionCount);
			}
			return this.question;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00008BAF File Offset: 0x00006DAF
		public ReadOnlyCollection<DnsResourceRecord> GetAnswers()
		{
			if (this.answer == null)
			{
				this.GetQuestions();
				this.answer = this.GetRRs((int)base.Header.AnswerCount);
			}
			return this.answer;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00008BDD File Offset: 0x00006DDD
		public ReadOnlyCollection<DnsResourceRecord> GetAuthority()
		{
			if (this.authority == null)
			{
				this.GetQuestions();
				this.GetAnswers();
				this.authority = this.GetRRs((int)base.Header.AuthorityCount);
			}
			return this.authority;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00008C12 File Offset: 0x00006E12
		public ReadOnlyCollection<DnsResourceRecord> GetAdditional()
		{
			if (this.additional == null)
			{
				this.GetQuestions();
				this.GetAnswers();
				this.GetAuthority();
				this.additional = this.GetRRs((int)base.Header.AdditionalCount);
			}
			return this.additional;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00008C50 File Offset: 0x00006E50
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.Header);
			stringBuilder.Append("Question:\r\n");
			foreach (DnsQuestion dnsQuestion in this.GetQuestions())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsQuestion);
			}
			stringBuilder.Append("Answer(s):\r\n");
			foreach (DnsResourceRecord dnsResourceRecord in this.GetAnswers())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsResourceRecord);
			}
			stringBuilder.Append("Authority:\r\n");
			foreach (DnsResourceRecord dnsResourceRecord2 in this.GetAuthority())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsResourceRecord2);
			}
			stringBuilder.Append("Additional:\r\n");
			foreach (DnsResourceRecord dnsResourceRecord3 in this.GetAdditional())
			{
				stringBuilder.AppendFormat("\t{0}\r\n", dnsResourceRecord3);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x040001FE RID: 510
		private static readonly ReadOnlyCollection<DnsResourceRecord> EmptyRR = new ReadOnlyCollection<DnsResourceRecord>(new DnsResourceRecord[0]);

		// Token: 0x040001FF RID: 511
		private static readonly ReadOnlyCollection<DnsQuestion> EmptyQS = new ReadOnlyCollection<DnsQuestion>(new DnsQuestion[0]);

		// Token: 0x04000200 RID: 512
		private ReadOnlyCollection<DnsQuestion> question;

		// Token: 0x04000201 RID: 513
		private ReadOnlyCollection<DnsResourceRecord> answer;

		// Token: 0x04000202 RID: 514
		private ReadOnlyCollection<DnsResourceRecord> authority;

		// Token: 0x04000203 RID: 515
		private ReadOnlyCollection<DnsResourceRecord> additional;

		// Token: 0x04000204 RID: 516
		private int offset = 12;
	}
}
