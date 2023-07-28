﻿using CommunityToolkit.Maui.Views;
using DoomBatteryApp_MAUI.Properties;
using System.Media;

namespace DoomBatteryApp_MAUI;

public partial class MainPage : ContentPage
{
    // http://www.wolfensteingoodies.com/archives/olddoom/music.htm

    int count = 0;

    BatteryState batteryState_old;
    BatteryState batteryState = Battery.State;

    int batteryLevel_old;
    int batteryLevel = int.Parse($"{Battery.ChargeLevel * 100:F0}");

    readonly Random rand = new Random();

    public class BatteryMonitor
    {

        // Custom EventArgs class to hold the battery information.
        public class BatteryInfoChangedEventArgs : EventArgs
        {
            public int BatteryLevel { get; set; }

            public BatteryState BatteryState { get; set; }

            // Add more properties related to battery information if needed.
        }

        public static event EventHandler<BatteryInfoChangedEventArgs> BatteryInfoChanged;


        // Method to raise the event when the battery information changes.
        public static void OnBatteryInfoChanged(int batteryLevel, BatteryState batteryState)
        {
            var args = new BatteryInfoChangedEventArgs { BatteryLevel = batteryLevel, BatteryState = batteryState };
            BatteryInfoChanged?.Invoke(null, args);
        }
    }

    public MainPage()
    {
        InitializeComponent();

        UpdateLabelWithBatteryPercentage();
        UpdateDoomFace(1);

        Battery.BatteryInfoChanged += BatteryMonitor_BatteryInfoChanged;
        BatteryMonitor.OnBatteryInfoChanged(batteryLevel, Battery.State);
    }

    private void BatteryMonitor_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
        batteryState_old = batteryState;
        batteryState = Battery.State;

        OnCounterClicked(sender, e);
    }

    /// <summary>
    /// This version use different pictures with a Task.Delay(delay) inbetween
    /// </summary>
    /// <param name="duration">Loop length of frame cycle</param>
    /// <param name="delay">Time between next frame</param>
    private async void UpdateDoomFace(int duration = 5, int delay = 1100)
    {

        // if (Battery.State == BatteryState.Full) { // Smile?}
        // When getting power - God Mode Yellow Eyes 🔋⚡
        if (Battery.State == BatteryState.Charging || Battery.State == BatteryState.Full)
        {
            await SmileGetPower(delay);
            await System.Threading.Tasks.Task.Delay(delay);  

            DoomGuyImage.Source = "gm3.png";
            //DoomGuyImage.Source = "gm1.png";

            await System.Threading.Tasks.Task.Delay(delay);
        }
        else // Mortal Man -- Gods Do Not Bleed 🤕🩸
        {
            await CheckPainDrain(delay);

            //  Normal Face Behavior based on Battery level  
            #region MainFaceLoigc

            if (batteryLevel <= 100 && batteryLevel >= 80)
            {
                //DoomGoodFace
                for (int i = 0; i < duration; i++)
                {
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomgoodfacel.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomgoodface.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomgoodfaces.png";
                }

            }
            else if (batteryLevel <= 79 && batteryLevel >= 60)
            {
                for (int i = 0; i < duration; i++)
                {
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doommussedl.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doommussed.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doommusseds.png";
                }
            }
            else if (batteryLevel <= 59 && batteryLevel >= 40)
            {
                for (int i = 0; i < duration; i++)
                {
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomswollenl.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomswollen.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomswollens.png";
                }
            }
            else if (batteryLevel <= 39 && batteryLevel >= 20)
            {
                for (int i = 0; i < duration; i++)
                {
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomdirtyl.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomdirty.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doomdirtys.png";
                }
            }
            else if (batteryLevel <= 19 && batteryLevel >= 1)
            {
                for (int i = 0; i < duration; i++)
                {
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doombloodyl.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doombloody.png";
                    await System.Threading.Tasks.Task.Delay(delay);
                    DoomGuyImage.Source = "doombloodys.png";
                }
            }

            #endregion

        }

    }

    /// <summary>
    /// Smile behavior when going from BatteryState.Discharging => BatteryState.Charging
    /// <para>Make a big smile, play the shotgun sound 😁🔫🔊</para>
    /// </summary>
    private async Task SmileGetPower(int delay)
    {
        // Initial behavior when going from BatteryState.Discharging to BatteryState.Charging
        if (batteryState_old == BatteryState.Discharging && Battery.State == BatteryState.Charging)
        {

            #region Big Smile 😁

            // Start with a big smile 😁
            if (batteryLevel <= 100 && batteryLevel >= 80) // DoomGoodFace
            {
                DoomGuyImage.Source = "s_doomgf.png"; // Smile Doom Good Face
            }
            else if (batteryLevel <= 79 && batteryLevel >= 60) // Doom Mussed
            {
                DoomGuyImage.Source = "s_doomm.png"; // Smile Doom Good Face
            }
            else if (batteryLevel <= 59 && batteryLevel >= 40) // Doom Swollen
            {
                DoomGuyImage.Source = "s_dooms.png"; // Smile Doom Good Face
            }
            else if (batteryLevel <= 39 && batteryLevel >= 20) // Doom Dirty
            {
                DoomGuyImage.Source = "s_doomd.png"; // Smile Doom Good Face
            }
            else if (batteryLevel <= 19 && batteryLevel >= 1) // Doom Bloody
            {
                DoomGuyImage.Source = "s_doomb.png"; // Smile Doom Good Face
            }

            #endregion

            PlayWeaponPickupSoundFromResource();
            await System.Threading.Tasks.Task.Delay(delay);
        }
    }



    private async Task CheckPainDrain(int delay)
    {
        int direction = rand.Next(1, 3);

        // Damage face & Sound effect, if battery goes down since last check
        if (batteryLevel < batteryLevel_old)
        {
            if (batteryLevel <= 100 && batteryLevel >= 80) // DoomGoodFace
            {
                switch (direction)
                {
                    case 1:
                        DoomGuyImage.Source = "p_doomgf.png"; // Damage Straight
                        break;
                    case 2:
                        DoomGuyImage.Source = "p_doomgfl.png"; // Damage Left
                        break;
                    case 3:
                        DoomGuyImage.Source = "p_doomgfr.png"; // Damage Right
                        break;
                }
            }
            else if (batteryLevel <= 79 && batteryLevel >= 60) // Doom Mussed
            {
                switch (direction)
                {
                    case 1:
                        DoomGuyImage.Source = "p_doomm.png"; // Damage Straight
                        break;
                    case 2:
                        DoomGuyImage.Source = "p_doomml.png"; // Damage Left
                        break;
                    case 3:
                        DoomGuyImage.Source = "p_doommr.png"; // Damage Right
                        break;
                }
            }
            else if (batteryLevel <= 59 && batteryLevel >= 40) // Doom Swollen
            {
                switch (direction)
                {
                    case 1:
                        DoomGuyImage.Source = "p_dooms.png"; // Damage Straight
                        break;
                    case 2:
                        DoomGuyImage.Source = "p_doomsl.png"; // Damage Left
                        break;
                    case 3:
                        DoomGuyImage.Source = "p_doomsr.png"; // Damage Right
                        break;
                }
            }
            else if (batteryLevel <= 39 && batteryLevel >= 20) // Doom Dirty
            {
                switch (direction)
                {
                    case 1:
                        DoomGuyImage.Source = "p_doomd.png"; // Damage Straight
                        break;
                    case 2:
                        DoomGuyImage.Source = "p_doomdl.png"; // Damage Left
                        break;
                    case 3:
                        DoomGuyImage.Source = "p_doomdr.png"; // Damage Right
                        break;
                }
            }
            else if (batteryLevel <= 19 && batteryLevel >= 1) // Doom Bloody
            {
                switch (direction)
                {
                    case 1:
                        DoomGuyImage.Source = "p_doomb.png"; // Damage Straight
                        break;
                    case 2:
                        DoomGuyImage.Source = "p_doombl.png"; // Damage Left
                        break;
                    case 3:
                        DoomGuyImage.Source = "p_doombr.png"; // Damage Right
                        break;
                }
            }







            // TODO: All Health Conditions
            await System.Threading.Tasks.Task.Delay(delay);
            // Play Sound Effect
            PlayPainSoundFromResource();
            await System.Threading.Tasks.Task.Delay(delay * 2);
        }
    }

    /// <summary>
    /// Updates the main label with current battery level 🔋⚡%
    /// </summary>
    private void UpdateLabelWithBatteryPercentage()
    {

        batteryLevel_old = batteryLevel;
        batteryLevel = int.Parse($"{Battery.ChargeLevel * 100:F0}");

        if (batteryLevel == -1)
        {
            Label_Hello.Text = "Battery information not available";
        }
        else
        {
            Label_Hello.Text = $"{batteryLevel}%";
        }
    }

    private void OnCounterClicked(object sender, EventArgs e)
    {
        count++;

        if (count == 1)
            CounterBtn.Text = $"Clicked {count} time";
        else
            CounterBtn.Text = $"Clicked {count} times";

        SemanticScreenReader.Announce(CounterBtn.Text);

        UpdateLabelWithBatteryPercentage();
        UpdateDoomFace();

    }

    /// <summary>
    /// <para>Play Damage Sound, when battery is draining 🔋⬇️</para>
    /// Creates a temp audio file with UnmanagedMemoryStream Resource dsplpain 📜🔊
    /// </summary>
    private void PlayPainSoundFromResource()
    {
        UnmanagedMemoryStream unmanagedMemoryStream = SoundResource.dsplpain;

        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempaudio1.wav");

        // Write the UnmanagedMemoryStream to the temporary file
        using (var fileStream = File.Create(tempFilePath))
        {
            unmanagedMemoryStream.CopyTo(fileStream);
        }

        MyMedia.Source = MediaSource.FromFile(tempFilePath);
        MyMedia.Play();
    }

    /// <summary>
    /// <para>Play Shotgun Sound, when charging 🔫🔊🔋🔌</para>
    /// Creates a temp audio file with UnmanagedMemoryStream Resource dswpnup 📜🔊
    /// </summary>
    private void PlayWeaponPickupSoundFromResource()
    {
        UnmanagedMemoryStream unmanagedMemoryStream = SoundResource.dswpnup;

        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempaudio2.wav");

        // Write the UnmanagedMemoryStream to the temporary file
        using (var fileStream = File.Create(tempFilePath))
        {
            unmanagedMemoryStream.CopyTo(fileStream);
        }

        MyMedia.Source = MediaSource.FromFile(tempFilePath);
        MyMedia.Play();
    }

    /// <summary>
    /// <para>Play Item Sound, when battery is charging 🔋⬆️</para>
    /// Creates a temp audio file with UnmanagedMemoryStream Resource dsitemup 📜🔊
    /// </summary>
    private void PlayItemSoundFromResource()
    {
        UnmanagedMemoryStream unmanagedMemoryStream = SoundResource.dsitemup;

        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempaudio3.wav");

        // Write the UnmanagedMemoryStream to the temporary file
        using (var fileStream = File.Create(tempFilePath))
        {
            unmanagedMemoryStream.CopyTo(fileStream);
        }

        MyMedia.Source = MediaSource.FromFile(tempFilePath);
        MyMedia.Play();
    }


}

