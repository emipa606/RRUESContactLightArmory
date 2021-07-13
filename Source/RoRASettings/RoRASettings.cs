using UnityEngine;
using Verse;

namespace RoRASettings
{
    // Token: 0x02000003 RID: 3
    public class RoRASettings : ModSettings
    {
        // Token: 0x04000005 RID: 5
        public int RoRAversionNumber;

        // Token: 0x06000006 RID: 6 RVA: 0x000021BE File Offset: 0x000003BE

        // Token: 0x06000005 RID: 5 RVA: 0x000021A1 File Offset: 0x000003A1
        public override void ExposeData()
        {
            Scribe_Values.Look(ref RoRAversionNumber, "RoRAversionNumber");
            base.ExposeData();
        }

        // Token: 0x02000004 RID: 4
        public class RoRAMod : Mod
        {
            // Token: 0x04000006 RID: 6
            private readonly RoRASettings settings1;

            // Token: 0x06000007 RID: 7 RVA: 0x000021CE File Offset: 0x000003CE
            public RoRAMod(ModContentPack content) : base(content)
            {
                settings1 = GetSettings<RoRASettings>();
            }

            // Token: 0x06000008 RID: 8 RVA: 0x000021E8 File Offset: 0x000003E8
            public override void DoSettingsWindowContents(Rect inRect)
            {
                var listing_Standard = new Listing_Standard();
                listing_Standard.Begin(inRect);
                listing_Standard.Label("RoRAUsePatchCEDesc".Translate());
                if (listing_Standard.RadioButton("RoRAUsePatchVanilla".Translate(),
                    settings1.RoRAversionNumber == 0, 8f))
                {
                    settings1.RoRAversionNumber = 0;
                }

                if (listing_Standard.RadioButton("RoRAUsePatchCE15".Translate(),
                    settings1.RoRAversionNumber == 1, 8f))
                {
                    settings1.RoRAversionNumber = 1;
                    Log.Message("[RoR Armors] Settings: Patch 1.5 Selected");
                }

                if (listing_Standard.RadioButton("RoRAUsePatchCE16".Translate(),
                    settings1.RoRAversionNumber == 2, 8f))
                {
                    settings1.RoRAversionNumber = 2;
                    Log.Message("[RoR Armors] Settings: Patch 1.6 Selected");
                }

                listing_Standard.End();
            }

            // Token: 0x06000009 RID: 9 RVA: 0x000022DC File Offset: 0x000004DC
            public override string SettingsCategory()
            {
                return "RoRASettingsModName".Translate();
            }
        }
    }
}