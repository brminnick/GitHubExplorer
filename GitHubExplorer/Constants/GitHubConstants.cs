using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace GitHubExplorer
{
    public static class GitHubConstants
    {
#error Missing Token, Follow these steps to create your Personal Access Token: https://help.github.com/articles/creating-a-personal-access-token-for-the-command-line/#creating-a-token
        public const string PersonalAccessToken = "Enter Token Here";
        public const string APIUrl = "https://api.github.com/graphql";

        public static ReadOnlyDictionary<string, string> GitHubRepoDictionary { get; } = new(new Dictionary<string, string>
        {
            {"Newtonsoft.Json","jamesnk"},
            {"Newtonsoft.Json.Schema","jamesnk"},

            {"AsyncAwaitBestPractices","brminnick"},
            {"ImproveXamarinBuildTimes","brminnick"},
            {"FaceOff","brminnick"},
            {"GitTrends","brminnick"},
            {"XamList","brminnick"},
            {"TextMood","brminnick"},

            {"allReady","BillWagner"},
            {"crisischeckin","BillWagner"},

            {"TensorFlowSharp","migueldeicaza"},
            {"gui.cs","migueldeicaza"},
            {"MonoTouch.Dialog","migueldeicaza"},
            {"mono-wasm","migueldeicaza"},
            {"TweetStation","migueldeicaza"},
            {"redis-sharp","migueldeicaza"},

            {"ModernHttpClient","paulcbetts"},
            {"SassAndCoffee","paulcbetts"},
            {"starter-mobile","paulcbetts"},
            {"grunt-build-atom-shell","paulcbetts"},
            {"spawn-rx","paulcbetts"},
            {"LinqToAwait","paulcbetts"},

            {"CodeBucket","thedillonb"},
            {"RepoStumble","thedillonb"},
            {"http-shutdown","thedillonb"},
            {"rails-angularjs-simple-forum","thedillonb"},
            {"twitter-cashtag-heatmap","thedillonb"},
            {"MonoTouch.SlideoutNavigation","thedillonb"},

            {"WebEssentials2013","madskristensen"},
            {"MiniBlog","madskristensen"},
            {"WebEssentials2015","madskristensen"},
            {"WebCompiler","madskristensen"},
            {"Miniblog.Core","madskristensen"},
            {"ShortcutExporter","madskristensen"},

            {"Xamarin.Plugins","jamesmontemagno"},
            {"Hanselman.Forms","jamesmontemagno"},
            {"MeetupManager","jamesmontemagno"},
            {"Xam.NavDrawer","jamesmontemagno"},

            {"PushSharp","Redth"},
            {"ZXing.Net.Mobile","Redth"},
            {"APNS-Sharp","Redth"},
            {"FlamedTVLauncher","Redth"},
            {"AndHUD","Redth"},
            {"HttpTwo","Redth"},
        });
    }
}
