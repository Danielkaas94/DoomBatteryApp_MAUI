using CommunityToolkit.Maui.Views;
using DoomBatteryApp_MAUI.Properties;
using System.Diagnostics;
using System.Media;
using System.Text;

namespace DoomBatteryApp_MAUI;

public partial class MainPage : ContentPage
{
    // http://www.wolfensteingoodies.com/archives/olddoom/music.htm

    int count = 0;

    int batteryLevel_old;
    int batteryLevel = int.Parse($"{Battery.ChargeLevel * 100:F0}");

    readonly Random rand = new Random();

    private SoundPlayer player;


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

        //MyMedia.Play();
        UpdateLabelWithBatteryPercentage();
        UpdateDoomFace();

        //MyMedia.Pause();
        //BatteryMonitor.BatteryInfoChanged += BatteryMonitor_BatteryInfoChanged;
        //BatteryMonitor.OnBatteryInfoChanged(batteryLevel, Battery.State);

        Battery.BatteryInfoChanged += BatteryMonitor_BatteryInfoChanged;
        BatteryMonitor.OnBatteryInfoChanged(batteryLevel, Battery.State);

        //UpdateLabelWithBatteryPercentage();
        //UpdateDoomFace(10);

        //Device.
        //Battery.BatteryInfoChanged();

    }

    private void BatteryMonitor_BatteryInfoChanged(object sender, BatteryMonitor.BatteryInfoChangedEventArgs e)
    {
        //throw new NotImplementedException();
        OnCounterClicked(sender, e);
    }

    private void BatteryMonitor_BatteryInfoChanged(object sender, BatteryInfoChangedEventArgs e)
    {
        //Console.Beep(1600, 700);
        OnCounterClicked(sender, e);
        //UpdateLabelWithBatteryPercentage();
        //UpdateDoomFace();
        //Console.Beep(700, 700);
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

    private async Task CheckPainDrain(int delay)
    {
        // Damage face & Sound effect, if battery goes down since last check
        if (batteryLevel < batteryLevel_old)
        {

            switch (rand.Next(1, 3))
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

            // TODO: All Health Conditions

            //DoomGuyImage.Source = "dotnet_bot.png";
            //DoomGuyImage.Source = "p_doomgf.png";
            await System.Threading.Tasks.Task.Delay(delay);

            // Play Sound Effect
            MyMedia.Play();
            await System.Threading.Tasks.Task.Delay(delay * 3);
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

        System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();

        var test2 = Environment.CurrentDirectory;
        var test3 = Environment.ProcessPath;
        //var test = "C:\\Users\\danie\\source\\repos\\DoomBatteryApp_MAUI";
        //var test = "C:\\Users\\danie\\source\\repos\\DoomBatteryApp_MAUI";
        //var test = "\\system\\";
        //var test = "/system/bin/app_process64";
        var test = "/system/bin/";
        var testA = "C:\\system\\bin";

        //int index = test.IndexOf("Doom");

        //string test2 = test.Substring(0, index);

        //MyMedia.Source = "C:\\Users\\danie\\source\\repos\\DoomBatteryApp_MAUI\\dsplpain.wav";

        //var test = Environment.GetFolderPath;

        string appDataDirectory1 = FileSystem.AppDataDirectory;
        string appDataDirectory2 = FileSystem.CacheDirectory;
        //string appDataDirectory = FileSystem.CacheDirectory;
        string searchFileName = "dsplpain.wav"; // Replace with the name of the file you want to find

        //string[] foundFilesA = Directory.GetFiles(testA, searchFileName, SearchOption.AllDirectories);

        string[] foundFiles;
        // foundFiles = Directory.GetFiles(test, searchFileName, SearchOption.AllDirectories);
        string[] foundFiles1 = Directory.GetFiles(appDataDirectory1, searchFileName, SearchOption.AllDirectories);
        string[] foundFiles2 = Directory.GetFiles(appDataDirectory2, searchFileName, SearchOption.AllDirectories);
        string filePath = "";
        foreach (string file in foundFiles1)
        {
            filePath = file;

            if (File.Exists(file))
            {
                MyMedia.Source = file;
                MyMedia.Play();
            }
        }

        /*
        string searchDirectory = Environment.ProcessPath; // Replace with the directory you want to search in
        searchDirectory = Environment.CurrentDirectory; // Replace with the directory you want to search in
        string searchFileName = "dsplpain.txt"; // Replace with the name of the file you want to find

        // Search for all files that match the searchFileName in the searchDirectory and its subdirectories
        string[] foundFiles = Directory.GetFiles(searchDirectory, searchFileName, SearchOption.AllDirectories);

        foreach ( string file in foundFiles ) {
        
            if (File.Exists(file))
            {
                MyMedia.Source = file;
                MyMedia.Play();


            }
        }
         */

        //CounterBtn.Text = $"Clicked {test} times";
        CounterBtn.Text = $"Clicked {filePath} times";
        //MyMedia.Source = filePath;
        //MyMedia.Play();
        //MyMedia.Source = "C:\\Users\\danie\\source\\repos\\DoomBatteryApp_MAUI\\dsplpain.wav";
        //MyMedia.Play();

        /*
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.IO.Stream stream = assembly.GetManifestResourceStream("<AssemblyName>.dsplpain.wav");
            SoundPlayer player = new SoundPlayer(stream);
            player.Play();
        */

        // C:\Users\danie\source\repos\DoomBatteryApp_MAUI\DoomBatteryApp_MAUI\Resources\Images\dsplpain.wav
        //MyMedia.Source = "http://www.wolfensteingoodies.com/archives/olddoom/sounds/dsplpain.wav";
        //MyMedia.Source = DoomBatteryApp_MAUI.Properties.Resource1.dsplpain;

        UnmanagedMemoryStream unmanagedMemoryStream = Properties.Resource1.dsplpain;

        //TestTest();

        /*
         
            if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return;
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            player.Stream = Properties.Resources.imsend;
            player.Play();

         */






        //SoundPlayer soundPlayer = new SoundPlayer(unmanagedMemoryStream);
        //soundPlayer.Play();


        // Create a temporary file path to save the audio data
        string tempFilePath = Path.Combine(Path.GetTempPath(), "tempaudio.wav");

        // Write the UnmanagedMemoryStream to the temporary file
        using (var fileStream = File.Create(tempFilePath))
        {
            unmanagedMemoryStream.CopyTo(fileStream);
        }


        MyMedia.Source = MediaSource.FromFile(tempFilePath);
        //MyMedia.Source = Properties.Resource1.dsplpain;
        MyMedia.Play();


        //MyMedia.Source = DoomBatteryApp_MAUI.Properties.Resource1.dsplpain;
        //MyMedia.Play();



        //MyMedia.Source = "dsplpain.wav";       
        //MyMedia.Source = test. + "dsplpain.wav";
        //MyMedia.Source = "\\DoomBatteryApp_MAUI\\Resources\\dsplpain.wav";
        //MyMedia.Source = new Uri(@"");


        //MyMedia.Source = "C:\\Users\\danie\\source\\repos\\DoomBatteryApp_MAUI\\DoomBatteryApp_MAUI\\Resources\\Images\\dsplpain.wav";
        //MyMedia.Source = "C:\\Users\\danie\\source\\repos\\DoomBatteryApp_MAUI\\DoomBatteryApp_MAUI\\dsplpain.wav";


    }

    private void TestTest()
    {
        /*

                // Create a MediaElement instance
                var mediaElement = new MediaElement();

                // Assuming Resource1.dsplpain is the UnmanagedMemoryStream containing the audio data.
                UnmanagedMemoryStream unmanagedMemoryStream = Resource1.dsplpain;

                // Create a MemoryStream and copy the data from UnmanagedMemoryStream to MemoryStream.
                MemoryStream memoryStream = new MemoryStream();
                unmanagedMemoryStream.CopyTo(memoryStream);

                // Set the MediaElement.Source to the MemoryStream
                //mediaElement.Source = MediaSource.FromStream(() => new MemoryStream(memoryStream.ToArray()));
                mediaElement.Source = MediaSource.FromResource("dsplpain");

                // Add the MediaElement to the Content of the MainPage
                Content = new StackLayout
                {
                    Children = { mediaElement }
                };

                // Start playing the audio (optional)
                mediaElement.Play();
        */

        UnmanagedMemoryStream unmanagedMemoryStream2 = Properties.Resource1.dsplpain;

        MemoryStream memoryStream = new MemoryStream();

        MyMedia.Source = MediaSource.FromResource(Properties.Resource1.dsplpain.ToString());
        MyMedia.Play();
        //MyMedia.Source = MediaSource.FromResource(unmanagedMemoryStream2);

    }
}

