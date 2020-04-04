using MvvmCross.ViewModels;
using DiabetesFoodJournal.Core.ViewModels;
using System;

namespace DiabetesFoodJournal.Core
{
    public class App : MvxApplication
    {
        public override void Initialize()
        {
            RegisterAppStart<HomeViewModel>(); 
        }
    }
}
