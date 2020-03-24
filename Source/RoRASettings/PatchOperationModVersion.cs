using System;
using System.Collections.Generic;
using System.Xml;
using Verse;

namespace RoRASettings
{
	// Token: 0x02000002 RID: 2
	public class PatchOperationModVersion : PatchOperation
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		protected override bool ApplyWorker(XmlDocument xml)
		{
			bool flag = false;
			bool flag2 = false;
			for (int i = 0; i < this.mods.Count; i++)
			{
				bool flag3 = ModLister.HasActiveModWithName(this.mods[i]);
				if (flag3)
				{
					flag = true;
					break;
				}
			}
			for (int j = 0; j < this.modSettingverNum.Count; j++)
			{
				bool flag4 = PatchOperationModVersion.CheckSettingsVersionNumber(this.modSettingverNum[j]);
				if (flag4)
				{
					flag2 = true;
					break;
				}
			}
			bool flag5 = flag && flag2;
			if (flag5)
			{
				Log.Message("[RoR Armors] PatchOperationModVersion passed: Loading selected patch", false);
				bool flag6 = this.match != null;
				if (flag6)
				{
					return this.match.Apply(xml);
				}
				bool flag7 = this.nomatch != null;
				if (flag7)
				{
					return this.nomatch.Apply(xml);
				}
			}
			return true;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
		public override string ToString()
		{
			return string.Format("{0}({1})", base.ToString(), this.mods.ToCommaList(false));
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002168 File Offset: 0x00000368
		public static bool CheckSettingsVersionNumber(int checkedout)
		{
			int roRAversionNumber = LoadedModManager.GetMod<RoRASettings.RoRAMod>().GetSettings<RoRASettings>().RoRAversionNumber;
			return roRAversionNumber == checkedout;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002198 File Offset: 0x00000398
		public PatchOperationModVersion()
		{
		}

		// Token: 0x04000001 RID: 1
		private List<int> modSettingverNum;

		// Token: 0x04000002 RID: 2
		private List<string> mods;

		// Token: 0x04000003 RID: 3
		private PatchOperation match;

		// Token: 0x04000004 RID: 4
		private PatchOperation nomatch;
	}
}
