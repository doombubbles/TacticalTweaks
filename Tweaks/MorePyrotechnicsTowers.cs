using BTD_Mod_Helper.Api.Enums;
using HarmonyLib;
using Il2CppAssets.Scripts.Models.Towers;
using Il2CppAssets.Scripts.Simulation.Towers.TowerFilters;

namespace TacticalTweaks.Tweaks;

public class MorePyrotechnicsTowers : ToggleableTweak
{
    protected override bool DefaultEnabled => true;

    public override string Description =>
        "Expands the towers affected by Gwen's Pyrotechnics Support buff. " +
        "Currently just adds The Blazing Sun Desperado.";

    protected override string Icon => VanillaSprites.BuffIconGwendolinPyrotechnics;

    [HarmonyPatch(typeof(PyrotechnicsSupportFilter), nameof(PyrotechnicsSupportFilter.FilterTowerModel))]
    internal static class PyrotechnicsSupportFilter_FilterTowerModel
    {
        [HarmonyPostfix]
        internal static void Postfix(TowerModel towerModel, ref bool __result)
        {
            if (!GetInstance<MorePyrotechnicsTowers>().Enabled) return;

            __result |= towerModel.baseId == TowerType.Desperado &&
                        towerModel.appliedUpgrades?.Contains(UpgradeType.TheBlazingSun) == true;
        }
    }
}
