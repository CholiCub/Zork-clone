//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


//namespace Zork.Tests
//{
//    [TestClass]
//    public class GameTest
//    {
//        [TestMethod]
//        public void TestLoadGame()
//        {
//            const string gameFileName = "Zork.json";

//            var game = Game.Load(gameFileName);

//            Assert.AreEqual("Welcome to Zork!", game.WelcomeMessage);
//            Assert.AreEqual("West of House", game.World.StartingLocation);
//            Assert.AreEqual(game.World.RoomsByName[game.World.StartingLocation], game.Player.CurrentRoom);
//        }
//    }
//}
