using System;
using GTA;
using GTA.Native;

public class RadioControl : Script
{
    private bool _radioEnabled;
    private bool _keyPressed;
    
    public RadioControl()
    {
        Tick += OnTick;
        Interval = 10;
    }

    private void OnTick(object sender, EventArgs e)
    {
        var player = Game.Player.Character;
        
        if (!player.IsInVehicle()) return;
        
        var vehicle = player.CurrentVehicle;

        var isKeyDown = Game.IsKeyPressed(System.Windows.Forms.Keys.Subtract);
        
        if (isKeyDown && !_keyPressed)
        {
            _radioEnabled = !_radioEnabled;
            
            Function.Call(Hash.SET_VEH_RADIO_STATION, vehicle, "RADIO_24"); 
            
            Function.Call(Hash.SET_VEHICLE_RADIO_ENABLED, vehicle, _radioEnabled);
            
            ShowNotification(_radioEnabled ? "~g~电台已开启" : "~r~电台已关闭");
            
            Audio.PlaySoundFrontend("NAV_UP_DOWN", "HUD_FRONTEND_DEFAULT_SOUNDSET");
        }
        
        _keyPressed = isKeyDown;
    }

    private void ShowNotification(string message)
    {
        Function.Call(
            Hash._SET_NOTIFICATION_TEXT_ENTRY,
            "CELL_EMAIL_BCON"
        );
        Function.Call(
            Hash._ADD_TEXT_COMPONENT_STRING,
            message
        );
        Function.Call(
            Hash._DRAW_NOTIFICATION, 
            false, 
            true
        );
    }
}    