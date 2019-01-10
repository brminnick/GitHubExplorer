﻿using System.Collections.Generic;

namespace GitHubExplorer
{
    public static class GitHubConstants
    {
#error Missing Bearer Token
        public const string BearerToken = "Enter Bearer Token Here";
        public const string APIUrl = "https://api.github.com/graphql";

        public static readonly Dictionary<string, string> GitHubRepoDictionary = new Dictionary<string, string>
        {
            {"AsyncAwaitBestPractices","brminnick"},
            {"ImproveXamarinBuildTimes","brminnick"},
            {"FaceOff","brminnick"},
            {"EntryCustomReturnPlugin","brminnick"},
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
            {"Fusillade","paulcbetts"},
            {"SassAndCoffee","paulcbetts"},
            {"punchclock","paulcbetts"},
            {"starter-mobile","paulcbetts"},
            {"grunt-build-atom-shell","paulcbetts"},

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

            {"Newtonsoft.Json","jamesnk"},
            {"Newtonsoft.Json.Schema","jamesnk"}
        };
    }
}
