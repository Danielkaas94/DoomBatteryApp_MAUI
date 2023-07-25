namespace DoomBatteryApp_MAUI;

public partial class MainPage : ContentPage
{
    int count = 0;

    int batteryLevel = int.Parse($"{Battery.ChargeLevel * 100:F0}");

    public MainPage()
    {
        InitializeComponent();

        UpdateLabelWithBatteryPercentage();

        UpdateDoomFace(10);
    }

    /// <summary>
    /// This version use different pictures with a Task.Delay()
    /// </summary>
    private async void UpdateDoomFace(int duration = 5, int delay = 1100)
    {
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
    }

    private void UpdateLabelWithBatteryPercentage()
    {
        batteryLevel = int.Parse($"{Battery.ChargeLevel * 100:F0}");

        if (batteryLevel == -1)
        {
            Label_Hello.Text = "Battery information not available";
        }
        else
        {
            Label_Hello.Text = $"{batteryLevel}%";
        }

        UpdateDoomFace();
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
    }
}

