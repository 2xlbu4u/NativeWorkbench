using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GTA;
using GTA.Math;
using GTA.Native;

internal class Snippets
{
    private void RCMod()
    {
if (Game.IsKeyPressed(Keys.NumPad0))
{
        
    Function.Call(Hash.RENDER_SCRIPT_CAMS, 0, 0, 0, 0, 0);
    UI.ShowSubtitle("~r~Gameplay Camera Active", 1000);
}
if (Game.IsKeyPressed(Keys.NumPad1))
{
    // ctrl creates the camera
    if (Function.Call<int>(Hash.IS_CONTROL_PRESSED, 2, 36) != 0)
    {
        if (NativeWorkbench.Camera[0] != null)
            NativeWorkbench.Camera[0].Destroy();
        NativeWorkbench.Camera[0] = World.CreateCamera(GameplayCamera.Position, GameplayCamera.Rotation, 50f);
        UI.ShowSubtitle("Camera ~r~1 Created", 1000);
    }
    Function.Call(Hash.RENDER_SCRIPT_CAMS, 1, 0, NativeWorkbench.Camera[0].Handle, 1, 1);
    UI.ShowSubtitle("Camera ~r~1 Active", 1000);
}
// point at player
  if (NativeWorkbench.Camera[0] != null)
  {
            NativeWorkbench.Camera[0].PointAt(Game.Player.Character);
      
  }



        private void keyControlXYZ()
        {
            var changed = false;
            Vector3 xyz = NativeWorkbench.Camera[0].Position;
            if (Game.IsKeyPressed(Keys.NumPad8)) { xyz.X -= .1f; changed = true; }
            if (Game.IsKeyPressed(Keys.NumPad5)) { xyz.X += .1f; changed = true; }
            if (Game.IsKeyPressed(Keys.NumPad4)) { xyz.Y -= .1f; changed = true; }
            if (Game.IsKeyPressed(Keys.NumPad6)) { xyz.Y += .1f; changed = true; }
            if (Game.IsKeyPressed(Keys.NumPad1)) { xyz.Z += .1f; changed = true; }
            if (Game.IsKeyPressed(Keys.NumPad3)) { xyz.Z -= .1f; changed = true; }
            if (changed)
                NativeWorkbench.Camera[0].Position = xyz;
        }

        private void cameraDemo()
        {
                // Official final code
            if (Function.Call<int>(Hash.IS_CONTROL_PRESSED, 2, 36) != 0)
            {
                var changed = false;
                var keypress = Game.IsKeyPressed(Keys.NumPad0);
                Vector3 xyz = keypress ? NativeWorkbench.Camera[0].Rotation :
                    NativeWorkbench.Camera[0].Position;
                if (Game.IsKeyPressed(Keys.NumPad8)) { xyz.X -= .1f; changed = true; }
                if (Game.IsKeyPressed(Keys.NumPad5)) { xyz.X += .1f; changed = true; }
                if (Game.IsKeyPressed(Keys.NumPad4)) { xyz.Y -= .1f; changed = true; }
                if (Game.IsKeyPressed(Keys.NumPad6)) { xyz.Y += .1f; changed = true; }
                if (Game.IsKeyPressed(Keys.NumPad1)) { xyz.Z += .1f; changed = true; }
                if (Game.IsKeyPressed(Keys.NumPad3)) { xyz.Z -= .1f; changed = true; }
                if (changed)
                {
                    if (keypress)
                        NativeWorkbench.Camera[0].Rotation = xyz;
                    else
                        NativeWorkbench.Camera[0].Position = xyz;
                }
            }
            if (Game.IsKeyPressed(Keys.PageUp))
            {
                NativeWorkbench.Bool[0] = true;
                NativeWorkbench.Camera[0] = World.CreateCamera(GameplayCamera.Position, GameplayCamera.Rotation, 50f);
                Function.Call(Hash.RENDER_SCRIPT_CAMS, 1, 0, NativeWorkbench.Camera[0].Handle, 1, 1);
            }
            if (Game.IsKeyPressed(Keys.PageDown))
            {
                NativeWorkbench.Bool[0] = false;
                Function.Call(Hash.RENDER_SCRIPT_CAMS, 0, 0, NativeWorkbench.Camera[0].Handle, 1, 1);
                World.DestroyAllCameras();
            }         
        }


        private string keypressShow()
        {
            NativeWorkbench.IntList.Clear();
            for (int i = 0; i < 375; i++)
                if (Function.Call<int>(Hash.IS_CONTROL_PRESSED, 2, i) != 0)
                    NativeWorkbench.IntList.Add(i);
            var outStr = "";
            foreach (var keyVal in NativeWorkbench.IntList)
                outStr += (keyVal + " ");
            return outStr;
        }

        private string getLastTargettedEntity()
        {
            var thisEntity = Game.Player.GetTargetedEntity();
            if (thisEntity != null && thisEntity.Model != null)
                NativeWorkbench.Entity[0] = thisEntity;
            if (NativeWorkbench.Entity[0] == null)
                return null;
            var model = NativeWorkbench.Entity[0].Model;
            var modelHash = model.Hash;
            if (modelHash == 0)
                return "Model Hash was 0";
            var targetName = "";

            if (NativeWorkbench.Entity[0] is Vehicle)
                targetName = Enum.GetName(typeof(VehicleHash), modelHash);

            else if (NativeWorkbench.Entity[0] is Ped)
                targetName = Enum.GetName(typeof(PedHash), modelHash);

            else if (NativeWorkbench.Entity[0] is Prop)
                targetName = modelHash.ToString();

            if (string.IsNullOrEmpty(targetName))
                targetName = modelHash.ToString();

            return string.Format("{0}: {1}, {2}, {3}", targetName,
                    NativeWorkbench.Entity[0].Position.X,
                    NativeWorkbench.Entity[0].Position.Y,
                    NativeWorkbench.Entity[0].Position.Z);
        }



        private object somecrazyassshit()
        {

            var pedlist = World.GetNearbyPeds(Game.Player.Character, 7);
            foreach (var pedx in pedlist)
            {
                Function.Call(Hash.SET_PED_TO_RAGDOLL, pedx, 500, 5000, 0, 0, 0, 0);
                Script.Wait(300);
                pedx.Velocity = new Vector3(0, 0, 0);
                Script.Wait(900);
            }
            foreach (var car in World.GetNearbyVehicles(Game.Player.Character, 20))
                car.Velocity = new Vector3(10, 10, 12);
            return null;
        }

    static void pedRag(Ped pedp)
    {
        Function.Call(Hash.SET_PED_TO_RAGDOLL, pedp, 600, 5000, 0, 0, 0, 0);
        Script.Wait(300);
        pedp.Velocity = new Vector3(0, 0, 0);
        Script.Wait(900);
    }

    private static void carTrouble()
    {
        foreach (var car in World.GetNearbyVehicles(Game.Player.Character, 20))
        {
            Function.Call(Hash._DETACH_VEHICLE_WINDSCREEN, car.Handle);
            for (var i = 0; i <= 7; i++)
                Function.Call(Hash.SET_VEHICLE_DOOR_OPEN, car.Handle, i, 1, 1);
            //car.Velocity = new Vector3(10, 10, 12);
        }
    }

    static void pedDred()
    {
        foreach (var pedp in World.GetNearbyPeds(Game.Player.Character, 7))
        {
            Function.Call(Hash.SET_PED_TO_RAGDOLL, pedp, 600, 5000, 0, 0, 0, 0);
            Script.Wait(300);
            pedp.Velocity = new Vector3(0, 0, 0);
            Script.Wait(900);
        }
    }

    private static void pedArmaged()
    {
        var pedlist = new List<Ped>();
        pedlist.AddRange(World.GetNearbyPeds(Game.Player.Character, 7).ToList());
        foreach (var car in World.GetNearbyVehicles(Game.Player.Character, 20))
        {
            pedlist.Add(Function.Call<Ped>(Hash.GET_PED_IN_VEHICLE_SEAT, car.Handle, -1));

        }
        foreach (var pedp in pedlist)
        {
            Function.Call(Hash.SET_PED_TO_RAGDOLL, pedp, 600, 5000, 0, 0, 0, 0);
            Script.Wait(300);
            pedp.Velocity = new Vector3(0, 0, 0);
            Script.Wait(900);
        }

    }



        private void raincars()
        {
            
            //GET_OFFSET_FROM_ENTITY_IN_WORLD_COORDS (Entity 2, float 0, float 5, float 0)
            //GET_RANDOM_INT_IN_RANGE (int 0, int 347)
            //GET_HASH_KEY (char* "burrito3")
            //REQUEST_MODEL (Hash 0x98171BD3)
            //HAS_MODEL_LOADED (Hash 0x98171BD3)
            //HAS_MODEL_LOADED (Hash 0x98171BD3)
            //HAS_MODEL_LOADED (Hash 0x98171BD3)
            //GET_RANDOM_FLOAT_IN_RANGE (float 15, float 50.8)
            //GET_RANDOM_FLOAT_IN_RANGE (float 1, float 11.8)
            //GET_RANDOM_FLOAT_IN_RANGE (float 1, float 11.8)
            //CREATE_VEHICLE (Hash 0x98171BD3, float 2.776833, float -871.0274, float 52.44382, float 0, BOOL 0, BOOL 1)

        }

        private string lastTarget()
        {

            //NativeWorkbench.Bool[0]
            Entity thisEntity;
            if ((thisEntity = Game.Player.GetTargetedEntity()) != null && thisEntity.Position != Vector3.Zero)
                NativeWorkbench.Entity[0] = thisEntity;
            else
                thisEntity = NativeWorkbench.Entity[0];

            if (thisEntity != null)
            {
                var model = thisEntity.Model;
                string targetName = model.Hash.ToString();
                var hashTypeName = thisEntity.GetType().ToString();
                switch (hashTypeName)
                {
                    case "GTA.Vehicle":
                        {
                            targetName = Enum.GetName(typeof(VehicleHash), model.Hash);
                            break;
                        }
                    case "GTA.Ped":
                        {
                            targetName = Enum.GetName(typeof(PedHash), model.Hash);
                            break;
                        }
                    default:
                        {
                            targetName = model.Hash.ToString();
                            break;
                        }

                }
                if (targetName == "")
                    targetName = model.Hash.ToString();
                return string.Format("{0}: {1}, {2}, {3}", targetName,
                    thisEntity.Position.X,
                    thisEntity.Position.Y,
                    thisEntity.Position.Z);
            }
            return null;

        }
        private void PedManipulation()
        {
            var ped = Game.Player.Character;
            var pedP = ped.Position;
            Game.Player.Character.FreezePosition = true;
            Game.Player.Character.IsVisible = false;
            var newPos = new Vector3(pedP.X, pedP.Y, pedP.Z);
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 232)) newPos.Y -= 0.9f; // w
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 233)) newPos.Y += 0.9f; // s
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 234)) newPos.X -= 0.9f; // a
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 235)) newPos.X += 0.9f; // d
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 206)) newPos.Z -= 0.9f; // e
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 205)) newPos.Z += 0.9f; // q
            Function.Call(Hash.SET_ENTITY_COORDS, ped, newPos.X, newPos.Y,
                newPos.Z - 1.0f, 0, 1, 0);
            Function.Call(Hash.DESTROY_MOBILE_PHONE);

            if (NativeWorkbench.Entity[0].Position != Vector3.Zero)
                Game.Player.Character.Position = NativeWorkbench.Entity[0].Position;
        }


        private void scenario()
        {
            var papPed = World.CreatePed(new Model(-322270187), new Vector3(381.6675f, 154.636f, 103.1651f), 148.8721f);
            papPed.Task.StartScenario("WORLD_HUMAN_PAPARAZZI", papPed.Position);

            Game.Player.Character.Position = new Vector3(381.6675f, 154.636f, 103.1651f);

            // Tp to observe
            Game.Player.Character.Position = new Vector3(1747f, 3273.7f, 41.1f);
            var Paparazzi1 = World.CreatePed(new Model(-322270187), new Vector3(299.8314f, 184.3725f, 104.1479f), 148.8721f);
            Paparazzi1.Task.StartScenario("WORLD_HUMAN_PAPARAZZI", Paparazzi1.Position);
            Paparazzi1.Task.ClearAll();

        }

        private void AlienTeleportEffect()
        {
            var ptfx1 = "scr_rcbarry1";
            var ptfx2 = "scr_alien_teleport";
            var sound = "SPAWN";
            var soundset = "BARRY_01_SOUNDSET";
            var plyrPed = Game.Player.Character;

            Function.Call(Hash.PLAY_SOUND_FROM_ENTITY, -1, sound, plyrPed, soundset, false, 0, false);

            if (!Function.Call<bool>(Hash.HAS_NAMED_PTFX_ASSET_LOADED, ptfx1))
                Function.Call(Hash.REQUEST_NAMED_PTFX_ASSET, ptfx1);

            for (var i = 0; i < 3; i++)
            {
                Function.Call(Hash._SET_PTFX_ASSET_NEXT_CALL, ptfx1);
                Function.Call(Hash.START_PARTICLE_FX_NON_LOOPED_ON_ENTITY, ptfx2, plyrPed, 0, 0, 0, 0, 0,
                    0, 1f, false, false, false);
                Function.Call(Hash.SET_PARTICLE_FX_NON_LOOPED_ALPHA, 1f);
            }
        }

        private void PlayAudio()
        {   // These work
            Function.Call(Hash._PLAY_AMBIENT_SPEECH2, Game.Player.Character.Handle, "GENERIC_CURSE_HIGH", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL", 1);
            Function.Call(Hash._PLAY_AMBIENT_SPEECH2, Game.Player.Character.Handle, "GENERIC_FRIGHTENED_MED", "SPEECH_PARAMS_FORCE_SHOUTED_CRITICAL", 1);
        }
        //SET_ENTITY_ALPHA (Entity 2, int 103, BOOL 0)


        private Prop createUFO()
        {
            Vector3 offsetInWorldCoords = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0f, 0f, 40f));
            int num = Function.Call<int>(Hash.GET_HASH_KEY, "p_spinning_anus_s");
            var ufo = Function.Call<Prop>(Hash.CREATE_OBJECT, num, (float) offsetInWorldCoords.X,
                (float) offsetInWorldCoords.Y, (float) offsetInWorldCoords.Z, false, false, true);
            ufo.FreezePosition = true;
            return ufo;

        }

        private string spawnWindTurbine()
        {
            var model = new Model(1952396163);
            model.Request(250);
            if (!model.IsInCdImage || !model.IsValid)
                NativeWorkbench.Str[0] = "Not a Valid Model";
            while (!model.IsLoaded)
                Script.Wait(50);
            World.CreateProp(new Model(1952396163), Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 20, 0)), true, true);
            model.MarkAsNoLongerNeeded();
            NativeWorkbench.Str[0] = "done";
            return NativeWorkbench.Str[0];
        }

        private void Vaporize()
        {
            var thisEntity = Game.Player.GetTargetedEntity();
            if (thisEntity != null)
            {
                Script.Wait(500);
                thisEntity.Delete();
            }
        }
    private void createTRacker()
    {
        NativeWorkbench.Prop[0] = World.CreateProp(new Model(-1874162628), GameplayCamera.Position, GameplayCamera.Rotation, true, false);
    }
        private string propCreate()
        {
            var model = new Model("prop_boogieboard_02");
            model.Request(250);
            if (!model.IsInCdImage || !model.IsValid)
                NativeWorkbench.Str[0] = "Not a Valid Model";
            while (!model.IsLoaded)
                Script.Wait(50);
            var placeAt = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 10, 0));
            World.CreateProp(model, placeAt, true, true);
            model.MarkAsNoLongerNeeded();
            NativeWorkbench.Model[0] = model;
            NativeWorkbench.Str[0] = "done!";
            return NativeWorkbench.Str[0];
        }

        private void pappaRazzi()
        {
            var Paparazzi1 = World.CreatePed(new Model(-322270187), new Vector3(299.8314f, 184.3725f, 104.1479f), 148.8721f);
            Paparazzi1.Task.StartScenario("WORLD_HUMAN_PAPARAZZI", Paparazzi1.Position);
        }
        private void killself()
        {
            Function.Call(Hash.EXPLODE_PED_HEAD, Game.Player.Character, (int)WeaponHash.Pistol);
        }

        public void PoliceDrive()
        {
            Function.Call(Hash.EXPLODE_PED_HEAD, NativeWorkbench.Entity[0], (int)WeaponHash.Pistol);

            var model = new Model("POLICE");
            model.Request(1000);
            if (model.IsInCdImage && model.IsValid)
            {
                // If the model isn't loaded, wait until it is
                while (!model.IsLoaded)
                {
                    Script.Wait(0);
                }
                var vehicle = World.CreateVehicle(model.Hash,
                    Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 10, 0)));
                vehicle.PlaceOnNextStreet();
                vehicle.PlaceOnGround();
                vehicle.SirenActive = true;
                vehicle.FreezePosition = true;

            }

            NativeWorkbench.Model[0] = new Model(PedHash.Cop01SMY);
            NativeWorkbench.Model[0].Request(1000);
            // Check to see if the model is valid (just to make sure you are not given a null exception)
            if (NativeWorkbench.Model[0].IsInCdImage && NativeWorkbench.Model[0].IsValid)
            {
                // If the model isn't loaded, wait until it is
                while (!NativeWorkbench.Model[0].IsLoaded)
                {
                    Script.Wait(0);
                }
                NativeWorkbench.Ped[0] = World.CreatePed(NativeWorkbench.Model[0].Hash,
                    NativeWorkbench.Vehicle[0].Position.Around(1));
                Script.Wait(5);

                NativeWorkbench.Ped[0].SetIntoVehicle(NativeWorkbench.Vehicle[0], VehicleSeat.Driver);
                NativeWorkbench.Ped[0].Weapons.Give(WeaponHash.Pistol, 1000, true, true);

                NativeWorkbench.Ped[1] = World.CreatePed(NativeWorkbench.Model[0].Hash,
                    NativeWorkbench.Vehicle[0].Position.Around(1));
                Script.Wait(5);
                NativeWorkbench.Ped[1].SetIntoVehicle(NativeWorkbench.Vehicle[0], VehicleSeat.RightFront);
                Script.Wait(50);
                NativeWorkbench.Ped[1].Weapons.Give(WeaponHash.Pistol, 1000, true, true);
            }

            var loc = Game.Player.Character.Position;
            Script.Wait(100);
            NativeWorkbench.Ped[0].Task.DriveTo(NativeWorkbench.Vehicle[0], loc, 12, 16, 1);
            var vEntity = ((Entity) NativeWorkbench.Vehicle[0]);
            vEntity.Position = new Vector3(vEntity.Position.X, vEntity.Position.Y, vEntity.Position.Z + 10);



        }
        private void createVehicleProperly()
        {

            var model = new Model("POLICE");
            if (!model.IsInCdImage && !model.IsValid)
                return;
            model.Request(1000);
            while (!model.IsLoaded)
                Script.Wait(0);
            var placeAt = Game.Player.Character.GetOffsetInWorldCoords(new Vector3(0, 10, 0));
        }

        private string controls()
        {
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 7)) NativeWorkbench.Str[0] = "7"; // l
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 8)) NativeWorkbench.Str[0] = "8"; // s
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 9)) NativeWorkbench.Str[0] = "9"; // d
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 10)) NativeWorkbench.Str[0] = "10"; // Page up 
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 11)) NativeWorkbench.Str[0] = "11"; // Page down
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 12)) NativeWorkbench.Str[0] = "12"; // mouse move down
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 13)) NativeWorkbench.Str[0] = "13"; // mouse move right
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 18)) NativeWorkbench.Str[0] = "18"; // space or enter
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 19)) NativeWorkbench.Str[0] = "19"; // L Alt
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 20)) NativeWorkbench.Str[0] = "20"; // z
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 21)) NativeWorkbench.Str[0] = "21"; // L Shift
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 23)) NativeWorkbench.Str[0] = "23"; // f
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 32)) NativeWorkbench.Str[0] = "32"; // w
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 34)) NativeWorkbench.Str[0] = "34"; // a
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 36)) NativeWorkbench.Str[0] = "36"; // L Ctrl
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 39)) NativeWorkbench.Str[0] = "39"; // [
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 40)) NativeWorkbench.Str[0] = "40"; // ]
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 44)) NativeWorkbench.Str[0] = "44"; // q
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 45)) NativeWorkbench.Str[0] = "45"; // r
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 51)) NativeWorkbench.Str[0] = "51"; // e
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 56)) NativeWorkbench.Str[0] = "56"; // F9
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 57)) NativeWorkbench.Str[0] = "57"; // F10
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 58)) NativeWorkbench.Str[0] = "58"; // g
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 60)) NativeWorkbench.Str[0] = "60"; // num pad 5
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 61)) NativeWorkbench.Str[0] = "61"; // num pad 8
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 197)) NativeWorkbench.Str[0] = "197"; // ]
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 199)) NativeWorkbench.Str[0] = "199"; // p
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 202)) NativeWorkbench.Str[0] = "202"; // Backspace
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 205)) NativeWorkbench.Str[0] = "205"; // q
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 206)) NativeWorkbench.Str[0] = "206"; // e
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 209)) NativeWorkbench.Str[0] = "209"; // L Shift
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 211)) NativeWorkbench.Str[0] = "211"; // tab
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 213)) NativeWorkbench.Str[0] = "213"; // home
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 214)) NativeWorkbench.Str[0] = "214"; // del
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 215)) NativeWorkbench.Str[0] = "215"; // enter
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 216)) NativeWorkbench.Str[0] = "216"; // spacebar
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 217)) NativeWorkbench.Str[0] = "217"; // Caps lock
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 224)) NativeWorkbench.Str[0] = "224"; // L Ctrl
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 232)) NativeWorkbench.Str[0] = "232"; // w
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 233)) NativeWorkbench.Str[0] = "233"; // s
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 234)) NativeWorkbench.Str[0] = "234"; // a
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 235)) NativeWorkbench.Str[0] = "235"; // d
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 236)) NativeWorkbench.Str[0] = "236"; // v
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 239)) NativeWorkbench.Str[0] = "239"; // Cam +
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 240)) NativeWorkbench.Str[0] = "240"; // Cam -
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 243)) NativeWorkbench.Str[0] = "243"; // ~
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 244)) NativeWorkbench.Str[0] = "244"; // m
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 245)) NativeWorkbench.Str[0] = "245"; // t
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 246)) NativeWorkbench.Str[0] = "246"; // y
            if (Function.Call<bool>(Hash.IS_CONTROL_PRESSED, 2, 249)) NativeWorkbench.Str[0] = "249"; // n
return NativeWorkbench.Str[0];
        }

        private string targetted()
        {

            Entity thisEntity;
            if ((thisEntity = Game.Player.GetTargetedEntity()) != null && thisEntity.Position != Vector3.Zero)
                NativeWorkbench.Entity[0] = thisEntity;
            else
                thisEntity = NativeWorkbench.Entity[0];

            if (thisEntity != null)
            {
                var model = thisEntity.Model;
                string targetName = model.Hash.ToString();
                var hashTypeName = thisEntity.GetType().ToString();
                switch (hashTypeName)
                {
                    case "GTA.Vehicle":
                        {
                            targetName = Enum.GetName(typeof(VehicleHash), model.Hash);
                            break;
                        }
                    case "GTA.Ped":
                        {
                            targetName = Enum.GetName(typeof(PedHash), model.Hash);
                            break;
                        }
                    default:
                        {
                            targetName = model.Hash.ToString();
                            break;
                        }
                }
                if (targetName == "")
                    targetName = model.Hash.ToString();
                return string.Format("{0}: {1}, {2}, {3}", targetName,
                    thisEntity.Position.X,
                    thisEntity.Position.Y,
                    thisEntity.Position.Z);
            }
            return null;

        }
    }

    class blah
    {
        static void OnTick()
        {
            // Define inline if not already defined
            if (NativeWorkbench.Func[0] == null)
                NativeWorkbench.Func[0] = (keyList) =>
                {
                    var keyListArray = (Keys[])keyList;
                    return keyListArray.FirstOrDefault(Game.IsKeyPressed);
                };

            // Just a more friendly name
            var getKeyPressed = NativeWorkbench.Func[0];
            var gotKey = (Keys)getKeyPressed(new Keys[] { Keys.Oem1, Keys.Oem2, Keys.Oem3, Keys.Oem4 });
            if (gotKey > 0)
            {
                Debug.WriteLine(gotKey);
            }
        }
    }

