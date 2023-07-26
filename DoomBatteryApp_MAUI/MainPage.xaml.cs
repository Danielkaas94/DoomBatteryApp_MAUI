using System.Diagnostics;

namespace DoomBatteryApp_MAUI;

public partial class MainPage : ContentPage
{
    int count = 0;

    int batteryLevel_old;
    int batteryLevel = int.Parse($"{Battery.ChargeLevel * 100:F0}");


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
        UpdateDoomFace();
        //BatteryMonitor.BatteryInfoChanged += BatteryMonitor_BatteryInfoChanged;
        //BatteryMonitor.OnBatteryInfoChanged(batteryLevel, Battery.State);

        Battery.BatteryInfoChanged += BatteryMonitor_BatteryInfoChanged;
        BatteryMonitor.OnBatteryInfoChanged(batteryLevel, Battery.State);

        //UpdateLabelWithBatteryPercentage();
        //UpdateDoomFace(10);

        //Battery.BatteryInfoChanged();

    }

    private void BatteryMonitor_BatteryInfoChanged(object sender, BatteryMonitor.BatteryInfoChangedEventArgs e)
    {
        //throw new NotImplementedException();
        OnCounterClicked(sender, e);
    }

    private void BatteryMonitor_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
        Console.Beep(1600, 700);
        OnCounterClicked(sender, e);
        //UpdateLabelWithBatteryPercentage();
        //UpdateDoomFace();
        Console.Beep(700, 700);
    }

    /// <summary>
    /// This version use different pictures with a Task.Delay(delay) inbetween
    /// </summary>
    /// <param name="duration">Loop length of frame cycle</param>
    /// <param name="delay">Time between next frame</param>
    private async void UpdateDoomFace(int duration = 5, int delay = 1100)
    {

        Debug.WriteLine($"{Battery.State}");

        // if (Battery.State == BatteryState.Full) { // Smile?}
        // When getting power - God Mode Yellow Eyes 🔋⚡
        if (Battery.State == BatteryState.Charging || Battery.State == BatteryState.Full) {

            // Start with a big smile? 😁

            DoomGuyImage.Source = "gm3.png";
            //DoomGuyImage.Source = "gm1.png";

            await System.Threading.Tasks.Task.Delay(delay);
            await System.Threading.Tasks.Task.Delay(delay);
            await System.Threading.Tasks.Task.Delay(delay);

        }
        else // Mortal Man -- Gods Do Not Bleed 🤕🩸
        {


            // Damage face & Sound effect, if battery goes down since last check
            if (batteryLevel < batteryLevel_old)
            {
                await System.Threading.Tasks.Task.Delay(delay);
                DoomGuyImage.Source = "dotnet_bot.png";

                // Play Sound Effect
                await System.Threading.Tasks.Task.Delay(delay);
                await System.Threading.Tasks.Task.Delay(delay);
                await System.Threading.Tasks.Task.Delay(delay);
                await System.Threading.Tasks.Task.Delay(delay);

            }



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

}

