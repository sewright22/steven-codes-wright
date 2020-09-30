using AmerPlayoffPool_blazor.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AmerPlayoffPool_blazor.Pages
{
    public class TestBase : ComponentBase
    {
        public Team AfcSeedOne = new Team() { Name = "Tennessee Titans", PrimaryColor = "#4790DE" };
        public Team AfcSeedTwo = new Team() { Name = "New England Patriots", PrimaryColor = "#002145" };
        public Team AfcSeedThree = new Team() { Name = "Minnesota Vikings", PrimaryColor = "#4F2683" };
        public Team AfcSeedFour = new Team() { Name = "New Orleans Saints", PrimaryColor = "#D3BC8D" };
        public Team AfcSeedFive = new Team() { Name = "Baltimore Ravens", PrimaryColor = "#241075" };
        public Team AfcSeedSix = new Team() { Name = "San Francisco 49ers", PrimaryColor = "#AA0000" };
        public Team NfcSeedOne = new Team() { Name = "Kansas City Chiefs", PrimaryColor = "#E60F31" };
        public Team NfcSeedTwo = new Team() { Name = "Green Bay Packers", PrimaryColor = "#203731" };
        public Team NfcSeedThree = new Team() { Name = "Seattle Seahawks", PrimaryColor = "#002244" };
        public Team NfcSeedFour = new Team() { Name = "Buffalo Bills", PrimaryColor = "#00338D" };
        public Team NfcSeedFive = new Team() { Name = "Houston Texans", PrimaryColor = "#03202F" };
        public Team NfcSeedSix = new Team() { Name = "Philadelphia Eagles", PrimaryColor = "#004C55" };
    }
}
