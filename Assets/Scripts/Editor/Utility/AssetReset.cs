#if UNITY_EDITOR
using Dennis.Variable;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Dennis.Utility
{
    [InitializeOnLoad]
    public static class AssetReset
    {
        static AssetReset()
        {
            EditorApplication.playModeStateChanged += ResetAssets;
        }

        private static void ResetAssets(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingPlayMode)
            {
                ResetAssetsOnExit();
            }
        }

        private static void ResetAssetsOnExit()
        {
            List<Object> resetAssets = Utility.FindAssetsByType<Object>().ToList().Where(x => x is IResetSOValues).ToList();

            if (resetAssets.Count == 0) return;
            foreach (var asset in resetAssets)
            {
                ((IResetSOValues)asset).ResetSOValues();
            }
        }
    }
}
#endif