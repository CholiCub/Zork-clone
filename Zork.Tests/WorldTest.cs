//using System;
//using System.Collections.Generic;
//using System.Text;
//using Microsoft.VisualStudio.TestTools.UnitTesting;


//namespace Zork.Tests
//{
//    [TestClass]
//    public class WorldTest
//    {
//        [TestMethod]
//        public void TestSpawnPlayer()
//        {
//            var world = new World();
//            var roomA = new Room("A");
//            world.RoomsByName = new Dictionary<string, Room>
//            {
//                { "A", roomA }
//            };
//            world.StartingLocation = "A";

//            var spawnedPlayer = world.SpawnPlayer();
//            Assert.AreSame(roomA, spawnedPlayer.CurrentRoom);
//        }
//    }
//}
