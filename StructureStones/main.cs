using HarmonyLib;
using MelonLoader;
using Il2CppRUMBLE.MoveSystem;
using RumbleModdingAPI;
using System;
using UnityEngine;
using Il2CppRUMBLE.Environment;
using RumbleModUI;

namespace StructureStones
{
    public class main : MelonMod
    {
        [HarmonyPatch(typeof(Structure), "Start")]
        public static class StructureSpawn
        {
            private static void Postfix(ref Structure __instance)
            {
                MeshRenderer structureMeshRenderer;
                try
                {
                    if (__instance.processableComponent.gameObject.GetComponent<Tetherball>() != null)
                    {
                        structureMeshRenderer = __instance.processableComponent.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[8].SavedValue];
                    }
                }
                catch (Exception e)
                {
                    MelonLogger.Error(e);
                    return;
                }
            }
        }

        [HarmonyPatch(typeof(Structure), "OnFetchFromPool")]
        public static class StructureRespawn
        {
            private static void Postfix(ref Structure __instance)
            {
                string name = __instance.processableComponent.gameObject.name;
                MeshRenderer structureMeshRenderer;
                try
                {
                    if (__instance.processableComponent.gameObject.GetComponent<Tetherball>() != null)
                    {
                        name = "BoulderBall";
                        structureMeshRenderer = __instance.processableComponent.gameObject.transform.GetChild(0).GetComponent<MeshRenderer>();
                    }
                    else if (__instance.processableComponent.gameObject.GetComponent<MeshRenderer>() != null)
                    {
                        structureMeshRenderer = __instance.processableComponent.gameObject.GetComponent<MeshRenderer>();
                    }
                    else
                    {
                        structureMeshRenderer = __instance.processableComponent.gameObject.transform.GetComponentInChildren<MeshRenderer>();
                    }
                }
                catch (Exception e)
                {
                    MelonLogger.Error(e);
                    return;
                }
                switch (name)
                {
                    case "LargeRock":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[7].SavedValue];
                        break;
                    case "SmallRock":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[6].SavedValue];
                        break;
                    case "Pillar":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[2].SavedValue];
                        break;
                    case "Disc":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[1].SavedValue];
                        break;
                    case "Wall":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[5].SavedValue];
                        break;
                    case "RockCube":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[4].SavedValue];
                        break;
                    case "Ball":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[3].SavedValue];
                        break;
                    case "BoulderBall":
                        structureMeshRenderer.material = stoneMaterials[(int)StructureStones.Settings[8].SavedValue];
                        break;
                }
            }
        }

        private bool sceneChanged = false;
        private string currentScene = "";
        private bool init = false;
        public GameObject materialsParent;
        public GameObject[] materialsHolder;
        public static Material[] stoneMaterials = new Material[8];
        private UI UI = UI.instance;
        public static Mod StructureStones = new Mod();
        private int[] preSavedValues = new int[8];
        
        public override void OnLateInitializeMelon()
        {
            StructureStones.ModName = "StructureStones";
            StructureStones.ModVersion = "2.0.2";
            StructureStones.SetFolder("StructureStones");
            StructureStones.AddDescription("Description", "Description", "Sets Structure Skins to Shiftstone Skins", new Tags { IsSummary = true });
            StructureStones.AddToList("Disc", 1, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("Pillar", 2, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("Ball", 3, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("Cube", 4, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("Wall", 5, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("Small Rock", 6, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("Large Rock", 0, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.AddToList("BoulderBall", 7, "Volatile Stone: 0" + Environment.NewLine + "Charge Stone: 1" + Environment.NewLine + "Surge Stone: 2" + Environment.NewLine + "Flow Stone: 3" + Environment.NewLine + "Guard Stone: 4" + Environment.NewLine + "Stubborn Stone: 5" + Environment.NewLine + "Adamant Stone: 6" + Environment.NewLine + "Vigor Stone: 7", new Tags { });
            StructureStones.GetFromFile();
            for(int i = 0; i < StructureStones.Settings.Count - 1; i++)
            {
                preSavedValues[i] = (int)StructureStones.Settings[i + 1].SavedValue;
            }
            UI.instance.UI_Initialized += UIInit;
            StructureStones.ModSaved += Save;
        }

        public override void OnSceneWasLoaded(int buildIndex, string sceneName)
        {
            currentScene = sceneName;
            sceneChanged = true;
        }

        public override void OnFixedUpdate()
        {
            if (sceneChanged)
            {
                try
                {
                    if ((currentScene == "Loader") && (!init))
                    {
                        //Initialization
                        GameObject.DontDestroyOnLoad(materialsParent = new GameObject());
                        materialsParent.name = "StructureStonesMaterialsParent";
                        GameObject pools = Calls.Pools.ShiftStones.GetPoolAdamantStone().transform.parent.gameObject;
                        materialsHolder = new GameObject[8];
                        for (int i = 0; i < 8; i++)
                        {
                            materialsHolder[i] = new GameObject();
                            materialsHolder[i].transform.parent = materialsParent.transform;
                            stoneMaterials[i] = new Material(pools.transform.GetChild(i+3).GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material);
                            materialsHolder[i].name = stoneMaterials[i].name;
                            materialsHolder[i].AddComponent<MeshRenderer>();
                            materialsHolder[i].GetComponent<MeshRenderer>().material = stoneMaterials[i];
                        }
                        init = true;
                        Log("Initialized");
                    }
                    //Stop if not Initialized
                    if (!init)
                    {
                        return;
                    }
                }
                catch (Exception e)
                {
                    MelonLogger.Error(e);
                    return;
                }
                sceneChanged = false;
            }
        }

        public static void Log(string msg)
        {
            MelonLogger.Msg(msg);
        }

        private void Save()
        {
            for (int i = 1; i < StructureStones.Settings.Count - 1; i++)
            {
                if (preSavedValues[i] != (int)StructureStones.Settings[i + 1].SavedValue)
                {
                    switch (i)
                    {
                        case 0: //disc
                            GameObject discPool = Calls.Pools.Structures.GetPoolDisc();
                            for (int j = 0; j < discPool.transform.childCount; j++)
                            {
                                if (discPool.transform.GetChild(j).gameObject.active)
                                    discPool.transform.GetChild(j).GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 1: //pillar
                            GameObject pillarPool = Calls.Pools.Structures.GetPoolPillar();
                            for (int j = 0; j < pillarPool.transform.childCount; j++)
                            {
                                if (pillarPool.transform.GetChild(j).gameObject.active)
                                    pillarPool.transform.GetChild(j).GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 2: //ball
                            GameObject ballPool = Calls.Pools.Structures.GetPoolBall();
                            for (int j = 0; j < ballPool.transform.childCount; j++)
                            {
                                if (ballPool.transform.GetChild(j).gameObject.active)
                                    ballPool.transform.GetChild(j).GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 3: //cube
                            GameObject cubePool = Calls.Pools.Structures.GetPoolCube();
                            for (int j = 0; j < cubePool.transform.childCount; j++)
                            {
                                if (cubePool.transform.GetChild(j).gameObject.active)
                                    cubePool.transform.GetChild(j).GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 4: //wall
                            GameObject wallPool = Calls.Pools.Structures.GetPoolWall();
                            for (int j = 0; j < wallPool.transform.childCount; j++)
                            {
                                if (wallPool.transform.GetChild(j).gameObject.active)
                                    wallPool.transform.GetChild(j).GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 5: //small rock
                            GameObject smallRockPool = Calls.Pools.Structures.GetPoolSmallRock();
                            for (int j = 0; j < smallRockPool.transform.childCount; j++)
                            {
                                if (smallRockPool.transform.GetChild(j).gameObject.active)
                                    smallRockPool.transform.GetChild(j).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 6: //large rock
                            GameObject largeRockPool = Calls.Pools.Structures.GetPoolLargeRock();
                            for (int j = 0; j < largeRockPool.transform.childCount; j++)
                            {
                                if (largeRockPool.transform.GetChild(j).gameObject.active)
                                    largeRockPool.transform.GetChild(j).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                        case 7: //boulderball
                            GameObject boulderBallPool = Calls.Pools.Structures.GetPoolBoulderBall();
                            for (int j = 0; j < boulderBallPool.transform.childCount; j++)
                            {
                                if (boulderBallPool.transform.GetChild(j).gameObject.active)
                                    boulderBallPool.transform.GetChild(j).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            if (currentScene == "Gym")
                            {
                                Calls.GameObjects.Gym.Logic.Toys.Tetherball0.Ball.GetGameObject().transform.GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                                Calls.GameObjects.Gym.Logic.Toys.Tetherball1.Ball.GetGameObject().transform.GetChild(0).GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            else if (currentScene == "Park")
                            {
                                Calls.GameObjects.Park.Logic.ParkToys.Tetherball0.Ball.Ball_.GetGameObject().GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                                Calls.GameObjects.Park.Logic.ParkToys.Tetherball1.Ball.Ball_.GetGameObject().GetComponent<MeshRenderer>().material = stoneMaterials[(int)StructureStones.Settings[i + 1].SavedValue];
                            }
                            break;
                    }
                    preSavedValues[i] = (int)StructureStones.Settings[i + 1].SavedValue;
                }
            }
        }

        private void UIInit()
        {
            UI.AddMod(StructureStones);
        }
    }
}
