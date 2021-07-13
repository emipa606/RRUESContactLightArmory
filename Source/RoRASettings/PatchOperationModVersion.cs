using System.Collections.Generic;
using System.Xml;
using Verse;

namespace RoRASettings
{
    // Token: 0x02000002 RID: 2
    public class PatchOperationModVersion : PatchOperation
    {
        // Token: 0x04000003 RID: 3
        private PatchOperation match;

        // Token: 0x04000002 RID: 2
        private List<string> mods;

        // Token: 0x04000001 RID: 1
        private List<int> modSettingverNum;

        // Token: 0x04000004 RID: 4
        private PatchOperation nomatch;

        // Token: 0x06000004 RID: 4 RVA: 0x00002198 File Offset: 0x00000398

        // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
        protected override bool ApplyWorker(XmlDocument xml)
        {
            var hasActiveMod = false;
            var hasRightVersion = false;
            foreach (var name in mods)
            {
                if (!ModLister.HasActiveModWithName(name))
                {
                    continue;
                }

                hasActiveMod = true;
                break;
            }

            foreach (var checkedout in modSettingverNum)
            {
                if (!CheckSettingsVersionNumber(checkedout))
                {
                    continue;
                }

                hasRightVersion = true;
                break;
            }

            if (!hasActiveMod || !hasRightVersion)
            {
                return true;
            }

            Log.Message("[RoR Armors] PatchOperationModVersion passed: Loading selected patch");
            if (match != null)
            {
                return match.Apply(xml);
            }

            if (nomatch != null)
            {
                return nomatch.Apply(xml);
            }

            return true;
        }

        // Token: 0x06000002 RID: 2 RVA: 0x00002138 File Offset: 0x00000338
        public override string ToString()
        {
            return $"{base.ToString()}({mods.ToCommaList()})";
        }

        // Token: 0x06000003 RID: 3 RVA: 0x00002168 File Offset: 0x00000368
        public static bool CheckSettingsVersionNumber(int checkedout)
        {
            var roRAversionNumber = LoadedModManager.GetMod<RoRASettings.RoRAMod>().GetSettings<RoRASettings>()
                .RoRAversionNumber;
            return roRAversionNumber == checkedout;
        }
    }
}